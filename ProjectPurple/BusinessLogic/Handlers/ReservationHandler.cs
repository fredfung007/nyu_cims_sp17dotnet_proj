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
        private readonly IRoomRepository _roomRepository;
        //private readonly IUserReservationQueryHandler _userReservationQueryHandler;

        public ReservationHandler()
        {
            // username should come from cookies
            //var username = "";
            _reservationRepository = new ReservationRepository(new CodeFirstHotelModel());
            _roomRepository = new RoomRepository(new CodeFirstHotelModel());
            //_userReservationQueryHandler = new UserReservationQueryHandler(username);
        }

        /// <summary>
        /// Create a new Reservation.
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

        public bool HasReservation(Guid confirmationNumber)
        {
            return _reservationRepository.GetReservation(confirmationNumber) != null;
        }

        public Reservation GetReservation(Guid confirmationNum)
        {
            return _reservationRepository.GetReservation(confirmationNum);
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

        /// <summary>
        /// Check in a reservation on specific date by its confirmation number
        /// </summary>
        /// <param name="confirmationNumber">confirmation number of the date</param>
        /// <param name="today">check in date</param>
        public bool CheckIn(Guid confirmationNumber, DateTime today)
        {
            Reservation reservation =
                _reservationRepository.GetReservation(confirmationNumber);

            if (reservation == null || reservation.CheckInDate > today || reservation.CheckOutDate < today)
            {
                return false;
            }

            DateTime checkDate = today;
            while (checkDate.CompareTo(reservation.CheckOutDate) < 0)
            {
                _roomRepository.UpdateRoomUsage(reservation.RoomType, checkDate, -1);
                checkDate = checkDate.AddDays(1);
            }

            reservation.CheckInDate = today;
            _reservationRepository.UpdateReservation(reservation);
            _reservationRepository.Save();
            return true;
        }

        /// <summary>
        /// Check out a reservation on specific date by its confirmation number
        /// </summary>
        /// <param name="confirmationNumber">confirmation number of the reservation</param>
        /// <param name="today">check out date</param>
        public bool CheckOut(Guid confirmationNumber, DateTime today)
        {
            Reservation reservation =
                _reservationRepository.GetReservation(confirmationNumber);

            if (reservation == null || reservation.CheckInDate == null || reservation.CheckInDate > today)
            {
                return false;
            }

            DateTime checkDate = today;
            while (checkDate.CompareTo(reservation.CheckOutDate) < 0)
            {
                _roomRepository.UpdateRoomUsage(reservation.RoomType, checkDate, +1);
                checkDate = checkDate.AddDays(1);
            }
            _roomRepository.Save();

            // loyalty program
            int stayLength = 0;
            AspNetUser user = reservation.AspNetUser;
            DateTime checkInDate = (DateTime)reservation.CheckInDate;

            if (user.LoyaltyYear != null && ((DateTime)user.LoyaltyYear).Year == today.Year)
            {
                // Checkout date is the same year as the loyalty program
                stayLength = Math.Min((today - checkInDate).Days, today.DayOfYear);
                reservation.AspNetUser.LoyaltyProgress += stayLength;
            }
            else
            {
                // Checkout date is a new year
                var newYear = new DateTime(today.Year, 1, 1);
                stayLength = (today - newYear).Days;
                reservation.AspNetUser.LoyaltyProgress = stayLength;
                reservation.AspNetUser.LoyaltyYear = newYear;
            }

            reservation.CheckOutDate = today;
            _reservationRepository.UpdateReservation(reservation);
            _reservationRepository.Save();
            return true;
        }

        /// <summary>
        /// Get all reservations that will check out today
        /// </summary>
        /// <param name="today"></param>
        /// <returns></returns>
        public IEnumerable<Reservation> GetReservationsCheckOutToday(DateTime today)
        {
            return _reservationRepository.GetReservationsByEndDate(today);
        }

        /// <summary>
        /// Get all reservations that will checkin toady
        /// </summary>
        /// <param name="today"></param>
        /// <returns></returns>
        public IEnumerable<Reservation> GetReservationsCheckInToday(DateTime today)
        {
            return _reservationRepository.GetReservationsByStartDate(today);
        }

        /// <summary>
        /// get all reservations that can be checked out, which means it is checkedin and still stay in the hotel
        /// </summary>
        /// <param name="today"></param>
        /// <returns></returns>
        public IEnumerable<Reservation> GetAllCheckedInReservations(DateTime today)
        {
            return _reservationRepository.GetReservationsCheckedInBeforeDate(today);
        }
    }
}
