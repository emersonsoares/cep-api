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

            config.Formatters.Insert(0, new JsonpMediaTypeFormatter());
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            config.Formatters.JsonFormatter.Indent = true;
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.EnableSystemDiagnosticsTracing();
        }
    }
}
