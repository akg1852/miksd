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

        public IEnumerable<Cocktail> Cocktails()
        {
            using (var db = new SqlConnection(connectionString))
            {
                var cocktails = db.Query<Cocktail>("SELECT * FROM Cocktail");
                foreach (var cocktail in cocktails)
                {
                    var sql = "SELECT I.Id as Ingredient, I.Name, CI.IsOptional, CI.Quantity " +
                        "FROM CocktailIngredient CI " +
                        "LEFT JOIN Ingredient I ON CI.Ingredient = I.Id " +
                        "WHERE CI.Cocktail = @Cocktail";
                    cocktail.Recipe = db.Query<CocktailIngredient>(sql, new { Cocktail = cocktail.Id });
                }
                return cocktails;
            }
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