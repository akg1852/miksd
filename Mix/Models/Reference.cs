using System.Collections.Generic;

using I = Mix.Models.Ingredient;
using C = Mix.Models.Cocktail;
using CI = Mix.Models.CocktailIngredient;
using i = Mix.Models.Ingredients;
using c = Mix.Models.Cocktails;

namespace Mix.Models
{
    public class Reference
    {
        private static decimal splash = 3M;
        private static decimal dash = 0.5M;
        private static decimal teaspoon = 5M;

        public static List<Ingredient> AllIngredients = new List<Ingredient>
        {
            new I(i.Spirit, "Spirit",
                i.Rum, i.Whisky, i.Vodka, i.Gin, i.Brandy, i.Tequila),

            new I(i.Rum, "Rum", true,
                i.WhiteRum, i.DarkRum),
            new I(i.WhiteRum, "White Rum"),
            new I(i.DarkRum, "Dark Rum"),

            new I(i.Whisky, "Whisk(e)y",
                i.Scotch, i.Bourbon, i.Rye),
            new I(i.Scotch, "Scotch"),
            new I(i.AmericanWhiskey, "American Whisky", true,
                i.Bourbon, i.Rye),
            new I(i.Bourbon, "Bourbon"),
            new I(i.Rye, "Rye Whiskey"),

            new I(i.Vodka, "Vodka"),

            new I(i.Gin, "Gin", true,
                i.OldTom, i.LondonDry),
            new I(i.OldTom, "Old Tom Gin"),
            new I(i.LondonDry, "London Dry Gin"),

            new I(i.Brandy, "Brandy", true,
                i.Cognac, i.Pisco),
            new I(i.Cognac, "Cognac"),
            new I(i.Pisco, "Pisco"),

            new I(i.Tequila, "Tequila"),

            new I(i.Absinthe, "Absinthe"),

            new I(i.Bitters, "Bitters", true,
                i.Angostura, i.OrangeBitters, i.Peychauds),
            new I(i.Angostura, "Angostura Bitters"),
            new I(i.OrangeBitters, "Orange Bitters"),
            new I(i.Peychauds, "Peychaud's Bitters"),

            new I(i.Campari, "Campari"),

            new I(i.Vermouth, "Vermouth", true,
                i.SweetVermouth, i.DryVermouth),
            new I(i.SweetVermouth, "Sweet Red Vermouth"),
            new I(i.DryVermouth, "Dry White Vermouth"),

            new I(i.OrangeLiqueur, "Orange Liqueur",
                i.TripleSec),
            new I(i.TripleSec, "Triple Sec", true,
                i.Cointreau),
            new I(i.Cointreau, "Cointreau"),
            new I(i.Maraschino, "Maraschino Liqueur"),
            new I(i.Drambuie, "Drambuie"),

            new I(i.Grenadine, "Grenadine"),
            new I(i.Sugar, "Sugar", true,
                i.SimpleSyrup),
            new I(i.SimpleSyrup, "Simple Syrup"),

            new I(i.Citrus, "Citrus Juice", true,
                i.LemonJuice, i.LimeJuice),
            new I(i.LemonJuice, "Lemon Juice"),
            new I(i.LimeJuice, "Lime Juice"),
            new I(i.OrangeJuice, "Orange Juice"),
            new I(i.PineappleJuice, "Pineapple Juice"),
            new I(i.CranberryJuice, "Cranberry Juice"),

            new I(i.Water, "Water"),
            new I(i.Soda, "Soda Water"),
            new I(i.Cola, "Cola"),

            new I(i.EggWhite, "Egg White"),
        };

        public static List<Cocktail> AllCocktails = new List<Cocktail>
        {
            new C(c.Americano, "Americano",
                new CI(i.Campari, 30M),
                new CI(i.SweetVermouth, 30M),
                new CI(i.Soda, splash)),

            new C(c.Daiquiri, "Daiquiri",
                new CI(i.WhiteRum, 45M),
                new CI(i.SimpleSyrup, 15M),
                new CI(i.LimeJuice, 25M)),

            new C(c.Martini, "Dry Martini",
                new CI(i.Gin, 60M),
                new CI(i.DryVermouth, 10M)),

            new C(c.Manhattan, "Manhattan",
                new CI(i.Rye, 50M),
                new CI(i.SweetVermouth, 20M),
                new CI(i.Angostura, dash)),

            new C(c.Negroni, "Negroni",
                new CI(i.Gin, 30M),
                new CI(i.Campari, 30M),
                new CI(i.SweetVermouth, 30M)),

            new C(c.OldFashioned, "Old Fashioned",
                new CI(i.Bourbon, 45M),
                new CI(i.Angostura, 2 * dash),
                new CI(i.Sugar, teaspoon),
                new CI(i.Water, splash)),

            new C(c.WhiskeySour, "Whiskey Sour",
                new CI(i.Bourbon, 45M),
                new CI(i.SimpleSyrup, 15M),
                new CI(i.LemonJuice, 30M),
                new CI(i.EggWhite, splash, true)),

            new C(c.PiscoSour, "Pisco Sour",
                new CI(i.Pisco, 45M),
                new CI(i.SimpleSyrup, 20M),
                new CI(i.LemonJuice, 30M),
                new CI(i.EggWhite, splash, true)),

            new C(c.Aviation, "Aviation",
                new CI(i.Gin, 45M),
                new CI(i.Maraschino, 15M),
                new CI(i.LemonJuice, 15M)),

            new C(c.Bacardi, "Bacardi",
                new CI(i.WhiteRum, 45M),
                new CI(i.LimeJuice, 20M),
                new CI(i.Grenadine, 10M)),

            new C(c.BetweenTheSheets, "Between the Sheets",
                new CI(i.Cognac, 30M),
                new CI(i.WhiteRum, 30M),
                new CI(i.TripleSec, 30M),
                new CI(i.LemonJuice, 20M)),

            new C(c.Casino, "Casino",
                new CI(i.OldTom, 40M),
                new CI(i.Maraschino, 10M),
                new CI(i.OrangeBitters, 10M),
                new CI(i.LemonJuice, 10M)),

            new C(c.GinFizz, "Gin Fizz",
                new CI(i.Gin, 45M),
                new CI(i.SimpleSyrup, 10M),
                new CI(i.LemonJuice, 30M),
                new CI(i.Soda, 80)),

            new C(c.TomCollins, "Tom Collins",
                new CI(i.OldTom, 45M),
                new CI(i.SimpleSyrup, 15M),
                new CI(i.LemonJuice, 30M),
                new CI(i.Soda, 60M)),

            new C(c.MonkeyGland, "Monkey Gland",
                new CI(i.Gin, 50M),
                new CI(i.OrangeJuice, 30M),
                new CI(i.Absinthe, splash),
                new CI(i.Grenadine, splash)),

            new C(c.PlantersPunch, "Planter's Punch",
                new CI(i.DarkRum, 45M),
                new CI(i.OrangeJuice, 35),
                new CI(i.PineappleJuice, 35),
                new CI(i.LemonJuice, 20M),
                new CI(i.Grenadine, 10M),
                new CI(i.SimpleSyrup, 10M),
                new CI(i.Angostura, 3 * dash)),

            new C(c.RustyNail, "Rusty Nail",
                new CI(i.Scotch, 45M),
                new CI(i.Drambuie, 25M)),

            new C(c.Sazerac, "Sazerac",
                new CI(i.Cognac, 50M),
                new CI(i.Absinthe, 10M),
                new CI(i.Sugar, teaspoon),
                new CI(i.Peychauds, 2 * dash)),

            new C(c.Screwdriver, "Screwdriver",
                new CI(i.Vodka, 50M),
                new CI(i.OrangeJuice, 10M)),

            new C(c.Sidecar, "Sidecar",
                new CI(i.Cognac, 50M),
                new CI(i.TripleSec, 20M),
                new CI(i.LemonJuice, 20M)),

            new C(c.Tuxedo, "Tuxedo",
                new CI(i.OldTom, 30M),
                new CI(i.DryVermouth, 30M),
                new CI(i.Maraschino, teaspoon / 2),
                new CI(i.Absinthe, teaspoon / 4),
                new CI(i.OrangeBitters, 3 * dash)),

            new C(c.WhiteLady, "White Lady",
                new CI(i.Gin, 40M),
                new CI(i.TripleSec, 30M),
                new CI(i.LemonJuice, 20M)),

            new C(c.LemonDrop, "Lemon Drop",
                new CI(i.Vodka, 25M),
                new CI(i.TripleSec, 20M),
                new CI(i.LemonJuice, 15M)),

            new C(c.Cosmopolitan, "Cosmopolitan",
                new CI(i.Vodka, 40M),
                new CI(i.Cointreau, 15M),
                new CI(i.CranberryJuice, 30M),
                new CI(i.LimeJuice, 15M)),

            new C(c.CubaLibre, "Cuba Libre",
                new CI(i.WhiteRum, 50M),
                new CI(i.Cola, 120M),
                new CI(i.LimeJuice, 10M)),

            new C(c.Margarita, "Margarita",
                new CI(i.Tequila, 35M),
                new CI(i.Cointreau, 20M),
                new CI(i.LimeJuice, 15M)),

            new C(c.Sunrise, "Tequila Sunrise",
                new CI(i.Tequila, 45M),
                new CI(i.OrangeJuice, 90M),
                new CI(i.Grenadine, 15M)),

            new C(c.Kamikaze, "Kamikaze",
                new CI(i.Vodka, 30M),
                new CI(i.TripleSec, 30M),
                new CI(i.LimeJuice, 30M)),
        };
    }
}