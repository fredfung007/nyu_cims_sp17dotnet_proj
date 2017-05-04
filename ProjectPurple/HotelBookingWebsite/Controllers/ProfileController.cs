using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BusinessLogic.Handlers;
using DataAccessLayer.EF;
using HotelBookingWebsite.Models;
using Microsoft.AspNet.Identity;

namespace HotelBookingWebsite.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Profile
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(Guid UserId)
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> UpcommingReservations()
        {
            var upComingReservations =
                await new ReservationHandler().GetUpComingReservations(User.Identity.GetUserId());

            var reservationViewModels = upComingReservations.Select(reservation => new ConfirmationViewModel
                {
                    ConfirmationId = reservation.Id.ToString()
                })
                .ToList();
            return View(reservationViewModels);
        }

        [HttpGet]
        public async Task<ActionResult> LoyaltyProgram()
        {
            return View();
        }
    }
}