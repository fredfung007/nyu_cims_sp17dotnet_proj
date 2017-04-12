using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Customer;

namespace BusinessLogic.Handlers
{
    /// <summary>
    /// A handler class for customer status editing by Staff member.
    /// </summary>
    class CustomerHandler
    {
        //TODO check using userId or ICustomer
        bool checkIn(int userId, DateTime date)
        {
            return false;
        }

        bool checkOut(int userId, DateTime date)
        {
            return false;
        }

        List<ICustomer> getCheckOutCustomersOnDate(DateTime date)
        {
            return null;
        }

        List<ICustomer> getCheckInCustomersOnDate(DateTime date)
        {
            return null;
        }
    }
}
