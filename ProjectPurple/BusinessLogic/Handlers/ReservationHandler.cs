using System;
using System.Collections.Generic;
using BusinessLogic.Constants;
using System.Data.Entity;
using DataAccessLayer;

namespace BusinessLogic.Handlers
{
    /// <summary>
    /// A handler class for editing reservation for user. 
    /// </summary>
    class ReservationHandler
    {
        DbContext context;
        public ReservationHandler(DbContext context)
        {
            this.context = context;
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
