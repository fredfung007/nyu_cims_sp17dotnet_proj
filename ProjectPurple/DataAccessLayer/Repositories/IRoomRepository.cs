using System;
using System.Collections.Generic;
using DataAccessLayer.Constants;

namespace DataAccessLayer.Repositories
{
    public interface IRoomRepository : IDisposable
    {
        RoomType getRoomType(Guid Id);
        RoomType getRoomType(ROOM_TYPE type);
        IEnumerable<RoomType> getRoomTypes();
        void InsertRoom(RoomType room);
        void DeleteRoom(Guid Id);
        void UpdateRoom(RoomType room);
        
        // room inventory
        void UpdateRoomOccupancy(RoomOccupancy roomOccupancy);
        IEnumerable<RoomOccupancy> getRoomOccupanciesByRoomTypeAfterDate(ROOM_TYPE type, DateTime date);
        int getMaxRoomOccupanciesByRoomTypeAfterDate(ROOM_TYPE type, DateTime date)

        // room usage, if check in a room, call UpdateRoomUsage(roomType, 1).
        // if check out a room, call UpdateRoomUsage(roomType, -1);
        void UpdateRoomUsage(RoomType room, DateTime date, int quantity);

        // How many room of the type is reserved on some date
        int GetRoomReservationAmount(RoomType room, DateTime date);
        // The total amount of a roomtype, roomtype.inventory
        int GetRoomTotalAmount(RoomType room);
        void save();
    }
}
