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
        IReservationRepository _reservationRepository;
        IUserReservationQueryHandler _userReservationQueryHandler;
        public ReservationHandler()
        {
            // username should come from cookies
            string username = "";
            _reservationRepository = new ReservationRepository(new HotelDataModelContainer());
            _userReservationQueryHandler = new UserReservationQueryHandler(username);
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
                User = _userReservationQueryHandler.User,
                startDate = start,
                endDate = end,
                Guests = guests,
                isPaid = false,
                DailyPrices = new List<DailyPrice>()
            };

            List<int> prices = (new RoomHandler()).GetRoomPriceList(type, start, end);
            foreach (int price in prices)
            {
                DailyPrice dailyPrice = new DailyPrice { Id = reservation.Id, Date = start, BillingPrice = price};
                start.AddDays(1);
                reservation.DailyPrices.Add(dailyPrice);
            }

            _reservationRepository.InsertReservation(reservation);
            _reservationRepository.Save();
            return reservation.Id;
        }

        void PayReservation(Guid confirmationNumber, Profile billingInfo)
        {
            Reservation reservation = _reservationRepository.GetReservation(confirmationNumber);
            if (reservation != null)
            {
                reservation.BillingInfo = billingInfo;
                reservation.isPaid = true;
            }
            _reservationRepository.UpdateReservation(reservation);
            _reservationRepository.Save();
        }

        /// <summary>
        /// Cacnel a reservation by its creator's username and confirmation number
        /// </summary>
        /// <param name="confirmationNumber">confirmation number of the reservation</param>
        /// <returns>true if successfully cancelled</returns>
        void CancelReservation(Guid confirmationNumber, DateTime today)
        {
            Reservation reservation = _reservationRepository.GetReservation(confirmationNumber);

            // refuse to cancel if checkin
            if (reservation.checkInDate != null && reservation.checkInDate < today)
            {
                return;
            }

            // refuse to cancel if the date is before the present date 
            if (reservation.startDate != null && reservation.startDate < today)
            {
                return;
            }

            _reservationRepository.DeleteReservation(confirmationNumber);
            _reservationRepository.Save();
        }

        // obsolete
        //Reservation GetReservation(Guid confirmationNumber)
        //{
        //    return null;
        //}

        
        List<Reservation> GetUpComingReservations(User user)
        {
            return new List<Reservation> (_reservationRepository.GetReservationsByUserId(user.Username));
        }

        // obsolete
        //bool FillGuestInfo(Reservation reservation, List<Guest> customers)
        //{
        //    return false;
        //}
    }
}
