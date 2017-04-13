using System;
using System.Collections.Generic;
using BusinessLogic.Constants;
using System.Data.Entity;
using DataAccessLayer;
using BusinessLogic.DAL;

namespace BusinessLogic.Handlers
{
    /// <summary>
    /// A handler class for editing reservation for user. 
    /// </summary>
    class ReservationHandler
    {
        IReservationRepository reservationRepository;
        public ReservationHandler()
        {
            reservationRepository = new ReservationRepository(new HotelDataModelContainer());
        }
        Guid makeReservation(int userId, roomType type, DateTime start, DateTime end)
        {
            return Guid.Empty;
        }
        bool cancelReservation(int userId, Guid confirmationNumber)
        {
            return false;
        }

        Reservation getReservation(Guid confirmationNumber)
        {
            return null;
        }

        List<Reservation> getUpComingReservations(Guest customer)
        {
            return null;
        }

        bool fillGuestInfo(Reservation reservation, List<Guest> customers)
        {
            return false;
        }
    }
}
