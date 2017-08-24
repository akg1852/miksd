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

    public enum Ingredients : long // todo: explicitly number the ingredients
    {
        None,
        Spirit,
        Whisky,
        Scotch,
        BourbonOrRye,
        Bourbon,
        Rye,
        CognacOrRye,
        Brandy,
        Cognac,
        Pisco,
        Rum,
        WhiteRum,
        DarkRum,
        Vodka,
        Gin,
        OldTom,
        LondonDry,
        Tequila,
        Absinthe,
        Bitters,
        Angostura,
        Peychauds,
        OrangeBitters,
        Campari,
        Vermouth,
        SweetVermouth,
        DryVermouth,
        SweetLiqueur,
        OrangeLiqueur,
        TripleSec,
        Cointreau,
        Maraschino,
        Drambuie,
        Cassis,
        Galliano,
        Amaretto,
        Benedictine,
        Grenadine,
        Wine,
        SparklingWine,
        Champagne,
        Prosecco,
        WhiteWine,
        Sweetener,
        Sugar,
        SimpleSyrup,
        GommeSyrup,
        Citrus,
        LemonJuice,
        LimeJuice,
        OrangeJuice,
        PineappleJuice,
        CranberryJuice,
        GrapefruitJuice,
        PeachPuree,
        Carbonated,
        Soda,
        Water,
        Cola,
        GingerBeer,
        EggWhite,
        Kahlúa,
        Cream,
        MintLeaf,
    }

    public static class IngredientHelpers
    {
        public static Ingredients Negate(this Ingredients i) => (Ingredients)(-(long)i);
    }
}