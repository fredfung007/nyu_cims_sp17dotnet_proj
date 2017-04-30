using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DataAccessLayer.EF;

namespace DataAccessLayer.Repositories
{
    // TODO using async
    public class ReservationRepository : IReservationRepository, IDisposable
    {
        private readonly CodeFirstHotelModel _context;

        public ReservationRepository(CodeFirstHotelModel context)
        {
            _context = context;
        }

        public Reservation GetReservation(Guid id)
        {
            return _context.Reservations.Find(id);
        }

        public IEnumerable<Reservation> GetReservations()
        {
            return _context.Reservations.ToList();
        }

        public IEnumerable<Reservation> GetReservationsByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }

        public void InsertReservation(Reservation reservation)
        {
            _context.Reservations.Add(reservation);
        }

        public void DeleteReservation(Guid id)
        {
            Reservation reservation = _context.Reservations.Find(id);
            _context.Reservations.Remove(reservation);
        }

        public void UpdateReservation(Reservation reservation)
        {
            _context.Entry(reservation).State = EntityState.Modified;
        }

        public IEnumerable<Reservation> GetReservationsByUserId(string username)
        {
            return _context.Reservations.Where(reservation => reservation.User.UserName == username).ToList();
        }

        public IEnumerable<Reservation> GetReservationsByCheckOutDate(DateTime checkOutDate)
        {
            return _context.Reservations.Where(reservatoin => reservatoin.EndDate == checkOutDate).ToList();
        }

        public IEnumerable<Reservation> GetReservationsByCheckInDate(DateTime checkInDate)
        {
            return _context.Reservations.Where(reservation => reservation.StartDate == checkInDate).ToList();
        }

        public void UpdateReservationCheckInDate(Reservation reservation, DateTime checkInDate)
        {
            reservation.CheckInDate = checkInDate;
            _context.Entry(reservation).State = EntityState.Modified;
        }

        public void UpdateReservationCheckOutDate(Reservation reservation, DateTime checkOutDate)
        {
            reservation.CheckOutDate = checkOutDate;
            _context.Entry(reservation).State = EntityState.Modified;
        }

        public IEnumerable<Reservation> GetReservationsByEndDate(DateTime endDate)
        {
            return _context.Reservations.Where(reservation => reservation.EndDate == endDate
                                                              && reservation.CheckInDate != null
                                                              && reservation.CheckInDate < endDate)
                .ToList();
        }

        public IEnumerable<Reservation> GetReservationsByStartDate(DateTime startDate)
        {
            return _context.Reservations.Where(reservation => reservation.StartDate == startDate).ToList();
        }

        public IEnumerable<Reservation> GetReservationsCheckedInBeforeDate(DateTime checkInDate)
        {
            return _context.Reservations.Where(reservation => reservation.CheckInDate != null
                                                              && reservation.CheckInDate < checkInDate
                                                              && reservation.EndDate >= checkInDate)
                .ToList();
        }

        // commentted for now, did not find use cases for this method
        // public IEnumerable<Reservation> getReservationsByPeriod(DateTime startDate, DateTime endDate)
        // {
        //     return context.Reservation
        //                 .Where(reservation => reservation.startDate == startDate && reservation.endDate == endDate)
        //                 .ToList();
        // }
        public void Save()
        {
            _context.SaveChanges();
        }

        #region IDisposable Support

        private bool _disposedValue; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                    _context.Dispose();

                _disposedValue = true;
            }
        }

        // ~ReservationRepository() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}