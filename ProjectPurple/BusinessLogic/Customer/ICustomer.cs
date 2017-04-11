using BusinessLogic.Address;
using BusinessLogic.Email;
using BusinessLogic.PhoneNumber;
using System;

namespace BusinessLogic.Customer
{
    // Interface for customer/guest who will stay at the hotel. 
    interface ICustomer
    {
        // Return the first name of the customer. 
        String getFirstName();
        // Return the last name of the customer. 
        String getLastName();
        // Return the address of the customer. 
        IAddress getAddress();
        // Return the email address of the customer. 
        IEmail getEmail();
        // Return the phone number of the customer. 
        IPhoneNumber getPhoneNumber();
    }
}
