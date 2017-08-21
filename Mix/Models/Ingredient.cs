﻿using System;
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
        }
    }

    [Flags]
    public enum IngredientOptions
    {
        None = 0,
        Equivalence = 1,
        Hidden = 2,
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
        Soda,
        Water,
        Cola,
        GingerBeer,
        EggWhite,
        Kahlúa,
        Cream,
    }

    public static class Quantity
    {
        public static decimal Dash = 0.5M;
        public static decimal Splash = 3M;
        public static decimal Teaspoon = 5M;
    }

    public static class IngredientHelpers
    {
        public static Ingredients Negate(this Ingredients i) => (Ingredients)(-(long)i);

        public static string CommonName(this decimal quantity)
        {
            if (quantity == 0) return "";
            else if (quantity < 3.5M * Quantity.Dash)
            {
                var dashes = Math.Round(quantity / Quantity.Dash);
                if (dashes == 1) return "1 dash";
                else return dashes + " dashes";
            }
            else if (quantity < Quantity.Teaspoon) return "A splash";
            else if (quantity <= 2 * Quantity.Teaspoon)
            {
                var teaspoons = Math.Round(quantity / Quantity.Teaspoon);
                if (teaspoons == 1) return "1 teaspoon";
                else return teaspoons + " teaspoons";
            }
            else return quantity.ToString("0.#") + " ml";
        }
    }
}