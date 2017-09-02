using Mix.Services;
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
    }
}