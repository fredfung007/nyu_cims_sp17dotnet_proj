﻿using System;
using System.Collections.Generic;
using DataAccessLayer;
using DataAccessLayer.Repositories;

namespace BusinessLogic.Handlers
{
    public class UserReservationQueryHandler : IUserReservationQueryHandler
    {
        private IReservationRepository _reservationRepository;
        private User User { get; }
        public UserReservationQueryHandler(string username)
        {
            _reservationRepository = new ReservationRepository(new HotelDataModelContainer());
            User = new AuthRepository(new HotelDataModelContainer()).GetUser(username);
        }

        User IUserReservationQueryHandler.User => User;

        public string FindLoyaltyProgramInfo()
        {
            throw new NotImplementedException("TODO: Add Implementation for loyalty program.");
        }

        public IEnumerable<Reservation> FindUpcomingReservations(DateTime date)
        {
            IEnumerable<Reservation> reservations = _reservationRepository.GetReservationsByUserId(User.Username);
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

        public Profile GetProfile()
        {
            return User.Profile;
        }
    }
}
