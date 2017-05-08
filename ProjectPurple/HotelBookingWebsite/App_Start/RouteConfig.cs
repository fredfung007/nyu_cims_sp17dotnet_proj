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
                "Default",
                "{controller}/{action}/{id}",
                new {controller = "Home", action = "Index", id = UrlParameter.Optional}
            );

            routes.MapRoute(
                "StaffDefault",
                "{controller}/{action}",
                new {controller = "Staff", action = "Index", id = UrlParameter.Optional}
            );

            routes.MapRoute(
                "Additional",
                "{controller}/{action}/{id}/{additional}",
                new
                {
                    controller = "Home",
                    action = "Index",
                    id = UrlParameter.Optional,
                    additional = UrlParameter.Optional
                }
            );
        }
    }
}