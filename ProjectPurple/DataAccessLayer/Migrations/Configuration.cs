namespace DataAccessLayer.Migrations
{
    using Constants;
    using EF;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DataAccessLayer.EF.CodeFirstHotelModel>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(DataAccessLayer.EF.CodeFirstHotelModel context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            RoomType room = new RoomType
            {
                Id = Guid.NewGuid(),
                BaseRate = 300,
                Type = ROOM_TYPE.QueenRoom,
                Inventory = 100
            };

            context.RoomTypes.AddOrUpdate(room);

            TestDataGenerator generator = new TestDataGenerator();

            Tuple<List<AspNetUser>, List<Profile>> users = generator.GenerateUsers(100);
            foreach(AspNetUser user in users.Item1)
            {
                context.AspNetUsers.AddOrUpdate(user);
            }
            foreach(Profile profile in users.Item2)
            {
                context.Profiles.AddOrUpdate(profile);
            }

            List<Reservation> reservations = generator.GenerateReservationsWithCheckInDate(
                room, users.Item1.GetRange(0, 25), DateTime.Today, false);
            reservations.Concat(generator.GenerateReservationsWithCheckOutDate(
                room, users.Item1.GetRange(25, 25), DateTime.Today.AddDays(7), false));
            reservations.Concat(generator.GenerateReservationsWithCheckInDate(
                room, users.Item1.GetRange(50, 25), DateTime.Today, true));
            reservations.Concat(generator.GenerateReservationsWithCheckOutDate(
                room, users.Item1.GetRange(75, 25), DateTime.Today.AddDays(7), true));

            foreach(Reservation reservation in reservations)
            {
                context.Reservations.AddOrUpdate(reservation);
                for(int i = 0;i < (reservation.EndDate - reservation.StartDate).Days;i++)
                {
                    var occupancyQuery = from o in context.RoomOccupancies
                                    where o.Date == reservation.StartDate.AddDays(i) && o.RoomType == room
                                    select o;
                    var occupancy = occupancyQuery.First();
                    if (occupancy == null)
                    {
                        occupancy = new RoomOccupancy
                        {
                            Id = Guid.NewGuid(),
                            Date = reservation.StartDate.AddDays(i),
                            Occupancy = 1,
                            RoomType = room,
                            RoomTypeId = room.Id
                        };
                    }
                    else
                    {
                        occupancy.Occupancy += 1;
                        context.Entry(occupancy).State = EntityState.Modified;
                    }
                }
            }

            context.SaveChanges();
        }
    }
}
