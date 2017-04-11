using System;
using System.Collections.Generic;
using BusinessLogic.Customer;
using BusinessLogic.BillingInfo;

namespace BusinessLogic.Reservation
{
    interface IReservation
    {
        // unique ID per reservation, should be generated in the constructor
        Guid confirmationNumber { get; }
        IBillingInfo billInfo { get; }
        List<ICustomer> guests { get; }
        DateTime startDate { get; }
        DateTime endDate { get; }
        int duration { get; }
        List<int> pricePerDay { get; }
        int getTotalPrice();
    }
}