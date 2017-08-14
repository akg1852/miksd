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
        public ActionResult Index(List<Ingredients> i)
        {
            var cocktails = cocktailService.Cocktails(i, true).ToList();
            var matchCount = i == null ? -1 : cocktails.TakeWhile(c => c.Count >= i.Count).Count();

            ViewBag.IngredientsFilter = i;
            ViewBag.Cocktails = cocktails;
            ViewBag.MatchCount = matchCount;
            ViewBag.Ingredients = cocktailService.Ingredients();

            return View();
        }
    }
}