using System;
using System.Collections.Generic;
using DataAccessLayer;

namespace DataAccessLayer.Repositories
{
    /// <summary>
    /// The class that is used to store all information of one reservation.
    /// Immutable. Created once the reservation is submitted.
    /// </summary>
    public interface IReservationRepository: IDisposable
    {
        // unique ID per reservation, should be generated in the constructor
        Reservation getReservation(Guid Id);
        IEnumerable<Reservation> getReservations();

        IEnumerable<Reservation> getReservationsByConfirmNum(Guid ConfirmationNumber);
        IEnumerable<Reservation> getReservationsByUserId(Guid UserId);
        IEnumerable<Reservation> getReservationsByCheckOutDate(DateTime CheckOutDate);
        IEnumerable<Reservation> getReservationsByCheckInDate(DateTime CheckInDate);
        // commentted for now, did not find use cases for this method
        // IEnumerable<Reservation> getReservationsByPeriod(DateTime start, DateTime end);

        void InsertReservation(Reservation reservation);
        void DeleteReservation(Guid Id);
        void UpdateReservation(Reservation reservation);
        void save();
    }
}