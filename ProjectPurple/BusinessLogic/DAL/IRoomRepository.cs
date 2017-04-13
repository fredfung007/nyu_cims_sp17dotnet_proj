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
        //Room getReservation(Guid Id);
        //IEnumerable<Room> getReservations();
        //void InsertRoom(Room room);
        //void DeleteRoom(Guid Id);
        //void UpdateRoom(Room room);
        void save();
    }
}
