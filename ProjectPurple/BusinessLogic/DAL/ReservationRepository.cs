using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace BusinessLogic.DAL
{
    // TODO using async
    class ReservationRepository:IReservationRepository, IDisposable
    {
        private HotelDataModelContainer context;

        public ReservationRepository(HotelDataModelContainer context)
        {
            this.context = context;
        }

        public Reservation getReservation(Guid Id)
        {
            return context.Reservations.Find(Id);
        }

        public IEnumerable<Reservation> getReservations()
        {
            return context.Reservations.ToList();
        }

        public void InsertReservation(Reservation reservation)
        {
            context.Reservations.Add(reservation);
        }

        public void DeleteReservation(Guid Id)
        {
            Reservation reservation = context.Reservations.Find(Id);
            context.Reservations.Remove(reservation);
        }

        public void UpdateReservation(Reservation reservation)
        {
            context.Entry(reservation).State = System.Data.Entity.EntityState.Modified;
        }

        public IEnumerable<Reservation> getReservationsByConfirmNum(Guid ConfirmationNumber)
        {
            return context.Reservations.Where(reservation => reservation.Id == ConfirmationNumber).ToList();
        }

        public IEnumerable<Reservation> getReservationsByUserId(Guid UserId)
        {
            return context.Reservations.Where(reservation => reservation.User.Id == UserId).ToList();
        }

        public IEnumerable<Reservation> getReservationsByCheckOutDate(DateTime CheckOutDate)
        {
            return context.Reservations.Where(reservatoin => reservatoin.endDate == CheckOutDate).ToList();
        }

        public IEnumerable<Reservation> getReservationsByCheckInDate(DateTime CheckInDate)
        {
            return context.Reservations.Where(reservation => reservation.startDate == CheckInDate).ToList();
        }

        public IEnumerable<Reservation> getReservationsByPeriod(DateTime startDate, DateTime endDate)
        {
            return context.Reservations
                        .Where(reservation => reservation.startDate == startDate && reservation.endDate == endDate)
                        .ToList();
        }
        public void save()
        {
            context.SaveChanges();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    context.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
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
