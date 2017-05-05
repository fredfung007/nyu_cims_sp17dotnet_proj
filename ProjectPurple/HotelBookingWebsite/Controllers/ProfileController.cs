using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BusinessLogic.Handlers;
using DataAccessLayer.EF;
using HotelBookingWebsite.Filters;
using HotelBookingWebsite.Models;
using Microsoft.AspNet.Identity;

namespace HotelBookingWebsite.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Profile
        [CustomAuthorize]
        public ActionResult Index()
        {
            ProfileViewModel viewModel = new ProfileViewModel();
            ProfileHandler profileHandler = new ProfileHandler();
            viewModel.SetByProfile(profileHandler.GetProfile(new AspNetUserHandler().GetAspNetUser(User.Identity.GetUserName()).Profile.Id));
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(Guid UserId)
        {
            return View();
        }

        [CustomAuthorize]
        public async Task<ActionResult> UpcommingReservations()
        {
            var upComingReservations =
                await new ReservationHandler().GetUpComingReservations(User.Identity.GetUserId());

            var reservationViewModels = upComingReservations.Select(reservation => new ConfirmationViewModel
                {
                    ConfirmationId = reservation.Id.ToString(),
                    StartDate = reservation.StartDate,
                    EndDate = reservation.EndDate,
                    Guests = reservation.Guests

            })
                .ToList();
            return View(reservationViewModels);
        }
    }
}