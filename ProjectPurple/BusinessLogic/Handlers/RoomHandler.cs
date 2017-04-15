using System;
using System.Collections.Generic;
using DataAccessLayer;
using DataAccessLayer.Repositories;
using DataAccessLayer.Constants;

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
        List<ROOM_TYPE> CheckAvailableTypeForDuration(DateTime start, DateTime end)
        {
            return null;
        }
        /// <summary>
        /// Get the price list for a room type from start date to end state
        /// </summary>
        /// <param name="type">Room type of ROOM_TYPE</param>
        /// <param name="start">Start date of DateTime</param>
        /// <param name="end">End date of DateTime</param>
        /// <returns>list for price</returns>
        List<double> GetRoomPriceList(ROOM_TYPE type, DateTime start, DateTime end)
        {
            return null;
        }

        /// <summary>
        /// Get price of a specific room type at date
        /// </summary>
        /// <param name="type">Room type of ROOM_TYPE</param>
        /// <param name="date">Date for DateTime</param>
        /// <returns>room price</returns>
        double GetRoomPrice(ROOM_TYPE type, DateTime date)
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
        int GetCurrentRoomAvailability(ROOM_TYPE type, DateTime date)
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
        /// <param name="type">room type of ROOM_TYPE</param>
        /// <param name="date">date of DateTime</param>
        /// <returns>booked room amount</returns>
        int GetBookedRoomOnDate(ROOM_TYPE type, DateTime date)
        {
            return 0;
        }

        /// <summary>
        /// Set room inventory
        /// </summary>
        /// <param name="type">Room type of ROOM_TYPE</param>
        /// <param name="amount">Room amount</param>
        /// <returns>true if succeeded</returns>
        void SetRoomInventory(ROOM_TYPE type, int amount)
        {
        }

        /// <summary>
        /// Get room inventory
        /// </summary>
        /// <param name="type">Room type of ROOM_TYPE</param>
        /// <returns>number of rooms</returns>
        int GetRooomInventory(ROOM_TYPE type)
        {
            return 0;
        }

        /// <summary>
        /// Get description of the room type
        /// </summary>
        /// <param name="type">Room type of ROOM_TYPE</param>
        /// <returns>description string</returns>
        string GetRoomDescription(ROOM_TYPE type)
        {
            return null;
        }

        /// <summary>
        /// Update description of the room type
        /// </summary>
        /// <param name="type">Room type of ROOM_TYPE</param>
        /// <param name="description">Description string</param>
        /// <returns>true if succeeded</returns>
        void UpdateRoomDescription(ROOM_TYPE type, string description)
        {

        }

        /// <summary>
        /// Get room ameneties for the room type
        /// </summary>
        /// <param name="type">Room type of ROOM_TYPE</param>
        /// <returns>Ameneties string</returns>
        string GetRoomAmeneties(ROOM_TYPE type)
        {
            return null;
        }

        /// <summary>
        /// Get room picture urls
        /// </summary>
        /// <param name="type">Room type of ROOM_TYPE</param>
        /// <returns>url list</returns>
        List<string> GetRoomPictureUrls(ROOM_TYPE type)
        {
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type">Room type of ROOM_TYPE</param>
        /// <param name="urls">Url List</param>
        /// <returns>true if succeeded</returns>
        bool UpdateRoomPictureUrls(ROOM_TYPE type, List<string> urls)
        {
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type">Room type of ROOM_TYPE</param>
        /// <param name="url">picture url</param>
        /// <returns>true if succeded</returns>
        bool InsertPictureUrl(ROOM_TYPE type, string url)
        {
            return false;
        }
    }
}
