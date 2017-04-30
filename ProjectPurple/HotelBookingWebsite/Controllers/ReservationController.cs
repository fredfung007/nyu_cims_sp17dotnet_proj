using BusinessLogic.Handlers;
using DataAccessLayer.Constants;
using HotelBookingWebsite.Models;
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
        private ReservationHandler _reservationHandler;
        private RoomHandler _roomHandler;

        public ReservationController()
        {
            _reservationHandler = new ReservationHandler();
            _roomHandler = new RoomHandler();
        }

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

        /// <summary>
        /// Return available room list within [start, end). The result type is AvailableRoomViewModel
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public async Task<ActionResult> Search(DateTime? start, DateTime? end)
        {
            // check null, here the function will throw exception for null input. The default checkIn is today, and checkOut is tomorrow
            DateTime checkIn = start ?? DateTime.Now;
            DateTime checkOut = end ?? DateTime.Now.AddDays(1);

            // try async
            List<ROOM_TYPE> availableTypes = await _roomHandler.CheckAvailableTypeForDurationAsync(checkIn, checkOut);
            List<AvailableRoom> availableRooms = new List<AvailableRoom>();

            foreach (ROOM_TYPE type in availableTypes)
            {
                availableRooms.Add(new AvailableRoom
                {
                    Name = type.ToString(),
                    // try async
                    AvaragePrice = await _roomHandler.GetAveragePriceAsync(type, checkIn, checkOut), 
                    Description = _roomHandler.GetRoomDescription(type),
                    Ameneties = _roomHandler.GetRoomAmeneties(type),
                    PictureUlrs = _roomHandler.GetRoomPictureUrls(type)
                });
            }

            return View(new AvailableRoomViewModel
            {
                AvailableRooms = availableRooms
            });
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
        public async Task<ActionResult> Confirm(Guid? SessionId)
        {
            return View();
        }
    }
}