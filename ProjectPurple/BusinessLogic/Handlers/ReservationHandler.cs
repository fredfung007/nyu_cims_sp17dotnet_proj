using System;
using System.Collections.Generic;
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
            Reservation r = new Reservation();
            return Guid.Empty;
        }
        /// <summary>
        /// Cacnel a reservation by its creator's username and confirmation number
        /// </summary>
        /// <param name="username">username of the User who created the reservation</param>
        /// <param name="confirmationNumber">confirmation number of the reservation</param>
        /// <returns>true if successfully cancelled</returns>
        bool CancelReservation(string username, Guid confirmationNumber)
        {
            bool isValid = false;
            Reservation r = reservationRepository.getReservation(confirmationNumber);
            if (r.User.Username == username)
            {
                reservationRepository.DeleteReservation(confirmationNumber);
                isValid = true;
            }
            else { }
            return isValid;
        }

        // obsolete
        //Reservation GetReservation(Guid confirmationNumber)
        //{
        //    return null;
        //}

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
