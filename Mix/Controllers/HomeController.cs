using Mix.Services;
using System.Web.Mvc;

namespace Mix.Controllers
{
    public class HomeController : BaseController
    {
        private CocktailService cocktailService;

        public HomeController()
        {
            cocktailService = new CocktailService();
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}