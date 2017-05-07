﻿using BusinessLogic.Handlers;
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
        private AspNetUserHandler _userHandler;

        public ReservationController()
        {
            _reservationHandler = new ReservationHandler();
            _roomHandler = new RoomHandler();
            _userHandler = new AspNetUserHandler();
        }

        // GET: Reservation
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Show(Guid? ConfirmationId)
        {
            Guid confirmationId = ConfirmationId ?? Guid.Empty;
            if (!_reservationHandler.HashReservation(confirmationId.ToString()))
            {
                return  RedirectToAction("Error", "Reservation", new ErrorViewModel { ErrorMsg = "Invalid Confirmation Id" });
            }
            Reservation reservation = _reservationHandler.GetReservation(confirmationId);
            
            // TODO check this, it's supposed to be not null
            //if (reservation == null)
            //{
            //    return RedirectToAction("Error", "Reservation", new ErrorViewModel { ErrorMsg = "Invalid Confirmation Id" });
            //}

            ViewBag.canCancel = _reservationHandler.CanBeCanceled(reservation.Id, DateTime.Now);

            // TODO extension functions
            var priceList = reservation.DailyPrices.Select(x => x.BillingPrice).ToList();

            return View(new ConfirmationViewModel
            {
                // TODO check why reservation.Id.ToString() not work
                ConfirmationId = reservation.Id.ToString(),
                StartDate = reservation.StartDate,
                EndDate = reservation.EndDate,
                Guests = reservation.Guests.ToList(),
                Type =  _roomHandler.GetRoomTypeName(reservation.RoomType),
                Ameneties = _roomHandler.GetRoomAmeneties(reservation.RoomType),
                IsCanceled = reservation.IsCancelled,
                PriceList = priceList,
            });
        }

        [HttpPost]
        public ActionResult Show(ConfirmationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // return redirct to profile url TODO
            return RedirectToAction("Cancel", "Reservation", new { ConfirmationViewModel = model, returnUrl = HttpContext.Request.RawUrl });
        }

        public async Task<ActionResult> Pay(Guid ConfirmationId, Profile billInfo)
        {
            // TODO guid or string
            _reservationHandler.PayReservation(ConfirmationId, billInfo);

            return View(new ConfirmationViewModel
            {
                ConfirmationId = ConfirmationId.ToString(),
            });
        }

        [HttpPost]
        public async Task<ActionResult> Pay(ConfirmationViewModel model)
        {
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Cancel(ConfirmationViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            bool success = _reservationHandler.CancelReservation(Guid.Parse(model.ConfirmationId), DateTime.Now);
            ViewBag.ReturnUrl = returnUrl;

            if (!success)
            {
                ViewBag.Error = true;
                return View(model);
            }
            // Redirect to profile url TODO
            //return RedirectToAction("Index", "Home");
            return RedirectToLocal(returnUrl);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        //public async Task<ActionResult> Cancel(Guid ConfirmationId)
        //{
        //    return View(new ReservationViewModel
        //    {
        //        ConfirmationId = ConfirmationId,
        //    });
        //}

        //public ActionResult Search(DateTime? startDate, DateTime? endDate)
        //{
        //    return View(new SearchInputModel
        //    {
        //        StartDate = startDate ?? DateTime.Now,
        //        EndDate = endDate ?? DateTime.Now.AddDays(1)
        //    });
        //}

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

            return RedirectToAction("Show", "Reservation", new { ConfirmationId = model.ConfirmationId });
        }

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
                    Name = _roomHandler.GetRoomTypeName(type),
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

                return RedirectToAction("Result", "Reservation", new { SessionId = sessionId });
            }
            else
            {
                return RedirectToAction("Error", "Reservation", new ErrorViewModel{ ErrorMsg = "No available room, please search again" });
            }

            //ViewBag.NoResult = true;
            return View();
        }

        public ActionResult Result(string SessionId)
        {
            if (!ReservationHandler.SearchResultPool.ContainsKey(SessionId))
            {
                return RedirectToAction("Error", "Reservation", new ErrorViewModel { ErrorMsg = "The reservation is expired or submitted" });
            }

            if (!ModelState.IsValid ||
                    String.IsNullOrEmpty(SessionId) ||
                    ReservationHandler.SearchResultPool[SessionId] == null)
            {
                return RedirectToAction("Search", "Reservation");
            }

            if ((ReservationHandler.SearchResultPool[SessionId] as RoomSearchResultModel).Expiration < DateTime.Now)
            {
                return RedirectToAction("Expired", "Reservation");
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
                return RedirectToAction("Expired", "Reservation");
            }

            (ReservationHandler.SearchResultPool[model.SessionId] as RoomSearchResultModel).SelectedIndex = model.SelectedIndex;


            return RedirectToAction("InputUser", "Reservation", new { SessionId = model.SessionId, Anomyous = false });
        }

        [CustomAuthorize]
        public async Task<ActionResult> InputUser(string SessionId, bool? Anonymous)
        {
            if (!ReservationHandler.SearchResultPool.ContainsKey(SessionId))
            {
                return RedirectToAction("Error", "Reservation", new ErrorViewModel { ErrorMsg = "The reservation is expired or submitted" });
            }

            if (!ModelState.IsValid ||
                    String.IsNullOrEmpty(SessionId) ||
                    ReservationHandler.SearchResultPool[SessionId] == null)
            {
                return RedirectToAction("Search", "Reservation");
            }

            if ((ReservationHandler.SearchResultPool[SessionId] as RoomSearchResultModel).Expiration < DateTime.Now)
            {
                return RedirectToAction("Expired", "Reservation");
            }

            var result = ReservationHandler.SearchResultPool[SessionId] as RoomSearchResultModel;
            var type = result.RoomPriceDetails[result.SelectedIndex].Type;
            result.Guests = _reservationHandler.GetEmptyGuestList(type);

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

        //[HttpPost]
        //public async Task<ActionResult> AnonymousInputUser(InputGuestViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }

        //    if (model.Expiration < DateTime.Now)
        //    {
        //        return RedirectToAction("Search");
        //    }

        //    (ReservationHandler.SearchResultPool[model.SessionId] as RoomSearchResultModel).Guests = model.Guests;

        //    return RedirectToAction("Create", new { SessionId = model.SessionId });
        //}


        [HttpPost]
        public async Task<ActionResult> InputUser(InputGuestViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.Expiration < DateTime.Now)
            {
                return RedirectToAction("Expired", "Reservation");
            }

            (ReservationHandler.SearchResultPool[model.SessionId] as RoomSearchResultModel).Guests = model.Guests;

            return RedirectToAction("Create", "Reservation", new { SessionId = model.SessionId });
        }

        public async Task<ActionResult> Create(string SessionId)
        {
            if (!ReservationHandler.SearchResultPool.ContainsKey(SessionId))
            {
                return RedirectToAction("Error", "Reservation", new ErrorViewModel { ErrorMsg = "The reservation is expired or submitted" });
            }

            if (!ModelState.IsValid ||
                    String.IsNullOrEmpty(SessionId) ||
                    ReservationHandler.SearchResultPool[SessionId] == null)
            {
                return RedirectToAction("Search", "Reservation");
            }

            if ((ReservationHandler.SearchResultPool[SessionId] as RoomSearchResultModel).Expiration < DateTime.Now)
            {
                return RedirectToAction("Expired", "Reservation");
            }

            var result = ReservationHandler.SearchResultPool[SessionId] as RoomSearchResultModel;

            return View(new CreateReservationViewModel
            {
                SessionId = result.SessionId,
                Expiration = result.Expiration,
                IsRoomAvailable = true,
                StartDate = result.RoomPriceDetails[result.SelectedIndex].StartDate,
                EndDate = result.RoomPriceDetails[result.SelectedIndex].EndDate,
                PriceList = result.RoomPriceDetails[result.SelectedIndex].PriceList,
                TypeName = result.RoomPriceDetails[result.SelectedIndex].Name,
            });
        }

        public ActionResult NotAvailable(string SessionId)
        {
            if (!ReservationHandler.SearchResultPool.ContainsKey(SessionId))
            {
                return RedirectToAction("Error", "Reservation", new ErrorViewModel { ErrorMsg = "The reservation is expired or submitted" });
            }

            if (!ModelState.IsValid ||
                String.IsNullOrEmpty(SessionId) ||
                ReservationHandler.SearchResultPool[SessionId] == null ||
                // no selected rooms
                (ReservationHandler.SearchResultPool[SessionId] as RoomSearchResultModel).RoomPriceDetails.Count == 0)
            {
                return RedirectToAction("Search", "Reservation");
            }

            var result = ReservationHandler.SearchResultPool[SessionId] as RoomSearchResultModel;
            ReservationHandler.SearchResultPool.Remove(SessionId);

            return View(result);
        }

        public ActionResult Expired()
        {
            return View();
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
                return RedirectToAction("Expired", "Reservation");
            }

            var result = ReservationHandler.SearchResultPool[model.SessionId] as RoomSearchResultModel;
            var roomInfo = result.RoomPriceDetails[result.SelectedIndex];

            if (result.IsConfirmed && result.ConfirmationId != null)
            {
                return RedirectToAction("Confirm", "Reservation", new { ConfirmationId = result.ConfirmationId });
            }

            if (!_roomHandler.IsAvailable(roomInfo.Type, roomInfo.StartDate, roomInfo.EndDate))
            {
                return RedirectToAction("NotAvailable", "Reservation", new { SessionId = model.SessionId });
            }

            string userName = null;
            if (User.Identity.IsAuthenticated)
            {
                userName = User.Identity.Name;
            }
            // comment for debug
            result.ReservationId = _reservationHandler.MakeReservation(userName, roomInfo.Type, roomInfo.StartDate, 
                roomInfo.EndDate, result.Guests, roomInfo.PriceList.ToList());
            result.IsConfirmed = true;

            //ReservationHandler.SearchResultPool.Remove(model.SessionId);

            return RedirectToAction("Confirm", "Reservation", new { ConfirmationId = result.ReservationId.ToString() });
        }

        public ActionResult Error(ErrorViewModel error)
        {
            return View(error);
        }

        public async Task<ActionResult> Confirm(string ConfirmationId)
        {
            Reservation reservation = _reservationHandler.GetReservation(Guid.Parse(ConfirmationId));

            //invalid confirmation Number
            if (reservation == null)
            {
                return RedirectToAction("Error", "Reservation", new ErrorViewModel{ ErrorMsg = "Wrong confirmation number"});
            }
            // TODO extension functions
            var priceList = reservation.DailyPrices.Select(x => x.BillingPrice).ToList();

            return View(new ConfirmationViewModel
            {
                ConfirmationId = ConfirmationId,
                StartDate = reservation.StartDate,
                EndDate = reservation.EndDate,
                Guests = reservation.Guests.ToList(),
                ReservationId = reservation.Id,
                Type = _roomHandler.GetRoomTypeName(reservation.RoomType),
                Ameneties = _roomHandler.GetRoomAmeneties(reservation.RoomType),
                PriceList = priceList,
            });
        }
    }
}