using System.Collections.Generic;

using I = Mix.Models.Ingredient;
using C = Mix.Models.Cocktail;
using CI = Mix.Models.CocktailIngredient;
using V = Mix.Models.Vessel;
using P = Mix.Models.PrepMethod;
using q = Mix.Models.CommonQuantity;
using i = Mix.Models.Ingredients;
using c = Mix.Models.Cocktails;
using o = Mix.Models.IngredientOptions;
using v = Mix.Models.Vessels;
using p = Mix.Models.PrepMethods;

namespace Mix.Models
{
    public class Reference
    {
        private const bool onTheRocks = true;

        public static List<Ingredient> AllIngredients = new List<Ingredient>
        {
            new I(i.Spirit, "Spirit", o.Hidden,
                i.Rum, i.Whisky, i.Vodka, i.Gin, i.Brandy, i.CognacOrRye, i.Tequila, i.Cachaça),

            new I(i.Rum, "Rum", o.Equivalence,
                i.WhiteRum, i.DarkRum),
            new I(i.WhiteRum, "White Rum"),
            new I(i.DarkRum, "Dark Rum"),
            new I(i.Cachaça, "Cachaça"),

            new I(i.Whisky, "Whisk(e)y",
                i.Scotch, i.BourbonOrRye),
            new I(i.Scotch, "Scotch"),
            new I(i.BourbonOrRye, "Bourbon / Rye", o.Equivalence,
                i.Bourbon, i.Rye),
            new I(i.Bourbon, "Bourbon", o.Hidden),
            new I(i.Rye, "Rye Whiskey", o.Hidden),

            new I(i.CognacOrRye, "Cognac / Rye", o.Hidden,
                i.Brandy, i.BourbonOrRye),

            new I(i.Brandy, "Brandy", o.Equivalence,
                i.Cognac, i.Pisco),
            new I(i.Cognac, "Cognac"),
            new I(i.Pisco, "Pisco"),

            new I(i.Vodka, "Vodka"),

            new I(i.Gin, "Gin", o.Equivalence,
                i.OldTom, i.LondonDry),
            new I(i.OldTom, "Old Tom Gin", o.Hidden),
            new I(i.LondonDry, "London Dry Gin", o.Hidden),

            new I(i.Tequila, "Tequila"),
            new I(i.Absinthe, "Absinthe"),

            new I(i.Bitters, "Bitters", o.Equivalence,
                i.Angostura, i.OrangeBitters, i.Peychauds),
            new I(i.Angostura, "Angostura Bitters", o.Hidden),
            new I(i.OrangeBitters, "Orange Bitters", o.Hidden),
            new I(i.Peychauds, "Peychaud's Bitters", o.Hidden),

            new I(i.Campari, "Campari"),

            new I(i.Vermouth, "Vermouth", o.Equivalence,
                i.SweetVermouth, i.DryVermouth),
            new I(i.SweetVermouth, "Sweet Red Vermouth"),
            new I(i.DryVermouth, "Dry White Vermouth"),

            new I(i.SweetLiqueur, "Liqueur (Sweet)", o.Hidden,
                i.OrangeLiqueur, i.Maraschino, i.Drambuie, i.Cassis, i.Galliano, i.Amaretto, i.Benedictine),
            new I(i.OrangeLiqueur, "Orange Liqueur",
                i.TripleSec, i.Curaçao),
            new I(i.TripleSec, "Triple Sec", o.Equivalence,
                i.Cointreau),
            new I(i.Cointreau, "Cointreau"),
            new I(i.Curaçao, "Curaçao", o.Equivalence,
                i.GrandMarnier),
            new I(i.GrandMarnier, "Grand Marnier"),
            new I(i.Maraschino, "Maraschino Liqueur"),
            new I(i.Drambuie, "Drambuie"),
            new I(i.Cassis, "Crème de cassis"),
            new I(i.Galliano, "Galliano"),
            new I(i.Amaretto, "Amaretto"),
            new I(i.Benedictine, "Benedictine"),
            new I(i.Grenadine, "Grenadine"),

            new I(i.Wine, "Wine", o.Equivalence,
                i.SparklingWine, i.WhiteWine),
            new I(i.SparklingWine, "Sparkling Wine", o.Equivalence,
                i.Champagne, i.Prosecco),
            new I(i.Champagne, "Champagne"),
            new I(i.Prosecco, "Prosecco"),
            new I(i.WhiteWine, "White Wine"),

            new I(i.Sweetener, "Sweetener", o.Hidden,
                i.Sugar, i.Orgeat, i.SweetLiqueur, i.Grenadine, i.LimeCordial),
            new I(i.Sugar, "Sugar", o.Equivalence,
                i.SimpleSyrup, i.GommeSyrup),
            new I(i.SimpleSyrup, "Simple Syrup"),
            new I(i.GommeSyrup, "Gomme Syrup"),
            new I(i.Orgeat, "Orgeat Syrup"),

            new I(i.Citrus, "Citrus", o.Equivalence,
                i.LemonJuice, i.Lime, i.LimeJuice, i.LimeCordial),
            new I(i.LemonJuice, "Lemon Juice"),
            new I(i.Lime, "Lime", o.Equivalence | o.Discrete,
                i.LimeJuice),
            new I(i.LimeJuice, "Lime Juice"),
            new I(i.LimeCordial, "Lime Cordial"),
            new I(i.OrangeJuice, "Orange Juice"),
            new I(i.PineappleJuice, "Pineapple Juice"),
            new I(i.CranberryJuice, "Cranberry Juice"),
            new I(i.GrapefruitJuice, "Grapefruit Juice"),
            new I(i.PeachPuree, "Peach Purée", o.Hidden),

            new I(i.Carbonated, "Carbonated Drink", o.Hidden,
                i.Soda, i.Cola, i.GingerBeer, i.Tonic),
            new I(i.Water, "Water", o.Hidden),
            new I(i.Soda, "Soda Water"),
            new I(i.Tonic, "Tonic Water"),
            new I(i.Cola, "Cola"),
            new I(i.GingerBeer, "Ginger Beer"),

            new I(i.EggWhite, "Egg White"),
            new I(i.Kahlúa, "Kahlúa"),
            new I(i.Baileys, "Baileys Irish Cream"),
            new I(i.Cream, "Cream"),
            new I(i.CoconutCream, "Coconut Cream"),

            new I(i.MintLeaf, "Mint Leaves", o.Discrete),
        };

        public static List<Cocktail> AllCocktails = new List<Cocktail>
        {
            new C(c.Americano, "Americano", onTheRocks,
                v.Rocks, p.Build,
                new CI(i.Campari, 30M),
                new CI(i.SweetVermouth, 30M),
                new CI(i.Soda, q.Splash)),

            new C(c.Daiquiri, "Daiquiri",
                v.Cocktail, p.Shake,
                new CI(i.WhiteRum, 45M),
                new CI(i.SimpleSyrup, 15M),
                new CI(i.LimeJuice, 25M)),

            new C(c.Martini, "Martini",
                v.Cocktail, p.Stir,
                new CI(i.Gin, 60M),
                new CI(i.DryVermouth, 10M)),

            new C(c.Manhattan, "Manhattan",
                v.Cocktail, p.Stir,
                new CI(i.Rye, 50M),
                new CI(i.SweetVermouth, 20M),
                new CI(i.Angostura, q.Dash)),

            new C(c.Martinez, "Martinez",
                v.Cocktail, p.Stir,
                new CI(i.OldTom, 50M),
                new CI(i.SweetVermouth, 30M),
                new CI(i.Maraschino, q.Teaspoon),
                new CI(i.Angostura, q.Dash)),

            new C(c.Negroni, "Negroni", onTheRocks,
                v.Rocks, p.Build,
                new CI(i.Gin, 30M),
                new CI(i.Campari, 30M),
                new CI(i.SweetVermouth, 30M)),

            new C(c.Boulevardier, "Boulevardier", onTheRocks,
                v.Rocks, p.Stir,
                new CI(i.Bourbon, 30M),
                new CI(i.Campari, 30M),
                new CI(i.SweetVermouth, 30M)),

            new C(c.OldFashioned, "Old Fashioned", onTheRocks,
                v.Rocks, p.Build,   
                new CI(i.BourbonOrRye, 45M),
                new CI(i.Angostura, 2 * q.Dash),
                new CI(i.Sugar, q.Teaspoon),
                new CI(i.Water, q.Splash)),

            new C(c.WhiskeySour, "Whiskey Sour",
                v.Cocktail, p.Shake,
                new CI(i.Bourbon, 45M),
                new CI(i.SimpleSyrup, 15M),
                new CI(i.LemonJuice, 30M),
                new CI(i.EggWhite, q.Splash, true)),

            new C(c.PiscoSour, "Pisco Sour",
                v.Rocks, p.Shake,
                new CI(i.Pisco, 45M),
                new CI(i.SimpleSyrup, 20M),
                new CI(i.LemonJuice, 30M),
                new CI(i.EggWhite, q.Splash, true)),

            new C(c.Aviation, "Aviation",
                v.Cocktail, p.Shake,
                new CI(i.Gin, 45M),
                new CI(i.Maraschino, 15M),
                new CI(i.LemonJuice, 15M)),

            new C(c.Bacardi, "Bacardi",
                v.Cocktail, p.Shake,
                new CI(i.WhiteRum, 45M),
                new CI(i.LimeJuice, 20M),
                new CI(i.Grenadine, 10M)),

            new C(c.BetweenTheSheets, "Between the Sheets",
                v.Cocktail, p.Shake,
                new CI(i.Cognac, 30M),
                new CI(i.WhiteRum, 30M),
                new CI(i.TripleSec, 30M),
                new CI(i.LemonJuice, 20M)),

            new C(c.Casino, "Casino",
                v.Cocktail, p.Shake,
                new CI(i.OldTom, 40M),
                new CI(i.Maraschino, 10M),
                new CI(i.OrangeBitters, 10M),
                new CI(i.LemonJuice, 10M)),

            new C(c.GinFizz, "Gin Fizz", onTheRocks,
                v.Highball, p.Shake,
                new CI(i.Gin, 45M),
                new CI(i.SimpleSyrup, 10M),
                new CI(i.LemonJuice, 30M),
                new CI(i.Soda, 80)),

            new C(c.TomCollins, "Tom Collins", onTheRocks,
                v.Highball, p.Build,
                new CI(i.OldTom, 45M),
                new CI(i.SimpleSyrup, 15M),
                new CI(i.LemonJuice, 30M),
                new CI(i.Soda, 60M)),

            new C(c.MonkeyGland, "Monkey Gland",
                v.Cocktail, p.Shake,
                new CI(i.Gin, 50M),
                new CI(i.OrangeJuice, 30M),
                new CI(i.Absinthe, q.Splash),
                new CI(i.Grenadine, q.Splash)),

            new C(c.PlantersPunch, "Planter's Punch", onTheRocks,
                v.Highball, p.Shake,
                new CI(i.DarkRum, 45M),
                new CI(i.OrangeJuice, 35),
                new CI(i.PineappleJuice, 35),
                new CI(i.LemonJuice, 20M),
                new CI(i.Grenadine, 10M),
                new CI(i.SimpleSyrup, 10M),
                new CI(i.Angostura, 3 * q.Dash)),

            new C(c.RustyNail, "Rusty Nail", onTheRocks,
                v.Rocks, p.Build,
                new CI(i.Scotch, 45M),
                new CI(i.Drambuie, 25M)),

            new C(c.Sazerac, "Sazerac",
                v.Rocks, p.Stir,
                new CI(i.CognacOrRye, 50M),
                new CI(i.Absinthe, 10M),
                new CI(i.Sugar, q.Teaspoon),
                new CI(i.Peychauds, 2 * q.Dash)),

            new C(c.Screwdriver, "Screwdriver", onTheRocks,
                v.Highball, p.Build,
                new CI(i.Vodka, 50M),
                new CI(i.OrangeJuice, 10M)),

            new C(c.Sidecar, "Sidecar",
                v.Cocktail, p.Shake,
                new CI(i.Cognac, 50M),
                new CI(i.TripleSec, 20M),
                new CI(i.LemonJuice, 20M)),

            new C(c.Tuxedo, "Tuxedo",
                v.Cocktail, p.Stir,
                new CI(i.OldTom, 30M),
                new CI(i.DryVermouth, 30M),
                new CI(i.Maraschino, q.Teaspoon / 2),
                new CI(i.Absinthe, q.Teaspoon / 4),
                new CI(i.OrangeBitters, 3 * q.Dash)),

            new C(c.WhiteLady, "White Lady",
                v.Cocktail, p.Shake,
                new CI(i.Gin, 40M),
                new CI(i.TripleSec, 30M),
                new CI(i.LemonJuice, 20M)),

            new C(c.LemonDrop, "Lemon Drop",
                v.Cocktail, p.Shake,
                new CI(i.Vodka, 25M),
                new CI(i.TripleSec, 20M),
                new CI(i.LemonJuice, 15M)),

            new C(c.Cosmopolitan, "Cosmopolitan",
                v.Cocktail, p.Shake,
                new CI(i.Vodka, 40M),
                new CI(i.Cointreau, 15M),
                new CI(i.CranberryJuice, 30M),
                new CI(i.LimeJuice, 15M)),

            new C(c.CubaLibre, "Cuba Libre", onTheRocks,
                v.Highball, p.Build,
                new CI(i.WhiteRum, 50M),
                new CI(i.Cola, 120M),
                new CI(i.LimeJuice, 10M)),

            new C(c.MoscowMule, "Moscow Mule", onTheRocks,
                v.Highball, p.Build,
                new CI(i.Vodka, 45M),
                new CI(i.GingerBeer, 120M),
                new CI(i.LimeJuice, 5M)),

            new C(c.GinAndTonic, "Gin and Tonic", onTheRocks,
                v.Highball, p.Build,
                new CI(i.Gin, 50M),
                new CI(i.Tonic, 120M)),

            new C(c.DarkNStormy, "Dark N Stormy", onTheRocks,
                v.Highball, p.Build,
                new CI(i.DarkRum, 60M),
                new CI(i.GingerBeer, 90M)),

            new C(c.Margarita, "Margarita",
                v.Cocktail, p.Shake,
                new CI(i.Tequila, 35M),
                new CI(i.Cointreau, 20M),
                new CI(i.LimeJuice, 15M)),

            new C(c.Sunrise, "Tequila Sunrise", onTheRocks,
                v.Highball, p.Build,
                new CI(i.Tequila, 45M),
                new CI(i.OrangeJuice, 90M),
                new CI(i.Grenadine, 15M)),

            new C(c.Kamikaze, "Kamikaze",
                v.Cocktail, p.Shake,
                new CI(i.Vodka, 30M),
                new CI(i.TripleSec, 30M),
                new CI(i.LimeJuice, 30M)),

            new C(c.Kir, "Kir",
                v.Flute, p.Build,
                new CI(i.WhiteWine, 90M),
                new CI(i.Cassis, 10M)),

            new C(c.Bellini, "Bellini",
                v.Flute, p.Build,
                new CI(i.Prosecco, 100M),
                new CI(i.PeachPuree, 50M)),

            new C(c.ChampagneCocktail, "Champagne Cocktail",
                v.Flute, p.Build,
                new CI(i.Champagne, 90M),
                new CI(i.Cognac, 10M),
                new CI(i.Angostura, 2 * q.Dash),
                new CI(i.Sugar, q.Teaspoon)),

            new C(c.Mimosa, "Mimosa",
                v.Flute, p.Build,
                new CI(i.Champagne, 75M),
                new CI(i.OrangeJuice, 75M)),

            new C(c.French75, "French 75",
                v.Flute, p.Shake,
                new CI(i.Gin, 30M),
                new CI(i.Champagne, 60M),
                new CI(i.LemonJuice, 15M),
                new CI(i.SimpleSyrup, 2 * q.Dash)),

            new C(c.FrenchConnection, "French Connection", onTheRocks,
                v.Rocks, p.Build,
                new CI(i.Cognac, 35M),
                new CI(i.Amaretto, 35M)),

            new C(c.Godfather, "Godfather", onTheRocks,
                v.Rocks, p.Build,
                new CI(i.Scotch, 35M),
                new CI(i.Amaretto, 35M)),

            new C(c.Godmother, "Godmother", onTheRocks,
                v.Rocks, p.Build,
                new CI(i.Vodka, 35M),
                new CI(i.Amaretto, 35M)),

            new C(c.SeaBreeze, "Sea Breeze", onTheRocks,
                v.Highball, p.Build,
                new CI(i.Vodka, 40M),
                new CI(i.CranberryJuice, 120M),
                new CI(i.GrapefruitJuice, 30M)),

            new C(c.MaryPickford, "MaryPickford",
                v.Cocktail, p.Shake,
                new CI(i.WhiteRum, 60M),
                new CI(i.PineappleJuice, 60M),
                new CI(i.Grenadine, 10M),
                new CI(i.Maraschino, 10M)),

            new C(c.BlackRussian, "Black Russian", onTheRocks,
                v.Rocks, p.Build,
                new CI(i.Vodka, 50M),
                new CI(i.Kahlúa, 20M)),

            new C(c.WhiteRussian, "White Russian", onTheRocks,
                v.Rocks, p.Build,
                new CI(i.Vodka, 50M),
                new CI(i.Kahlúa, 20M),
                new CI(i.Cream, 30M)),

            new C(c.LongIsland, "Long Island Iced Tea", onTheRocks,
                v.Highball, p.Build,
                new CI(i.Tequila, 15M),
                new CI(i.Vodka, 15M),
                new CI(i.Gin, 15M),
                new CI(i.WhiteRum, 15M),
                new CI(i.TripleSec, 15M),
                new CI(i.LemonJuice, 25M),
                new CI(i.GommeSyrup, 30M),
                new CI(i.Cola, q.Dash)),

            new C(c.HemingwaySpecial, "Hemingway Special",
                v.Cocktail, p.Shake,
                new CI(i.WhiteRum, 60M),
                new CI(i.GrapefruitJuice, 40M),
                new CI(i.LimeJuice, 15M),
                new CI(i.Maraschino, 15M)),

            new C(c.HarveyWallbanger, "Harvey Wallbanger", onTheRocks,
                v.Highball, p.Build,
                new CI(i.Vodka, 45M),
                new CI(i.Galliano, 15M),
                new CI(i.OrangeJuice, 90M)),

            new C(c.GoldenDream, "Golden Dream",
                v.Cocktail, p.Shake,
                new CI(i.Galliano, 20M),
                new CI(i.TripleSec, 20M),
                new CI(i.OrangeJuice, 20M),
                new CI(i.Cream, 10M)),

            new C(c.VieuxCarré, "Vieux Carré", onTheRocks,
                v.Rocks, p.Stir,
                new CI(i.BourbonOrRye, 60M),
                new CI(i.Cognac, 60M),
                new CI(i.SweetVermouth, 60M),
                new CI(i.Benedictine, q.Teaspoon),
                new CI(i.Angostura, q.Dash),
                new CI(i.Peychauds, q.Dash)),

            new C(c.Mojito, "Mojito", onTheRocks,
                v.Highball, p.Build,
                new CI(i.WhiteRum, 50M),
                new CI(i.LimeJuice, 30M),
                new CI(i.MintLeaf, 12),
                new CI(i.Sugar, 2 * q.Teaspoon),
                new CI(i.Soda, 120M)),

            new C(c.MintJulep, "Mint Julep", onTheRocks,
                v.Highball, p.Build,
                new CI(i.Bourbon, 60M),
                new CI(i.MintLeaf, 12),
                new CI(i.SimpleSyrup, 15M)),

            new C(c.Gimlet, "Gimlet",
                v.Cocktail, p.Shake,
                new CI(i.Gin, 60M),
                new CI(i.LimeCordial, 20M)),

            new C(c.MaiTai, "Mai Tai", onTheRocks,
                v.Rocks, p.Shake,
                new CI(i.WhiteRum, 40M),
                new CI(i.DarkRum, 20M),
                new CI(i.Curaçao, 15),
                new CI(i.Orgeat, 15),
                new CI(i.LimeJuice, 10M)),

            new C(c.PiñaColada, "Piña Colada", onTheRocks,
                v.Highball, p.Shake,
                new CI(i.WhiteRum, 60M),
                new CI(i.CoconutCream, 20M),
                new CI(i.PineappleJuice, 90M),
                new CI(i.LimeJuice, 15M)),

            new C(c.B52, "B-52",
                v.Shot, p.Layer,
                new CI(i.Kahlúa, 20M),
                new CI(i.Baileys, 20M),
                new CI(i.GrandMarnier, 20M)),

            new C(c.Caipirinha, "Caipirinha", onTheRocks,
                v.Rocks, p.Build,
                new CI(i.Cachaça, 60M),
                new CI(i.Lime, 0.5M),
                new CI(i.Sugar, 2 * q.Teaspoon)),
        };

        public static List<Vessel> AllVessels = new List<Vessel>
        {
            new V(v.Cocktail, "Cocktail glass"),
            new V(v.Rocks, "Old Fashioned glass"),
            new V(v.Highball, "Highball/Collins glass"),
            new V(v.Shot, "Shot glass"),
            new V(v.Flute, "Champagne Flute"),
        };

        public static List<PrepMethod> AllPrepMethods = new List<PrepMethod>
        {
            new P(p.Shake, "Shake"),
            new P(p.Stir, "Stir"),
            new P(p.Build, "Build"),
            new P(p.Layer, "Layer"),
        };
    }
}