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
        void InsertReservation(Reservation reservation);
        void CancelReservation(Guid id);
        void UpdateReservation(Reservation reservation);
        void InsertReservationWithAspnetUser(Reservation reservation, string userName);
        IEnumerable<Reservation> GetReservationsByEndDate(DateTime endTime);
        IEnumerable<Reservation> GetReservationsByStartDate(DateTime startTime);
        IEnumerable<Reservation> GetReservationsCheckedInBeforeDate(DateTime endTime);
        IEnumerable<Reservation> GetExpiredReservations(DateTime endTime);
        void Save();
    }
}