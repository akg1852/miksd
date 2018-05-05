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
            return View();
        }

        public ActionResult Search(string q)
        {
            var results = cocktailService.Search(q);
            var json = JsonConvert.SerializeObject(results?.Select(c => new { c.Id, c.Name }));
            return Content(json, "application/json");
        }

        public ActionResult Ingredients()
        {
            var results = cocktailService.IngredientCategories();
            var json = JsonConvert.SerializeObject(results.Select(c => new {
                Category = c.Name,
                Ingredients = c.Ingredients.Select(i => new { i.Id, i.Name })
            }));
            return Content(json, "application/json");
        }
    }
}