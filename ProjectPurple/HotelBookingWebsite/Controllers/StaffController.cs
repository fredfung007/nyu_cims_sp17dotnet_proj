using DataAccessLayer.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HotelBookingWebsite.Controllers
{
    public class StaffController : Controller
    {
        // GET: Staff
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CheckIn(Guid UserId)
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CheckOut(Guid UserId)
        {
            return View();
        }

        public async Task<ActionResult> ViewCheckInList(DateTime date)
        {
            return View();
        }

        public async Task<ActionResult> ViewCheckoutList(DateTime date)
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ModifiyRoomInventory(ROOM_TYPE type)
        {
            return View();
        }

    }
}