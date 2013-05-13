using System.Net.Http.Formatting;
using System.Web.Http;
using Newtonsoft.Json.Serialization;
using WebApiContrib.Formatting.Jsonp;

namespace CorreiosRestful.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "cep/{cep}",
                defaults: new { controller = "Cep", action = "Get" },
				constraints: new { cep = @"^\d+$" } // Apenas inteiros
            );

            GlobalConfiguration.Configuration.Formatters.Insert(0, new JsonpMediaTypeFormatter());
            GlobalConfiguration.Configuration.Formatters.Remove(config.Formatters.XmlFormatter);
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.Indent = true;
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}