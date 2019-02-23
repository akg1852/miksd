using System;
using System.Collections.Generic;

namespace Mix.Models
{
    public class Ingredient
    {
        public Ingredients Id;
        public string Name;
        public Ingredients Category;
        public IEnumerable<Ingredients> Children;
        public bool Equivalence;
        public bool IsSuper;
        public bool IsDiscrete;

        public Ingredient() { }

        public Ingredient(Ingredients id, string name, params Ingredients[] children)
            : this(id, name, IngredientOptions.None, children) { }

        public Ingredient(Ingredients id, string name, IngredientOptions options, params Ingredients[] children)
        {
            Id = id;
            Name = name;
            Children = children;
            Equivalence = options.HasFlag(IngredientOptions.Equivalence);
            IsSuper = options.HasFlag(IngredientOptions.Super);
            IsDiscrete = options.HasFlag(IngredientOptions.Discrete);
        }
    }

    [Flags]
    public enum IngredientOptions
    {
        None = 0,
        Equivalence = 1,
        Super = 2,
        Discrete = 4,
    }

    public enum Ingredients : long
    {
        None = 0,
        Spirit = 1,
        Whisky = 2,
        Scotch = 3,
        BourbonOrRye = 4,
        Bourbon = 5,
        Rye = 6,
        CognacOrRye = 7,
        Brandy = 8,
        Cognac = 9,
        Pisco = 10,
        Rum = 11,
        WhiteRum = 12,
        DarkRum = 13,
        Vodka = 14,
        Gin = 15,
        OldTom = 16,
        LondonDry = 17,
        Tequila = 18,
        Absinthe = 19,
        Bitters = 20,
        Angostura = 21,
        Peychauds = 22,
        OrangeBitters = 23,
        Campari = 24,
        Vermouth = 25,
        SweetVermouth = 26,
        DryVermouth = 27,
        SweetLiqueur = 28,
        OrangeLiqueur = 29,
        TripleSec = 30,
        Cointreau = 31,
        Maraschino = 32,
        Drambuie = 33,
        Cassis = 34,
        Galliano = 35,
        Amaretto = 36,
        Benedictine = 37,
        Grenadine = 38,
        Wine = 39,
        SparklingWine = 40,
        Champagne = 41,
        Prosecco = 42,
        WhiteWine = 43,
        Sweetener = 44,
        Sugar = 45,
        SimpleSyrup = 46,
        GommeSyrup = 47,
        Citrus = 48,
        LemonJuice = 49,
        LimeJuice = 50,
        OrangeJuice = 51,
        PineappleJuice = 52,
        CranberryJuice = 53,
        GrapefruitJuice = 54,
        PeachPuree = 55,
        Carbonated = 56,
        Soda = 57,
        Water = 58,
        Cola = 59,
        GingerBeer = 60,
        EggWhite = 61,
        Kahlúa = 62,
        Cream = 63,
        MintLeaf = 64,
        Tonic = 65,
        LimeCordial = 66,
        Orgeat = 67,
        Curaçao = 68,
        CoconutCream = 69,
        Baileys = 70,
        GrandMarnier = 71,
        Lime = 72,
        Cachaça = 73,
        Violette = 74,
        WineAll = 75,
        Liqueur = 76,
        BitterLiqueur = 77,
        GingerAle = 78,
        RhumAgricole = 79,
        IslayScotch = 80,
        HoneySyrup = 81,
        GingerSlice = 82,
        Aperol = 83,
        ChartreuseVerte = 84,
        Mûre = 85,
        CherryHeering = 86,
        Cacao = 87,
        SouthernComfort = 88,
        FortifiedWine = 89,
        Port = 90,
        LilletBlanc = 91,
        EggYolk = 92,
        PeachSchnapps = 93,
        Midori = 94,
        BlueCuraçao = 95,
        Lemonade = 96,
        PlymouthGin = 97,
        SloeGin = 98,
        ApricotLiqueur = 99,
        Sherry = 100,
        FruitJuice = 101,
        Mixer = 102,
        ChartreuseJaune = 103,
        Calvados = 104,
        CoconutRum = 105,
        OverproofRum = 106,
        Falernum = 107,
        PassionFruitSyrup = 108,
        CondensedMilk = 109,
        NonCitrusFruitJuice = 110,
        Modifier = 111,
    }

    public static class IngredientHelpers
    {
        public static Ingredients Negate(this Ingredients i) => (Ingredients)(-(long)i);
    }
}