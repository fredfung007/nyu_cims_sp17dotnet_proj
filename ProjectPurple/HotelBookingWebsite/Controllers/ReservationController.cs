using BusinessLogic.Handlers;
using DataAccessLayer.Constants;
using DataAccessLayer.EF;
using HotelBookingWebsite.Filters;
using HotelBookingWebsite.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
                return RedirectToAction("Error", "Reservation", new ErrorViewModel { ErrorMsg = "Invalid Confirmation Id" });
            }
            Reservation reservation = _reservationHandler.GetReservation(confirmationId);

            // TODO check this, it's supposed to be not null
            //if (reservation == null)
            //{
            //    return RedirectToAction("Error", "Reservation", new ErrorViewModel { ErrorMsg = "Invalid Confirmation Id" });
            //}

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
        public async Task<ActionResult> Cancel(ConfirmationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            bool success = _reservationHandler.CancelReservation(Guid.Parse(model.ConfirmationId), DateTimeHandler.GetCurrentTime());

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
                Guests = reservation.Guests.ToList(),
                ReservationId = reservation.Id,
                Type = _roomHandler.GetRoomTypeName(reservation.RoomType),
                Ameneties = _roomHandler.GetRoomAmeneties(reservation.RoomType),
                PriceList = priceList,
            };
        }

        public ActionResult Cancel(Guid? ConfirmationId, string returnUrl)
        {
            if (ConfirmationId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var reservation = _reservationHandler.GetReservation((Guid)ConfirmationId);
            if (reservation == null)
            {
                return RedirectToAction("Error", "Reservation", new ErrorViewModel { ErrorMsg = "Invalid Confirmation Id" });
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

            if (model.StartDate.Date < DateTime.Now.Date)
            {
                return RedirectToAction("Error", "Reservation", new ErrorViewModel { ErrorMsg = "start date must be before end date" });
            }

            if (model.StartDate.Date >= model.EndDate.Date)
            {
                return RedirectToAction("Error", "Reservation", new ErrorViewModel { ErrorMsg = "Start date must not be before now" });
            }

            ViewBag.NoResult = false;
            DateTime startDate = model.StartDate.Date.AddHours(12);
            DateTime endDate = model.EndDate.Date.AddHours(14);
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
                    Expiration = DateTimeHandler.GetCurrentTime().AddMinutes(10),
                    RoomPriceDetails = availableRooms
                };

                return RedirectToAction("Result", "Reservation", new { SessionId = sessionId });
            }

            return RedirectToAction("Error", "Reservation", new ErrorViewModel { ErrorMsg = "No available room, please search again" });
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

            if ((ReservationHandler.SearchResultPool[SessionId] as RoomSearchResultModel).Expiration < DateTimeHandler.GetCurrentTime())
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

            if (model.Expiration < DateTimeHandler.GetCurrentTime())
            {
                return RedirectToAction("Expired", "Reservation");
            }

            (ReservationHandler.SearchResultPool[model.SessionId] as RoomSearchResultModel).SelectedIndex = model.SelectedIndex;


            return RedirectToAction("InputUser", "Reservation", new { SessionId = model.SessionId, Anomyous = false });
        }


        public ActionResult AddGuest(int order)
        {
            return PartialView("_EmptyGuest", new Guest { Id = Guid.NewGuid(), Order = order });
        }

        private List<Guest> GetGuests(List<GuestViewModel> guestModels)
        {
            List<Guest> guests = new List<Guest>();
            int order = 0;
            for (int i = 0; i < guestModels.Count; i++)
            {
                if (string.IsNullOrEmpty(guestModels[i].LastName) && string.IsNullOrEmpty(guestModels[i].FirstName))
                {
                    continue;
                }
                guests.Add(new Guest
                {
                    Id = guestModels[i].Id,
                    FirstName = guestModels[i].FirstName,
                    LastName = guestModels[i].LastName,
                    Order = order++,
                });
            }
            return guests;
        }

        private List<GuestViewModel> GetEmptyGuestModelList(ROOM_TYPE type)
        {
            var guests = new List<GuestViewModel>();
            int guestMaxCount = (type == ROOM_TYPE.DoubleBedRoom || type == ROOM_TYPE.Suite) ? 4 : 2;

            for (int i = 0; i < guestMaxCount; i++)
            {
                guests.Add(new GuestViewModel() { Id = Guid.NewGuid(), Order = i });
            }

            return guests;
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

            if ((ReservationHandler.SearchResultPool[SessionId] as RoomSearchResultModel).Expiration < DateTimeHandler.GetCurrentTime())
            {
                return RedirectToAction("Expired", "Reservation");
            }

            var result = ReservationHandler.SearchResultPool[SessionId] as RoomSearchResultModel;
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
        public async Task<ActionResult> InputUser(InputGuestViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.Expiration < DateTimeHandler.GetCurrentTime())
            {
                return RedirectToAction("Expired", "Reservation");
            }

            model.Guests.RemoveAll(guest => string.IsNullOrEmpty(guest.FirstName) && string.IsNullOrEmpty(guest.LastName));

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

            if ((ReservationHandler.SearchResultPool[SessionId] as RoomSearchResultModel).Expiration < DateTimeHandler.GetCurrentTime())
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

            if (model.Expiration < DateTimeHandler.GetCurrentTime())
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

            // TODO check guests
            result.ReservationId = _reservationHandler.MakeReservation(userName, roomInfo.Type, roomInfo.StartDate,
                roomInfo.EndDate, GetGuests(result.Guests), roomInfo.PriceList.ToList());
            result.IsConfirmed = true;

            //ReservationHandler.SearchResultPool.Remove(model.SessionId);

            return RedirectToAction("Confirm", "Reservation", new { ConfirmationId = result.ReservationId.ToString(), NoCancel = true });
        }

        public ActionResult Error(ErrorViewModel error)
        {
            return View(error);
        }

        public async Task<ActionResult> Confirm(Guid? ConfirmationId, bool? NoCancel)
        {
            ViewBag.NoCancel = NoCancel ?? false;

            Reservation reservation = _reservationHandler.GetReservation(ConfirmationId ?? Guid.NewGuid());

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
                // TODO check links
                return RedirectToAction("Cancel", "Reservation", new { ConfirmationId = model.ConfirmationId, returnUrl = "~/Home/Index" });
            }

            return RedirectToAction("Index", "Home");
        }
    }
}