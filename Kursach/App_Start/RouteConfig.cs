using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Kursach
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Home",
                url: "",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
               name: "CreateMetal",
               url: "Scrap/Create",
               defaults: new { controller = "Scrap", action = "Create", id = UrlParameter.Optional }
           );

            routes.MapRoute(
               name: "Arraiving",
               url: "Arraiving/Index",
               defaults: new { controller = "Arraiving", action = "Index", id = UrlParameter.Optional }
           );

            routes.MapRoute(
              name: "Consumption",
              url: "Consumption/Index",
              defaults: new { controller = "Consumption", action = "Index", id = UrlParameter.Optional }
          );

            routes.MapRoute(
              name: "VIPProviders",
              url: "Provider/VIPProdivers",
              defaults: new { controller = "Provider", action = "VIPProdivers", id = UrlParameter.Optional }
          );
            
            routes.MapRoute(
               name: "ConvertHtmlPageToPdf",
               url: "Scrap/ConvertHtmlPageToPdf",
               defaults: new { controller = "Scrap", action = "ConvertHtmlPageToPdf", id = UrlParameter.Optional }
           );
        }
    }
}