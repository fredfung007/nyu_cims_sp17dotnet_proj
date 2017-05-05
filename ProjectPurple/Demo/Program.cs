using BusinessLogic.Handlers;
using DataAccessLayer.Constants;
using DataAccessLayer.EF;
using DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            //RoomType room = new RoomType
            //{
            //    BaseRate = 300,
            //    Type = ROOM_TYPE.QueenRoom,
            //    Inventory = 50,
            //    Ameneties = "am",
            //    Description = "desc",
            //    ImageUrl = "img"
            //};
            //RoomRepository roomRepo = new RoomRepository(new HotelModelContext());
            //roomRepo.InsertRoom(room);
            //roomRepo.Save();

            ReservationHandler reservationHandler = new ReservationHandler();
            RoomHandler roomHandler = new RoomHandler();
            Guid confirmation = reservationHandler.MakeReservation(
                "root",
                ROOM_TYPE.QueenRoom,
                DateTime.Today,
                DateTime.Today.AddDays(5),
                new List<Guest> { new Guest
                {
                    FirstName = "oot",
                    LastName = "R",
                } },
                roomHandler.GetRoomPriceList(ROOM_TYPE.QueenRoom, DateTime.Today, DateTime.Today.AddDays(5))
                );
            Console.WriteLine(confirmation);
            Console.ReadKey();
        }
    }
}
