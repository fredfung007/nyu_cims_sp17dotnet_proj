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
        /// <summary>
        /// list of RoomType that are available during given date
        /// </summary>
        IRoomRepository roomRepository;
        IReservationRepository reservationRepository;

        public RoomHandler()
        {
            roomRepository = new RoomRepository(new HotelDataModelContainer());
            reservationRepository = new ReservationRepository(new HotelDataModelContainer());
        }

        /// <summary>
        /// Return true if the RoomType is available during [start, end).
        /// </summary>
        /// <param name="r">RoomType instance</param>
        /// <param name="start">check-in date</param>
        /// <param name="end">check-out date</param>
        /// <returns></returns>
        private bool IsAvailable(RoomType r, DateTime start, DateTime end)
        {
            bool available = true;
            while (start.CompareTo(end) < 1)
            {
                available = available && 
                    (roomRepository.GetRoomTotalAmount(r) > roomRepository.GetRoomReservationAmount(r, start));
                if (!available)
                {
                    break;
                }
                start = start.AddDays(1);
            }
            return available;
        }

        /// <summary>
        /// Return a list of RoomType that is available during the the date [start, end).
        /// </summary>
        /// <param name="start">check-in date</param>
        /// <param name="end">check-out date</param>
        /// <returns>a list of RoomTypes that are available during the given date</returns>
        public List<ROOM_TYPE> CheckAvailableTypeForDuration(DateTime start, DateTime end)
        {
            List<ROOM_TYPE> roomList = new List<ROOM_TYPE>();
            foreach(RoomType r in roomRepository.getRoomTypes())
            {
                if(IsAvailable(r, start, end))
                {
                    roomList.Add(r.Type);
                }
            }
            return roomList;
        }

        /// <summary>
        /// Get the price list for a room type during [start, end).
        /// </summary>
        /// <param name="type">Room type of ROOM_TYPE</param>
        /// <param name="start">check-in date</param>
        /// <param name="end">check-out date</param>
        /// <returns>list for price</returns>
        public List<int> GetRoomPriceList(ROOM_TYPE type, DateTime start, DateTime end)
        {
            List<int> priceList = new List<int>();
            while(start.CompareTo(end) < 1)
            {
                priceList.Add(GetRoomPrice(type, start));
                start.AddDays(1);
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
            return (int)Math.Ceiling(roomRepository.getRoomType(type).BaseRate * rate);
        }

        /// <summary>
        /// Get current room availibility
        /// </summary>
        /// <param name="type"></param>
        /// <param name="date"></param>
        /// <returns>current available rooms</returns>
        public int GetCurrentRoomAvailability(ROOM_TYPE type, DateTime date)
        {
            RoomType room = roomRepository.getRoomType(type);
            return roomRepository.GetRoomTotalAmount(room) - roomRepository.GetRoomReservationAmount(room, date);
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
            IEnumerable<RoomType> types = roomRepository.getRoomTypes();
            foreach(RoomType room in types)
            {
                totalQuantity += roomRepository.GetRoomTotalAmount(room);
                totalOccupation += roomRepository.GetRoomReservationAmount(room, date);
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
            return roomRepository.GetRoomReservationAmount(roomRepository.getRoomType(type), date);
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
            return roomRepository.getRoomType(type).Inventory;
        }

        /// <summary>
        /// Get description of the room type
        /// </summary>
        /// <param name="type">Room type of ROOM_TYPE</param>
        /// <returns>description string</returns>
        string GetRoomDescription(ROOM_TYPE type)
        {
            return roomRepository.getRoomType(type).Description;
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
            return roomRepository.getRoomType(type).Ameneties;
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

        /// <summary>
        /// Update room inventory quantity. Will first validate the new quantity
        /// by chekcing the minimum occupancy of the specific room type: if the
        /// new quantity value is invalid, it will throw ArgumentOutOfRangeException
        /// </summary>
        /// <param name="room">room type</param>
        /// <param name="quantity">new value of inventory quantity</param>
        public void UpdateRoomInventory(ROOM_TYPE type, int quantity)
        {
            List<RoomOccupancy> roomOccupancies =
                new List<RoomOccupancy>(roomRepository.getRoomOccupanciesByRoomType(type));
            int minOccupancy = int.MaxValue;
            foreach (RoomOccupancy roomOccupancy in roomOccupancies)
            {
                minOccupancy = Math.Min(roomOccupancy.Occupancy, minOccupancy);
            }
            if (quantity > minOccupancy)
            {
                RoomType room = roomRepository.getRoomType(type);
                room.Inventory = quantity;
                roomRepository.UpdateRoom(room);
            }
            else
            {
                throw new ArgumentOutOfRangeException(
                    "new room inventory cannot be smaller than the occupied room amount");
            }
        }

        /// <summary>
        /// Check in a reservation on specific date by its confirmation number
        /// </summary>
        /// <param name="confirmationNumber">confirmation number of the date</param>
        /// <param name="date">check in date</param>
        public void CheckIn(Guid confirmationNumber, DateTime date)
        {
            Reservation reservation =
                reservationRepository.getReservation(confirmationNumber);
            if (reservation != null)
            {
                while(date.CompareTo(reservation.endDate) < 0)
                {
                    roomRepository.UpdateRoomUsage(reservation.RoomType, date, -1);
                }
            }
        }

        /// <summary>
        /// Check out a reservation on specific date by its confirmation number
        /// </summary>
        /// <param name="confirmationNumber">confirmation number of the reservation</param>
        /// <param name="date">check out date</param>
        public void CheckOut(Guid confirmationNumber, DateTime date)
        {
            Reservation reservation =
                reservationRepository.getReservation(confirmationNumber);
            if (reservation != null)
            {
                while(date.CompareTo(reservation.endDate) < 0)
                {
                    roomRepository.UpdateRoomUsage(reservation.RoomType, date, +1);
                }
            }
        }
    }
}
