using Dapper;
using Mix.Models;
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
                    InsertIngredients(db);
                    InsertVessels(db);
                    InsertPrepMethods(db);
                    InsertGarnishes(db);
                    InsertCocktails(db);
                }
            }
        }

        public IEnumerable<Ingredient> AllIngredients()
        {
            using (var db = new SqlConnection(connectionString))
            {
                return db.Query<Ingredient>("SELECT * FROM Ingredient ORDER BY Name");
            }
        }

        public IEnumerable<CocktailMatch> FeaturedCocktails()
        {
            return Cocktails(null, null, Vessels.None);
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
                        SELECT C.Id, C.Name, C.Vessel, C.VesselName, C.PrepMethod, C.PrepMethodName,
                        (CAST(C.FullnessCount AS float) / COUNT(*)) AS Fullness,
                        (CAST(C.CompletenessCount AS float) / NULLIF(@ingredientsCount, 0)) AS Completeness
                        FROM (
                            SELECT C.Id, C.Name,
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
                            GROUP BY C.Id, C.Name, C.Vessel, V.Name, C.PrepMethod, P.Name
                        ) AS C
                        LEFT JOIN CocktailIngredient CI ON CI.Cocktail = C.Id
                        WHERE CI.IsOptional = 0
                        GROUP BY C.Id, C.Name,
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
                    @"SELECT C.Id, C.Name, C.Ice,
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

        private IEnumerable<CocktailIngredient> CocktailIngredients(SqlConnection db, Cocktails cocktail)
        {
            var ingredientSql =
                "SELECT I.Id as Ingredient, I.Name, CI.IsOptional, CI.Quantity, I.IsDiscrete " +
                "FROM CocktailIngredient CI " +
                "LEFT JOIN Ingredient I ON CI.Ingredient = I.Id " +
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

        private void InsertIngredients(SqlConnection db)
        {
            foreach (var ingredient in Reference.AllIngredients)
            {
                db.Execute("INSERT INTO Ingredient (Id, Name, Equivalence, IsHidden, IsDiscrete) " +
                    "VALUES (@Id, @Name, @Equivalence, @IsHidden, @IsDiscrete)",
                    new { ingredient.Id, ingredient.Name, ingredient.Equivalence,
                        ingredient.IsHidden, ingredient.IsDiscrete });
            }
            foreach (var parent in Reference.AllIngredients)
            {
                foreach (var child in parent.Children)
                {
                    db.Execute("INSERT INTO IngredientRelationship (Parent, Child) VALUES (@Parent, @Child)",
                    new { Parent = parent.Id, Child = child });
                }
            }
        }

        private void InsertVessels(SqlConnection db)
        {
            foreach (var vessel in Reference.AllVessels)
            {
                db.Execute("INSERT INTO Vessel (Id, Name) VALUES (@Id, @Name)",
                    new { vessel.Id, vessel.Name });
            }
        }

        private void InsertPrepMethods(SqlConnection db)
        {
            foreach (var prep in Reference.AllPrepMethods)
            {
                db.Execute("INSERT INTO PrepMethod (Id, Name) VALUES (@Id, @Name)",
                    new { prep.Id, prep.Name });
            }
        }

        private void InsertGarnishes(SqlConnection db)
        {
            foreach (var garnish in Reference.AllGarnishes)
            {
                db.Execute("INSERT INTO Garnish (Id, Name) VALUES (@Id, @Name)",
                    new { garnish.Id, garnish.Name });
            }
        }

        private void InsertCocktails(SqlConnection db)
        {
            foreach (var cocktail in Reference.AllCocktails)
            {
                var garnish = (cocktail.Garnish == Garnishes.None) ? (Garnishes?)null : cocktail.Garnish;

                db.Execute("INSERT INTO Cocktail (Id, Name, Vessel, PrepMethod, Ice, Garnish) " +
                    "VALUES (@Id, @Name, @Vessel, @PrepMethod, @Ice, @Garnish)",
                    new { cocktail.Id, cocktail.Name, cocktail.Vessel, cocktail.PrepMethod, cocktail.Ice, garnish });

                foreach (var ingredient in cocktail.Recipe)
                {
                    db.Execute("INSERT INTO CocktailIngredient (Cocktail, Ingredient, IsOptional, Quantity) " +
                        "VALUES (@Cocktail, @Ingredient, @IsOptional, @Quantity)",
                    new {
                        Cocktail = cocktail.Id,
                        Ingredient = ingredient.Ingredient,
                        IsOptional = ingredient.IsOptional,
                        Quantity = ingredient.Quantity
                    });
                }
            }
        }
    }

    public class CocktailMatch : Cocktail
    {
        public double Fullness;
        public double Completeness;
    }
}