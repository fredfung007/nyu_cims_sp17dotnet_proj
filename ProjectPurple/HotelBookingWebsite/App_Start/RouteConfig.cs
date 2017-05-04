﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HotelBookingWebsite
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
                name: "StaffDefault",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Staff", action = "ViewCheckInList", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Additional",
                url: "{controller}/{action}/{id}/{additional}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional, additional = UrlParameter.Optional }
            );
        }
    }
}