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
        public ActionResult Index()
        {
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

        [HttpGet]
        public ActionResult Search()
        {
            var searchInput = new SearchInputModel();
            return View(searchInput);
        }

        /// <summary>
        /// Return available room list within [start, end). The result type is AvailableRoomViewModel
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public async Task<ActionResult> Search(SearchInputModel model)
        {
            // check null, here the function will throw exception for null input. The default checkIn is today, and checkOut is tomorrow
            DateTime checkIn = model.CheckInDate;
            DateTime checkOut = model.CheckOutDate;

            if (ModelState.IsValid)
            {
                // try async
                List<ROOM_TYPE> availableTypes = _roomHandler.CheckAvailableTypeForDuration(checkIn, checkOut);
                //List<ROOM_TYPE> availableTypes = await _roomHandler.CheckAvailableTypeForDurationAsync(checkIn, checkOut);
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
                        //AvaragePrice = await _roomHandler.GetAveragePriceAsync(type, checkIn, checkOut),
                        //PriceList = await _roomHandler.GetRoomPriceListAsync(type, checkIn, checkOut),
                        AvaragePrice = _roomHandler.GetAveragePrice(type, checkIn, checkOut),
                        PriceList = _roomHandler.GetRoomPriceList(type, checkIn, checkOut),
                        Description = _roomHandler.GetRoomDescription(type),
                        Ameneties = _roomHandler.GetRoomAmeneties(type),
                        PictureUlrs = _roomHandler.GetRoomPictureUrls(type)
                    });
                }

                AvailableRoomViewModel resultModel = new AvailableRoomViewModel
                {
                    SessionId = Guid.NewGuid().ToString(),
                    Expiration = DateTime.Now.AddMinutes(10),
                    RoomSearchResults = availableRooms
                };

                TempData[resultModel.SessionId] = resultModel;

                return RedirectToAction("Result", "Reservation", new { SessionId = resultModel.SessionId });
            }
            return View(model);
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
            if (roomConfirmInfo.Expiration < DateTime.Now)
            {
                return RedirectToAction("Search");
            }

            /*
             * if not autheticated 
            RedirectToAction("Account/Login", new RouteValueDictionary(new
            {
                returnUrl
            } 
            else 
            add a guest to the first guest 
            */
            return View(roomConfirmInfo);
        }

        [HttpGet]
        public ActionResult ShowResult()
        {
            return RedirectToAction("Search");
        }

        //[HttpPost]
        [HttpGet]
        public ActionResult Result(string SessionId)
        {
            
            if (!ModelState.IsValid || String.IsNullOrEmpty(SessionId) || TempData[SessionId] == null || 
                    (TempData[SessionId] as AvailableRoomViewModel).Expiration < DateTime.Now)
            {
                return RedirectToAction("Search");
            }

            return View(TempData[SessionId] as AvailableRoomViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> CreateReservation(ConfirmRoomViewModel roomConfirmInfo)
        {
            if (roomConfirmInfo.Expiration < DateTime.Now)
            {
                return RedirectToAction("Search");
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
            if (roomConfirmInfo.Expiration < DateTime.Now)
            {
                return RedirectToAction("Search");
            }

            return View(roomConfirmInfo);
        }
    }
}