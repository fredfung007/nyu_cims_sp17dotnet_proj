using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HotelBookingWebsite.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Profile
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(Guid UserId)
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> UpcommingReservations(Guid UserId)
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> LoyaltyProgram(Guid UserId)
        {
            return View();
        }
    }
}