using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Reservation;
using BusinessLogic.Constants;
using BusinessLogic.Customer;
using BusinessLogic.BillingInfo;

namespace BusinessLogic.ReservationCreator
{
    interface IReservationCreator
    {
        IReservation createNewReservation(DateTime start, DateTime end, roomType type, List<int> pricePerDay, List<ICustomer> guests, IBillingInfo billingInfo);
        void createCancelRequest(Guid confirmationNumber);
    }
}
