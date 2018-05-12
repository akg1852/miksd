using Mix.Services;
using Newtonsoft.Json;
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