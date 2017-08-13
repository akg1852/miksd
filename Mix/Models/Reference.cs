using System.Collections.Generic;

namespace Mix.Models
{
    public class Reference
    {
        private static decimal splash = 3M;
        private static decimal dash = 0.5M;
        private static decimal teaspoon = 5M;

        public static List<Ingredient> AllIngredients = new List<Ingredient>
        {
            new Ingredient(Ingredients.Rum, "Rum",
                Ingredients.WhiteRum, Ingredients.DarkRum),
            new Ingredient(Ingredients.WhiteRum, "White Rum"),
            new Ingredient(Ingredients.DarkRum, "Dark Rum"),

            new Ingredient(Ingredients.Whisky, "Whisk(e)y",
                Ingredients.Scotch, Ingredients.Bourbon, Ingredients.Rye),
            new Ingredient(Ingredients.Scotch, "Scotch"),
            new Ingredient(Ingredients.Bourbon, "Bourbon"),
            new Ingredient(Ingredients.Rye, "Rye Whisky"),

            new Ingredient(Ingredients.Gin, "Gin"),

            new Ingredient(Ingredients.Angostura, "Angostura Bitters"),
            new Ingredient(Ingredients.Campari, "Campari"),

            new Ingredient(Ingredients.Vermouth, "Vermouth",
                Ingredients.RedVermouth, Ingredients.WhiteVermouth),
            new Ingredient(Ingredients.RedVermouth, "Sweet Red Vermouth"),
            new Ingredient(Ingredients.WhiteVermouth, "Dry White Vermouth"),

            new Ingredient(Ingredients.OrangeLiqueur, "Orange Liqueur",
                Ingredients.TripleSec),
            new Ingredient(Ingredients.TripleSec, "Triple Sec",
                Ingredients.Cointreau),
            new Ingredient(Ingredients.Cointreau, "Cointreau"),

            new Ingredient(Ingredients.Citrus, "Citrus Juice",
                Ingredients.LemonJuice, Ingredients.LimeJuice),
            new Ingredient(Ingredients.LemonJuice, "Lemon Juice"),
            new Ingredient(Ingredients.LimeJuice, "Lime Juice"),

            new Ingredient(Ingredients.Sugar, "Sugar",
                Ingredients.SimpleSyrup),
            new Ingredient(Ingredients.SimpleSyrup, "Simple Syrup"),

            new Ingredient(Ingredients.Water, "Water"),
            new Ingredient(Ingredients.Soda, "Soda"),

            new Ingredient(Ingredients.EggWhite, "Egg White"),
        };

        public static List<Cocktail> AllCocktails = new List<Cocktail>
        {
            new Cocktail(Cocktails.Americano, "Americano",
                new CocktailIngredient(Ingredients.Campari, 30M),
                new CocktailIngredient(Ingredients.RedVermouth, 30M),
                new CocktailIngredient(Ingredients.Soda, splash)),

            new Cocktail(Cocktails.Daiquiri, "Daiquiri",
                new CocktailIngredient(Ingredients.WhiteRum, 45M),
                new CocktailIngredient(Ingredients.SimpleSyrup, 15M),
                new CocktailIngredient(Ingredients.LimeJuice, 25M)),

            new Cocktail(Cocktails.Martini, "Dry Martini",
                new CocktailIngredient(Ingredients.Gin, 60M),
                new CocktailIngredient(Ingredients.WhiteVermouth, 10M)),

            new Cocktail(Cocktails.Manhattan, "Manhattan",
                new CocktailIngredient(Ingredients.Rye, 50M),
                new CocktailIngredient(Ingredients.RedVermouth, 20M),
                new CocktailIngredient(Ingredients.Angostura, dash)),

            new Cocktail(Cocktails.Negroni, "Negroni",
                new CocktailIngredient(Ingredients.Gin, 30M),
                new CocktailIngredient(Ingredients.Campari, 30M),
                new CocktailIngredient(Ingredients.RedVermouth, 30M)),

            new Cocktail(Cocktails.OldFashioned, "Old Fashioned",
                new CocktailIngredient(Ingredients.Bourbon, 45M),
                new CocktailIngredient(Ingredients.Angostura, 2 * dash),
                new CocktailIngredient(Ingredients.Sugar, teaspoon),
                new CocktailIngredient(Ingredients.Water, splash)),

            new Cocktail(Cocktails.WhiskeySour, "Whiskey Sour",
                new CocktailIngredient(Ingredients.Bourbon, 45M),
                new CocktailIngredient(Ingredients.SimpleSyrup, 15M),
                new CocktailIngredient(Ingredients.LemonJuice, 30M),
                new CocktailIngredient(Ingredients.EggWhite, splash, true)),
        };
    }
}