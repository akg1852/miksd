using Mix.Services;
using Newtonsoft.Json;
using System.Linq;
using System.Web.Mvc;

namespace Mix.Controllers
{
    public class CocktailController : Controller
    {
        private CocktailService cocktailService;

        public CocktailController()
        {
            cocktailService = new CocktailService();
        }

        public ActionResult Index(long id)
        {
            ViewBag.Cocktail = cocktailService.Cocktail(id);
            ViewBag.Ingredients = cocktailService.IngredientCategories();
            return View();
        }

        public ActionResult Search(string q)
        {
            var results = cocktailService.Search(q);
            var json = JsonConvert.SerializeObject(results?.Select(c => new { c.Id, c.Name }));
            return Content(json, "application/json");
        }
    }
}