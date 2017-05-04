using BusinessLogic.Handlers;
using BusinessLogic.Helpers;
using DataAccessLayer.Constants;
using DataAccessLayer.EF;
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
            RoomType room = new RoomType
            {
                Id = Guid.NewGuid(),
                BaseRate = 300,
                Type = ROOM_TYPE.QueenRoom,
                Inventory = 30
            };

            TestDataGenerator generator = new TestDataGenerator();
            Tuple<List<AspNetUser>, List<Profile>> userInfo = generator.GenerateUsers(10);
            List<Reservation> reservations = generator.GenerateReservationsWithCheckInDate(userInfo.Item1, DateTime.Today, false);
            Console.Write("num reservations = " + reservations.Count + "\n\n");
            foreach(Reservation reservation in reservations)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(reservation.AspNetUser.Profile.FirstName).Append("\t");
                sb.Append(reservation.AspNetUser.Profile.LastName).Append("\n");
                sb.Append(reservation.AspNetUser.Profile.Email).Append("\n");
                sb.Append(reservation.StartDate).Append("\t");
                sb.Append(reservation.EndDate).Append("\n");
                foreach(DailyPrice price in reservation.DailyPrices)
                {
                    sb.Append(price.BillingPrice).Append("\t");
                }
                sb.Append("\n=======================================\n\n");
                Console.Write(sb.ToString());

            }
            Console.ReadKey();
        }
    }
}
