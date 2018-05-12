using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
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
    }
}