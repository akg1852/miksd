﻿using System.Collections.Generic;

using I = Mix.Models.Ingredient;
using i = Mix.Models.Ingredients;
using o = Mix.Models.IngredientOptions;

namespace Mix.Models
{
    public static partial class Reference
    {
        public static List<Ingredient> AllIngredients = new List<Ingredient>
        {
            new I(i.Spirit, "Spirit",
                i.Rum, i.Whisky, i.Vodka, i.Gin, i.Brandy, i.CognacOrRye, i.Tequila, i.Cachaça, i.Absinthe),
            new I(i.WineAll, "Wine",
                i.Wine, i.FortifiedWine),
            new I(i.Liqueur, "Liqueur",
                i.SweetLiqueur, i.BitterLiqueur, i.Baileys, i.Kahlúa),

            new I(i.Rum, "Rum", o.Equivalence,
                i.WhiteRum, i.DarkRum, i.RhumAgricole),
            new I(i.WhiteRum, "White Rum"),
            new I(i.DarkRum, "Dark Rum"),
            new I(i.RhumAgricole, "Rhum Agricole"),
            new I(i.Cachaça, "Cachaça"),

            new I(i.Whisky, "Whisk(e)y",
                i.Scotch, i.BourbonOrRye),
            new I(i.Scotch, "Scotch", o.Super,
                i.IslayScotch),
            new I(i.IslayScotch, "Islay Scotch"),
            new I(i.BourbonOrRye, "Bourbon / Rye", o.Super,
                i.Bourbon, i.Rye),
            new I(i.Bourbon, "Bourbon"),
            new I(i.Rye, "Rye Whiskey"),

            new I(i.CognacOrRye, "Cognac / Rye",
                i.Brandy, i.BourbonOrRye),

            new I(i.Brandy, "Brandy", o.Super,
                i.Cognac, i.Pisco),
            new I(i.Cognac, "Cognac"),
            new I(i.Pisco, "Pisco"),

            new I(i.Vodka, "Vodka"),

            new I(i.Gin, "Gin", o.Super,
                i.OldTom, i.LondonDry, i.PlymouthGin),
            new I(i.OldTom, "Old Tom Gin"),
            new I(i.LondonDry, "London Dry Gin"),
            new I(i.PlymouthGin, "Plymouth Gin"),

            new I(i.Tequila, "Tequila"),
            new I(i.Absinthe, "Absinthe"),

            new I(i.Bitters, "Bitters", o.Super,
                i.Angostura, i.OrangeBitters, i.Peychauds),
            new I(i.Angostura, "Angostura Bitters"),
            new I(i.OrangeBitters, "Orange Bitters"),
            new I(i.Peychauds, "Peychaud's Bitters"),

            new I(i.BitterLiqueur, "Liqueur (Bitter)",
                i.Campari, i.Aperol),
            new I(i.Campari, "Campari"),
            new I(i.Aperol, "Aperol"),

            new I(i.SweetLiqueur, "Liqueur (Sweet)",
                i.OrangeLiqueur, i.Maraschino, i.Drambuie, i.Cassis, i.Mûre, i.Violette, i.Cacao,
                i.Galliano, i.Amaretto, i.Benedictine, i.ChartreuseVerte, i.CherryHeering,
                i.SouthernComfort, i.PeachSchnapps, i.Midori, i.SloeGin, i.ApricotLiqueur),
            new I(i.OrangeLiqueur, "Triple Sec / Curaçao", o.Super,
                i.TripleSec, i.Curaçao),
            new I(i.TripleSec, "Triple Sec", o.Equivalence,
                i.Cointreau),
            new I(i.Cointreau, "Cointreau"),
            new I(i.Curaçao, "Curaçao", o.Equivalence,
                i.GrandMarnier, i.BlueCuraçao),
            new I(i.BlueCuraçao, "Blue Curaçao"),
            new I(i.GrandMarnier, "Grand Marnier"),
            new I(i.Maraschino, "Maraschino Liqueur"),
            new I(i.Drambuie, "Drambuie"),
            new I(i.Cassis, "Crème de Cassis"),
            new I(i.Mûre, "Crème de Mûre"),
            new I(i.Violette, "Crème de Violette"),
            new I(i.Cacao, "Crème de Cacao"),
            new I(i.Galliano, "Galliano"),
            new I(i.Amaretto, "Amaretto"),
            new I(i.Benedictine, "Benedictine"),
            new I(i.ChartreuseVerte, "Chartreuse (Green)"),
            new I(i.CherryHeering, "Cherry Heering"),
            new I(i.SouthernComfort, "Southern Comfort"),
            new I(i.PeachSchnapps, "Peach Schnapps"),
            new I(i.Midori, "Midori"),
            new I(i.SloeGin, "Sloe Gin"),
            new I(i.ApricotLiqueur, "Apricot Brandy (Liqueur)"),

            new I(i.Wine, "Wine", o.Equivalence,
                i.SparklingWine, i.WhiteWine),
            new I(i.SparklingWine, "Sparkling Wine", o.Equivalence,
                i.Champagne, i.Prosecco),
            new I(i.Champagne, "Champagne"),
            new I(i.Prosecco, "Prosecco"),
            new I(i.WhiteWine, "White Wine"),

            new I(i.FortifiedWine, "Fortified Wine",
                i.Port, i.Sherry, i.Vermouth, i.LilletBlanc),
            new I(i.Port, "Port"),
            new I(i.Sherry, "Sherry"),
            new I(i.Vermouth, "Vermouth", o.Equivalence,
                i.SweetVermouth, i.DryVermouth),
            new I(i.SweetVermouth, "Sweet Red Vermouth"),
            new I(i.DryVermouth, "Dry White Vermouth"),
            new I(i.LilletBlanc, "Lillet Blanc"),

            new I(i.Sweetener, "Sweetener",
                 i.Sugar, i.Orgeat, i.SweetLiqueur, i.Grenadine, i.LimeCordial, i.HoneySyrup),
            new I(i.Sugar, "Sugar", o.Super,
                i.SimpleSyrup, i.GommeSyrup),
            new I(i.SimpleSyrup, "Simple Syrup"),
            new I(i.GommeSyrup, "Gomme Syrup"),
            new I(i.Grenadine, "Grenadine"),
            new I(i.Orgeat, "Orgeat Syrup"),
            new I(i.HoneySyrup, "Honey Syrup"),

            new I(i.Mixer, "Mixer",
                i.FruitJuice, i.Carbonated),

            new I(i.FruitJuice, "Fruit Juice",
                i.Citrus, i.OrangeJuice, i.PineappleJuice, i.CranberryJuice, i.GrapefruitJuice),
            new I(i.Citrus, "Citrus", o.Equivalence,
                i.LemonJuice, i.LimeJuice),
            new I(i.LemonJuice, "Lemon Juice"),
            new I(i.LimeJuice, "Lime Juice", o.Super,
                i.Lime, i.LimeCordial),
            new I(i.Lime, "Lime", o.Discrete),
            new I(i.LimeCordial, "Lime Cordial"),
            new I(i.OrangeJuice, "Orange Juice"),
            new I(i.PineappleJuice, "Pineapple Juice"),
            new I(i.CranberryJuice, "Cranberry Juice"),
            new I(i.GrapefruitJuice, "Grapefruit Juice"),

            new I(i.Carbonated, "Carbonated Drink",
                i.Soda, i.Cola, i.GingerBeer, i.GingerAle, i.Tonic, i.Lemonade),
            new I(i.Soda, "Soda Water"),
            new I(i.Tonic, "Tonic Water"),
            new I(i.Cola, "Cola"),
            new I(i.GingerBeer, "Ginger Beer"),
            new I(i.GingerAle, "Ginger Ale"),
            new I(i.Lemonade, "Lemonade"),

            new I(i.Water, "Water"),

            new I(i.EggWhite, "Egg White"),
            new I(i.EggYolk, "Egg Yolk"),

            new I(i.Kahlúa, "Kahlúa"),
            new I(i.Baileys, "Baileys Irish Cream"),
            new I(i.Cream, "Cream"),
            new I(i.CoconutCream, "Coconut Cream"),

            new I(i.MintLeaf, "Mint leaves", o.Discrete),
            new I(i.GingerSlice, "Ginger slices", o.Discrete),
            new I(i.PeachPuree, "Peach Purée"),
        };
    }
}