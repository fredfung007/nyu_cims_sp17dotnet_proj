using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HotelBookingWebsite.Controllers
{
    public class ReservationController : Controller
    {
        // GET: Reservation
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Show(Guid? ConfirmationId)
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Pay(Guid? ConfirmationId)
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Cancel(Guid? ConfirmationId)
        {
            return View();
        }

        public async Task<ActionResult> Search(DateTime start, DateTime end)
        {
            return View();
        }

        public async Task<ActionResult> ShowResult(Guid? SessionId)
        {
            return View();
        }

        public async Task<ActionResult> SelectRoomType(Guid? SessionId)
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> InputUser(Guid? SessionId)
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateReservation(Guid? SessionId)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Confirm(Guid? SessionId)
        {
            return View();
        }
    }
}