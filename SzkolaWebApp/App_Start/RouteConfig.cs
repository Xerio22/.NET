﻿using System.Web.Mvc;
using System.Web.Routing;

namespace SzkolaWebApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Login",
                url: "login/",
                defaults: new { controller = "Account", action = "Login" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Articles", id = UrlParameter.Optional }
            );
        }
    }
}
