using Dapper;
using Mix.Models;
using System.Data.SqlClient;

namespace Mix.Services
{
    public static class ReferenceService
    {
        public static void InsertReferenceData(SqlConnection db)
        {
            InsertIngredients(db);
            InsertVessels(db);
            InsertPrepMethods(db);
            InsertGarnishes(db);
            InsertCocktails(db);
        }

        private static void InsertIngredients(SqlConnection db)
        {
            foreach (var ingredient in Reference.AllIngredients)
            {
                db.Execute("INSERT INTO Ingredient (Id, Name, Equivalence, IsHidden, IsDiscrete) " +
                    "VALUES (@Id, @Name, @Equivalence, @IsHidden, @IsDiscrete)",
                    new
                    {
                        ingredient.Id,
                        ingredient.Name,
                        ingredient.Equivalence,
                        ingredient.IsHidden,
                        ingredient.IsDiscrete
                    });
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

        private static void InsertVessels(SqlConnection db)
        {
            foreach (var vessel in Reference.AllVessels)
            {
                db.Execute("INSERT INTO Vessel (Id, Name) VALUES (@Id, @Name)",
                    new { vessel.Id, vessel.Name });
            }
        }

        private static void InsertPrepMethods(SqlConnection db)
        {
            foreach (var prep in Reference.AllPrepMethods)
            {
                db.Execute("INSERT INTO PrepMethod (Id, Name) VALUES (@Id, @Name)",
                    new { prep.Id, prep.Name });
            }
        }

        private static void InsertGarnishes(SqlConnection db)
        {
            foreach (var garnish in Reference.AllGarnishes)
            {
                db.Execute("INSERT INTO Garnish (Id, Name) VALUES (@Id, @Name)",
                    new { garnish.Id, garnish.Name });
            }
        }

        private static void InsertCocktails(SqlConnection db)
        {
            foreach (var cocktail in Reference.AllCocktails)
            {
                db.Execute("INSERT INTO Cocktail (Id, Name, Vessel, PrepMethod, Ice, Garnish) " +
                    "VALUES (@Id, @Name, @Vessel, @PrepMethod, @Ice, @Garnish)",
                    new { cocktail.Id, cocktail.Name, cocktail.Vessel, cocktail.PrepMethod, cocktail.Ice, cocktail.Garnish });

                foreach (var ingredient in cocktail.Recipe)
                {
                    db.Execute("INSERT INTO CocktailIngredient (Cocktail, Ingredient, IsOptional, Quantity) " +
                        "VALUES (@Cocktail, @Ingredient, @IsOptional, @Quantity)",
                    new
                    {
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