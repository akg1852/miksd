using Mix.Models;
using Mix.Services;
using System.Collections.Generic;
using System.Linq;
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

        public ActionResult Index(string title = "Cocktails")
        {
            return View();
        }
    }
}