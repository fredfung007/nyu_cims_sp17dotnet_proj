using System;
using System.Collections.Generic;
using DataAccessLayer.EF;

namespace DataAccessLayer.Repositories
{
    /// <summary>
    ///     The class that is used to store all information of one reservation.
    ///     Immutable. Created once the reservation is submitted.
    /// </summary>
    public interface IReservationRepository : IDisposable
    {
        // unique ID per reservation, should be generated in the constructor
        Reservation GetReservation(Guid id);

        IEnumerable<Reservation> GetReservations();
        IEnumerable<Reservation> GetReservationsByUserId(string username);
        IEnumerable<Reservation> GetReservationsByCheckOutDate(DateTime checkOutDate);
        IEnumerable<Reservation> GetReservationsByCheckInDate(DateTime checkInDate);

        [Obsolete]
        IEnumerable<Reservation> GetReservationsByPeriod(DateTime start, DateTime end);

        void InsertReservation(Reservation reservation);
        void DeleteReservation(Guid id);
        void CancelReservation(Guid id);
        void UpdateReservation(Reservation reservation);
        void UpdateReservationWithAspnetUser(Reservation reservation);
        void UpdateReservationCheckInDate(Reservation reservation, DateTime checkInDate);
        void UpdateReservationCheckOutDate(Reservation reservation, DateTime checkOutDate);
        IEnumerable<Reservation> GetReservationsByEndDate(DateTime endDate);
        IEnumerable<Reservation> GetReservationsByStartDate(DateTime startDate);
        IEnumerable<Reservation> GetReservationsCheckedInBeforeDate(DateTime checkInDate);
        void Save();
    }
}