using System.Web.Mvc;
using System.Web.Routing;

namespace HotelBookingWebsite.Filters
{
    public class CustomAuthorize : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            bool anonymous = filterContext.ActionParameters.ContainsKey("Anonymous") && (bool) filterContext.ActionParameters["Anonymous"];
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
}