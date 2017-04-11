using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Constants;

namespace BusinessLogic.Interfaces
{
    /// Interface for room inventory management, price query
    interface IRoom
    {
        /// <summary>
        /// Get the price list for a room type from start date to end state
        /// </summary>
        /// <param name="type">Room type of roomType</param>
        /// <param name="start">Start date of DateTime</param>
        /// <param name="end">End date of DateTime</param>
        /// <returns>list for price</returns>
        List<double> getRoomPriceList(roomType type, DateTime start, DateTime end);

        /// <summary>
        /// Get price of a specific room type at date
        /// </summary>
        /// <param name="type">Room type of roomType</param>
        /// <param name="date">Date for DateTime</param>
        /// <returns>room price</returns>
        double getRoomPrice(roomType type, DateTime date);

        /// <summary>
        ///  Get current room availibility
        /// </summary>
        /// <param name="type"></param>
        /// <param name="date"></param>
        /// <returns>current available rooms</returns>
        int getCurrentRoomAvailability(roomType type, DateTime date);

        /// <summary>
        /// Set room inventory
        /// </summary>
        /// <param name="type">Room type of roomType</param>
        /// <param name="amount">Room amount</param>
        /// <returns>true if succeeded</returns>
        bool setRoomInventory(roomType type, int amount);

        /// <summary>
        /// Get room inventory
        /// </summary>
        /// <param name="type">Room type of roomType</param>
        /// <returns>number of rooms</returns>
        int getRooomInventory(roomType type);

        /// <summary>
        /// Get description of the room type
        /// </summary>
        /// <param name="type">Room type of roomType</param>
        /// <returns>description string</returns>
        string getRoomDescription(roomType type);

        /// <summary>
        /// Update description of the room type
        /// </summary>
        /// <param name="type">Room type of roomType</param>
        /// <param name="description">Description string</param>
        /// <returns>true if succeeded</returns>
        bool updateRoomDescription(roomType type, string description);

        /// <summary>
        /// Get room ameneties for the room type
        /// </summary>
        /// <param name="type">Room type of roomType</param>
        /// <returns>Ameneties string</returns>
        string getRoomAmeneties(roomType type);

        /// <summary>
        /// Get room picture urls
        /// </summary>
        /// <param name="type">Room type of roomType</param>
        /// <returns>url list</returns>
        List<string> getRoomPictureUrls(roomType type);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type">Room type of roomType</param>
        /// <param name="urls">Url List</param>
        /// <returns>true if succeeded</returns>
        bool updateRoomPictureUrls(roomType type, List<string> urls);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type">Room type of roomType</param>
        /// <param name="url">picture url</param>
        /// <returns>true if succeded</returns>
        bool insertPictureUrl(roomType type, string url);
    }
}
