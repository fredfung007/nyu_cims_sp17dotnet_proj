using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Address;
using BusinessLogic.Email;
using BusinessLogic.PhoneNumber;
using BusinessLogic.Customer;

namespace BusinessLogic.Handlers
{
    /// <summary>
    /// A handler class for maintaining profile information for user
    /// </summary>
    // maintain customer name, home address, room preference, loyalty program number
    // loyalty program status
    class ProfileHandler
    {
        ICustomer getCustomerInfo(int userId)
        {
            return null;
        }

        //TODO? use ICustomer or userId
        IAddress getAddress(int userId)
        {
            return null;
        }

        IEmail getEmail(int userId)
        {
            return null;
        }

        IPhoneNumber getPhoneNumber(int userId)
        {
            return null;
        }

        // get room preference
        // get loyalty program number

        bool setAddress(int userId, IAddress address)
        {
            return false;
        }

        bool setEmail(int userId, IEmail email)
        {
            return false;
        }

        bool setPhoneNumber(int userId, IPhoneNumber phoneNumber)
        {
            return false;
        }

        // set room preference

        // set loyalty program number ?
        
        // TODO: update loyalty program status
       
    }
}
