using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Constants;
using System.Data.Entity;
using BusinessLogic.DAL;
using DataAccessLayer;

namespace BusinessLogic.Handlers
{
    /// <summary>
    /// A handler class for room inventory management, price query, availability check
    /// </summary>
    class RoomHandler
    {
        IRoomRepository roomRepository;
        public RoomHandler()
        {
            roomRepository = new RoomRepository(new HotelDataModelContainer());
        }
        List<roomType> CheckAvailableTypeForDuration(DateTime start, DateTime end)
        {
            return null;
        }
        /// <summary>
        /// Get the price list for a room type from start date to end state
        /// </summary>
        /// <param name="type">Room type of roomType</param>
        /// <param name="start">Start date of DateTime</param>
        /// <param name="end">End date of DateTime</param>
        /// <returns>list for price</returns>
        List<double> GetRoomPriceList(roomType type, DateTime start, DateTime end)
        {
            return null;
        }

        /// <summary>
        /// Get price of a specific room type at date
        /// </summary>
        /// <param name="type">Room type of roomType</param>
        /// <param name="date">Date for DateTime</param>
        /// <returns>room price</returns>
        double GetRoomPrice(roomType type, DateTime date)
        {
            //TODO add base price
            return 0.0;
        }

        /// <summary>
        /// Get current room availibility
        /// </summary>
        /// <param name="type"></param>
        /// <param name="date"></param>
        /// <returns>current available rooms</returns>
        int GetCurrentRoomAvailability(roomType type, DateTime date)
        {
            return 0;
        }

        /// <summary>
        /// Get occupancy percentage of a room on date
        /// </summary>
        /// <param name="date"></param>
        /// <returns>occupancy percentage</returns>
        double GetHotelOccupancy(DateTime date)
        {
            return 0.0;
        }

        /// <summary>
        /// Get booked room on a sepcifiic date
        /// </summary>
        /// <param name="type">room type of roomType</param>
        /// <param name="date">date of DateTime</param>
        /// <returns>booked room amount</returns>
        int GetBookedRoomOnDate(roomType type, DateTime date)
        {
            return 0;
        }

        /// <summary>
        /// Set room inventory
        /// </summary>
        /// <param name="type">Room type of roomType</param>
        /// <param name="amount">Room amount</param>
        /// <returns>true if succeeded</returns>
        void SetRoomInventory(roomType type, int amount)
        {
        }

        /// <summary>
        /// Get room inventory
        /// </summary>
        /// <param name="type">Room type of roomType</param>
        /// <returns>number of rooms</returns>
        int GetRooomInventory(roomType type)
        {
            return 0;
        }

        /// <summary>
        /// Get description of the room type
        /// </summary>
        /// <param name="type">Room type of roomType</param>
        /// <returns>description string</returns>
        string GetRoomDescription(roomType type)
        {
            return null;
        }

        /// <summary>
        /// Update description of the room type
        /// </summary>
        /// <param name="type">Room type of roomType</param>
        /// <param name="description">Description string</param>
        /// <returns>true if succeeded</returns>
        void UpdateRoomDescription(roomType type, string description)
        {

        }

        /// <summary>
        /// Get room ameneties for the room type
        /// </summary>
        /// <param name="type">Room type of roomType</param>
        /// <returns>Ameneties string</returns>
        string GetRoomAmeneties(roomType type)
        {
            return null;
        }

        /// <summary>
        /// Get room picture urls
        /// </summary>
        /// <param name="type">Room type of roomType</param>
        /// <returns>url list</returns>
        List<string> GetRoomPictureUrls(roomType type)
        {
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type">Room type of roomType</param>
        /// <param name="urls">Url List</param>
        /// <returns>true if succeeded</returns>
        bool UpdateRoomPictureUrls(roomType type, List<string> urls)
        {
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type">Room type of roomType</param>
        /// <param name="url">picture url</param>
        /// <returns>true if succeded</returns>
        bool InsertPictureUrl(roomType type, string url)
        {
            return false;
        }
    }
}
