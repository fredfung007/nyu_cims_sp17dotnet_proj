using BusinessLogic.Handlers;
using DataAccessLayer.Constants;
using DataAccessLayer.EF;
using HotelBookingWebsite.Filters;
using HotelBookingWebsite.Models;
using Microsoft.AspNet.Identity;
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

        public ActionResult Show(Guid ConfirmationId)
        {
            Reservation reservation = _reservationHandler.GetReservation(ConfirmationId);

            return View(new ReservationViewModel
            {
                ConfirmationId = reservation.Id,
            });
        }

        [HttpPost]
        public ActionResult Show(ReservationViewModel model)
        {
            return View(model);
        }

        public async Task<ActionResult> Pay(Guid ConfirmationId, Profile billInfo)
        {
            _reservationHandler.PayReservation(ConfirmationId, billInfo);
            return View(new ReservationViewModel
            {
                ConfirmationId = ConfirmationId,
            });
        }

        [HttpPost]
        public async Task<ActionResult> Pay(ReservationViewModel model)
        {
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Cancel(ReservationViewModel model)
        {
            return View(model);
        }

        // TODO ? use post here?
        public async Task<ActionResult> Cancel(Guid ConfirmationId)
        {
            _reservationHandler.CancelReservation(ConfirmationId, DateTime.Now);
            return View(new ReservationViewModel
            {
                ConfirmationId = ConfirmationId,
            });
        }

        //public ActionResult Search(DateTime? startDate, DateTime? endDate)
        //{
        //    return View(new SearchInputModel
        //    {
        //        StartDate = startDate ?? DateTime.Now,
        //        EndDate = endDate ?? DateTime.Now.AddDays(1)
        //    });
        //}

        public ActionResult Search()
        {
            return View(new SearchInputModel());
        }

        /// <summary>
        /// Return available room list within [start, end). The result type is AvailableRoomViewModel
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Search(SearchInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            ViewBag.NoResult = false;
            DateTime startDate = model.StartDate;
            DateTime endDate = model.EndDate;
            List<ROOM_TYPE> availableTypes = _roomHandler.CheckAvailableTypeForDuration(startDate, endDate);
            // try async
            //List<ROOM_TYPE> availableTypes = await _roomHandler.CheckAvailableTypeForDurationAsync(checkIn, checkOut);
            List<RoomPriceDetail> availableRooms = new List<RoomPriceDetail>();

            foreach (ROOM_TYPE type in availableTypes)
            {
                availableRooms.Add(new RoomPriceDetail
                {
                    StartDate = startDate,
                    EndDate = endDate,
                    Name = type.ToString(),
                    Type = type,
                    // try async
                    //AvaragePrice = await _roomHandler.GetAveragePriceAsync(type, checkIn, checkOut),
                    //PriceList = await _roomHandler.GetRoomPriceListAsync(type, checkIn, checkOut),
                    AvaragePrice = _roomHandler.GetAveragePrice(type, startDate, endDate),
                    PriceList = _roomHandler.GetRoomPriceList(type, startDate, endDate),
                    Description = _roomHandler.GetRoomDescription(type),
                    Ameneties = _roomHandler.GetRoomAmeneties(type),
                    PictureUlrs = _roomHandler.GetRoomPictureUrls(type)
                });
            }

            if (availableTypes.Count > 0)
            {
                string sessionId = Guid.NewGuid().ToString();
                ReservationHandler.SearchResultPool[sessionId] = new RoomSearchResultModel
                {
                    SessionId = Guid.NewGuid().ToString(),
                    Expiration = DateTime.Now.AddMinutes(10),
                    RoomPriceDetails = availableRooms
                };

                return RedirectToAction("Result", new { SessionId = sessionId });
            }

            ViewBag.NoResult = true;
            return View();
        }

        public ActionResult Result(string SessionId)
        {
            if (!ModelState.IsValid ||
                    String.IsNullOrEmpty(SessionId) ||
                    ReservationHandler.SearchResultPool[SessionId] == null ||
                    (ReservationHandler.SearchResultPool[SessionId] as RoomSearchResultModel).Expiration < DateTime.Now)
            {
                return RedirectToAction("Search");
            }

            var result = ReservationHandler.SearchResultPool[SessionId] as RoomSearchResultModel;

            return View(new ResultViewModel
            {
                SessionId = result.SessionId,
                Expiration = result.Expiration,
                RoomPriceDetails = result.RoomPriceDetails,
            });
        }

        [HttpPost]
        public ActionResult Result(ResultViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.Expiration < DateTime.Now)
            {
                return RedirectToAction("Search");
            }

            (ReservationHandler.SearchResultPool[model.SessionId] as RoomSearchResultModel).SelectedIndex = model.SelectedIndex;// ?? 0;

            return RedirectToAction("InputUser", new { SessionId = model.SessionId });
        }

        [CustomAuthorize]
        public async Task<ActionResult> InputUser(string SessionId)
        {
            if (!ModelState.IsValid ||
                    String.IsNullOrEmpty(SessionId) ||
                    ReservationHandler.SearchResultPool[SessionId] == null ||
                    (ReservationHandler.SearchResultPool[SessionId] as RoomSearchResultModel).Expiration < DateTime.Now)
            {
                return RedirectToAction("Search");
            }

            string UserId = User.Identity.Name;
            
            // TODO
            /*
             * Fill in the data to guest
            */

            var result = ReservationHandler.SearchResultPool[SessionId] as RoomSearchResultModel;
            var type = result.RoomPriceDetails[result.SelectedIndex].Type;
            var guests = _reservationHandler.GetEmptyGuestList(type);

            return View(new InputGuestViewModel
            {
                SessionId = result.SessionId,
                Expiration = result.Expiration,
                Guests = guests
            });
        }

        [HttpPost]
        public async Task<ActionResult> InputUser(InputGuestViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.Expiration < DateTime.Now)
            {
                return RedirectToAction("Search");
            }

            (ReservationHandler.SearchResultPool[model.SessionId] as RoomSearchResultModel).Guests = model.Guests;

            return RedirectToAction("Create", new { SessionId = model.SessionId });
        }

        public async Task<ActionResult> Create(string SessionId)
        {
            if (!ModelState.IsValid ||
                    String.IsNullOrEmpty(SessionId) ||
                    ReservationHandler.SearchResultPool[SessionId] == null ||
                    (ReservationHandler.SearchResultPool[SessionId] as RoomSearchResultModel).Expiration < DateTime.Now)
            {
                return RedirectToAction("Search");
            }

            var result = ReservationHandler.SearchResultPool[SessionId] as RoomSearchResultModel;

            return View(new CreateReservationViewModel
            {
                SessionId = result.SessionId,
                Expiration = result.Expiration,
                StartDate = result.RoomPriceDetails[result.SelectedIndex].StartDate,
                EndDate = result.RoomPriceDetails[result.SelectedIndex].EndDate,
                PriceList = result.RoomPriceDetails[result.SelectedIndex].PriceList
            });
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateReservationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.Expiration < DateTime.Now)
            {
                return RedirectToAction("Search");
            }

            var result = ReservationHandler.SearchResultPool[model.SessionId] as RoomSearchResultModel;
            var roomInfo = result.RoomPriceDetails[result.SelectedIndex];
            var userName = User.Identity.Name;
            // comment for debug

            //(ReservationHandler.SearchResultPool[model.SessionId] as RoomSearchResultModel).ReservationId = _reservationHandler.MakeReservation(userName,
            //    roomInfo.Type, roomInfo.StartDate, roomInfo.EndDate, model.Guests, roomInfo.PriceList.ToList());

            return RedirectToAction("Confirm", new { SessionId = model.SessionId });
        }

        public async Task<ActionResult> Confirm(string SessionId)
        {
            return View(ReservationHandler.SearchResultPool[SessionId] as RoomSearchResultModel);
        }

        [HttpPost]
        public async Task<ActionResult> Confirm(RoomSearchResultModel model)
        {
            ReservationHandler.SearchResultPool.Remove(model.SessionId);
            return View(model);
        }
    }
}