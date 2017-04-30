using System;
using System.Collections.Generic;
using DataAccessLayer;
using DataAccessLayer.Repositories;
using DataAccessLayer.Constants;
using DataAccessLayer.EF;

namespace BusinessLogic.Handlers
{
    /// <summary>
    /// A handler class for editing reservation for user. 
    /// </summary>
    public class ReservationHandler
    {
        private readonly IReservationRepository _reservationRepository;
        //private readonly IUserReservationQueryHandler _userReservationQueryHandler;

        public ReservationHandler()
        {
            // username should come from cookies
            //var username = "";
            _reservationRepository = new ReservationRepository(new CodeFirstHotelModel());
            //_userReservationQueryHandler = new UserReservationQueryHandler(username);
        }

        /// <summary>
        /// Create a new Reservation.
        /// !!!!!!!! Not set User yet !!!!!!!
        /// </summary>
        /// <param name="type">ROOM_TYPE</param>
        /// <param name="start">check-in date</param>
        /// <param name="end">check-out date</param>
        /// <param name="guests">list of guests attending</param>
        /// <returns>TODO RETURNS</returns>
        public Guid MakeReservation(string username, ROOM_TYPE type, DateTime start, DateTime end, List<Guest> guests, List<int> prices)
        {
            IUserReservationQueryHandler userReservationQueryHandler = new UserReservationQueryHandler(username);
            Reservation reservation = new Reservation
            {
                Id = Guid.NewGuid(),
                AspNetUser = userReservationQueryHandler.User,
                StartDate = start,
                EndDate = end,
                Guests = guests,
                IsPaid = false,
                DailyPrices = new List<DailyPrice>()
            };

            //var prices = new RoomHandler().GetRoomPriceList(type, start, end);
            foreach (int price in prices)
            {
                var dailyPrice = new DailyPrice {Id = reservation.Id, Date = start, BillingPrice = price};
                // TODO POTENTIAL BUG. WAIT FOR TEST CASES.
                start = start.AddDays(1);
                reservation.DailyPrices.Add(dailyPrice);
            }

            _reservationRepository.InsertReservation(reservation);
            _reservationRepository.Save();
            return reservation.Id;
        }

        public void PayReservation(Guid confirmationNumber, Profile billingInfo)
        {
            throw new NotImplementedException();
            //Reservation reservation = _reservationRepository.GetReservation(confirmationNumber);
            //if (reservation != null)
            //{
            //    reservation.BillingInfo = billingInfo;
            //    reservation.isPaid = true;
            //}
            //_reservationRepository.UpdateReservation(reservation);
            //_reservationRepository.Save();
        }

        /// <summary>
        /// Cacnel a reservation by its creator's username and confirmation number
        /// </summary>
        /// <param name="confirmationNumber">confirmation number of the reservation</param>
        /// <returns>true if successfully cancelled</returns>
        public void CancelReservation(Guid confirmationNumber, DateTime today)
        {
            Reservation reservation = _reservationRepository.GetReservation(confirmationNumber);

            // refuse to cancel if checkin
            if (reservation.CheckInDate != null && reservation.CheckInDate < today)
            {
                return;
            }

            // refuse to cancel if the date is before the present date 
            // TODO EXPRESSION IS ALWAYS TRUE.
            if (reservation.StartDate != null && reservation.StartDate < today)
            {
                return;
            }

            _reservationRepository.DeleteReservation(confirmationNumber);
            _reservationRepository.Save();
        }

        [Obsolete]
        public Reservation GetReservation(Guid confirmationNumber)
        {
            return null;
        }


        public List<Reservation> GetUpComingReservations(AspNetUser user)
        {
            return new List<Reservation>(_reservationRepository.GetReservationsByUserId(user.UserName));
        }

        [Obsolete]
        public bool FillGuestInfo(Reservation reservation, List<Guest> customers)
        {
            return false;
        }
    }
}
