using Mix.Models;
using Mix.Services;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Mix.Controllers
{
    public class HomeController : Controller
    {
        private CocktailService cocktailService;

        public HomeController()
        {
            cocktailService = new CocktailService();
        }

        // GET: Home
        /*
         * i: ingredient(s)
         * c: complete (all ingredients in the query must be in the cocktail)
         * f: full  (all ingredients in the cocktail must be in the query)
         * v: vessel
         * n: name of category
         */
        public ActionResult Index(List<Ingredients> i, byte c = 0, byte f = 0,
            Vessels v = Vessels.None, string n = "Cocktails")
        {
            IEnumerable<CocktailMatch> cocktails;
            var matchCount = -1;

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
                if (c == 0 && f == 0)
                {
                    matchCount = cocktails.TakeWhile(co => (co.Fullness + co.Completeness) > 1).Count();
                }
            }

            ViewBag.IngredientsFilter = i;
            ViewBag.Cocktails = cocktails.ToList();
            ViewBag.MatchCount = matchCount;
            ViewBag.Ingredients = cocktailService.AllIngredients().Where(ii => !ii.IsHidden);
            ViewBag.Category = n;

            return View();
        }
    }
}