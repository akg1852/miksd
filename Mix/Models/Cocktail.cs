using System.Collections.Generic;

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

        public CocktailIngredient() { }
        public CocktailIngredient(Ingredients ingredient, decimal quantity, bool isOptional = false)
        {
            Ingredient = ingredient;
            Quantity = quantity;
            IsOptional = isOptional;
        }
    }

    public enum Cocktails : long
    {
        None,
        Americano,
        Daiquiri,
        Martini,
        Manhattan,
        Negroni,
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
        Margarita,
        Sunrise,
        Kamikaze,
        LemonDrop,
        PiscoSour,
        Kir,
        Bellini,
        ChampagneCocktail,
    }
}