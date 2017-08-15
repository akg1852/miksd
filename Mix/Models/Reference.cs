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
            new Ingredient(Ingredients.Spirit, "Spirit",
                Ingredients.Rum, Ingredients.Whisky, Ingredients.Vodka,
                Ingredients.Gin, Ingredients.Brandy, Ingredients.Tequila),

            new Ingredient(Ingredients.Rum, "Rum", true,
                Ingredients.WhiteRum, Ingredients.DarkRum),
            new Ingredient(Ingredients.WhiteRum, "White Rum"),
            new Ingredient(Ingredients.DarkRum, "Dark Rum"),

            new Ingredient(Ingredients.Whisky, "Whisk(e)y",
                Ingredients.Scotch, Ingredients.Bourbon, Ingredients.Rye),
            new Ingredient(Ingredients.Scotch, "Scotch"),
            new Ingredient(Ingredients.AmericanWhiskey, "American Whisky", true,
                Ingredients.Bourbon, Ingredients.Rye),
            new Ingredient(Ingredients.Bourbon, "Bourbon"),
            new Ingredient(Ingredients.Rye, "Rye Whiskey"),

            new Ingredient(Ingredients.Vodka, "Vodka"),

            new Ingredient(Ingredients.Gin, "Gin", true,
                Ingredients.OldTom, Ingredients.LondonDry),
            new Ingredient(Ingredients.OldTom, "Old Tom Gin"),
            new Ingredient(Ingredients.LondonDry, "London Dry Gin"),

            new Ingredient(Ingredients.Brandy, "Brandy", true,
                Ingredients.Cognac, Ingredients.Pisco),
            new Ingredient(Ingredients.Cognac, "Cognac"),
            new Ingredient(Ingredients.Pisco, "Pisco"),

            new Ingredient(Ingredients.Tequila, "Tequila"),

            new Ingredient(Ingredients.Absinthe, "Absinthe"),

            new Ingredient(Ingredients.Bitters, "Bitters", true,
                Ingredients.Angostura, Ingredients.OrangeBitters, Ingredients.Peychauds),
            new Ingredient(Ingredients.Angostura, "Angostura Bitters"),
            new Ingredient(Ingredients.OrangeBitters, "Orange Bitters"),
            new Ingredient(Ingredients.Peychauds, "Peychaud's Bitters"),

            new Ingredient(Ingredients.Campari, "Campari"),

            new Ingredient(Ingredients.Vermouth, "Vermouth", true,
                Ingredients.SweetVermouth, Ingredients.DryVermouth),
            new Ingredient(Ingredients.SweetVermouth, "Sweet Red Vermouth"),
            new Ingredient(Ingredients.DryVermouth, "Dry White Vermouth"),

            new Ingredient(Ingredients.OrangeLiqueur, "Orange Liqueur",
                Ingredients.TripleSec),
            new Ingredient(Ingredients.TripleSec, "Triple Sec", true,
                Ingredients.Cointreau),
            new Ingredient(Ingredients.Cointreau, "Cointreau"),
            new Ingredient(Ingredients.Maraschino, "Maraschino Liqueur"),
            new Ingredient(Ingredients.Drambuie, "Drambuie"),

            new Ingredient(Ingredients.Grenadine, "Grenadine"),
            new Ingredient(Ingredients.Sugar, "Sugar", true,
                Ingredients.SimpleSyrup),
            new Ingredient(Ingredients.SimpleSyrup, "Simple Syrup"),

            new Ingredient(Ingredients.Citrus, "Citrus Juice", true,
                Ingredients.LemonJuice, Ingredients.LimeJuice),
            new Ingredient(Ingredients.LemonJuice, "Lemon Juice"),
            new Ingredient(Ingredients.LimeJuice, "Lime Juice"),
            new Ingredient(Ingredients.OrangeJuice, "Orange Juice"),
            new Ingredient(Ingredients.PineappleJuice, "Pineapple Juice"),
            new Ingredient(Ingredients.CranberryJuice, "Cranberry Juice"),

            new Ingredient(Ingredients.Water, "Water"),
            new Ingredient(Ingredients.Soda, "Soda Water"),
            new Ingredient(Ingredients.Cola, "Cola"),

            new Ingredient(Ingredients.EggWhite, "Egg White"),
        };

        public static List<Cocktail> AllCocktails = new List<Cocktail>
        {
            new Cocktail(Cocktails.Americano, "Americano",
                new CocktailIngredient(Ingredients.Campari, 30M),
                new CocktailIngredient(Ingredients.SweetVermouth, 30M),
                new CocktailIngredient(Ingredients.Soda, splash)),

            new Cocktail(Cocktails.Daiquiri, "Daiquiri",
                new CocktailIngredient(Ingredients.WhiteRum, 45M),
                new CocktailIngredient(Ingredients.SimpleSyrup, 15M),
                new CocktailIngredient(Ingredients.LimeJuice, 25M)),

            new Cocktail(Cocktails.Martini, "Dry Martini",
                new CocktailIngredient(Ingredients.Gin, 60M),
                new CocktailIngredient(Ingredients.DryVermouth, 10M)),

            new Cocktail(Cocktails.Manhattan, "Manhattan",
                new CocktailIngredient(Ingredients.Rye, 50M),
                new CocktailIngredient(Ingredients.SweetVermouth, 20M),
                new CocktailIngredient(Ingredients.Angostura, dash)),

            new Cocktail(Cocktails.Negroni, "Negroni",
                new CocktailIngredient(Ingredients.Gin, 30M),
                new CocktailIngredient(Ingredients.Campari, 30M),
                new CocktailIngredient(Ingredients.SweetVermouth, 30M)),

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

            new Cocktail(Cocktails.PiscoSour, "Pisco Sour",
                new CocktailIngredient(Ingredients.Pisco, 45M),
                new CocktailIngredient(Ingredients.SimpleSyrup, 20M),
                new CocktailIngredient(Ingredients.LemonJuice, 30M),
                new CocktailIngredient(Ingredients.EggWhite, splash, true)),

            new Cocktail(Cocktails.Aviation, "Aviation",
                new CocktailIngredient(Ingredients.Gin, 45M),
                new CocktailIngredient(Ingredients.Maraschino, 15M),
                new CocktailIngredient(Ingredients.LemonJuice, 15M)),

            new Cocktail(Cocktails.Bacardi, "Bacardi",
                new CocktailIngredient(Ingredients.WhiteRum, 45M),
                new CocktailIngredient(Ingredients.LimeJuice, 20M),
                new CocktailIngredient(Ingredients.Grenadine, 10M)),

            new Cocktail(Cocktails.BetweenTheSheets, "Between the Sheets",
                new CocktailIngredient(Ingredients.Cognac, 30M),
                new CocktailIngredient(Ingredients.WhiteRum, 30M),
                new CocktailIngredient(Ingredients.TripleSec, 30M),
                new CocktailIngredient(Ingredients.LemonJuice, 20M)),

            new Cocktail(Cocktails.Casino, "Casino",
                new CocktailIngredient(Ingredients.OldTom, 40M),
                new CocktailIngredient(Ingredients.Maraschino, 10M),
                new CocktailIngredient(Ingredients.OrangeBitters, 10M),
                new CocktailIngredient(Ingredients.LemonJuice, 10M)),

            new Cocktail(Cocktails.GinFizz, "Gin Fizz",
                new CocktailIngredient(Ingredients.Gin, 45M),
                new CocktailIngredient(Ingredients.SimpleSyrup, 10M),
                new CocktailIngredient(Ingredients.LemonJuice, 30M),
                new CocktailIngredient(Ingredients.Soda, 80)),

            new Cocktail(Cocktails.TomCollins, "Tom Collins",
                new CocktailIngredient(Ingredients.OldTom, 45M),
                new CocktailIngredient(Ingredients.SimpleSyrup, 15M),
                new CocktailIngredient(Ingredients.LemonJuice, 30M),
                new CocktailIngredient(Ingredients.Soda, 60M)),

            new Cocktail(Cocktails.MonkeyGland, "Monkey Gland",
                new CocktailIngredient(Ingredients.Gin, 50M),
                new CocktailIngredient(Ingredients.OrangeJuice, 30M),
                new CocktailIngredient(Ingredients.Absinthe, splash),
                new CocktailIngredient(Ingredients.Grenadine, splash)),

            new Cocktail(Cocktails.PlantersPunch, "Planter's Punch",
                new CocktailIngredient(Ingredients.DarkRum, 45M),
                new CocktailIngredient(Ingredients.OrangeJuice, 35),
                new CocktailIngredient(Ingredients.PineappleJuice, 35),
                new CocktailIngredient(Ingredients.LemonJuice, 20M),
                new CocktailIngredient(Ingredients.Grenadine, 10M),
                new CocktailIngredient(Ingredients.SimpleSyrup, 10M),
                new CocktailIngredient(Ingredients.Angostura, 3 * dash)),

            new Cocktail(Cocktails.RustyNail, "Rusty Nail",
                new CocktailIngredient(Ingredients.Scotch, 45M),
                new CocktailIngredient(Ingredients.Drambuie, 25M)),

            new Cocktail(Cocktails.Sazerac, "Sazerac",
                new CocktailIngredient(Ingredients.Cognac, 50M),
                new CocktailIngredient(Ingredients.Absinthe, 10M),
                new CocktailIngredient(Ingredients.Sugar, teaspoon),
                new CocktailIngredient(Ingredients.Peychauds, 2 * dash)),

            new Cocktail(Cocktails.Screwdriver, "Screwdriver",
                new CocktailIngredient(Ingredients.Vodka, 50M),
                new CocktailIngredient(Ingredients.OrangeJuice, 10M)),

            new Cocktail(Cocktails.Sidecar, "Sidecar",
                new CocktailIngredient(Ingredients.Cognac, 50M),
                new CocktailIngredient(Ingredients.TripleSec, 20M),
                new CocktailIngredient(Ingredients.LemonJuice, 20M)),

            new Cocktail(Cocktails.Tuxedo, "Tuxedo",
                new CocktailIngredient(Ingredients.OldTom, 30M),
                new CocktailIngredient(Ingredients.DryVermouth, 30M),
                new CocktailIngredient(Ingredients.Maraschino, teaspoon / 2),
                new CocktailIngredient(Ingredients.Absinthe, teaspoon / 4),
                new CocktailIngredient(Ingredients.OrangeBitters, 3 * dash)),

            new Cocktail(Cocktails.WhiteLady, "White Lady",
                new CocktailIngredient(Ingredients.Gin, 40M),
                new CocktailIngredient(Ingredients.TripleSec, 30M),
                new CocktailIngredient(Ingredients.LemonJuice, 20M)),

            new Cocktail(Cocktails.LemonDrop, "Lemon Drop",
                new CocktailIngredient(Ingredients.Vodka, 25M),
                new CocktailIngredient(Ingredients.TripleSec, 20M),
                new CocktailIngredient(Ingredients.LemonJuice, 15M)),

            new Cocktail(Cocktails.Cosmopolitan, "Cosmopolitan",
                new CocktailIngredient(Ingredients.Vodka, 40M),
                new CocktailIngredient(Ingredients.Cointreau, 15M),
                new CocktailIngredient(Ingredients.CranberryJuice, 30M),
                new CocktailIngredient(Ingredients.LimeJuice, 15M)),

            new Cocktail(Cocktails.CubaLibre, "Cuba Libre",
                new CocktailIngredient(Ingredients.WhiteRum, 50M),
                new CocktailIngredient(Ingredients.Cola, 120M),
                new CocktailIngredient(Ingredients.LimeJuice, 10M)),

            new Cocktail(Cocktails.Margarita, "Margarita",
                new CocktailIngredient(Ingredients.Tequila, 35M),
                new CocktailIngredient(Ingredients.Cointreau, 20M),
                new CocktailIngredient(Ingredients.LimeJuice, 15M)),

            new Cocktail(Cocktails.Sunrise, "Tequila Sunrise",
                new CocktailIngredient(Ingredients.Tequila, 45M),
                new CocktailIngredient(Ingredients.OrangeJuice, 90M),
                new CocktailIngredient(Ingredients.Grenadine, 15M)),

            new Cocktail(Cocktails.Kamikaze, "Kamikaze",
                new CocktailIngredient(Ingredients.Vodka, 30M),
                new CocktailIngredient(Ingredients.TripleSec, 30M),
                new CocktailIngredient(Ingredients.LimeJuice, 30M)),
        };
    }
}