using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Address;
using BusinessLogic.PhoneNumber;
using BusinessLogic.Email;

namespace BusinessLogic.BillingInfo
{
    interface IBillingInfo
    {
        IAddress address { get; }
        IPhoneNumber phoneNumber { get; }
        IEmail email { get; }
        string firstName { get; }
        string lastName { get; }
    }
}
