using Mix.Services;
using Newtonsoft.Json;
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
            var cocktails = cocktailService.Cocktails();
            ViewBag.Cocktails = JsonConvert.SerializeObject(cocktails);
            return View();
        }
    }
}