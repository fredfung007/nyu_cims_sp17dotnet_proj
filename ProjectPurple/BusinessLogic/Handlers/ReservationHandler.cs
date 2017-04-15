using System;
using System.Collections.Generic;
using BusinessLogic.Constants;
using System.Data.Entity;
using DataAccessLayer;
using DataAccessLayer.Repositories;

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
        Guid MakeReservation(int userId, roomType type, DateTime start, DateTime end)
        {
            return Guid.Empty;
        }
        bool CancelReservation(int userId, Guid confirmationNumber)
        {
            return false;
        }

        Reservation GetReservation(Guid confirmationNumber)
        {
            return null;
        }

        List<Reservation> GetUpComingReservations(Guest customer)
        {
            return null;
        }

        bool FillGuestInfo(Reservation reservation, List<Guest> customers)
        {
            return false;
        }
    }
}
