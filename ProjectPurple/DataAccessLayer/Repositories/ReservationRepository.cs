using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using DataAccessLayer.EF;

namespace DataAccessLayer.Repositories
{
    // TODO using async
    public class ReservationRepository : IReservationRepository, IDisposable
    {
        private readonly HotelModelContext _context;

        public ReservationRepository(HotelModelContext context)
        {
            _context = context;
        }

        public Reservation GetReservation(Guid id)
        {
            return _context.Reservations.Include(rsv => rsv.Guests)
                .Include(rsv => rsv.DailyPrices)
                .Include(rsv => rsv.AspNetUser)
                .FirstOrDefault(rsv => rsv.Id == id);
        }

        public IEnumerable<Reservation> GetReservations()
        {
            return _context.Reservations.Include(rsv => rsv.AspNetUser)
                .Include(rsv => rsv.Guests)
                .ToList();
        }

        public void InsertReservation(Reservation reservation)
        {
            _context.Reservations.Add(reservation);
        }

        public void UpdateReservation(Reservation reservation)
        {
            _context.Entry(reservation).State = EntityState.Modified;
        }

        public void InsertReservationWithAspnetUser(Reservation reservation, string userName)
        {
            _context.Reservations.Add(reservation);
            AspNetUser aspUser = _context.AspNetUsers.Include(user => user.Profile)
                .FirstOrDefault(user => user.UserName == userName);
            var attachedEntry = _context.Entry(reservation);
            reservation.AspNetUser = aspUser;

            attachedEntry.CurrentValues.SetValues(reservation);
        }

        public IEnumerable<Reservation> GetReservationsByEndDate(DateTime endDate)
        {
            DateTime tomorrow = endDate.AddDays(1);
            return _context.Reservations.Where(reservation => reservation.EndDate >= endDate
                                                              && reservation.EndDate < tomorrow
                                                              && reservation.CheckInDate != null
                                                              && reservation.CheckInDate < endDate)
                .ToList();
        }

        public void CancelReservation(Guid id)
        {
            Reservation reservation = GetReservation(id);
            CancelReservation(reservation);
        }

        public IEnumerable<Reservation> GetReservationsByStartDate(DateTime startTime)
        {
            DateTime tomorrow = startTime.AddDays(1);
            return _context.Reservations.Include(rsv => rsv.AspNetUser)
                .Include(rsv => rsv.DailyPrices)
                .Include(rsv => rsv.Guests)
                .Where(reservation => reservation.StartDate >= startTime && reservation.StartDate < tomorrow)
                .ToList();
        }

        public IEnumerable<Reservation> GetReservationsCheckedInBeforeDate(DateTime endTime)
        {
            return _context.Reservations.Include(rsv => rsv.AspNetUser)
                .Include(rsv => rsv.DailyPrices)
                .Include(rsv => rsv.Guests)
                .Where(reservation => reservation.CheckInDate != null
                                      && reservation.CheckInDate < endTime
                                      && reservation.EndDate <= endTime)
                .ToList();
        }

        public void Save()
        {
            //_context.SaveChanges();
            try
            {
                _context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (DbEntityValidationResult validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError validationError in validationErrors.ValidationErrors)
                {
                    var message = string.Format("{0}:{1}",
                        validationErrors.Entry.Entity,
                        validationError.ErrorMessage);
                    // raise a new exception nesting
                    // the current instance as InnerException
                    raise = new InvalidOperationException(message, raise);
                }
                }

                throw raise;
            }
        }

        private void CancelReservation(Reservation reservation)
        {
            reservation.IsCancelled = true;
            _context.Entry(reservation).State = EntityState.Modified;
        }

        #region IDisposable Support

        private bool _disposedValue; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

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