using System;

namespace BusinessLogic.Customer
{
    /// <summary>
    /// Interface for customer/guest who will stay at the hotel. 
    /// </summary>
    interface ICustomer
    {
        /// <summary>
        /// First name of the customer. 
        /// </summary>
        String firstName { get; }
        
        /// <summary>
        /// Last name of the customer. 
        /// </summary>
        String lastName { get; }
    }
}
