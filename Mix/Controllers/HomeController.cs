﻿using Mix.Models;
using Mix.Services;
using System;
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
        /*
         * i: ingredient(s)
         * m: match (all ingredients in the query must be in the cocktail)
         * f: full  (all ingredients in the cocktail must be in the query)
         * v: vessel
         * n: name of category
         */
        public ActionResult Index(List<Ingredients> i, byte m = 0, byte f = 0,
            Vessels v = Vessels.None, string n = "Cocktails")
        {
            IEnumerable<CocktailMatch> cocktails;
            var matchCount = -1;

            if (i == null && v == Vessels.None)
            {
                cocktails = cocktailService.FeaturedCocktails();
            }
            else
            {
                var includedIngredients = i?.Where(ii => ii > 0);
                var excludedIngredients = i?.Where(ii => ii < 0)?.Select(ii => ii.Negate());

                cocktails = cocktailService.Cocktails(includedIngredients, excludedIngredients, v, true);

                Func<CocktailMatch, bool> IsMatch = c => c.MatchCount >= (includedIngredients?.Count() ?? 0);
                Func<CocktailMatch, bool> IsFull = c => c.Fullness == 1;

                if (m != 0)
                {
                    cocktails = cocktails.Where(IsMatch);
                }
                if (f != 0)
                {
                    cocktails = cocktails.Where(IsFull);
                }
                if (m == 0 && f == 0)
                {
                    matchCount = cocktails.TakeWhile(c => IsFull(c) || IsMatch(c)).Count();
                }
            }

            ViewBag.IngredientsFilter = i;
            ViewBag.Cocktails = cocktails.ToList();
            ViewBag.MatchCount = matchCount;
            ViewBag.Ingredients = cocktailService.Ingredients().Where(ii => !ii.IsHidden);
            ViewBag.Categories = Categories;
            ViewBag.Category = n;

            return View();
        }

        private List<CocktailCategory> Categories = new List<CocktailCategory>
        {
            new CocktailCategory
            {
                Name = "Sours",
                Ingredients = new List<Ingredients> { Ingredients.Spirit, Ingredients.Citrus }
            },
            new CocktailCategory
            {
                Name = "Ancestrals",
                Ingredients = new List<Ingredients> { Ingredients.Spirit, Ingredients.Sugar, Ingredients.Bitters,
                    Ingredients.Citrus.Negate()}
            },
            new CocktailCategory
            {
                Name = "Spirit Forward",
                Ingredients = new List<Ingredients> { Ingredients.Spirit, Ingredients.Vermouth,
                    Ingredients.Citrus.Negate()}
            },
            new CocktailCategory
            {
                Name = "Wine Cocktails",
                Ingredients = new List<Ingredients> { Ingredients.Wine }
            },
            new CocktailCategory
            {
                Name = "Highballs",
                Vessel = Vessels.Highball
            }
        };
    }

    public class CocktailCategory
    {
        public string Name;
        public List<Ingredients> Ingredients;
        public Vessels Vessel;
    }
}