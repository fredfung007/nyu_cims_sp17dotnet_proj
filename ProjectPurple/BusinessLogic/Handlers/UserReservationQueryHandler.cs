using System;
using System.Collections.Generic;
using DataAccessLayer;
using DataAccessLayer.EF;
using DataAccessLayer.Repositories;

namespace BusinessLogic.Handlers
{
    public class UserReservationQueryHandler : IUserReservationQueryHandler
    {
        private readonly IReservationRepository _reservationRepository;
        private AspNetUser User { get; }
        public UserReservationQueryHandler(string username)
        {
            _reservationRepository = new ReservationRepository(new CodeFirstHotelModel());
            User = new AuthRepository(new CodeFirstHotelModel()).GetUser(username);
        }

        AspNetUser IUserReservationQueryHandler.User => User;

        public string FindLoyaltyProgramInfo()
        {
            throw new NotImplementedException("TODO: Add Implementation for loyalty program.");
        }

        public IEnumerable<Reservation> FindUpcomingReservations(DateTime date)
        {
            IEnumerable<Reservation> reservations = _reservationRepository.GetReservationsByUserId(User.UserName);
            List<Reservation> upcomingReservations = new List<Reservation>();
            foreach (Reservation reservation in reservations)
            {
                if (reservation.EndDate.Date.CompareTo(date) > 0)
                {
                    upcomingReservations.Add(reservation);
                }
            }
            return upcomingReservations;
        }

        public ICollection<Profile> GetProfile()
        {
            return User.Profiles;
        }
    }
}
