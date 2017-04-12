using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.BillingInfo;
using BusinessLogic.Constants;
using BusinessLogic.Customer;
using BusinessLogic.Reservation;
using BusinessLogic.Users;
using System.Data.Entity;

namespace BusinessLogic.Handlers
{
    /// <summary>
    /// A handler class for editing reservation for user. 
    /// </summary>
    class ReservationHandler
    {
        DbContext context;
        public ReservationHandler(DbContext context)
        {
            this.context = context;
        }
        Guid makeReservation(int userId, roomType type, DateTime start, DateTime end)
        {
            return Guid.Empty;
        }
        bool cancelReservation(int userId, Guid confirmationNumber)
        {
            return false;
        }

        IReservation getReservation(Guid confirmationNumber)
        {
            return null;
        }

        List<IReservation> getUpComingReservations(ICustomer customer)
        {
            return null;
        }

        bool fillGuestInfo(IReservation reservation, List<ICustomer> customers)
        {
            return false;
        }
    }
}
