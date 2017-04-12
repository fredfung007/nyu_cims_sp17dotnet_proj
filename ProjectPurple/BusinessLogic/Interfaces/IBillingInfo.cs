using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Address;
using BusinessLogic.PhoneNumber;
using BusinessLogic.Email;
using BusinessLogic.Customer;

namespace BusinessLogic.BillingInfo
{
    /// <summary>
    /// Used to store billing information of one reservation.
    /// Can be different with information of the user
    /// who creates the reservation.
    /// </summary>
    interface IBillingInfo : ICustomer
    {
        IAddress address { get; }
        IPhoneNumber phoneNumber { get; }
        IEmail email { get; }
    }
}
