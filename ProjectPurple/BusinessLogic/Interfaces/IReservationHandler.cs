using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.ReservationHandler
{
    interface IReservationHandler
    {
        /// <summary>
        /// Method to check in a reservation by its confirmation number
        /// </summary>
        /// <param name="confirmationNumber"> the confirmation number of the reservation to check in</param>
        void checkIn(Guid confirmationNumber);

        /// <summary>
        /// Method to check out a reservation by its confirmation number
        /// </summary>
        /// <param name="confirmationNumber"> the confirmation number of the reservation to check out</param>
        void checkOut(Guid confirmationNumber);
    }
}
