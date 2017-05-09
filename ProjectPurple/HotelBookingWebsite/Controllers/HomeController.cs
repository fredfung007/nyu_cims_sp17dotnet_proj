using System.Web.Mvc;
using BusinessLogic.Handlers;
using HotelBookingWebsite.Models;

namespace HotelBookingWebsite.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (User != null)
            {
                ViewBag.isStaff = User.IsInRole("staff");
            }
            return View(new GlobalTimeViewModel {CurrentTime = DateTimeHandler.GetCurrentTime()});
        }

        [HttpPost]
        public ActionResult SetCurrentTime(GlobalTimeViewModel model)
        {
            DateTimeHandler.SetCurrentTime(model.CurrentTime);
            DateTimeHandler.Enabled = true;
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult DisableCustomTime()
        {
            DateTimeHandler.Enabled = false;
            return RedirectToAction("Index");
        }
    }
}