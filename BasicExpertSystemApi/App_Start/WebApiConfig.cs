using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace BasicExpertSystemApi
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			// Конфигурация и службы веб-API

			// Маршруты веб-API
			config.MapHttpAttributeRoutes();
			var corsAttr = new EnableCorsAttribute("*", "*", "*");
			config.EnableCors(corsAttr);
			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);
		}
	}
}
