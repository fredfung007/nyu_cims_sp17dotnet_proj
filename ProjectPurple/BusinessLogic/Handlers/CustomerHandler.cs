using System;
using System.Collections.Generic;
using System.Data.Entity;
using DataAccessLayer;
using DataAccessLayer.Repositories;

namespace BusinessLogic.Handlers
{
    /// <summary>
    /// A handler class for customer status editing by Staff member.
    /// </summary>
    class CustomerHandler
    {
        IRoomRepository roomRepository;
        public CustomerHandler()
        {
            roomRepository = new RoomRepository(new HotelDataModelContainer());   
        }

        //TODO check using userId or ICustomer
        bool checkIn(int userId, DateTime date)
        {
            return false;
        }

        bool checkOut(int userId, DateTime date)
        {
            return false;
        }

        List<Guest> getCheckOutCustomersOnDate(DateTime date)
        {
            return null;
        }

        List<Guest> getCheckInCustomersOnDate(DateTime date)
        {
            return null;
        }
    }
}
