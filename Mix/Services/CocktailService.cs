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
        private string connectionString = ConfigurationManager.ConnectionStrings["MixDB"].ConnectionString;

        public CocktailService()
        {
            using (var db = new SqlConnection(connectionString))
            {
                if (!db.Query("SELECT TOP 1 1 FROM Ingredient").Any())
                {
                    InsertIngredients(db);
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

        public IEnumerable<CocktailMatch> Cocktails(IEnumerable<Ingredients> ingredients = null, bool getSimilar = false)
        {
            using (var db = new SqlConnection(connectionString))
            {
                string cocktailSql;
                if (ingredients == null)
                {
                    cocktailSql = "SELECT * FROM Cocktail ORDER BY Name ASC";
                }
                else
                {
                    cocktailSql = @"
                        WITH cte AS(
                            SELECT * FROM Ingredient WHERE Id IN @ingredients
                            UNION ALL
                            SELECT I.* FROM cte C
                            JOIN IngredientRelationship IR ON IR.Parent = C.Id
                            JOIN Ingredient I ON I.Id = IR.Child
                        )
                        SELECT C.*, COUNT(*) AS IngredientCount,
                        (CAST(C.MatchCount AS float) / COUNT(*)) AS Fullness
                        FROM(
                            SELECT C.*, COUNT(*) AS MatchCount
                            FROM Cocktail C
                            LEFT JOIN CocktailIngredient CI ON CI.Cocktail = C.Id
                            WHERE CI.Ingredient IN (SELECT Id FROM cte)
                            GROUP BY C.Id, C.Name
                        ) AS C
                        LEFT JOIN CocktailIngredient CI ON CI.Cocktail = C.Id
                        GROUP BY C.Id, C.Name, C.MatchCount
                        ORDER BY Fullness DESC, IngredientCount ASC, Name ASC
                    ";
                }

                var cocktails = db.Query<CocktailMatch>(cocktailSql, new { ingredients });
                foreach (var cocktail in cocktails)
                {
                    var ingredientSql =
                        "SELECT I.Id as Ingredient, I.Name, CI.IsOptional, CI.Quantity " +
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
                db.Execute("INSERT INTO Ingredient (Id, Name, Equivalence) VALUES (@Id, @Name, @Equivalence)",
                    new { ingredient.Id, ingredient.Name, ingredient.Equivalence });
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

        private void InsertCocktails(SqlConnection db)
        {
            foreach (var cocktail in Reference.AllCocktails)
            {
                db.Execute("INSERT INTO Cocktail (Id, Name) VALUES (@Id, @Name)",
                    new { cocktail.Id, cocktail.Name });

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
        public int MatchCount;
    }
}