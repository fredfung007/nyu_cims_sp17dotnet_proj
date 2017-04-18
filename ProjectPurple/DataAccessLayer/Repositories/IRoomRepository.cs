using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
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
        // TODO
        void UpdateRoomInventory(Constants.ROOM_TYPE type, int quantity);

        void CheckIn(RoomType room, DateTime date);
        void CheckOut(RoomType room, DateTime date);

        // room usage, if check in a room, call UpdateRoomUsage(roomType, 1).
        // if check out a room, call UpdateRoomUsage(roomType, -1);
        void UpdateRoomUsage(RoomType room, DateTime date, int quantity);

        // How many room of the type is reserved on some date
        int GetRoomReservationAmount(ROOM_TYPE type, DateTime date);
        // The total amount of a roomtype, roomtype.inventory
        int GetRoomTotalAmount(ROOM_TYPE type);
        int GetBaseRate(ROOM_TYPE type);
        void save();
    }
}
