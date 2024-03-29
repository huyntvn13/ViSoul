﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace UmbracoBP
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            //routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //);

            // Api
            routes.MapRoute(
                "ApiSurface", // Route name
                "Api/{action}", // URL with parameters
                new { action = "LoadPosts", controller = "ApiSurface" }, // parameter defaults 
                new[] { "UmbracoBP.Controllers.SurfaceControllers" } // controller namespaces
            );
        }
    }
}