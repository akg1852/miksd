using System.Collections.Generic;
using i = Mix.Models.Ingredients;

namespace Mix.Models
{
    public class CocktailCategory
    {
        public string Name;
        public List<i> Ingredients;
        public Vessels Vessel;
        public bool Full;

        public static List<CocktailCategory> Categories = new List<CocktailCategory>
        {
            new CocktailCategory
            {
                Name = "Sours",
                Ingredients = new List<i> { i.Spirit, i.Citrus, i.Sweetener,
                    i.Carbonated.Negate()}
            },
            new CocktailCategory
            {
                Name = "Ancestrals",
                Ingredients = new List<i> { i.Spirit, i.Sweetener, i.Bitters,
                    i.Citrus.Negate(), i.Wine.Negate() }
            },
            new CocktailCategory
            {
                Name = "Spirit Forward",
                Ingredients = new List<i> { i.Spirit, i.Vermouth,
                    i.Citrus.Negate()}
            },
            new CocktailCategory
            {
                Name = "Wine Cocktails",
                Ingredients = new List<i> { i.Wine }
            },
            new CocktailCategory
            {
                Name = "Highballs",
                Vessel = Vessels.Highball
            },
            new CocktailCategory
            {
                Name = "Duos",
                Ingredients = new List<i> { i.Spirit, i.SweetLiqueur },
                Full = true
            },
        };
    }
}