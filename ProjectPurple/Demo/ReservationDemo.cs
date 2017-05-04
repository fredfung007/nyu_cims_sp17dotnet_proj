using System;
using BusinessLogic.Handlers;
using DataAccessLayer.Constants;
using System.Collections.Generic;
using System.Text;

namespace Demo
{
    class ReservationDemo
    {
        //static void Main(string[] args)
        //{
        //    ReservationDemo demo = new ReservationDemo();
        //    Console.WriteLine(demo.QueryAvailableRooms(new RoomHandler(), DateTime.Today, DateTime.Today.AddDays(5)));
        //    Console.ReadKey();
        //}

        private string QueryAvailableRooms(RoomHandler roomHandler, DateTime checkIn, DateTime checkOut)
        {
            List<ROOM_TYPE> types = roomHandler.CheckAvailableTypeForDuration(checkIn, checkOut);
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("Available Rooms:\n");
            foreach (ROOM_TYPE type in types)
            {
                stringBuilder.AppendFormat("{0}", type.ToString())
                             .Append("\tAverage Price Per Day:\t$")
                             .Append(roomHandler.GetAveragePrice(type, checkIn, checkOut)).Append("\n")
                             .Append("Description:").Append("\n")
                             .Append(roomHandler.GetRoomDescription(type)).Append("\n")
                             .Append("==============\n\n");
            }
            return stringBuilder.ToString();
        }
    }
}
