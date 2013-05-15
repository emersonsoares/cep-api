using System.Web.Http;
using Newtonsoft.Json.Serialization;
using WebApiContrib.Formatting.Jsonp;

namespace AddressApi.RestApi.App_Start
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "cep/{zipCode}",
                defaults: new { controller = "Address", action = "Get" },
                constraints: new { zipCode = @"^\d+$" }
            );

            var jsonpFormatter = new JsonpMediaTypeFormatter { Indent = true };

            config.Formatters.Insert(0, jsonpFormatter);
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.Formatters.JsonFormatter.Indent = true;
            config.EnableSystemDiagnosticsTracing();
        }
    }
}
