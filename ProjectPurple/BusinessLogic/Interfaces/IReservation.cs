using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Customer;
using BusinessLogic.BillingInfo;

namespace BusinessLogic.Reservation
{
    interface IReservation
    {
        // unique ID per reservation, should be generated in the constructor
        long confirmationNumber { get; }
        IBillingInfo billInfo { get; }
        List<ICustomer> guests { get; }
        DateTime startDate { get; }
        DateTime endDate { get; }
        int duration { get; }
        List<int> pricePerDay { get; }
        int getTotalPrice();
    }
}