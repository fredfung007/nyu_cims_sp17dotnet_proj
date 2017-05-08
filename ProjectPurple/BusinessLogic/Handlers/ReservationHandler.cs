using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.Type;
using DataAccessLayer.Constants;
using DataAccessLayer.EF;
using DataAccessLayer.Repositories;

namespace BusinessLogic.Handlers
{
    /// <summary>
    ///     A handler class for editing reservation for user.
    /// </summary>
    public class ReservationHandler
    {
        // TODO add a class for this to maintaining the search reasults with expiration date
        public static Dictionary<string, TimeExpirationType> SearchResultPool =
            new Dictionary<string, TimeExpirationType>();

        private readonly IReservationRepository _reservationRepository;

        private readonly IRoomRepository _roomRepository;
        //private readonly IUserReservationQueryHandler _userReservationQueryHandler;

        public ReservationHandler()
        {
            // username should come from cookies
            //var username = "";
            _reservationRepository = new ReservationRepository(new HotelModelContext());
            _roomRepository = new RoomRepository(new HotelModelContext());
            //_userReservationQueryHandler = new UserReservationQueryHandler(username);
        }

        /// <summary>
        ///     Create a new Reservation.
        /// </summary>
        /// <param name="username">The username used to make this reservation</param>
        /// <param name="type">ROOM_TYPE</param>
        /// <param name="start">check-in date</param>
        /// <param name="end">check-out date</param>
        /// <param name="guests">list of guests attending</param>
        /// <param name="prices">list of prices for each day of the reservation</param>
        /// <returns>the Id of the created reservation</returns>
        public Guid MakeReservation(string username, ROOM_TYPE type, DateTime start, DateTime end,
            List<Guest> guests, List<int> prices)
        {
            var dailyPriceList = new List<DailyPrice>();
            Guid rsvId = Guid.NewGuid();

            DateTime curDate = start;
            foreach (var price in prices)
            {
                dailyPriceList.Add(new DailyPrice
                {
                    Id = Guid.NewGuid(),
                    Date = curDate,
                    BillingPrice = price
                });
                curDate = curDate.AddDays(1);
            }

            var reservation = new Reservation
            {
                Id = rsvId,
                StartDate = start,
                EndDate = end,
                Guests = guests,
                IsPaid = true,
                IsCancelled = false,
                DailyPrices = dailyPriceList,
                RoomType = type
            };

            if (username != null)
            {
                _reservationRepository.InsertReservationWithAspnetUser(reservation, username);
                _reservationRepository.Save();
            }
            else
            {
                _reservationRepository.InsertReservation(reservation);
                _reservationRepository.Save();
            }

            // update room occupancy
            DateTime checkDate = start.Date;
            while (checkDate.Date.CompareTo(reservation.EndDate.Date) <= 0)
            {
                _roomRepository.UpdateRoomOccupancy(reservation.RoomType, checkDate, 1);
                checkDate = checkDate.AddDays(1);
            }
            _roomRepository.Save();

            return rsvId;
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
        ///     Cacnel a reservation by its creator's username and confirmation number
        /// </summary>
        /// <param name="confirmationNumber">confirmation number of the reservation</param>
        /// <returns>true if successfully cancelled</returns>
        public bool CancelReservation(Guid confirmationNumber, DateTime now)
        {
            if (!CanBeCanceled(confirmationNumber, now))
            {
                return false;
            }

            Reservation reservation = _reservationRepository.GetReservation(confirmationNumber);
            _reservationRepository.CancelReservation(confirmationNumber);
            DateTime checkDate = now.Date;
            while (checkDate < reservation.EndDate)
            {
                _roomRepository.UpdateRoomOccupancy(reservation.RoomType, checkDate, 1);
                checkDate = checkDate.AddDays(1);
            }
            _roomRepository.Save();
            _reservationRepository.Save();
            return true;
        }

        public bool CanBeCanceled(Guid confirmationNumber, DateTime now)
        {
            Reservation reservation = _reservationRepository.GetReservation(confirmationNumber);

            // is already canceled
            if (reservation.IsCancelled)
            {
                return false;
            }

            // refuse to cancel if checkin
            if (reservation.CheckInDate != null && reservation.CheckInDate < now)
            {
                return false;
            }

            // refuse to cancel if the date is before the present date, now must 
            if (reservation.StartDate == null || reservation.StartDate < now)
            {
                return false;
            }

            return true;
        }

        // check guid and check confirmation id
        public bool HashReservation(string confirmationNumberStr)
        {
            Guid confirmationId;
            if (!Guid.TryParse(confirmationNumberStr, out confirmationId))
            {
                return false;
            }

            return _reservationRepository.GetReservation(confirmationId) != null;
        }

        public Reservation GetReservation(Guid confirmationNum)
        {
            return _reservationRepository.GetReservation(confirmationNum);
        }


        public async Task<List<Reservation>> GetUpComingReservations(string userId)
        {
            var reservations = _reservationRepository.GetReservations();

            return reservations.Where(reservation => reservation.AspNetUser != null &&
                                                     reservation.AspNetUser.Id.Equals(userId) &&
                                                     reservation.EndDate.CompareTo(DateTimeHandler.GetCurrentTime()) >
                                                     0 &&
                                                     reservation.IsCancelled == false &&
                                                     reservation.CheckOutDate == null)
                .ToList();
        }

        /// <summary>
        ///     Check in a reservation on specific date by its confirmation number
        /// </summary>
        /// <param name="confirmationNumber">confirmation number of the date</param>
        /// <param name="today">check in date</param>
        public bool CheckIn(Guid confirmationNumber, DateTime today)
        {
            Reservation reservation = _reservationRepository.GetReservation(confirmationNumber);

            if (reservation == null || reservation.CheckInDate != null || reservation.StartDate > today ||
                reservation.EndDate < today)
            {
                return false;
            }

            // check current checkedin number v.s. inventory number
            var currentAmount = _roomRepository.GetRoomOccupancyByDate(reservation.RoomType, today);
            var totalAmount = _roomRepository.GetRoomTotalAmount(reservation.RoomType);
            if (currentAmount >= totalAmount)
            {
                return false;
            }

            reservation.CheckInDate = today;
            _reservationRepository.UpdateReservation(reservation);
            _reservationRepository.Save();
            return true;
        }

        /// <summary>
        ///     Check out a reservation on specific date by its confirmation number
        /// </summary>
        /// <param name="confirmationNumber">confirmation number of the reservation</param>
        /// <param name="today">check out date</param>
        public bool CheckOut(Guid confirmationNumber, DateTime today)
        {
            Reservation reservation = _reservationRepository.GetReservation(confirmationNumber);

            if (reservation == null || reservation.CheckOutDate != null || reservation.CheckInDate == null ||
                reservation.CheckInDate > today)
            {
                return false;
            }

            DateTime checkDate = today.Date;
            // if stay shorter, here should use today. But this is not required
            while (checkDate.Date.CompareTo(reservation.EndDate.Date) <= 0)
            {
                _roomRepository.UpdateRoomOccupancy(reservation.RoomType, checkDate, -1);
                checkDate = checkDate.AddDays(1);
            }
            _roomRepository.Save();

            // loyalty program
            AspNetUser user = reservation.AspNetUser;
            var checkInDate = (DateTime) reservation.CheckInDate;
            if (user != null)
            {
                var stayLength = 0;
                if (user.LoyaltyYear != null && ((DateTime) user.LoyaltyYear).Year == today.Year)
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
            }
            reservation.CheckOutDate = today;
            _reservationRepository.UpdateReservation(reservation);
            _reservationRepository.Save();
            return true;
        }

        /// <summary>
        ///     Get all reservations that will check out today
        /// </summary>
        /// <param name="today"></param>
        /// <returns></returns>
        public IEnumerable<Reservation> GetReservationsCheckOutToday(DateTime today)
        {
            return _reservationRepository.GetReservationsByEndDate(today);
        }

        /// <summary>
        ///     Get all reservations that will checkin toady
        /// </summary>
        /// <param name="today"></param>
        /// <returns></returns>
        public IEnumerable<Reservation> GetReservationsCheckInToday(DateTime today)
        {
            return _reservationRepository.GetReservationsByStartDate(today);
        }

        /// <summary>
        ///     get all reservations that can be checked out, which means it is checkedin and still stay in the hotel
        /// </summary>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public IEnumerable<Reservation> GetAllReservationsCanBeCheckedOut(DateTime endTime)
        {
            return _reservationRepository.GetReservationsCheckedInBeforeDate(endTime);
        }
    }
}