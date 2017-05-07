using BusinessLogic.Handlers;
using DataAccessLayer.Constants;
using DataAccessLayer.EF;
using HotelBookingWebsite.Filters;
using HotelBookingWebsite.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HotelBookingWebsite.Controllers
{
    public class StaffController : Controller
    {
        private ReservationHandler _reservationHandler;
        private RoomHandler _roomHandler;

        public StaffController()
        {
            _reservationHandler = new ReservationHandler();
            _roomHandler = new RoomHandler();
        }

        // GET: Staff
        [StaffAuthorize]
        public ActionResult Index(DateTime? date)
        {
            return View(new DashboardModel
            {
                CheckInList = getViewCheckInList(),
                CheckOutList = getViewCheckoutListAll(),
                Inventory = getInventory(date?? DateTime.Today),
                Occupancy = getOccupancy(date?? DateTime.Today)
            });
        }

        private OccupancyModel getOccupancy(DateTime checkDate)
        {
            return new OccupancyModel
            {
                Date = checkDate,
                Rate = _roomHandler.GetHotelOccupancyRate(checkDate).ToString("P", CultureInfo.InvariantCulture)
            };
        }

        [HttpGet]
        [StaffAuthorize]
        public async Task<ActionResult> Occupancy(DateTime? date)
        {
            DateTime checkDate = date ?? DateTime.Today;
            return PartialView(getOccupancy(checkDate));
        }

        [HttpGet]
        [StaffAuthorize]
        public async Task<ActionResult> Operations(Guid? ConfirmationNum)
        {
            Guid ConfirmationNumNotNull = ConfirmationNum ?? Guid.Empty;
            if (!_reservationHandler.HashReservation(ConfirmationNumNotNull.ToString()))
            {
                return  RedirectToAction("Error", "Reservation", new ErrorViewModel { ErrorMsg = "Invalid Confirmation Id" });
            }
            Reservation reservation = _reservationHandler.GetReservation(ConfirmationNumNotNull);
            ViewBag.IsCheckedIn = reservation.CheckInDate != null;
            ViewBag.canCancel = _reservationHandler.CanBeCanceled(ConfirmationNumNotNull, DateTime.Now);

            // TODO extension functions
            var priceList = reservation.DailyPrices.Select(x => x.BillingPrice).ToList();
            
            return View(new OperationModel { Confirmation = new ConfirmationViewModel {
                ConfirmationId = reservation.Id.ToString(),
                StartDate = reservation.StartDate,
                EndDate = reservation.EndDate,
                Guests = reservation.Guests.ToList(),
                Type =  _roomHandler.GetRoomTypeName(reservation.RoomType),
                Ameneties = _roomHandler.GetRoomAmeneties(reservation.RoomType),
                IsCanceled = reservation.IsCancelled,
                PriceList = priceList
            }});
        }

        [HttpGet]
        [StaffAuthorize]
        public async Task<ActionResult> CheckIn(Guid? ConfirmationNum)
        {
            return View(new CheckInOutModel
            {
                ConfirmationNum = ConfirmationNum ?? Guid.NewGuid(),
                IsSuccess = _reservationHandler.CheckIn(ConfirmationNum ?? Guid.NewGuid(), DateTime.Now)
            });
        }

        [HttpGet]
        [StaffAuthorize]
        public async Task<ActionResult> CheckOut(Guid? ConfirmationNum)
        {
            return View(new CheckInOutModel
            {
                ConfirmationNum = ConfirmationNum ?? Guid.NewGuid(),
                IsSuccess = _reservationHandler.CheckOut(ConfirmationNum ?? Guid.NewGuid(), DateTime.Now)
            });
        }

        private List<CheckInListModel> getViewCheckInList()
        {
            List<Reservation> reservations = new List<Reservation>(_reservationHandler.GetReservationsCheckInToday(DateTime.Today));
            List<CheckInListModel> models = new List<CheckInListModel>();
            foreach (Reservation reservation in reservations)
            {
                Guest firstGuest = reservation.Guests.OrderBy(guest => guest.Order).FirstOrDefault();
                string firstName = "";
                string lastName = "";
                if (firstGuest != null)
                {
                    firstName = firstGuest.FirstName;
                    lastName = firstGuest.LastName;
                }
                if (reservation.CheckInDate == null && !reservation.IsCancelled)
                {
                    models.Add(new CheckInListModel
                    {
                        Id = reservation.Id,
                        FirstName = firstName,
                        LastName = lastName,
                        CheckInDate = reservation.StartDate,
                        CheckOutDate = reservation.EndDate
                    });
                }
            }
            return models;
        }

        private ActionResult ViewCheckInList()
        {
            return View(getViewCheckInList());
        }

        private List<CheckOutListModel> getViewCheckoutList()
        {
            List<Reservation> reservations = new List<Reservation>(_reservationHandler.GetReservationsCheckOutToday(DateTime.Today));
            List<CheckOutListModel> models = new List<CheckOutListModel>();
            foreach(Reservation reservation in reservations)
            {
                Guest firstGuest = reservation.Guests.OrderBy(guest => guest.Order).FirstOrDefault();
                string firstName = "";
                string lastName = "";
                if (firstGuest != null)
                {
                    firstName = firstGuest.FirstName;
                    lastName = firstGuest.LastName;
                }
                if (reservation.CheckInDate != null && reservation.CheckOutDate == null && !reservation.IsCancelled)
                {
                    models.Add(new CheckOutListModel
                    {
                        Id = reservation.Id,
                        FirstName = firstName,
                        LastName = lastName,
                        CheckInDate = reservation.StartDate,
                        CheckOutDate = reservation.EndDate,
                        ActualCheckInDate = reservation.CheckInDate ?? DateTime.Today.Subtract(TimeSpan.FromDays(1))
                    });
                }
            }
            return models;
        }

        [HttpGet]
        private ActionResult ViewCheckoutList()
        {
            return View(getViewCheckoutList());
        }

        [HttpGet]
        public ActionResult CheckOutAllExpired()
        {
            List<Reservation> reservations = new List<Reservation>(_reservationHandler.GetAllCheckedInReservations(DateTime.Today));

            // check out today's reservation if passed 2:00 p.m.
            bool includeToday = DateTime.Now > DateTime.Today.AddHours(14);
            foreach(Reservation reservation in reservations)
            {
                if (reservation.EndDate < DateTime.Today || (reservation.EndDate == DateTime.Today && includeToday))
                {
                    _reservationHandler.CheckOut(reservation.Id, DateTime.Today);
                }
            }

            return View();
        }

        private List<CheckOutListModel> getViewCheckoutListAll()
        {
            List<Reservation> reservations = new List<Reservation>(
                _reservationHandler.GetAllCheckedInReservations(DateTime.Today.AddDays(1)));
            List<CheckOutListModel> models = new List<CheckOutListModel>();
            foreach(Reservation reservation in reservations)
            {
                Guest firstGuest = reservation.Guests.OrderBy(guest => guest.Order).FirstOrDefault();
                string firstName = "";
                string lastName = "";
                if (firstGuest != null)
                {
                    firstName = firstGuest.FirstName;
                    lastName = firstGuest.LastName;
                }
                if (reservation.CheckInDate != null && reservation.CheckOutDate == null && !reservation.IsCancelled)
                {
                    models.Add(new CheckOutListModel
                    {
                        Id = reservation.Id,
                        FirstName = firstName,
                        LastName = lastName,
                        CheckInDate = reservation.StartDate,
                        CheckOutDate = reservation.EndDate,
                        ActualCheckInDate = reservation.CheckInDate ?? DateTime.Today.Subtract(TimeSpan.FromDays(1))
                    });
                }
            }
            return models;
        }

        [HttpGet]
        private ActionResult ViewCheckoutListAll()
        {
            return View(getViewCheckoutListAll());
        }

        private List<InventoryModel> getInventory(DateTime date)
        {
            List<InventoryModel> models = new List<InventoryModel>();
            foreach(ROOM_TYPE type in _roomHandler.GetRoomTypes())
            {
                models.Add(new InventoryModel {
                    Type = type,
                    Inventory = _roomHandler.GetRoomInventory(type),
                    Rate = _roomHandler.GetRoomOccupancyRate(type, date).ToString("P", CultureInfo.InvariantCulture)
                });
            }
            return models;
        }

        [HttpGet]
        public PartialViewResult Inventory(DateTime? date)
        {
            return PartialView(getInventory(date?? DateTime.Today));
        }

        [StaffAuthorize]
        public async Task<ActionResult> ModifyRoomInventory(ROOM_TYPE? Type, int? Inventory)
        {
            if (Type != null && Inventory != null)
            {
                try
                {
                    _roomHandler.UpdateRoomInventory((ROOM_TYPE)Type, (int)Inventory);
                    return RedirectToAction("Index");
                }
                catch (ArgumentOutOfRangeException useless)
                {
                    ViewBag.Status = "Unsuccessful! The new inventory value is invalid.";
                }
            }
            return View();
        }

    }
}