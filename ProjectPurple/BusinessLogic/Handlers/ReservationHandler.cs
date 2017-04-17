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

        /// <summary>
        /// Create a new Reservation.
        /// !!!!!!!! Not set User yet !!!!!!!
        /// </summary>
        /// <param name="type">ROOM_TYPE</param>
        /// <param name="start">check-in date</param>
        /// <param name="end">check-out date</param>
        /// <param name="guests">list of guests attending</param>
        /// <returns></returns>
        Guid MakeReservation(ROOM_TYPE type, DateTime start, DateTime end, List<Guest> guests)
        {
            Reservation reservation = new Reservation
            {
                Id = Guid.NewGuid(),
                startDate = start,
                endDate = end,
                Guests = guests,
                isPaid = false,
                DailyPrices = new List<DailyPrice>()
            };

            List<int> prices = (new RoomHandler()).GetRoomPriceList(type, start, end);
            foreach (int price in prices)
            {
                DailyPrice dailyPrice = new DailyPrice { Id = r.Id, Date = start, BillingPrice = price};
                start.AddDays(1);
                reservation.DailyPrices.Add(dailyPrice);
            }

            reservationRepository.InsertReservation(reservation);
            return reservation.Id;
        }

        void PayReservation(Guid confirmationNumber, Profile billingInfo)
        {
            Reservation reservation = reservationRepository.getReservation(confirmationNumber);
            if (reservation != null)
            {
                reservation.BillingInfo = billingInfo;
                reservation.isPaid = true;
            }
        }

        /// <summary>
        /// Cacnel a reservation by its creator's username and confirmation number
        /// </summary>
        /// <param name="confirmationNumber">confirmation number of the reservation</param>
        /// <returns>true if successfully cancelled</returns>
        void CancelReservation(Guid confirmationNumber)
        {
            reservationRepository.DeleteReservation(confirmationNumber);
        }

        // obsolete
        //Reservation GetReservation(Guid confirmationNumber)
        //{
        //    return null;
        //}

        List<Reservation> GetUpComingReservations(User u)
        {
            return reservationRepository.getReservationsByUserId(u.Username);
        }

        // obsolete
        //bool FillGuestInfo(Reservation reservation, List<Guest> customers)
        //{
        //    return false;
        //}
    }
}
