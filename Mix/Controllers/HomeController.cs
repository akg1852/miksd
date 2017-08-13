using Mix.Services;
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
            ViewBag.Cocktails = cocktailService.Cocktails();
            return View();
        }
    }
}