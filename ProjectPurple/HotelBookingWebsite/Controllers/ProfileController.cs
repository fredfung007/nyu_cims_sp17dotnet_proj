using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using BusinessLogic.Handlers;
using HotelBookingWebsite.Filters;
using HotelBookingWebsite.Helper;
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
            var viewModel = new ProfileViewModel();
            var profileHandler = new ProfileHandler();
            viewModel.SetByProfile(profileHandler.GetProfile(new AspNetUserHandler()
                .GetAspNetUser(User.Identity.GetUserName())
                .Profile.Id));
            return View(viewModel);
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
                    Guests = reservation.Guests.ToList().ToGuestModelList()
                })
                .ToList();
            return View(reservationViewModels);
        }
    }
}