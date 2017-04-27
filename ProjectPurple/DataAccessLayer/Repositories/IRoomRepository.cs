using System;
using System.Collections.Generic;
using DataAccessLayer.EF;

namespace DataAccessLayer.Repositories
{
    public interface IRoomRepository : IDisposable
    {
        RoomType GetRoomType(Guid id);
        RoomType GetRoomType(Constants.RoomType type);
        IEnumerable<RoomType> GetRoomTypes();
        void InsertRoom(RoomType room);
        void DeleteRoom(Guid id);
        void UpdateRoom(RoomType room);

        // room inventory
        void UpdateRoomOccupancy(RoomOccupancy roomOccupancy);

        IEnumerable<RoomOccupancy> GetRoomOccupanciesByRoomTypeAfterDate(Constants.RoomType type, DateTime date);
        int GetMaxRoomOccupanciesByRoomTypeAfterDate(Constants.RoomType type, DateTime date);

        // room usage, if check in a room, call UpdateRoomUsage(roomType, 1).
        // if check out a room, call UpdateRoomUsage(roomType, -1);
        void UpdateRoomUsage(RoomType room, DateTime date, int quantity);

        // How many room of the type is reserved on some date
        int GetRoomReservationAmount(RoomType room, DateTime date);

        // The total amount of a roomtype, roomtype.inventory
        int GetRoomTotalAmount(RoomType room);

        void Save();
    }
}