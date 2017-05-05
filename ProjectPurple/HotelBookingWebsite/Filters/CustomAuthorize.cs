﻿using System.Web.Mvc;
using System.Web.Routing;

namespace HotelBookingWebsite.Filters
{
    public class CustomAuthorize : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            bool anonymous = false;
            if (!filterContext.ActionParameters.ContainsKey("Anonymous"))
            {
                anonymous = false;
            }
            else
            {
                anonymous = (bool?) filterContext.ActionParameters["Anonymous"] ?? false;
            }

            if (!anonymous && (filterContext.HttpContext.User.Identity == null || !filterContext.HttpContext.User.Identity.IsAuthenticated))
            {
                filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary
                {
                    { "controller", "Account" },
                    { "action", "Login" },
                    { "returnUrl",    filterContext.HttpContext.Request.RawUrl }
                });
            }
        }
    }

    public class StaffAuthorize : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity == null ||
                !filterContext.HttpContext.User.Identity.IsAuthenticated ||
                filterContext.HttpContext.User.Identity.Name != "staff"
                )
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary
                    {
                        {"controller", "Home" },
                        {"action", "Index" }
                    });
            }
        }
    }
}