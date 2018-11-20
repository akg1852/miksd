using Mix.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Mix.Controllers
{
    public abstract class BaseController : Controller
    {
        private JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        public ContentResult JsonContent(object value)
        {
            var json = JsonConvert.SerializeObject(value, jsonSerializerSettings);
            return Content(json, "application/json");
        }

        protected object CocktailSummary(Cocktail cocktail)
        {
            var prepInVessel = cocktail.PrepMethod == PrepMethods.Build || cocktail.PrepMethod == PrepMethods.Layer;
            return new
            {
                cocktail.Id,
                cocktail.Name,
                recipe = RecipeSummary(cocktail.Recipe),
                description = cocktail.PrepMethodName + (prepInVessel ? " in " : " & strain into ") + @cocktail.VesselName,
                thumbnail = Services.ImageService.CocktailImage(cocktail, 50, "cocktail-thumbnail"),
            };
        }

        protected object RecipeSummary(IEnumerable<CocktailIngredient> ingredients)
        {
            return ingredients
                .Where(ingredient => !ingredient.IsOptional)
                .Select(ingredient => ingredient.Name);
        }
    }
}