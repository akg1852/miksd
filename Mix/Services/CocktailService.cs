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

        public IEnumerable<Cocktail> Cocktails(IEnumerable<Ingredients> ingredients = null, bool getSimilar = false)
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
                    cocktailSql =
                        "WITH cte AS ( " +
                            "SELECT* " +
                            "FROM Ingredient WHERE Id in @ingredients " +
                            "UNION ALL " +
                            "SELECT I.* " +
                            "FROM cte C " +
                            "JOIN IngredientRelationship IR ON IR.Parent = C.Id " +
                            "JOIN Ingredient I ON I.Id = IR.Child " +
                        ") " +
                        "SELECT C.*, COUNT(C.Id) AS Count " +
                        "FROM Cocktail C " +
                        "RIGHT JOIN CocktailIngredient CI ON CI.Cocktail = C.Id " +
                        "WHERE CI.Ingredient IN (SELECT Id FROM cte) " +
                        "GROUP by C.Id, C.Name " +
                        "ORDER BY Count DESC, Name ASC ";
                }

                var cocktails = db.Query<Cocktail>(cocktailSql, new { ingredients });
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
                "SELECT Parent FROM IngredientRelationship " +
                "WHERE Child IN @ingredients",
                new { ingredients });
            return Cocktails(ingredients.Concat(similarIngredients).Distinct())
                .Where(c => c.Id != cocktail.Id)
                .Take(3);
        }

        private void InsertIngredients(SqlConnection db)
        {
            foreach (var ingredient in Reference.AllIngredients)
            {
                db.Execute("INSERT INTO Ingredient (Id, Name) VALUES (@Id, @Name)",
                    new { ingredient.Id, ingredient.Name });
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
}