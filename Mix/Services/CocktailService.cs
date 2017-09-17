using Dapper;
using Mix.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Mix.Services
{
    public class CocktailService
    {
        private string connectionString = DbHelpers.GetConnectionString();

        public CocktailService()
        {
            using (var db = new SqlConnection(connectionString))
            {
                if (!db.Query("SELECT TOP 1 1 FROM Ingredient").Any())
                {
                    ReferenceService.InsertReferenceData(db);
                }
            }
        }

        public List<IngredientCategory> IngredientCategories()
        {
            using (var db = new SqlConnection(connectionString))
            {
                var ingredientSql = @"
                    WITH Categorised AS (
                        SELECT Id, Id AS Category
                        FROM Ingredient
                        WHERE Id IN @categoryIds
                        UNION ALL
                        SELECT IR.Child AS Id, P.Category
                        FROM Categorised P
                        JOIN IngredientRelationship IR ON IR.Parent = P.Id
                    )
                    SELECT DISTINCT I.*, C.Category
                    FROM Ingredient I
                    LEFT JOIN Categorised C ON C.Id = I.Id
                    WHERE I.IsHidden = 0
                    ORDER BY I.Name
                ";
                var categoryIds = new List<Ingredients> { Ingredients.Spirit, Ingredients.WineAll, Ingredients.Liqueur };
                var results = db.Query<Ingredient>(ingredientSql, new { categoryIds })
                    .GroupBy(i => i.Category)
                    .ToDictionary(i => i.Key, i => i.ToList());

                var categories = new List<IngredientCategory>
                {
                    new IngredientCategory { Id = Ingredients.Spirit, Name = "Spirits" },
                    new IngredientCategory { Id = Ingredients.Liqueur, Name = "Liqueurs" },
                    new IngredientCategory { Id = Ingredients.WineAll, Name = "Wine" },
                    new IngredientCategory { Id = Ingredients.None, Name = "Other" },
                };

                categories.ForEach(c => c.Ingredients = results[c.Id]);
                return categories;
            }
        }

        public IEnumerable<CocktailMatch> FeaturedCocktails()
        {
            using (var db = new SqlConnection(connectionString))
            {
                var dailySeed = (int)(DateTime.Now - new DateTime(1970, 1, 1)).TotalDays;
                var rand = new Random(dailySeed);

                var cocktailIds = ((Cocktails[])Enum.GetValues(typeof(Cocktails)))
                    .Where(i => i != Models.Cocktails.None)
                    .OrderBy(o => rand.Next())
                    .Take(10).ToList();

                var featuredSql = @"
                    SELECT C.Id, C.Name, C.Color,
                    C.Vessel, V.Name AS VesselName,
                    C.PrepMethod, P.Name AS PrepMethodName
                    FROM Cocktail C
                    LEFT JOIN Vessel V on V.Id = C.Vessel
                    LEFT JOIN PrepMethod P on P.Id = C.PrepMethod
                    WHERE C.Id IN @cocktailIds
                ";

                var cocktails = db.Query<CocktailMatch>(featuredSql, new { cocktailIds })
                    .OrderBy(c => cocktailIds.IndexOf(c.Id));

                foreach (var cocktail in cocktails)
                {
                    cocktail.Recipe = CocktailIngredients(db, cocktail.Id);
                }
                return cocktails;
            }
        }

        public IEnumerable<CocktailMatch> Cocktails(IEnumerable<Ingredients> ingredients = null,
                                                    IEnumerable<Ingredients> exgredients = null,
                                                    Vessels vessel = Vessels.None)
        {
            using (var db = new SqlConnection(connectionString))
            {
                var cocktailSql = @"
                    WITH
                    IncludedIngredient AS (
                        SELECT *, Id AS QueryIngredient
                        FROM Ingredient WHERE Id IN @ingredients
                        UNION ALL
                        SELECT C.*, P.QueryIngredient
                        FROM IncludedIngredient P
                        JOIN IngredientRelationship IR ON IR.Parent = P.Id
                        JOIN Ingredient C ON C.Id = IR.Child
                    ),
                    ExcludedIngredient AS (
                        SELECT *
                        FROM Ingredient WHERE Id IN @exgredients
                        UNION ALL
                        SELECT C.*
                        FROM ExcludedIngredient P
                        JOIN IngredientRelationship IR ON IR.Parent = P.Id
                        JOIN Ingredient C ON C.Id = IR.Child
                    ),
                    NonExcludedCocktail AS (
                        SELECT C.*
                        FROM Cocktail C
                        LEFT JOIN CocktailIngredient CI ON CI.Cocktail = C.Id
                        AND CI.Ingredient IN (SELECT Id FROM ExcludedIngredient)
                        WHERE CI.Id IS NULL
                    ),
                    MatchingCocktail AS (
                        SELECT C.Id, C.Name, C.Color, C.Vessel, C.VesselName, C.PrepMethod, C.PrepMethodName,
                        (CAST(C.FullnessCount AS float) / COUNT(*)) AS Fullness,
                        (CAST(C.CompletenessCount AS float) / NULLIF(@ingredientsCount, 0)) AS Completeness
                        FROM (
                            SELECT C.Id, C.Name, C.Color,
                            C.Vessel, V.Name AS VesselName,
                            C.PrepMethod, P.Name AS PrepMethodName,
                            COUNT(DISTINCT (CASE WHEN CI.IsOptional = 0 THEN II.Id END)) AS FullnessCount,
                            COUNT(DISTINCT II.QueryIngredient) AS CompletenessCount
                            FROM NonExcludedCocktail C
                            LEFT JOIN CocktailIngredient CI ON CI.Cocktail = C.Id
                            LEFT JOIN IncludedIngredient II ON II.Id = CI.Ingredient
                            LEFT JOIN Vessel V ON V.Id = C.Vessel
                            LEFT JOIN PrepMethod P ON P.Id = C.PrepMethod
                            WHERE (@vessel = 0 OR C.Vessel = @vessel)
                            AND (NOT EXISTS (SELECT TOP 1 * FROM IncludedIngredient)
                            OR II.Id IS NOT NULL)
                            GROUP BY C.Id, C.Name, C.Color, C.Vessel, V.Name, C.PrepMethod, P.Name
                        ) AS C
                        LEFT JOIN CocktailIngredient CI ON CI.Cocktail = C.Id
                        WHERE CI.IsOptional = 0
                        GROUP BY C.Id, C.Name, C.Color,
                        C.Vessel, C.VesselName, C.PrepMethod, C.PrepMethodName,
                        C.FullnessCount, C.CompletenessCount
                    )
                    SELECT * FROM MatchingCocktail
                    ORDER BY Fullness + Completeness DESC, Name ASC
                ";

                var cocktails = db.Query<CocktailMatch>(cocktailSql,
                    new { ingredientsCount = ingredients?.Count() ?? 0, ingredients, exgredients, vessel });

                foreach (var cocktail in cocktails)
                {
                    cocktail.Recipe = CocktailIngredients(db, cocktail.Id);
                }
                return cocktails;
            }
        }

        public Cocktail Cocktail(long id)
        {
            using (var db = new SqlConnection(connectionString))
            {
                var results = db.Query<Cocktail>(
                    @"SELECT C.Id, C.Name, C.Ice, C.Color,
                    C.Vessel, V.Name AS VesselName,
                    C.PrepMethod, P.Name AS PrepMethodName,
                    C.Garnish, STUFF(
                        (SELECT ', and ' + Name
                        FROM Garnish G
                        WHERE (C.Garnish & G.Id) != 0
                        For XML PATH ('')), 1, 6, '') AS GarnishName
                    FROM Cocktail C
                    LEFT JOIN Vessel V ON V.Id = C.Vessel
                    LEFT JOIN PrepMethod P ON P.Id = C.PrepMethod
                    WHERE C.Id = @id"
                    , new { id });

                if (!results.Any()) return null;

                var cocktail = results.First();
                cocktail.Recipe = CocktailIngredients(db, cocktail.Id);
                cocktail.Similar = SimilarCocktails(db, cocktail);
                return cocktail;
            }
        }

        public IEnumerable<Cocktail> Search(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return null;
            }

            using (var db = new SqlConnection(connectionString))
            {
                var textSearchSql = $@"
                    SELECT C.Id, C.Name
                    FROM Cocktail C
                    LEFT JOIN CONTAINSTABLE(Cocktail, Name, @search) M ON  M.[KEY] = C.Id
                    WHERE M.RANK IS NOT NULL
                    ORDER BY M.RANK DESC, C.Name
                ";
                return db.Query<Cocktail>(textSearchSql, new { search = ContainsSearchCondition(query) });
            }
        }

        private IEnumerable<CocktailIngredient> CocktailIngredients(SqlConnection db, Cocktails cocktail)
        {
            var ingredientSql =
                "SELECT CI.Ingredient, I.Name, CI.IsOptional, CI.Quantity, I.IsDiscrete, " +
                "CI.SpecialPrep, S.Name AS SpecialPrepName " +
                "FROM CocktailIngredient CI " +
                "LEFT JOIN Ingredient I ON I.Id = CI.Ingredient " +
                "LEFT JOIN SpecialPrep S ON S.Id = CI.SpecialPrep " +
                "WHERE CI.Cocktail = @Cocktail";
            return db.Query<CocktailIngredient>(ingredientSql, new { Cocktail = cocktail });
        }

        private IEnumerable<Cocktail> SimilarCocktails(SqlConnection db, Cocktail cocktail)
        {
            var ingredients = cocktail.Recipe.Select(i => i.Ingredient);
            var similarIngredients = db.Query<Ingredients>(
                "SELECT Parent " +
                "FROM IngredientRelationship IR " +
                "LEFT JOIN Ingredient I ON I.Id = IR.Parent " +
                "WHERE Child IN @ingredients " +
                "AND I.Equivalence = 1",
                new { ingredients });
            return Cocktails(ingredients.Concat(similarIngredients))
                .Where(c => c.Id != cocktail.Id)
                .Take(6);
        }

        private string ContainsSearchCondition(string query)
        {
            var terms = query.Replace("\"", "")
                .Split(new char[0], StringSplitOptions.RemoveEmptyEntries).ToList();
            var conditions = terms.Select(t => $"FORMSOF(INFLECTIONAL, \"{t}\")").ToList();
            if (terms.Count == 1 || terms.Last().Length > 1)
            {
                conditions.Add($"\"{terms.Last()}*\"");
            }
            return string.Join(" OR ", conditions);
        }
    }

    public class CocktailMatch : Cocktail
    {
        public double Fullness;
        public double Completeness;
    }
}