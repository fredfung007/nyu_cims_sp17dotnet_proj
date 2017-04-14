using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace BusinessLogic.DAL
{
    interface IRoomRepository : IDisposable
    {
        RoomType getRoomType(Guid Id);
        IEnumerable<RoomType> getRoomTypes();
        void InsertRoom(RoomType room);
        void DeleteRoom(Guid Id);
        void UpdateRoom(RoomType room);
        
        // room inventory
        void UpdateRoomInventory(RoomType room, int quantity);

        void CheckIn(RoomType room, DateTime date);
        void CheckOut(RoomType room, DateTime date);

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
