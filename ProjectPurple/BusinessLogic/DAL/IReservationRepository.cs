using System;
using System.Collections.Generic;
using DataAccessLayer;

namespace BusinessLogic.DAL
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
        void InsertReservation(Reservation reservation);
        void DeleteReservation(Guid Id);
        void UpdateReservation(Reservation reservation);
        void save();
    }
}