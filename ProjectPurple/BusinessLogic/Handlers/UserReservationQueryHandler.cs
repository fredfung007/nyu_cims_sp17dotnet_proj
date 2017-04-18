using System;
using System.Collections.Generic;
using DataAccessLayer;
using DataAccessLayer.Repositories;

namespace BusinessLogic.Handlers
{
    public class UserReservationQueryHandler : IUserReservationQueryHandler
    {
        private IReservationRepository reservationRepository;
        private User user { get; }
        public UserReservationQueryHandler(string username)
        {
            reservationRepository = new ReservationRepository(new HotelDataModelContainer());
            user = new AuthRepository(new HotelDataModelContainer()).getUser(username);
        }

        User IUserReservationQueryHandler.user => user;

        public string findLoyaltyProgramInfo()
        {
            throw new NotImplementedException("TODO: Add Implementation for loyalty program.");
        }

        public IEnumerable<Reservation> findUpcomingReservations(DateTime date)
        {
            IEnumerable<Reservation> reservations = reservationRepository.getReservationsByUserId(user.Username);
            List<Reservation> upcomingReservations = new List<Reservation>();
            foreach (Reservation reservation in reservations)
            {
                if (reservation.endDate.Date.CompareTo(date) > 0)
                {
                    upcomingReservations.Add(reservation);
                }
            }
            return upcomingReservations;
        }

        public Profile getProfile()
        {
            return user.Profile;
        }
    }
}
