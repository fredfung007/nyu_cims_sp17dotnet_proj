﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Constants;
using DataAccessLayer.EF;
using DataAccessLayer.Repositories;

namespace BusinessLogic.Handlers
{
    /// <summary>
    ///     A handler class for room inventory management, price query, availability check
    /// </summary>
    public class RoomHandler
    {
        /// <summary>
        ///     list of RoomType that are available during given date
        /// </summary>
        private readonly IRoomRepository _roomRepository;

        public RoomHandler()
        {
            _roomRepository = new RoomRepository(new HotelModelContext());
        }

        public RoomHandler(IRoomRepository roomRepo)
        {
            _roomRepository = roomRepo;
        }

        public List<ROOM_TYPE> GetRoomTypes()
        {
            var types = new List<ROOM_TYPE>();
            foreach (RoomType room in _roomRepository.GetRoomTypes())
            {
                types.Add(room.Type);
            }

            return types;
        }

        /// <summary>
        ///     Return true if the RoomType is available during [startDateTime, endDateTime).
        /// </summary>
        /// <param name="type">RoomType instance</param>
        /// <param name="startDateTime">check-in date</param>
        /// <param name="endDateTime">check-out date</param>
        /// <returns></returns>
        public bool IsAvailable(ROOM_TYPE type, DateTime startDateTime, DateTime endDateTime)
        {
            return IsRoomAvailableForNRoom(type, startDateTime, endDateTime, 1);
        }

        /// <summary>
        ///     Return true if the roomType is available for roomAmount rooms during [CheckIn, endDateTime).
        /// </summary>
        /// <param name="type"></param>
        /// <param name="startDateTime"></param>
        /// <param name="endDateTime"></param>
        /// <param name="roomAmount"></param>
        /// <returns></returns>
        private bool IsRoomAvailableForNRoom(ROOM_TYPE type, DateTime startDateTime, DateTime endDateTime,
            int roomAmount)
        {
            DateTime checkDate = startDateTime.Date;

            while (checkDate.CompareTo(endDateTime.Date) < 0)
            {
                if (GetCurrentRoomAvailability(type, checkDate) < roomAmount)
                {
                    return false;
                }

                checkDate = checkDate.AddDays(1);
            }
            return true;
        }

        /// <summary>
        ///     Return a list of RoomType that is available during the the date [startDateTime, endDateTime).
        /// </summary>
        /// <param name="startDateTime">check-in date</param>
        /// <param name="endDateTime">check-out date</param>
        /// <returns>a list of RoomTypes that are available during the given date</returns>
        /// <exception cref="ArgumentException">if check-in date is later than the check-out date</exception>
        public List<ROOM_TYPE> CheckAvailableTypeForDuration(DateTime startDateTime, DateTime endDateTime)
        {
            if (startDateTime == null || endDateTime == null)
            {
                throw new ArgumentException("null check-in date or check-out date");
            }

            if (startDateTime >= endDateTime)
            {
                throw new ArgumentException("check-in date later then check-out date");
            }

            return (from room in _roomRepository.GetRoomTypes()
                where IsAvailable(room.Type, startDateTime, endDateTime)
                select room.Type).ToList();
        }

        public async Task<List<ROOM_TYPE>> CheckAvailableTypeForDurationAsync(DateTime startDateTime,
            DateTime endDateTime)
        {
            if (startDateTime == null || endDateTime == null)
            {
                throw new ArgumentException("null check-in date or check-out date");
            }

            if (startDateTime >= endDateTime)
            {
                throw new ArgumentException("check-in date later then check-out date");
            }

            return (from room in _roomRepository.GetRoomTypes()
                where IsAvailable(room.Type, startDateTime, endDateTime)
                select room.Type).ToList();
        }

        /// <summary>
        ///     Return a list of roomType that is available during the date [startDateTime, endDateTime) for roomAmount of rooms.
        ///     For multiple room selection extension
        /// </summary>
        /// <param name="checkIn"></param>
        /// <param name="checkOut"></param>
        /// <returns></returns>
        public List<ROOM_TYPE> CheckAvailableTypeForDurationForNRoom(DateTime checkIn, DateTime checkOut,
            int roomAmount)
        {
            if (checkIn == null || checkOut == null)
            {
                throw new ArgumentException("null check-in date or check-out date");
            }

            if (checkIn >= checkOut)
            {
                throw new ArgumentException("check-in date later then check-out date");
            }

            return (from room in _roomRepository.GetRoomTypes()
                where IsRoomAvailableForNRoom(room.Type, checkIn, checkOut, roomAmount)
                select room.Type).ToList();
        }

        /// <summary>
        ///     Get the price list for a room type during [startDateTime, endDateTime).
        /// </summary>
        /// <param name="type">Room type of ROOM_TYPE</param>
        /// <param name="startDateTime">check-in date</param>
        /// <param name="endDateTime">check-out date</param>
        /// <returns>list for price</returns>
        public List<int> GetRoomPriceList(ROOM_TYPE type, DateTime startDateTime, DateTime endDateTime)
        {
            if (startDateTime == null || endDateTime == null)
            {
                throw new ArgumentException("null check-in date or check-out date");
            }

            if (startDateTime >= endDateTime)
            {
                throw new ArgumentException("check-in date later then check-out date");
            }

            var priceList = new List<int>();
            DateTime checkDate = startDateTime.Date;
            int baseRate = _roomRepository.GetRoomType(type).BaseRate;
            while (checkDate.CompareTo(endDateTime.Date) < 0)
            {
                priceList.Add(GetRoomPrice(type, baseRate, checkDate));
                checkDate = checkDate.AddDays(1);
            }
            return priceList;
        }

        public async Task<List<int>> GetRoomPriceListAsync(ROOM_TYPE type, DateTime startDateTime, DateTime endDateTime)
        {
            if (startDateTime == null || endDateTime == null)
            {
                throw new ArgumentException("null check-in date or check-out date");
            }

            if (startDateTime >= endDateTime)
            {
                throw new ArgumentException("check-in date later then check-out date");
            }

            var priceList = new List<int>();
            DateTime checkDate = startDateTime.Date;
            int baseRate = _roomRepository.GetRoomType(type).BaseRate;
            while (checkDate.CompareTo(endDateTime.Date) < 0)
            {
                priceList.Add(GetRoomPrice(type, baseRate, checkDate));
                checkDate = checkDate.AddDays(1);
            }
            return priceList;
        }

        /// <summary>
        ///     Get price of a specific room type at date give.
        ///     Price on specific day = base price * (1 + occupation rate), ceiling if has decimals.
        /// </summary>
        /// <param name="baseRate">base price of the room type</param>
        /// <param name="type">Room type</param>
        /// <param name="date">Date for DateTime</param>
        /// <returns>room price</returns>
        private int GetRoomPrice(ROOM_TYPE type, int baseRate, DateTime date)
        {
            // compute price multipler
            var rate = 1.0 + GetRoomOccupancyRate(type, date);
            return (int) Math.Ceiling(baseRate * rate);
        }

        /// <summary>
        ///     Get current room availibility
        /// </summary>
        /// <param name="type"></param>
        /// <param name="date"></param>
        /// <returns>current available rooms</returns>
        private int GetCurrentRoomAvailability(ROOM_TYPE type, DateTime date)
        {
            RoomType room = _roomRepository.GetRoomType(type);
            return _roomRepository.GetRoomTotalAmount(room.Type) -
                   _roomRepository.GetRoomOccupancyByDate(room.Type, date);
        }

        /// <summary>
        ///     Get occupency percentage of a room on date
        /// </summary>
        /// <param name="date"></param>
        /// <returns>occupency percentage</returns>
        public double GetHotelOccupancyRate(DateTime date)
        {
            var totalQuantity = 0;
            var totalOccupation = 0;
            var types = _roomRepository.GetRoomTypes();

            foreach (RoomType room in types)
            {
                totalQuantity += _roomRepository.GetRoomTotalAmount(room.Type);
                totalOccupation += _roomRepository.GetRoomOccupancyByDate(room.Type, date);
            }
            return totalOccupation * 1.0 / totalQuantity;
        }

        public double GetRoomOccupancyRate(ROOM_TYPE type, DateTime date)
        {
            var totalQuantity = _roomRepository.GetRoomTotalAmount(type);
            var totalOccupation = _roomRepository.GetRoomOccupancyByDate(type, date);

            return totalOccupation * 1.0 / totalQuantity;
        }

        /// <summary>
        ///     Get booked room on a sepcifiic date
        /// </summary>
        /// <param name="type">room type of ROOM_TYPE</param>
        /// <param name="date">date of DateTime</param>
        /// <returns>booked room amount</returns>
        public int GetBookedRoomOnDate(ROOM_TYPE type, DateTime date)
        {
            return _roomRepository.GetRoomOccupancyByDate(type, date);
        }

        // obsolete. duplicated with UpdateRoomInventory()
        ///// <summary>
        ///// Set room inventory
        ///// </summary>
        ///// <param name="type">Room type of ROOM_TYPE</param>
        ///// <param name="amount">Room amount</param>
        ///// <returns>true if succeeded</returns>
        //void SetRoomInventory(ROOM_TYPE type, int amount)
        //{
        //}

        /// <summary>
        ///     Get room inventory
        /// </summary>
        /// <param name="type">Room type of ROOM_TYPE</param>
        /// <returns>number of rooms</returns>
        public int GetRoomInventory(ROOM_TYPE type)
        {
            return _roomRepository.GetRoomType(type).Inventory;
        }

        /// <summary>
        ///     Get description of the room type
        /// </summary>
        /// <param name="type">Room type of ROOM_TYPE</param>
        /// <returns>description string</returns>
        public string GetRoomDescription(ROOM_TYPE type)
        {
            return _roomRepository.GetRoomType(type).Description;
        }

        /// <summary>
        ///     Update description of the room type
        /// </summary>
        /// <param name="type">Room type of ROOM_TYPE</param>
        /// <param name="description">Description string</param>
        /// <returns>true if succeeded</returns>
        public void UpdateRoomDescription(ROOM_TYPE type, string description)
        {
            RoomType room = _roomRepository.GetRoomType(type);
            room.Description = description;
            _roomRepository.UpdateRoom(room);
            _roomRepository.Save();
        }

        /// <summary>
        ///     Get room ameneties for the room type
        /// </summary>
        /// <param name="type">Room type of ROOM_TYPE</param>
        /// <returns>Ameneties string</returns>
        public string GetRoomAmeneties(ROOM_TYPE type)
        {
            return _roomRepository.GetRoomType(type).Ameneties;
        }

        // TODO
        public string GetRoomTypeName(ROOM_TYPE type)
        {
            var idx = (int) type;
            if (idx < 0 || idx >= NameString.ROOM_TYPE_NAME.Count())
            {
                return "Invalid Room Type";
            }

            return NameString.ROOM_TYPE_NAME[idx];
        }

        /// <summary>
        ///     Get room picture urls
        /// </summary>
        /// <param name="type">Room type of ROOM_TYPE</param>
        /// <returns>url list</returns>
        public string GetRoomPictureUrls(ROOM_TYPE type)
        {
            //TODO return a url or a list?
            return _roomRepository.GetRoomType(type).ImageUrl;
        }

        /// <summary>
        /// </summary>
        /// <param name="type">Room type of ROOM_TYPE</param>
        /// <param name="urls">Url List</param>
        /// <returns>true if succeeded</returns>
        public bool UpdateRoomPictureUrls(RoomType type, List<string> urls)
        {
            // TODO
            return false;
        }

        /// <summary>
        /// </summary>
        /// <param name="type">Room type of ROOM_TYPE</param>
        /// <param name="url">picture url</param>
        /// <returns>true if succeded</returns>
        public bool InsertPictureUrl(RoomType type, string url)
        {
            return false;
        }

        /// <summary>
        ///     Update room inventory quantity. Will first validate the new quantity
        ///     by chekcing the minimum occupancy of the specific room type: if the
        ///     new quantity value is invalid, it will throw ArgumentOutOfRangeException
        /// </summary>
        /// <param name="type">room type</param>
        /// <param name="quantity">new value of inventory quantity</param>
        public void UpdateRoomInventory(ROOM_TYPE type, int quantity)
        {
            RoomType room = _roomRepository.GetRoomType(type);
            var currentQuantity = _roomRepository.GetRoomTotalAmount(type);

            if (quantity < currentQuantity)
            {
                var maxOccupancy =
                    _roomRepository.GetMaxRoomOccupanciesByRoomTypeAfterDate(type, DateTimeHandler.GetCurrentDate());
                if (maxOccupancy > quantity)
                {
                    throw new ArgumentOutOfRangeException(
                        "new room inventory cannot be smaller than the occupied room amount");
                }
            }

            room.Inventory = quantity;
            _roomRepository.UpdateRoom(room);
            _roomRepository.Save();
        }

        public async Task<int> GetAveragePriceAsync(ROOM_TYPE type, DateTime checkIn, DateTime checkOut)
        {
            var priceList = await GetRoomPriceListAsync(type, checkIn, checkOut);
            var total = priceList.Sum();
            return total / (checkOut - checkIn).Days;
        }

        public int GetAveragePrice(ROOM_TYPE type, DateTime checkIn, DateTime checkOut)
        {
            var total = GetRoomPriceList(type, checkIn, checkOut).Sum();
            return total / (checkOut - checkIn).Days;
        }
    }
}