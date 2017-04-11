using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.BillingInfo;
using BusinessLogic.Constants;
using BusinessLogic.Customer;
using BusinessLogic.Reservation;
using BusinessLogic.Users;

namespace BusinessLogic.StaffLogic
{ 
    class StaffHandler
    {
        /// <summary>
        /// Method to check in a reservation by its confirmation number
        /// </summary>
        /// <param name="confirmationNumber"> the confirmation number of the reservation to check in</param>
        void checkIn(Guid confirmationNumber)
        {
        }

        /// <summary>
        /// Method to check out a reservation by its confirmation number
        /// </summary>
        /// <param name="confirmationNumber"> the confirmation number of the reservation to check out</param>
        void checkOut(Guid confirmationNumber)
        {
        }

        /// <summary>
        /// Set room inventory
        /// </summary>
        /// <param name="type">Room type of roomType</param>
        /// <param name="amount">Room amount</param>
        /// <returns>true if succeeded</returns>
        bool setRoomInventory(roomType type, int amount)
        {
        }

        /// <summary>
        /// Get room inventory
        /// </summary>
        /// <param name="type">Room type of roomType</param>
        /// <returns>number of rooms</returns>
        int getRooomInventory(roomType type)
        {
        }
    }
}
