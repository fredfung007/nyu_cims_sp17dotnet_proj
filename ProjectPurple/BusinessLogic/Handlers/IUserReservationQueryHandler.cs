using DataAccessLayer;
using System;
using System.Collections.Generic;

namespace BusinessLogic.Handlers
{
    /// <summary>
    /// Handler for quering user reservation related information.
    /// </summary>
    interface IUserReservationQueryHandler
    {
        User User { get; }

        /// <summary>
        /// Find all upcoming reservations for the user
        /// </summary>
        /// <returns>Upcoming reservations for the user.</returns>
        IEnumerable<Reservation> FindUpcomingReservations(DateTime date);

        /// <summary>
        /// Calculate the loyalty program progress information of the user.
        /// </summary>
        /// <returns>loyalty program progress</returns>
        string FindLoyaltyProgramInfo();

        /// <summary>
        /// Get the billing information(Profile) of the user.
        /// </summary>
        /// <returns>Billing Profile</returns>
        Profile GetProfile();
    }
}
