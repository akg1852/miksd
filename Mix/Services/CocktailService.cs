using Dapper;
using Mix.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

using i = Mix.Models.Ingredients;

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

        private string SuperAndSubIngredient = $@"
           SuperIngredient AS (
                SELECT Id, Name
                FROM Ingredient
                WHERE IsSuper = 1
            ),
            SubIngredient AS (
                SELECT IR.Child AS Id, P.Id AS Super
                FROM SuperIngredient P
                JOIN IngredientRelationship IR ON IR.Parent = P.Id
                UNION ALL
                SELECT IR.Child AS Id, P.Super
                FROM SubIngredient P
                JOIN IngredientRelationship IR ON IR.Parent = P.Id
            )
        ";

        public List<IngredientCategory> IngredientCategories()
        {
            using (var db = new SqlConnection(connectionString))
            {
                var ingredientSql = $@"
                    WITH
                    {SuperAndSubIngredient},
                    Categorised AS (
                        SELECT Id, Id AS Category
                        FROM Ingredient
                        WHERE Id IN @categoryIds
                        UNION ALL
                        SELECT IR.Child AS Id, P.Category
                        FROM Categorised P
                        JOIN IngredientRelationship IR ON IR.Parent = P.Id
                    ),
                    LeafIngredient AS (
                        SELECT I.Id, I.Name
                        FROM Ingredient I
                        LEFT JOIN IngredientRelationship IR ON IR.Parent = I.Id
                        WHERE IR.Parent IS NULL
                    )
                    SELECT DISTINCT I.*, C.Category
                    FROM (
                        SELECT Id, Name
                        FROM SuperIngredient
                        UNION
                        SELECT L.Id, L.Name
                        FROM LeafIngredient L
                        LEFT JOIN SubIngredient S ON S.Id = L.Id
                        WHERE S.Id IS NULL
                    ) AS I
                    LEFT JOIN Categorised C ON C.Id = I.Id
                    ORDER BY I.Name
                ";
                var categoryIds = new List<Ingredients> {
                    Ingredients.Spirit,
                    Ingredients.WineAll,
                    Ingredients.Liqueur,
                    Ingredients.Mixer,
                };
                var results = db.Query<Ingredient>(ingredientSql, new { categoryIds })
                    .GroupBy(i => i.Category)
                    .ToDictionary(i => i.Key, i => i.ToList());

                var categories = new List<IngredientCategory>
                {
                    new IngredientCategory { Id = Ingredients.Spirit, Name = "Spirit" },
                    new IngredientCategory { Id = Ingredients.WineAll, Name = "Wine" },
                    new IngredientCategory { Id = Ingredients.Liqueur, Name = "Liqueur" },
                    new IngredientCategory { Id = Ingredients.Mixer, Name = "Mixer" },
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

        public IEnumerable<CocktailMatch> FindCocktails(IEnumerable<Ingredients> ingredients = null,
                                                    IEnumerable<Ingredients> exgredients = null,
                                                    IEnumerable<Vessels> vessels = null)
        {
            ingredients = ingredients?.Distinct()?.ToList();
            var nonOptionalIngredients = "CI.IsOptional = 0 AND CI.Ingredient not in @nonFullnessIngredients";

            using (var db = new SqlConnection(connectionString))
            {
                var cocktailSql = $@"
                    WITH
                    IncludedIngredientRoot AS (
                        SELECT *
                        FROM Ingredient WHERE Id IN @ingredients
                    ),
                    IncludedIngredientUp AS (
                        SELECT *, Id AS QueryIngredient
                        FROM IncludedIngredientRoot
                        UNION ALL
                        SELECT P.*, C.QueryIngredient
                        FROM IncludedIngredientUp C
                        JOIN IngredientRelationship IR ON IR.Child = C.Id
                        JOIN Ingredient P ON P.Id = IR.Parent
                    ),
                    IncludedIngredientDown AS (
                        SELECT *, Id AS QueryIngredient
                        FROM IncludedIngredientRoot
                        UNION ALL
                        SELECT C.*, P.QueryIngredient
                        FROM IncludedIngredientDown P
                        JOIN IngredientRelationship IR ON IR.Parent = P.Id
                        JOIN Ingredient C ON C.Id = IR.Child
                    ),
                    IncludedIngredient AS (
                        SELECT * FROM IncludedIngredientUp
                        UNION
                        SELECT * FROM IncludedIngredientDown
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
                        COUNT(*) AS IngredientCount,
                        (CAST(C.FullnessCount AS float) / COUNT(*)) AS Fullness,
                        (CAST(C.CompletenessCount AS float) / NULLIF(@ingredientsCount, 0)) AS Completeness
                        FROM (
                            SELECT C.Id, C.Name, C.Color,
                            C.Vessel, V.Name AS VesselName,
                            C.PrepMethod, P.Name AS PrepMethodName,
                            COUNT(DISTINCT (CASE WHEN {nonOptionalIngredients} THEN II.Id END)) AS FullnessCount,
                            COUNT(DISTINCT II.QueryIngredient) AS CompletenessCount
                            FROM NonExcludedCocktail C
                            LEFT JOIN CocktailIngredient CI ON CI.Cocktail = C.Id
                            LEFT JOIN IncludedIngredient II ON II.Id = CI.Ingredient
                            LEFT JOIN Vessel V ON V.Id = C.Vessel
                            LEFT JOIN PrepMethod P ON P.Id = C.PrepMethod
                            WHERE (@noVessels = 1 OR C.Vessel in @vessels)
                            AND (NOT EXISTS (SELECT TOP 1 * FROM IncludedIngredient)
                            OR II.Id IS NOT NULL)
                            GROUP BY C.Id, C.Name, C.Color, C.Vessel, V.Name, C.PrepMethod, P.Name
                        ) AS C
                        LEFT JOIN CocktailIngredient CI ON CI.Cocktail = C.Id
                        WHERE {nonOptionalIngredients}
                        GROUP BY C.Id, C.Name, C.Color,
                        C.Vessel, C.VesselName, C.PrepMethod, C.PrepMethodName,
                        C.FullnessCount, C.CompletenessCount
                    )
                    SELECT * FROM MatchingCocktail
                    ORDER BY
                        IIF(Fullness >= Completeness, Fullness, Completeness) DESC,
                        IIF(Fullness < Completeness, Fullness, Completeness) DESC,
                        IngredientCount ASC,
                        Id ASC
                ";

                var cocktails = db.Query<CocktailMatch>(cocktailSql,
                    new {
                        ingredientsCount = ingredients?.Count() ?? 0,
                        ingredients,
                        exgredients,
                        noVessels = vessels == null,
                        vessels,
                        nonFullnessIngredients = new List<i> {
                            i.Water, i.Sugar,
                            i.Bitters, i.Angostura, i.OrangeBitters, i.Peychauds
                        }
                    });

                foreach (var cocktail in cocktails)
                {
                    cocktail.Recipe = CocktailIngredients(db, cocktail.Id);
                }
                return cocktails;
            }
        }

        public IEnumerable<Cocktail> Cocktails(IEnumerable<Cocktails> ids)
        {
            var idsList = ids.ToList();

            using (var db = new SqlConnection(connectionString))
            {
                var cocktails = db.Query<Cocktail>(
                    @"SELECT C.Id, C.Name
                    FROM Cocktail C
                    WHERE C.Id IN @ids"
                    , new { ids });

                foreach (var cocktail in cocktails)
                {
                    cocktail.Recipe = CocktailIngredients(db, cocktail.Id);
                }
                return cocktails.OrderBy(c => idsList.IndexOf(c.Id));
            }
        }

        public Cocktail Cocktail(Cocktails id)
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
                $"WITH {SuperAndSubIngredient}" +
                "SELECT CI.Ingredient, I.Name, CI.IsOptional, CI.Quantity, I.IsDiscrete, " +
                "CI.SpecialPrep, S.Name AS SpecialPrepName, SI.Super " +
                "FROM CocktailIngredient CI " +
                "LEFT JOIN Ingredient I ON I.Id = CI.Ingredient " +
                "LEFT JOIN SpecialPrep S ON S.Id = CI.SpecialPrep " +
                "LEFT JOIN SubIngredient SI ON SI.Id = I.Id " +
                "WHERE CI.Cocktail = @Cocktail";
            return db.Query<CocktailIngredient>(ingredientSql, new { Cocktail = cocktail });
        }

        private IEnumerable<Cocktail> SimilarCocktails(SqlConnection db, Cocktail cocktail)
        {
            var ingredients = cocktail.Recipe.Select(i => i.Ingredient);
            var equivalence = db.Query(
                "SELECT Parent, Child " +
                "FROM IngredientRelationship IR " +
                "LEFT JOIN Ingredient I ON I.Id = IR.Parent " +
                "WHERE Child IN @ingredients " +
                "AND (I.Equivalence = 1 OR I.IsSuper = 1)",
                new { ingredients }).ToList();
            var similarIngredients = ingredients.Where(i => !equivalence.Any(e => (Ingredients)e.Child == i))
                .Concat(equivalence.Select(e => (Ingredients)e.Parent));

            return FindCocktails(similarIngredients)
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