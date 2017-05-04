﻿using BusinessLogic.Handlers;
using DataAccessLayer.Constants;
using DataAccessLayer.EF;
using HotelBookingWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CheckIn(Guid ConfirmationNum)
        {
            if (_reservationHandler.CheckIn(ConfirmationNum, DateTime.Today))
            {
                return Index();
            }
            else
            {
                //TODO: return failure page then redirect to index after 3 sec
                return View();
            }
        }

        [HttpPost]
        public async Task<ActionResult> CheckOut(Guid ConfirmationNum)
        {
            if (_reservationHandler.CheckOut(ConfirmationNum, DateTime.Today))
            {
                return Index();
            }
            else
            {
                //TODO: return failure page then redirect to index after 3 sec
                return View();
            }
        }

        [HttpGet]
        public async Task<ActionResult> ViewCheckInList()
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
            return View(models);
        }

        [HttpGet]
        public async Task<ActionResult> ViewCheckoutList()
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
            return View(models);
        }

        [HttpGet]
        public async Task<ActionResult> ViewCheckoutListAll()
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
            return View(models);
        }

        [HttpGet]
        public async Task<ActionResult> Occupancey()
        {
            List<OccupanceyModel> models = new List<OccupanceyModel>();
            foreach(ROOM_TYPE type in _roomHandler.GetRoomTypes())
            {
                models.Add(new OccupanceyModel {
                    type = type,
                    inventory = _roomHandler.GetRoomInventory(type),
                    occupancey = _roomHandler.GetRoomInventory(type) - _roomHandler.GetBookedRoomOnDate(type, DateTime.Today)
                });
            }
            return View(models);
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