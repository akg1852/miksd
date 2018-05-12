using Mix.Models;
using Mix.Services;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Mix.Controllers
{
    public class CocktailController : BaseController
    {
        private CocktailService cocktailService;

        public CocktailController()
        {
            cocktailService = new CocktailService();
        }

        public ActionResult Index(long id)
        {
            ViewBag.Cocktail = cocktailService.Cocktail(id);
            return View();
        }

        // GET: Home
        /*
         * i: ingredient(s)
         * c: complete (all ingredients in the query must be in the cocktail)
         * f: full  (all ingredients in the cocktail must be in the query)
         * v: vessel
         * n: name of category
         */
        public ActionResult List(List<Ingredients> i, byte c = 0, byte f = 0, Vessels v = Vessels.None)
        {
            IEnumerable<CocktailMatch> cocktails;

            if (i == null && v == Vessels.None)
            {
                cocktails = cocktailService.FeaturedCocktails();
            }
            else
            {
                var includedIngredients = i?.Where(ii => ii > 0);
                var excludedIngredients = i?.Where(ii => ii < 0)?.Select(ii => ii.Negate());

                cocktails = cocktailService.Cocktails(includedIngredients, excludedIngredients, v);

                if (c != 0)
                {
                    cocktails = cocktails.Where(co => co.Completeness == 1);
                }
                if (f != 0)
                {
                    cocktails = cocktails.Where(co => co.Fullness == 1);
                }
            }

            return JsonContent(cocktails.Select(cocktail => {
                var prepInVessel = cocktail.PrepMethod == PrepMethods.Build || cocktail.PrepMethod == PrepMethods.Layer;
                return new {
                    cocktail.Id,
                    cocktail.Name,
                    recipe = cocktail.Recipe
                        .Where(ingredient => !ingredient.IsOptional)
                        .Select(ingredient => ingredient.Name),
                    description = cocktail.PrepMethodName + (prepInVessel ? " in " : " & strain into ") + @cocktail.VesselName,
                    thumbnail = ImageService.CocktailImage(cocktail, 50, "cocktail-thumbnail"),
                };
            }));
        }

        public ActionResult Search(string q)
        {
            var results = cocktailService.Search(q);
            return JsonContent(results?.Select(c => new { c.Id, c.Name }));
        }

        public ActionResult Ingredients()
        {
            var ingredients = cocktailService.IngredientCategories();
            return JsonContent(ingredients.Select(c => new {
                Category = c.Name,
                Ingredients = c.Ingredients.Select(i => new { i.Id, i.Name })
            }));
        }
    }
}