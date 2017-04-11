using System;

namespace BusinessLogic.Customer
{
    // Interface for customer/guest who will stay at the hotel. 
    interface ICustomer
    {
        // First name of the customer. 
        String firstName { get; }
        // Last name of the customer. 
        String lastName { get; }
    }
}
