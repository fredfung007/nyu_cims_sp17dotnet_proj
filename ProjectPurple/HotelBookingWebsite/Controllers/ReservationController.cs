﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using BusinessLogic.Handlers;
using DataAccessLayer.Constants;
using DataAccessLayer.EF;
using HotelBookingWebsite.Filters;
using HotelBookingWebsite.Helper;
using HotelBookingWebsite.Models;

namespace HotelBookingWebsite.Controllers
{
    public class ReservationController : Controller
    {
        private readonly ReservationHandler _reservationHandler;
        private readonly RoomHandler _roomHandler;
        private readonly AspNetUserHandler _userHandler;

        public ReservationController()
        {
            _reservationHandler = new ReservationHandler();
            _roomHandler = new RoomHandler();
            _userHandler = new AspNetUserHandler();
        }

        // GET: Reservation
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Show(Guid? ConfirmationId)
        {
            var confirmationId = ConfirmationId ?? Guid.Empty;
            if (!_reservationHandler.HashReservation(confirmationId.ToString()))
            {
                return RedirectToAction("Error", "Reservation",
                    new ErrorViewModel { ErrorMsg = "Invalid Confirmation Id" });
            }

            var reservation = _reservationHandler.GetReservation(confirmationId);

            ViewBag.canCancel = _reservationHandler.CanBeCanceled(reservation.Id, DateTimeHandler.GetCurrentTime());

            return View(GetConfirmationViewModel(reservation));
        }

        [HttpPost]
        public ActionResult Show(ConfirmationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            return RedirectToAction("Cancel", "Reservation",
                new { ConfirmationViewModel = model, returnUrl = HttpContext.Request.RawUrl });
        }

        [HttpPost]
        public ActionResult Cancel(ConfirmationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var success = _reservationHandler.CancelReservation(Guid.Parse(model.ConfirmationId),
                DateTimeHandler.GetCurrentTime());

            if (!success)
            {
                ViewBag.Failed = true;
                return View(model);
            }

            if (string.IsNullOrEmpty(ViewBag.returnUrl))
            {
                ViewBag.returnUrl = "~/Home/Index";
            }

            return RedirectToLocal(ViewBag.returnUrl);
        }

        private ConfirmationViewModel GetConfirmationViewModel(Reservation reservation)
        {
            var priceList = reservation.DailyPrices.Select(x => x.BillingPrice).ToList();
            return new ConfirmationViewModel
            {
                ConfirmationId = reservation.Id.ToString(),
                StartDate = reservation.StartDate,
                EndDate = reservation.EndDate,
                Guests = reservation.Guests.ToList().ToGuestModelList(),
                Type = _roomHandler.GetRoomTypeName(reservation.RoomType),
                Ameneties = _roomHandler.GetRoomAmeneties(reservation.RoomType),
                PriceList = priceList,
                IsCanceled = reservation.IsCancelled,
            };
        }

        public ActionResult Cancel(Guid? confirmationId, string returnUrl)
        {
            if (confirmationId == null)
            {
                return RedirectToAction("Error", "Reservation",
                    new ErrorViewModel { ErrorMsg = "No Confirmation Id" });
            }

            var reservation = _reservationHandler.GetReservation((Guid)confirmationId);
            if (reservation == null)
            {
                return RedirectToAction("Error", "Reservation",
                    new ErrorViewModel { ErrorMsg = "Invalid Confirmation Id" });
            }

            ViewBag.returnUrl = returnUrl;

            return View(GetConfirmationViewModel(reservation));
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Retrieve()
        {
            return View(new RetrieveModel());
        }

        [HttpPost]
        public ActionResult Retrieve(RetrieveModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            return RedirectToAction("Confirm", "Reservation", new { model.ConfirmationId });
        }

        public ActionResult Search()
        {
            return View(new SearchInputModel());
        }

        private RoomPriceDetail GetPriceDetails(DateTime startDate, DateTime endDate, ROOM_TYPE type)
        {
            var prices = _roomHandler.GetRoomPriceList(type, startDate, endDate);
            return new RoomPriceDetail
            {
                StartDate = startDate,
                EndDate = endDate,
                Name = _roomHandler.GetRoomTypeName(type),
                Type = type,
                PriceList = prices,
                AvaragePrice = prices.Sum() / (endDate - startDate).Days,
                Description = _roomHandler.GetRoomDescription(type),
                Ameneties = _roomHandler.GetRoomAmeneties(type),
                PictureUlrs = _roomHandler.GetRoomPictureUrls(type)
            };
        }

        /// <summary>
        ///     Return available room list within [start, end). The result type is AvailableRoomViewModel
        /// </summary>
        [HttpPost]
        public ActionResult Search(SearchInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.StartDate.Date >= model.EndDate.Date)
            {
                return RedirectToAction("Error", "Reservation",
                    new ErrorViewModel { ErrorMsg = "start date must be before end date" });
            }


            if (model.StartDate.Date < DateTimeHandler.GetCurrentDate())
            {
                return RedirectToAction("Error", "Reservation",
                    new ErrorViewModel { ErrorMsg = "Start date must not be before today" });
            }

            ViewBag.NoResult = false;
            var startDate = model.StartDate.GetStartDateTime();
            var endDate = model.EndDate.Date.GetEndDateTime();
            var availableRooms = new List<RoomPriceDetail>();

            foreach (ROOM_TYPE type in Enum.GetValues(typeof(ROOM_TYPE)))
            {
                if (_roomHandler.IsAvailable(type, startDate, endDate))
                {
                    availableRooms.Add(GetPriceDetails(startDate, endDate, type));
                }
            }

            if (availableRooms.Count <= 0)
            {
                return RedirectToAction("Error", "Reservation", new ErrorViewModel { ErrorMsg = "No available room, please search again" });
            }

            var sessionId = Guid.NewGuid().ToString();
            ReservationHandler.SearchResultPool[sessionId] = new RoomSearchResultModel
            {
                SessionId = sessionId,
                Expiration = DateTimeHandler.GetCurrentTime().AddMinutes(10),
                RoomPriceDetails = availableRooms
            };

            return RedirectToAction("Result", "Reservation", new { SessionId = sessionId });
        }

        public ActionResult Result(string sessionId)
        {
            if (!ReservationHandler.SearchResultPool.ContainsKey(sessionId))
            {
                return RedirectToAction("Error", "Reservation",
                    new ErrorViewModel { ErrorMsg = "The reservation is expired or submitted" });
            }

            if (!ModelState.IsValid ||
                string.IsNullOrEmpty(sessionId) ||
                ReservationHandler.SearchResultPool[sessionId] == null)
            {
                return RedirectToAction("Search", "Reservation");
            }

            var resultModel = ReservationHandler.SearchResultPool[sessionId] as RoomSearchResultModel;
            if (resultModel == null || resultModel.Expiration <
                DateTimeHandler.GetCurrentTime())
            {
                return RedirectToAction("Expired", "Reservation");
            }

            var result = ReservationHandler.SearchResultPool[sessionId] as RoomSearchResultModel;

            return View(new ResultViewModel
            {
                SessionId = result.SessionId,
                Expiration = result.Expiration,
                RoomPriceDetails = result.RoomPriceDetails
            });
        }

        [HttpPost]
        public ActionResult Result(ResultViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.Expiration < DateTimeHandler.GetCurrentTime() || ReservationHandler.SearchResultPool[model.SessionId] == null)
            {
                return RedirectToAction("Expired", "Reservation");
            }

            var roomSearchResultModel = ReservationHandler.SearchResultPool[model.SessionId] as RoomSearchResultModel;
            if (roomSearchResultModel != null)
            {
                roomSearchResultModel.SelectedIndex =
                    model.SelectedIndex;
            }

            return RedirectToAction("InputUser", "Reservation", new { model.SessionId, anomyous = false });
        }

        private List<GuestViewModel> GetEmptyGuestModelList(ROOM_TYPE type)
        {
            var guests = new List<GuestViewModel>();
            var guestMaxCount = type == ROOM_TYPE.DoubleBedRoom || type == ROOM_TYPE.Suite ? 4 : 2;

            for (var i = 0; i < guestMaxCount; i++)
            {
                guests.Add(new GuestViewModel { Id = Guid.NewGuid(), Order = i });
            }

            return guests;
        }

        [CustomAuthorize]
        public ActionResult InputUser(string sessionId, bool? anonymous)
        {
            if (!ReservationHandler.SearchResultPool.ContainsKey(sessionId))
            {
                return RedirectToAction("Error", "Reservation",
                    new ErrorViewModel { ErrorMsg = "The reservation is expired or submitted" });
            }

            if (!ModelState.IsValid ||
                string.IsNullOrEmpty(sessionId) ||
                ReservationHandler.SearchResultPool[sessionId] == null)
            {
                return RedirectToAction("Search", "Reservation");
            }

            var resultModel = ReservationHandler.SearchResultPool[sessionId] as RoomSearchResultModel;
            if (resultModel == null || resultModel.Expiration <
                DateTimeHandler.GetCurrentTime())
            {
                return RedirectToAction("Expired", "Reservation");
            }

            var result = ReservationHandler.SearchResultPool[sessionId] as RoomSearchResultModel;
            var type = result.RoomPriceDetails[result.SelectedIndex].Type;
            result.Guests = GetEmptyGuestModelList(type);

            // TOOD check here use extension function
            if (User.Identity.IsAuthenticated)
            {
                var profile = _userHandler.GetProfile(User.Identity.Name);
                result.Guests[0].FirstName = profile.FirstName;
                result.Guests[0].LastName = profile.LastName;
            }

            return View(new InputGuestViewModel
            {
                SessionId = result.SessionId,
                Expiration = result.Expiration,
                Guests = result.Guests
            });
        }

        [HttpPost]
        public ActionResult InputUser(InputGuestViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.Expiration < DateTimeHandler.GetCurrentTime() || ReservationHandler.SearchResultPool[model.SessionId] == null)
            {
                return RedirectToAction("Expired", "Reservation");
            }

            //model.Guests.RemoveAll(guest => string.IsNullOrWhiteSpace(guest.FirstName) &&
            //                                string.IsNullOrWhiteSpace(guest.LastName));

            (ReservationHandler.SearchResultPool[model.SessionId] as RoomSearchResultModel).Guests = model.Guests.CleanEmpty();

            return RedirectToAction("Create", "Reservation", new { model.SessionId });
        }

        public ActionResult Create(string sessionId)
        {
            if (!ReservationHandler.SearchResultPool.ContainsKey(sessionId))
            {
                return RedirectToAction("Error", "Reservation",
                    new ErrorViewModel { ErrorMsg = "The reservation is expired or submitted" });
            }

            if (!ModelState.IsValid ||
                string.IsNullOrEmpty(sessionId) ||
                ReservationHandler.SearchResultPool[sessionId] == null)
            {
                return RedirectToAction("Search", "Reservation");
            }

            var resultModel = ReservationHandler.SearchResultPool[sessionId] as RoomSearchResultModel;
            if (resultModel == null || resultModel.Expiration <
                DateTimeHandler.GetCurrentTime())
            {
                return RedirectToAction("Expired", "Reservation");
            }

            var result = ReservationHandler.SearchResultPool[sessionId] as RoomSearchResultModel;

            return View(new CreateReservationViewModel
            {
                SessionId = result.SessionId,
                Expiration = result.Expiration,
                IsRoomAvailable = true,
                StartDate = result.RoomPriceDetails[result.SelectedIndex].StartDate,
                EndDate = result.RoomPriceDetails[result.SelectedIndex].EndDate,
                PriceList = result.RoomPriceDetails[result.SelectedIndex].PriceList,
                TypeName = result.RoomPriceDetails[result.SelectedIndex].Name
            });
        }

        public ActionResult NotAvailable(string sessionId)
        {
            if (!ReservationHandler.SearchResultPool.ContainsKey(sessionId))
            {
                return RedirectToAction("Error", "Reservation",
                    new ErrorViewModel { ErrorMsg = "The reservation is expired or submitted" });
            }

            var resultModel = ReservationHandler.SearchResultPool[sessionId] as RoomSearchResultModel;
            if (resultModel == null || !ModelState.IsValid || string.IsNullOrEmpty(sessionId) ||
                ReservationHandler.SearchResultPool[sessionId] == null || resultModel.RoomPriceDetails.Count == 0)
            {
                return RedirectToAction("Search", "Reservation");
            }

            var result = ReservationHandler.SearchResultPool[sessionId] as RoomSearchResultModel;
            ReservationHandler.SearchResultPool.Remove(sessionId);

            return View(result);
        }

        public ActionResult Expired()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CreateReservationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.Expiration < DateTimeHandler.GetCurrentTime() || ReservationHandler.SearchResultPool[model.SessionId] == null)
            {
                return RedirectToAction("Expired", "Reservation");
            }

            var result = ReservationHandler.SearchResultPool[model.SessionId] as RoomSearchResultModel;
            var roomInfo = result.RoomPriceDetails[result.SelectedIndex];

            if (result.IsConfirmed && result.ConfirmationId != null)
            {
                return RedirectToAction("Confirm", "Reservation", new { result.ConfirmationId });
            }

            if (!_roomHandler.IsAvailable(roomInfo.Type, roomInfo.StartDate, roomInfo.EndDate))
            {
                return RedirectToAction("NotAvailable", "Reservation", new { model.SessionId });
            }

            string userName = null;
            if (User.Identity.IsAuthenticated)
            {
                userName = User.Identity.Name;
            }

            result.ReservationId = _reservationHandler.MakeReservation(userName, roomInfo.Type, roomInfo.StartDate,
                roomInfo.EndDate, result.Guests.ToGuestList(), roomInfo.PriceList.ToList());
            result.IsConfirmed = true;

            //ReservationHandler.SearchResultPool.Remove(model.SessionId);

            return RedirectToAction("Confirm", "Reservation",
                new { ConfirmationId = result.ReservationId.ToString(), NoCancel = true });
        }

        public ActionResult Error(ErrorViewModel error)
        {
            return View(error);
        }

        public ActionResult Confirm(Guid? confirmationId, bool? noCancel)
        {
            ViewBag.NoCancel = noCancel ?? false;

            var reservation = _reservationHandler.GetReservation(confirmationId ?? Guid.NewGuid());

            //invalid confirmation Number
            if (reservation == null)
            {
                return RedirectToAction("Error", "Reservation", new ErrorViewModel { ErrorMsg = "Wrong confirmation number" });
            }

            return View(GetConfirmationViewModel(reservation));
        }

        [HttpPost]
        public ActionResult Confirm(ConfirmationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.IsCanceled)
            {
                return RedirectToAction("Cancel", "Reservation",
                    new { model.ConfirmationId, returnUrl = "~/Home/Index" });
            }

            return RedirectToAction("Index", "Home");
        }
    }
}