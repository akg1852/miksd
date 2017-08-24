using Dapper;
using Mix.Models;
using System.Collections.Generic;
using System.Configuration;
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
                    InsertCocktails(db);
                }
            }
        }

        public IEnumerable<Ingredient> Ingredients()
        {
            using (var db = new SqlConnection(connectionString))
            {
                return db.Query<Ingredient>("SELECT * FROM Ingredient ORDER BY Name");
            }
        }

        public IEnumerable<CocktailMatch> FeaturedCocktails()
        {
            return Cocktails(null, null, Vessels.None, true);
        }

        public IEnumerable<CocktailMatch> Cocktails(IEnumerable<Ingredients> ingredients = null,
                                                    IEnumerable<Ingredients> exgredients = null,
                                                    Vessels vessel = Vessels.None,
                                                    bool getSimilar = false)
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
                        SELECT C.Id, C.Name, C.Vessel, V.Name AS VesselName,
                        COUNT(DISTINCT (CASE WHEN CI.IsOptional = 0 THEN II.Id END)) AS FullnessCount,
                        COUNT(DISTINCT II.QueryIngredient) AS CompletenessCount
                        FROM NonExcludedCocktail C
                        LEFT JOIN CocktailIngredient CI ON CI.Cocktail = C.Id
                        LEFT JOIN IncludedIngredient II ON II.Id = CI.Ingredient
                        LEFT JOIN Vessel V ON V.Id = C.Vessel
                        WHERE (@vessel = 0 OR C.Vessel = @vessel)
                        AND (NOT EXISTS (SELECT TOP 1 * FROM IncludedIngredient)
                        OR II.Id IS NOT NULL)
                        GROUP BY C.Id, C.Name, C.Vessel, V.Name
                    )
                    SELECT C.Id, C.Name, C.Vessel, C.VesselName,
                    (CAST(C.FullnessCount AS float) / COUNT(*)) AS Fullness,
                    (CAST(C.CompletenessCount AS float) / NULLIF(@ingredientsCount, 0)) AS Completeness
                    FROM MatchingCocktail AS C
                    LEFT JOIN CocktailIngredient CI ON CI.Cocktail = C.Id
                    WHERE CI.IsOptional = 0
                    GROUP BY C.Id, C.Name, C.Vessel, C.VesselName, C.FullnessCount, C.CompletenessCount
                    ORDER BY Fullness DESC, Completeness DESC, Name ASC
                ";

                var cocktails = db.Query<CocktailMatch>(cocktailSql,
                    new { ingredientsCount = ingredients?.Count() ?? 0, ingredients, exgredients, vessel });

                foreach (var cocktail in cocktails)
                {
                    var ingredientSql =
                        "SELECT I.Id as Ingredient, I.Name, CI.IsOptional, CI.Quantity, I.IsDiscrete " +
                        "FROM CocktailIngredient CI " +
                        "LEFT JOIN Ingredient I ON CI.Ingredient = I.Id " +
                        "WHERE CI.Cocktail = @Cocktail";
                    cocktail.Recipe = db.Query<CocktailIngredient>(ingredientSql, new { Cocktail = cocktail.Id });

                    if (getSimilar)
                    {
                        cocktail.Similar = SimilarCocktails(db, cocktail);
                    }
                }
                return cocktails;
            }
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
                .Take(3);
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

        private void InsertCocktails(SqlConnection db)
        {
            foreach (var cocktail in Reference.AllCocktails)
            {
                db.Execute("INSERT INTO Cocktail (Id, Name, Vessel) VALUES (@Id, @Name, @Vessel)",
                    new { cocktail.Id, cocktail.Name, cocktail.Vessel });

                foreach (var ingredient in cocktail.Recipe)
                {
                    db.Execute("INSERT INTO CocktailIngredient (Cocktail, Ingredient, IsOptional, Quantity) VALUES " +
                        "(@Cocktail, @Ingredient, @IsOptional, @Quantity)",
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