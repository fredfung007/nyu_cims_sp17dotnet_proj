using System;
using System.Collections.Generic;
using DataAccessLayer.Constants;
using DataAccessLayer.EF;

namespace DataAccessLayer.Repositories
{
    public interface IRoomRepository : IDisposable
    {
        RoomType GetRoomType(ROOM_TYPE type);
        IEnumerable<RoomType> GetRoomTypes();
        void UpdateRoom(RoomType room);
        void UpdateRoomOccupancy(RoomOccupancy roomOccupancy);
        int GetMaxRoomOccupanciesByRoomTypeAfterDate(ROOM_TYPE type, DateTime date);

        // room usage, if check in a room, call UpdateRoomUsage(roomType, 1).
        // if check out a room, call UpdateRoomUsage(roomType, -1);
        void UpdateRoomOccupancy(ROOM_TYPE type, DateTime date, int quantity);

        // How many room of the type is reserved on some date
        int GetRoomOccupancyByDate(ROOM_TYPE type, DateTime date);

        // The total amount of a roomtype, roomtype.inventory
        int GetRoomTotalAmount(ROOM_TYPE type);

        void Save();
    }
}