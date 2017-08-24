using System;
using System.Collections.Generic;
using System.Web;

namespace Mix.Models
{
    public class Cocktail
    {
        public Cocktails Id;
        public string Name;
        public IEnumerable<CocktailIngredient> Recipe;
        public Vessels Vessel;
        public string VesselName;
        public IEnumerable<Cocktail> Similar;

        public Cocktail() { }
        public Cocktail(Cocktails id, string name, Vessels vessel, params CocktailIngredient[] recipe)
        {
            Id = id;
            Name = name;
            Recipe = recipe;
            Vessel = vessel;
        }
    }

    public class CocktailIngredient
    {
        public Ingredients Ingredient;
        public string Name;
        public bool IsOptional;
        public decimal Quantity;
        public bool IsDiscrete;

        public CocktailIngredient() { }
        public CocktailIngredient(Ingredients ingredient, decimal quantity, bool isOptional = false)
        {
            Ingredient = ingredient;
            Quantity = quantity;
            IsOptional = isOptional;
        }

        public HtmlString QuantityHtml()
        {
            var quantity = Quantity.ToString("0.#") + (IsDiscrete ? "" : " ml");

            if (IsDiscrete) return new HtmlString(quantity);

            string quantityString;
            if (Quantity < 3.5M * CommonQuantity.Dash)
            {
                var dashes = Math.Round(Quantity / CommonQuantity.Dash);
                if (dashes == 1) quantityString = "1 dash";
                else quantityString = dashes + " dashes";
            }
            else if (Quantity < CommonQuantity.Teaspoon) quantityString = "A splash";
            else if (Quantity <= 2 * CommonQuantity.Teaspoon)
            {
                var teaspoons = Math.Round(Quantity / CommonQuantity.Teaspoon);
                if (teaspoons == 1) quantityString = "1 teaspoon";
                else quantityString = teaspoons + " teaspoons";
            }
            else quantityString = quantity;

            return new HtmlString($"<span title=\"{quantity}\" >{quantityString}</span>");
        }
    }

    public static class CommonQuantity
    {
        public static decimal Dash = 0.5M;
        public static decimal Splash = 3M;
        public static decimal Teaspoon = 5M;
    }

    public enum Cocktails : long
    {
        None,
        Americano,
        Daiquiri,
        Martini,
        Martinez,
        Manhattan,
        Negroni,
        Boulevardier,
        OldFashioned,
        WhiskeySour,
        Aviation,
        Bacardi,
        BetweenTheSheets,
        Casino,
        GinFizz,
        TomCollins,
        MonkeyGland,
        PlantersPunch,
        RustyNail,
        Sazerac,
        Screwdriver,
        Sidecar,
        Tuxedo,
        WhiteLady,
        Cosmopolitan,
        CubaLibre,
        MoscowMule,
        Margarita,
        Sunrise,
        Kamikaze,
        LemonDrop,
        PiscoSour,
        Kir,
        Bellini,
        ChampagneCocktail,
        Mimosa,
        French75,
        FrenchConnection,
        Godfather,
        Godmother,
        SeaBreeze,
        MaryPickford,
        BlackRussian,
        WhiteRussian,
        LongIsland,
        HemingwaySpecial,
        HarveyWallbanger,
        GoldenDream,
        VieuxCarré,
        Mojito,
    }
}