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
            ViewBag.IngredientsFilter = i;
            ViewBag.Cocktails = cocktailService.Cocktails(i, true);
            ViewBag.Ingredients = cocktailService.Ingredients();

            return View();
        }
    }
}