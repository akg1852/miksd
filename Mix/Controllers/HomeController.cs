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
        public ActionResult Index()
        {
            var ingredientsFiler = IngredientsFilter();

            ViewBag.IngredientsFilter = ingredientsFiler;
            ViewBag.Cocktails = cocktailService.Cocktails(ingredientsFiler, true);
            ViewBag.Ingredients = cocktailService.Ingredients();

            return View();
        }

        private IEnumerable<Ingredients> IngredientsFilter()
        {
            var ii = Request.QueryString["i"];
            if (ii != null)
            {
                return ii.Split(',').Select(i => (Ingredients)long.Parse(i)).ToList();
            }
            else return null;
        }
    }
}