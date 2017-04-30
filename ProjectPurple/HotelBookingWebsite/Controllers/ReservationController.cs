using BusinessLogic.Handlers;
using DataAccessLayer.Constants;
using DataAccessLayer.EF;
using HotelBookingWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

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
        public ActionResult Index(DateTime? start, DateTime? end)
        {
            ViewBag.Start = start ?? DateTime.Now;
            ViewBag.End = end ?? DateTime.Now.AddDays(1);

            return View();
        }

        [HttpGet]
        public ActionResult Show(Guid ConfirmationId)
        {
            Reservation reservation = _reservationHandler.GetReservation(ConfirmationId);

            return View(new ReservationViewModel
            {
                ConfirmationId = reservation.Id,
            });
        }

        [HttpPost]
        public async Task<ActionResult> Pay(Guid ConfirmationId, Profile billInfo)
        {
            _reservationHandler.PayReservation(ConfirmationId, billInfo);
            return View(new ReservationViewModel
            {
                ConfirmationId = ConfirmationId,
            });
        }

        [HttpPost]
        public async Task<ActionResult> Cancel(Guid ConfirmationId)
        {
            _reservationHandler.CancelReservation(ConfirmationId, DateTime.Now);
            return View(new ReservationViewModel
            {
                ConfirmationId = ConfirmationId,
            });
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
            List<RoomSearchResult> availableRooms = new List<RoomSearchResult>();

            foreach (ROOM_TYPE type in availableTypes)
            {
                availableRooms.Add(new RoomSearchResult
                {
                    CheckIn = checkIn,
                    CheckOut = checkOut,
                    Name = type.ToString(),
                    Type = type,
                    // try async
                    AvaragePrice = await _roomHandler.GetAveragePriceAsync(type, checkIn, checkOut),
                    PriceList = await _roomHandler.GetRoomPriceListAsync(type, checkIn, checkOut),
                    Description = _roomHandler.GetRoomDescription(type),
                    Ameneties = _roomHandler.GetRoomAmeneties(type),
                    PictureUlrs = _roomHandler.GetRoomPictureUrls(type)
                });
            }

            return View(new AvailableRoomViewModel
            {
                SessionId = Guid.NewGuid().ToString(),
                Expiration = DateTime.Now.AddMinutes(10),
                RoomSearchResults = availableRooms
            });
        }

        //public async Task<ActionResult> ShowResult(AvailableRoomViewModel searchResult)
        //{
        //    return View();
        //}

        //public async Task<ActionResult> SelectRoomType(AvailableRoomViewModel searchResult)
        //{
        //    return View();
        //}

        [HttpPost]
        public async Task<ActionResult> InputUser(ConfirmRoomViewModel roomConfirmInfo)
        {
            if (roomConfirmInfo.Expiration > DateTime.Now)
            {
                return RedirectToAction("Index", new RouteValueDictionary(new
                {
                    start = roomConfirmInfo.RoomSearchResult.CheckIn,
                    end = roomConfirmInfo.RoomSearchResult.CheckOut
                }));
            }

            /*
            RedirectToAction("Account/Login", new RouteValueDictionary(new
            {
                returnUrl
            }    
            */
            return View(roomConfirmInfo);
        }

        [HttpPost]
        public async Task<ActionResult> CreateReservation(ConfirmRoomViewModel roomConfirmInfo)
        {
            if (roomConfirmInfo.Expiration > DateTime.Now)
            {
                return RedirectToAction("Index", new RouteValueDictionary(new
                {
                    start = roomConfirmInfo.RoomSearchResult.CheckIn,
                    end = roomConfirmInfo.RoomSearchResult.CheckOut
                }));
            }

            var roomInfo = roomConfirmInfo.RoomSearchResult;
            var guests = roomConfirmInfo.Guests;
            var userName = "";
            roomConfirmInfo.ReservationId = _reservationHandler.MakeReservation(userName,
                                                roomInfo.Type, roomInfo.CheckIn, roomInfo.CheckOut, guests, roomInfo.PriceList.ToList());

            return View(roomConfirmInfo);
        }

        [HttpPost]
        public async Task<ActionResult> Confirm(ConfirmRoomViewModel roomConfirmInfo)
        {
            if (roomConfirmInfo.Expiration > DateTime.Now)
            {
                return RedirectToAction("Index", new RouteValueDictionary(new
                {
                    start = roomConfirmInfo.RoomSearchResult.CheckIn,
                    end = roomConfirmInfo.RoomSearchResult.CheckOut
                }));
            }

            return View(roomConfirmInfo);
        }
    }
}