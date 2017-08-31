using System;
using System.Collections.Generic;

namespace Mix.Models
{
    public class Ingredient
    {
        public Ingredients Id;
        public string Name;
        public IEnumerable<Ingredients> Children;
        public bool Equivalence;
        public bool IsHidden;
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
            IsHidden = options.HasFlag(IngredientOptions.Hidden);
            IsDiscrete = options.HasFlag(IngredientOptions.Discrete);
        }
    }

    [Flags]
    public enum IngredientOptions
    {
        None = 0,
        Equivalence = 1,
        Hidden = 2,
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
    }

    public static class IngredientHelpers
    {
        public static Ingredients Negate(this Ingredients i) => (Ingredients)(-(long)i);
    }
}