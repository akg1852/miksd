using Mix.Models;
using Mix.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Mix.Controllers
{
    public class MenuController : BaseController
    {
        private CocktailService cocktailService;

        public MenuController()
        {
            cocktailService = new CocktailService();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Edit(Guid id)
        {
            return View();
        }

        public ActionResult View(Guid id)
        {
            return View();
        }

        public ActionResult Data(List<Cocktails> c)
        {
            var cocktails = cocktailService.Cocktails(c);
            return JsonContent(cocktails.Select(cocktail => new {
                cocktail.Id,
                cocktail.Name,
                recipe = RecipeSummary(cocktail.Recipe)
            }));
        }

        public ActionResult Create()
        {
            return JsonContent(new
            {
                id = Guid.NewGuid()
            });
        }
    }
}