using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.EF;
using DataAccessLayer.Repositories;

namespace BusinessLogic.Handlers
{
    public class AspNetUserHandler
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IAuthRepository _userRepository;

        public AspNetUserHandler()
        {
            _reservationRepository = new ReservationRepository(new HotelModelContext());
            _userRepository = new AuthRepository(new HotelModelContext());
        }

        public AspNetUser GetAspNetUser(string username)
        {
            return _userRepository.GetUser(username);
        }

        /// <summary>
        /// Get the loyalty program progress by username.
        /// </summary>
        /// <param name="username">Username</param>
        /// <returns>Corresponding loyalty program gress</returns>
        public int FindLoyaltyProgramInfo(string username)
        {
            var user = _userRepository.GetUser(username);
            return user.LoyaltyProgress;
        }

        /// <summary>
        /// Find reservations by username which end after the given date.
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="date">Date</param>
        /// <returns>Reservations that is both made by the User and ends after the given date.</returns>
        public IEnumerable<Reservation> FindUpcomingReservations(string username, DateTime date)
        {
            var reservations = _reservationRepository.GetReservationsByUserId(username);
            return reservations.Where(reservation => reservation.EndDate.Date.CompareTo(date) > 0).ToList();
        }

        /// <summary>
        /// Find the Profile of the given user.
        /// </summary>
        /// <param name="username"></param>
        /// <returns>The Profile of the given user.</returns>
        public Profile GetProfile(string username)
        {
            var user = _userRepository.GetUser(username);
            return user.Profile;
        }
    }
}
