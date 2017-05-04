using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer;
using DataAccessLayer.Constants;
using DataAccessLayer.EF;
using DataAccessLayer.Repositories;
using System.Threading.Tasks;

namespace BusinessLogic.Handlers
{
    /// <summary>
    /// A handler class for room inventory management, price query, availability check
    /// </summary>
    public class RoomHandler
    {
        /// <summary>
        /// list of RoomType that are available during given date
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
            List<ROOM_TYPE> types = new List<ROOM_TYPE>();
            foreach (RoomType room in _roomRepository.GetRoomTypes())
            {
                types.Add(room.Type);
            }
            return types;
        }

        /// <summary>
        /// Return true if the RoomType is available during [checkIn, checkOut).
        /// </summary>
        /// <param name="type">RoomType instance</param>
        /// <param name="checkIn">check-in date</param>
        /// <param name="checkOut">check-out date</param>
        /// <returns></returns>
        public bool IsAvailable(ROOM_TYPE type, DateTime checkIn, DateTime checkOut)
        {
            return IsRoomAvailableForNRoom(type, checkIn, checkOut, 1);
        }

        /// <summary>
        /// Return true if the roomType is available for RoomAmount rooms during [CheckIn, checkOut).
        /// </summary>
        /// <param name="type"></param>
        /// <param name="checkIn"></param>
        /// <param name="checkOut"></param>
        /// <param name="RoomAmount"></param>
        /// <returns></returns>
        private bool IsRoomAvailableForNRoom(ROOM_TYPE type, DateTime checkIn, DateTime checkOut, int RoomAmount)
        {
            DateTime checkDate = checkIn;

            while (checkDate.CompareTo(checkOut) < 0)
            {
                if (GetCurrentRoomAvailability(type, checkDate) < RoomAmount)
                {
                    return false;
                }
                checkDate = checkDate.AddDays(1);
            }
            return true;
        }

        /// <summary>
        /// Return a list of RoomType that is available during the the date [checkIn, checkOut).
        /// </summary>
        /// <param name="checkIn">check-in date</param>
        /// <param name="checkOut">check-out date</param>
        /// <returns>a list of RoomTypes that are available during the given date</returns>
        /// <exception cref="ArgumentException">if check-in date is later than the check-out date</exception>
        public List<ROOM_TYPE> CheckAvailableTypeForDuration(DateTime checkIn, DateTime checkOut)
        {
            if (checkIn == null || checkOut == null)
            {
                throw new ArgumentException("null check-in date or check-out date");
            }
            else if (checkIn >= checkOut)
            {
                throw new ArgumentException("check-in date later then check-out date");
            }
            return (from room in _roomRepository.GetRoomTypes() where IsAvailable(room.Type, checkIn, checkOut) select room.Type).ToList();
        }

        public async Task<List<ROOM_TYPE>> CheckAvailableTypeForDurationAsync(DateTime checkIn, DateTime checkOut)
        {
            if (checkIn == null || checkOut == null)
            {
                throw new ArgumentException("null check-in date or check-out date");
            }
            else if (checkIn >= checkOut)
            {
                throw new ArgumentException("check-in date later then check-out date");
            }
            return (from room in _roomRepository.GetRoomTypes() where IsAvailable(room.Type, checkIn, checkOut) select room.Type).ToList();
        }

        /// <summary>
        /// Return a list of roomType that is available during the date [checkIn, checkOut) for RoomAmount of rooms. For multiple room selection extension
        /// </summary>
        /// <param name="checkIn"></param>
        /// <param name="checkOut"></param>
        /// <returns></returns>
        public List<ROOM_TYPE> CheckAvailableTypeForDurationForNRoom(DateTime checkIn, DateTime checkOut, int roomAmount)
        {
            if (checkIn == null || checkOut == null)
            {
                throw new ArgumentException("null check-in date or check-out date");
            }
            else if (checkIn >= checkOut)
            {
                throw new ArgumentException("check-in date later then check-out date");
            }
            return (from room in _roomRepository.GetRoomTypes() where IsRoomAvailableForNRoom(room.Type, checkIn, checkOut, roomAmount) select room.Type).ToList();
        }

        /// <summary>
        /// Get the price list for a room type during [checkIn, checkOut).
        /// </summary>
        /// <param name="type">Room type of ROOM_TYPE</param>
        /// <param name="checkIn">check-in date</param>
        /// <param name="checkOut">check-out date</param>
        /// <returns>list for price</returns>
        public List<int> GetRoomPriceList(ROOM_TYPE type, DateTime checkIn, DateTime checkOut)
        {
            if (checkIn == null || checkOut == null)
            {
                throw new ArgumentException("null check-in date or check-out date");
            }
            else if (checkIn >= checkOut)
            {
                throw new ArgumentException("check-in date later then check-out date");
            }
            List<int> priceList = new List<int>();
            DateTime checkDate = checkIn;
            while(checkDate.CompareTo(checkOut) < 0)
            {
                priceList.Add(GetRoomPrice(type, checkDate));
                checkDate = checkDate.AddDays(1);
            }
            return priceList;
        }

        public async Task<List<int>> GetRoomPriceListAsync(ROOM_TYPE type, DateTime checkIn, DateTime checkOut)
        {
            if (checkIn == null || checkOut == null)
            {
                throw new ArgumentException("null check-in date or check-out date");
            }
            else if (checkIn >= checkOut)
            {
                throw new ArgumentException("check-in date later then check-out date");
            }
            List<int> priceList = new List<int>();
            DateTime checkDate = checkIn;
            while (checkDate.CompareTo(checkOut) < 0)
            {
                priceList.Add(GetRoomPrice(type, checkDate));
                checkDate = checkDate.AddDays(1);
            }
            return priceList;
        }

        /// <summary>
        /// Get price of a specific room type at date give.
        /// Price on specific day = base price * (1 + occupation rate), ceiling if has decimals.
        /// </summary>
        /// <param name="type">Room type of roomType</param>
        /// <param name="date">Date for DateTime</param>
        /// <returns>room price</returns>
        private int GetRoomPrice(ROOM_TYPE type, DateTime date)
        {
            // compute price multipler
            double rate = 1.0 + GetHotelOccupancy(date);
            return (int) Math.Ceiling(_roomRepository.GetRoomType(type).BaseRate * rate);
        }

        /// <summary>
        /// Get current room availibility
        /// </summary>
        /// <param name="type"></param>
        /// <param name="date"></param>
        /// <returns>current available rooms</returns>
        private int GetCurrentRoomAvailability(ROOM_TYPE type, DateTime date)
        {
            DataAccessLayer.EF.RoomType room = _roomRepository.GetRoomType(type);
            return _roomRepository.GetRoomTotalAmount(room) - _roomRepository.GetRoomReservationAmount(room, date);
        }

        /// <summary>
        /// Get occupency percentage of a room on date
        /// </summary>
        /// <param name="date"></param>
        /// <returns>occupency percentage</returns>
        private double GetHotelOccupancy(DateTime date)
        {
            int totalQuantity = 0;
            int totalOccupation = 0;
            IEnumerable<DataAccessLayer.EF.RoomType> types = _roomRepository.GetRoomTypes();

            foreach (DataAccessLayer.EF.RoomType room in types)
            {
                totalQuantity += _roomRepository.GetRoomTotalAmount(room);
                totalOccupation += _roomRepository.GetRoomReservationAmount(room, date);
            }
            return totalOccupation * 1.0 / totalQuantity;
        }

        /// <summary>
        /// Get booked room on a sepcifiic date
        /// </summary>
        /// <param name="type">room type of ROOM_TYPE</param>
        /// <param name="date">date of DateTime</param>
        /// <returns>booked room amount</returns>
        public int GetBookedRoomOnDate(ROOM_TYPE type, DateTime date)
        {
            return _roomRepository.GetRoomReservationAmount(_roomRepository.GetRoomType(type), date);
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
        /// Get room inventory
        /// </summary>
        /// <param name="type">Room type of ROOM_TYPE</param>
        /// <returns>number of rooms</returns>
        public int GetRoomInventory(ROOM_TYPE type)
        {
            return _roomRepository.GetRoomType(type).Inventory;
        }

        /// <summary>
        /// Get description of the room type
        /// </summary>
        /// <param name="type">Room type of ROOM_TYPE</param>
        /// <returns>description string</returns>
        public string GetRoomDescription(ROOM_TYPE type)
        {
            return _roomRepository.GetRoomType(type).Description;
        }

        /// <summary>
        /// Update description of the room type
        /// </summary>
        /// <param name="type">Room type of ROOM_TYPE</param>
        /// <param name="description">Description string</param>
        /// <returns>true if succeeded</returns>
        public void UpdateRoomDescription(ROOM_TYPE type, string description)
        {
            DataAccessLayer.EF.RoomType room = _roomRepository.GetRoomType(type);
            room.Description = description;
            _roomRepository.UpdateRoom(room);
            _roomRepository.Save();
        }

        /// <summary>
        /// Get room ameneties for the room type
        /// </summary>
        /// <param name="type">Room type of ROOM_TYPE</param>
        /// <returns>Ameneties string</returns>
        public string GetRoomAmeneties(ROOM_TYPE type)
        {
            return _roomRepository.GetRoomType(type).Ameneties;
        }

        /// <summary>
        /// Get room picture urls
        /// </summary>
        /// <param name="type">Room type of ROOM_TYPE</param>
        /// <returns>url list</returns>
        public string GetRoomPictureUrls(ROOM_TYPE type)
        {
            //TODO return a url or a list?
            return _roomRepository.GetRoomType(type).ImageUrl;
        }

        /// <summary>
        /// 
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
        /// 
        /// </summary>
        /// <param name="type">Room type of ROOM_TYPE</param>
        /// <param name="url">picture url</param>
        /// <returns>true if succeded</returns>
        public bool InsertPictureUrl(RoomType type, string url)
        {
            return false;
        }

        /// <summary>
        /// Update room inventory quantity. Will first validate the new quantity
        /// by chekcing the minimum occupancy of the specific room type: if the
        /// new quantity value is invalid, it will throw ArgumentOutOfRangeException
        /// </summary>
        /// <param name="type">room type</param>
        /// <param name="quantity">new value of inventory quantity</param>
        public void UpdateRoomInventory(ROOM_TYPE type, int quantity)
        {
            DataAccessLayer.EF.RoomType room = _roomRepository.GetRoomType(type);
            int currentQuantity = _roomRepository.GetRoomTotalAmount(room);

            if (quantity < currentQuantity)
            {
                int maxOccupancy = _roomRepository.GetMaxRoomOccupanciesByRoomTypeAfterDate(type, DateTime.Today);
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
