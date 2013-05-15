using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Newtonsoft.Json.Serialization;

namespace AddressApi.RestApi
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

            config.Formatters.Remove(config.Formatters.XmlFormatter);
            config.Formatters.JsonFormatter.Indent = true;
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.EnableSystemDiagnosticsTracing();
        }
    }
}
