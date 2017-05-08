using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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
    public class StaffController : Controller
    {
        private readonly ReservationHandler _reservationHandler;
        private readonly RoomHandler _roomHandler;

        public StaffController()
        {
            _reservationHandler = new ReservationHandler();
            _roomHandler = new RoomHandler();
        }

        // GET: Staff
        [StaffAuthorize]
        public ActionResult Index(DateTime? date)
        {
            ViewBag.isStaff = User.IsInRole("staff");
            return View(new DashboardModel
            {
                CheckInList = GetViewCheckInList(),
                CheckOutList = GetViewCheckoutListAll(),
                Inventory = GetInventory(date ?? DateTimeHandler.GetCurrentTime()),
                Occupancy = GetOccupancy(date ?? DateTimeHandler.GetCurrentTime()),
                CheckDate = date ?? DateTimeHandler.GetCurrentTime(),
                CurrentTime = DateTimeHandler.GetCurrentTime()
            });
        }

        private OccupancyModel GetOccupancy(DateTime checkDate)
        {
            return new OccupancyModel
            {
                Date = checkDate.Date,
                Rate = _roomHandler.GetHotelOccupancyRate(checkDate.Date).ToString("P", CultureInfo.InvariantCulture)
            };
        }

        [HttpGet]
        [StaffAuthorize]
        public async Task<ActionResult> Occupancy(DateTime? date)
        {
            DateTime checkDate = date ?? DateTimeHandler.GetCurrentTime();
            return PartialView(GetOccupancy(checkDate));
        }

        [HttpGet]
        [StaffAuthorize]
        public async Task<ActionResult> Operations(Guid? confirmationNum)
        {
            Guid confirmationNumNotNull = confirmationNum ?? Guid.Empty;
            if (!_reservationHandler.HashReservation(confirmationNumNotNull.ToString()))
            {
                return RedirectToAction("Error", "Reservation",
                    new ErrorViewModel {ErrorMsg = "Invalid Confirmation Id"});
            }

            Reservation reservation = _reservationHandler.GetReservation(confirmationNumNotNull);
            ViewBag.IsCheckedIn = reservation.CheckInDate != null;
            ViewBag.canCancel =
                _reservationHandler.CanBeCanceled(confirmationNumNotNull, DateTimeHandler.GetCurrentTime());

            // TODO extension functions
            var priceList = reservation.DailyPrices.Select(x => x.BillingPrice).ToList();

            return View(new OperationModel
            {
                Confirmation = new ConfirmationViewModel
                {
                    ConfirmationId = reservation.Id.ToString(),
                    StartDate = reservation.StartDate,
                    EndDate = reservation.EndDate,
                    Guests = reservation.Guests.ToList().ToGuestModelList(),
                    Type = _roomHandler.GetRoomTypeName(reservation.RoomType),
                    Ameneties = _roomHandler.GetRoomAmeneties(reservation.RoomType),
                    IsCanceled = reservation.IsCancelled,
                    PriceList = priceList
                }
            });
        }

        [HttpGet]
        [StaffAuthorize]
        public async Task<ActionResult> CheckIn(Guid? confirmationNum)
        {
            return View(new CheckInOutModel
            {
                ConfirmationNum = confirmationNum ?? Guid.NewGuid(),
                IsSuccess = _reservationHandler.CheckIn(confirmationNum ?? Guid.NewGuid(),
                    DateTimeHandler.GetCurrentTime())
            });
        }

        [HttpGet]
        [StaffAuthorize]
        public async Task<ActionResult> CheckOut(Guid? confirmationNum)
        {
            return View(new CheckInOutModel
            {
                ConfirmationNum = confirmationNum ?? Guid.NewGuid(),
                IsSuccess = _reservationHandler.CheckOut(confirmationNum ?? Guid.NewGuid(),
                    DateTimeHandler.GetCurrentTime())
            });
        }

        private List<CheckInListModel> GetViewCheckInList()
        {
            var reservations = new List<Reservation>(
                _reservationHandler.GetReservationsCheckInToday(DateTimeHandler.GetCurrentStartTime()));
            var models = new List<CheckInListModel>();
            foreach (Reservation reservation in reservations)
            {
                Guest firstGuest = reservation.Guests.OrderBy(guest => guest.Order).FirstOrDefault();
                var firstName = "";
                var lastName = "";
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
            return View(GetViewCheckInList());
        }

        private List<CheckOutListModel> GetViewCheckoutList()
        {
            var reservations = new List<Reservation>(
                _reservationHandler.GetReservationsCheckOutToday(DateTimeHandler.GetCurrentEndTime()));
            var models = new List<CheckOutListModel>();
            foreach (Reservation reservation in reservations)
            {
                Guest firstGuest = reservation.Guests.OrderBy(guest => guest.Order).FirstOrDefault();
                var firstName = "";
                var lastName = "";
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
                        ActualCheckInDate = reservation.CheckInDate ??
                                            DateTimeHandler.GetCurrentTime().Subtract(TimeSpan.FromDays(1))
                    });
                }
            }
            return models;
        }

        [HttpGet]
        private ActionResult ViewCheckoutList()
        {
            return View(GetViewCheckoutList());
        }

        [HttpGet]
        public ActionResult CheckOutAllExpired()
        {
            var reservations = new List<Reservation>(
                _reservationHandler.GetExpiredReservations(DateTimeHandler.GetCurrentEndTime()));

            // check out today's reservation if passed 2:00 p.m.
            var includeToday = DateTimeHandler.GetCurrentTime() > DateTimeHandler.GetCurrentEndTime();
            List<CheckOutListModel> models = new List<CheckOutListModel>();
            foreach (Reservation reservation in reservations)
            {
                if (reservation.EndDate < DateTimeHandler.GetCurrentEndTime() ||
                    reservation.EndDate == DateTimeHandler.GetCurrentEndTime() && includeToday)
                {
                    _reservationHandler.CheckOut(reservation.Id, DateTimeHandler.GetCurrentTime());
                    Guest firstGuest = reservation.Guests.OrderBy(guest => guest.Order).FirstOrDefault();
                    var firstName = "";
                    var lastName = "";
                    if (firstGuest != null)
                    {
                        firstName = firstGuest.FirstName;
                        lastName = firstGuest.LastName;
                    }
                    models.Add(new CheckOutListModel
                    {
                        Id = reservation.Id,
                        FirstName = firstName,
                        LastName = lastName,
                        CheckInDate = reservation.StartDate,
                        CheckOutDate = reservation.EndDate,
                        ActualCheckInDate = reservation.CheckInDate ??
                                            DateTimeHandler.GetCurrentTime().Subtract(TimeSpan.FromDays(1))
                    });
                }
            }

            return View(models);
        }

        private List<CheckOutListModel> GetViewCheckoutListAll()
        {
            var reservations = new List<Reservation>(
                _reservationHandler.GetAllReservationsCanBeCheckedOut(DateTimeHandler.GetCurrentEndTime().AddDays(1)));
            var models = new List<CheckOutListModel>();
            foreach (Reservation reservation in reservations)
            {
                Guest firstGuest = reservation.Guests.OrderBy(guest => guest.Order).FirstOrDefault();
                var firstName = "";
                var lastName = "";
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
                        ActualCheckInDate = reservation.CheckInDate ??
                                            DateTimeHandler.GetCurrentTime().Subtract(TimeSpan.FromDays(1))
                    });
                }
            }
            return models;
        }

        [HttpGet]
        private ActionResult ViewCheckoutListAll()
        {
            return View(GetViewCheckoutListAll());
        }

        private List<InventoryModel> GetInventory(DateTime date)
        {
            var models = new List<InventoryModel>();
            foreach (ROOM_TYPE type in _roomHandler.GetRoomTypes())
            {
                models.Add(new InventoryModel
                {
                    Type = type,
                    Inventory = _roomHandler.GetRoomInventory(type),
                    Rate = _roomHandler.GetRoomOccupancyRate(type, date.Date).ToString("P", CultureInfo.InvariantCulture)
                });
            }

            return models;
        }

        [HttpGet]
        public PartialViewResult Inventory(DateTime? date)
        {
            return PartialView(GetInventory(date ?? DateTimeHandler.GetCurrentTime()));
        }

        [StaffAuthorize]
        public async Task<ActionResult> ModifyRoomInventory(ROOM_TYPE? type, int? inventory)
        {
            if (type != null && inventory != null)
            {
                try
                {
                    _roomHandler.UpdateRoomInventory((ROOM_TYPE) type, (int) inventory);
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