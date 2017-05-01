using DataAccessLayer.EF;
using DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            RoomRepository roomRepository = new RoomRepository(new CodeFirstHotelModel());
            roomRepository.InsertRoom(new DataAccessLayer.EF.RoomType {
                Id = Guid.NewGuid(),
                BaseRate = 100,
                Inventory = 20,
                Type = DataAccessLayer.Constants.ROOM_TYPE.DoubleBedRoom,
                Ameneties = "two queen beds, Wifi",
                Description = "Double Bed Room",
                ImageUrl = "http://www.topnotchresort.com/wp-content/uploads/2013/06/deluxe-double-room.jpg",
            });
            roomRepository.InsertRoom(new DataAccessLayer.EF.RoomType
            {
                Id = Guid.NewGuid(),
                BaseRate = 100,
                Inventory = 50,
                Type = DataAccessLayer.Constants.ROOM_TYPE.QueenRoom,
                Ameneties = "1 queen beds, Wifi",
                Description = "Queen Room",
                ImageUrl = "http://www.topnotchresort.com/wp-content/uploads/2013/06/deluxe-double-room.jpg",
            });
            roomRepository.InsertRoom(new DataAccessLayer.EF.RoomType
            {
                Id = Guid.NewGuid(),
                BaseRate = 120,
                Inventory = 30,
                Type = DataAccessLayer.Constants.ROOM_TYPE.KingBedRoom,
                Ameneties = "1 King bed, Wifi",
                Description = "King Bed Room",
                ImageUrl = "http://www.topnotchresort.com/wp-content/uploads/2013/06/deluxe-double-room.jpg",
            });
            roomRepository.InsertRoom(new DataAccessLayer.EF.RoomType
            {
                Id = Guid.NewGuid(),
                BaseRate = 200,
                Inventory = 10,
                Type = DataAccessLayer.Constants.ROOM_TYPE.Suite,
                Ameneties = "two king beds, Wifi",
                Description = "Suite",
                ImageUrl = "http://www.topnotchresort.com/wp-content/uploads/2013/06/deluxe-double-room.jpg",
            });
            roomRepository.Save();
        }
    }
}
