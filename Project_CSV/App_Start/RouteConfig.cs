using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Project_CSV
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Main",
                url: "Main/Main",
                defaults: new { controller = "Main", action = "Main", id = UrlParameter.Optional }
            );

            routes.MapRoute(
               name: "Insert",
               url: "Main/Insert",
               defaults: new { controller = "Main", action = "Insert", id = UrlParameter.Optional }
           );

            routes.MapRoute(
            name: "Upload",
            url: "Main/Upload",
            defaults: new { controller = "Main", action = "Upload", id = UrlParameter.Optional }
            );

            routes.MapRoute(
            name: "Download",
            url: "Main/Download",
            defaults: new { controller = "Main", action = "Download", id = UrlParameter.Optional }
            );      
        }
    }
}
