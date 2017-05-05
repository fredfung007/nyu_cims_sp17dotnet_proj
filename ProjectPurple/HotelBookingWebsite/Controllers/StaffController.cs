﻿using BusinessLogic.Handlers;
using DataAccessLayer.Constants;
using DataAccessLayer.EF;
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
        public ActionResult Index()
        {
            return View(new DashboardModel
            {
                checkInList = getViewCheckInList(),
                checkOutList = getViewCheckoutListAll(),
                inventory = getInventory()
            });
        }

        [HttpGet]
        public async Task<ActionResult> Occupancy(DateTime? date)
        {
            DateTime checkDate = date ?? DateTime.Today;
            return View(new OccupancyModel {
                date = checkDate,
                rate = _roomHandler.GetHotelOccupancy(checkDate).ToString("P", CultureInfo.InvariantCulture)
            });
        }

        [HttpGet]
        public async Task<ActionResult> CheckIn(Guid? ConfirmationNum)
        {
            return View(new CheckInOutModel
            {
                confirmationNum = ConfirmationNum ?? Guid.NewGuid(),
                isSuccess = _reservationHandler.CheckIn(ConfirmationNum ?? Guid.NewGuid(), DateTime.Today)
            });
        }

        [HttpGet]
        public async Task<ActionResult> CheckOut(Guid? ConfirmationNum)
        {
            return View(new CheckInOutModel
            {
                confirmationNum = ConfirmationNum ?? Guid.NewGuid(),
                isSuccess = _reservationHandler.CheckOut(ConfirmationNum ?? Guid.NewGuid(), DateTime.Today)
            });
        }

        private List<CheckInListModel> getViewCheckInList()
        {
            //TODO: add another layer so that we do not use EF.Reservation directly here
            List<Reservation> reservations = new List<Reservation>(_reservationHandler.GetReservationsCheckInToday(DateTime.Today));
            List<CheckInListModel> models = new List<CheckInListModel>();
            foreach(Reservation reservation in reservations)
            {
                models.Add(new CheckInListModel
                {
                    Id = reservation.Id,
                    email = reservation.AspNetUser.Email,
                    firstName = reservation.AspNetUser.Profile.FirstName,
                    lastName = reservation.AspNetUser.Profile.LastName,
                    checkInDate = reservation.StartDate,
                    checkOutDate = reservation.EndDate
                });
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
                models.Add(new CheckOutListModel
                {
                    Id = reservation.Id,
                    email = reservation.AspNetUser.Email,
                    firstName = reservation.AspNetUser.Profile.FirstName,
                    lastName = reservation.AspNetUser.Profile.LastName,
                    checkInDate = reservation.StartDate,
                    checkOutDate = reservation.EndDate,
                    actualCheckInDate = reservation.CheckInDate?? DateTime.Today.Subtract(TimeSpan.FromDays(1))
                });
            }
            return models;
        }

        [HttpGet]
        private ActionResult ViewCheckoutList()
        {
            return View(getViewCheckoutList());
        }

        private List<CheckOutListModel> getViewCheckoutListAll()
        {
            List<Reservation> reservations = new List<Reservation>(_reservationHandler.GetAllCheckedInReservations(DateTime.Today));
            List<CheckOutListModel> models = new List<CheckOutListModel>();
            foreach(Reservation reservation in reservations)
            {
                models.Add(new CheckOutListModel
                {
                    Id = reservation.Id,
                    email = reservation.AspNetUser.Email,
                    firstName = reservation.AspNetUser.Profile.FirstName,
                    lastName = reservation.AspNetUser.Profile.LastName,
                    checkInDate = reservation.StartDate,
                    checkOutDate = reservation.EndDate,
                    actualCheckInDate = reservation.CheckInDate?? DateTime.Today.Subtract(TimeSpan.FromDays(1))
                });
            }
            return models;
        }

        [HttpGet]
        private ActionResult ViewCheckoutListAll()
        {
            return View(getViewCheckoutListAll());
        }

        private List<InventoryModel> getInventory()
        {
            List<InventoryModel> models = new List<InventoryModel>();
            foreach(ROOM_TYPE type in _roomHandler.GetRoomTypes())
            {
                models.Add(new InventoryModel {
                    type = type,
                    inventory = _roomHandler.GetRoomInventory(type),
                    occupancy = _roomHandler.GetRoomInventory(type) - _roomHandler.GetBookedRoomOnDate(type, DateTime.Today)
                });
            }
            return models;
        }

        [HttpGet]
        private ActionResult Inventory()
        {
            return View(getInventory());
        }

        [HttpPost]
        public async Task<ActionResult> ModifiyRoomInventory(ROOM_TYPE type, int value)
        {
            try
            {
                _roomHandler.UpdateRoomInventory(type, value);
            }
            catch (ArgumentOutOfRangeException useless)
            {
                // TODO: return a failure page then redirect
            }
            return Index();
        }

    }
}