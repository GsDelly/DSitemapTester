﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DSitemapTester
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional });


            routes.MapRoute(
                name: "Test",
                url: "{controller}/{action}/{id}/{timeout}/{testsCount}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional, timeout = UrlParameter.Optional, testsCount = UrlParameter.Optional });
        }
    }
}
