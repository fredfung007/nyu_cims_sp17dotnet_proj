using System.Data.Entity;
using DataAccessLayer;

namespace BusinessLogic.Handlers
{
    /// <summary>
    /// A handler class for maintaining profile information for user
    /// </summary>
    // maintain customer name, home address, room preference, loyalty program number
    // loyalty program status
    class ProfileHandler
    {
        DbContext context;
        public ProfileHandler(DbContext context)
        {
            this.context = context;
        }
        Guest getCustomerInfo(int userId)
        {
            return null;
        }

        //TODO? use ICustomer or userId
        DataAccessLayer.Address getAddress(int userId)
        {
            return null;
        }

        Email getEmail(int userId)
        {
            return null;
        }

        PhoneNumber getPhoneNumber(int userId)
        {
            return null;
        }

        // get room preference
        // get loyalty program number

        bool setAddress(int userId, DataAccessLayer.Address address)
        {
            return false;
        }

        bool setEmail(int userId, DataAccessLayer.Email email)
        {
            return false;
        }

        bool setPhoneNumber(int userId, PhoneNumber phoneNumber)
        {
            return false;
        }

        // set room preference

        // set loyalty program number ?
        
        // TODO: update loyalty program status
       
    }
}
