using System.Collections.Generic;
using i = Mix.Models.Ingredients;
using v = Mix.Models.Vessels;

namespace Mix.Models
{
    public class CocktailCategory
    {
        public string Name;
        public List<i> Ingredients;
        public List<v> Vessels;
        public bool Full;

        public static List<CocktailCategory> Categories = new List<CocktailCategory>
        {
            new CocktailCategory
            {
                Name = "Spirit Forward",
                Ingredients = new List<i> { i.Spirit, i.Modifier,
                    i.Citrus.Negate(), i.Wine.Negate(), i.Carbonated.Negate() }
            },
            new CocktailCategory
            {
                Name = "Sours",
                Vessels = new List<v> { v.Cocktail, v.Rocks },
                Ingredients = new List<i> { i.Spirit, i.Citrus, i.Sweetener,
                    i.Carbonated.Negate() }
            },
            new CocktailCategory
            {
                Name = "Sweets",
                Vessels = new List<v> { v.Cocktail, v.Rocks, v.Shot },
                Ingredients = new List<i> { i.SweetLiqueur,
                    i.WineAll.Negate() }
            },
            new CocktailCategory
            {
                Name = "Wine Cocktails",
                Ingredients = new List<i> { i.Wine }
            },
            new CocktailCategory
            {
                Name = "Highballs",
                Vessels = new List<v> { v.Highball, v.Rocks },
                Ingredients = new List<i> { i.Spirit, i.Carbonated }
            },
            new CocktailCategory
            {
                Name = "Fruity Cocktails",
                Ingredients = new List<i> { i.NonCitrusFruitJuice,
                    i.Wine.Negate(), i.Vermouth.Negate() }
            },
        };
    }
}