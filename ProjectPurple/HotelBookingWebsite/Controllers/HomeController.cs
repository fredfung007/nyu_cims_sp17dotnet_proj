using BusinessLogic.Handlers;
using HotelBookingWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelBookingWebsite.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View(new GlobalTimeViewModel { CurrentTime = DateTimeHandler.GetCurrentTime() });
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
            return RedirectToAction("Index");
        }
    }
}