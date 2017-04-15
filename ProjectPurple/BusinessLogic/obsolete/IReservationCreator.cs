//using System;
//using System.Collections.Generic;
//using BusinessLogic.Constants;
//using BusinessLogic.Customer;
//using DataAccessLayer;

//namespace BusinessLogic.ReservationCreator
//{
//    interface IReservationCreator
//    {
//        /// <summary>
//        /// Method that returns a new Reservation instance.
//        /// </summary>
//        /// <param name="start"> the start date of the reservation </param>
//        /// <param name="end"> the end date of the reservation</param>
//        /// <param name="type"> the room type the user reserved</param>
//        /// <param name="pricePerDay"> a list of integers to show price of the room on each day between (inclusive) the start date and the end date</param>
//        /// <param name="guests"> a list of Customer instances that will stay in the room</param>
//        /// <param name="billingInfo"> a instance of BillingInfo to store necessary information for the billing</param>
//        /// <returns></returns>
//        Reservation createNewReservation(DateTime start, DateTime end, roomType type, List<int> pricePerDay, List<ICustomer> guests, IBillingInfo billingInfo);

//        /// <summary>
//        /// Method to to cancel a reservation by a confirmation number
//        /// </summary>
//        /// <param name="confirmationNumber"> the confirmation number of a reservation</param>
//        void createCancelRequest(Guid confirmationNumber);
//    }
//}
