using System.Web.Mvc;
using BusinessLogic.Handlers;
using HotelBookingWebsite.Models;

namespace HotelBookingWebsite.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View(new GlobalTimeViewModel {CurrentTime = DateTimeHandler.GetCurrentTime()});
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
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