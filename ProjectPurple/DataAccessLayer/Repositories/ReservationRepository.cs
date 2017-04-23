using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace DataAccessLayer.Repositories
{
    // TODO using async
    public class ReservationRepository:IReservationRepository, IDisposable
    {
        private HotelDataModelContainer _context;

        public ReservationRepository(HotelDataModelContainer context)
        {
            this._context = context;
        }

        public Reservation GetReservation(Guid id)
        {
            return _context.Reservations.Find(id);
        }

        public IEnumerable<Reservation> GetReservations()
        {
            return _context.Reservations.ToList();
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
            _context.Entry(reservation).State = System.Data.Entity.EntityState.Modified;
        }

        public IEnumerable<Reservation> GetReservationsByUserId(String username)
        {
            return _context.Reservations.Where(reservation => reservation.User.Username == username).ToList();
        }

        public IEnumerable<Reservation> GetReservationsByCheckOutDate(DateTime checkOutDate)
        {
            return _context.Reservations.Where(reservatoin => reservatoin.endDate == checkOutDate).ToList();
        }

        public IEnumerable<Reservation> GetReservationsByCheckInDate(DateTime checkInDate)
        {
            return _context.Reservations.Where(reservation => reservation.startDate == checkInDate).ToList();
        }

        public void UpdateReservationCheckInDate(Reservation reservation, DateTime checkInDate)
        {
            reservation.checkInDate = checkInDate;
            _context.Entry(reservation).State = System.Data.Entity.EntityState.Modified;
        }
        public void UpdateReservationCheckOutDate(Reservation reservation, DateTime checkOutDate)
        {
            reservation.checkOutDate = checkOutDate;
            _context.Entry(reservation).State = System.Data.Entity.EntityState.Modified;
        }

        public IEnumerable<Reservation> GetReservationsByEndDate(DateTime endDate)
        {
            return _context.Reservations.Where(reservation => reservation.endDate == endDate
                                            && reservation.checkInDate != null
                                            && reservation.checkInDate < endDate).ToList();
        }

        public IEnumerable<Reservation> GetReservationsByStartDate(DateTime startDate)
        {
            return _context.Reservations.Where(reservation => reservation.startDate == startDate).ToList();
        }

        public IEnumerable<Reservation> GetReservationsCheckedInBeforeDate(DateTime checkInDate)
        {
            return _context.Reservations.Where(reservation => reservation.checkInDate != null
                                            && reservation.checkInDate < checkInDate
                                            && reservation.endDate >= checkInDate).ToList();
        }

        // commentted for now, did not find use cases for this method
        // public IEnumerable<Reservation> getReservationsByPeriod(DateTime startDate, DateTime endDate)
        // {
        //     return context.Reservations
        //                 .Where(reservation => reservation.startDate == startDate && reservation.endDate == endDate)
        //                 .ToList();
        // }
        public void Save()
        {
            _context.SaveChanges();
        }

        #region IDisposable Support
        private bool _disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                _disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~ReservationRepository() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
