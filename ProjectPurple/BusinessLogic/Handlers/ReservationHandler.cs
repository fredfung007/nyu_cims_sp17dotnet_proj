using System;
using System.Collections.Generic;
using System.Data.Entity;
using DataAccessLayer;
using DataAccessLayer.Repositories;
using DataAccessLayer.Constants;

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
        Guid MakeReservation(int userId, ROOM_TYPE type, DateTime start, DateTime end)
        {
            return Guid.Empty;
        }
        bool CancelReservation(int userId, Guid confirmationNumber)
        {
            return false;
        }

        Reservation GetReservation(Guid confirmationNumber)
        {


            return reservationRepository.getReservationsByConfirmNum(confirmationNumber);
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
