using DataAccessLayer.Constructor;

namespace DataAccessLayer.Migrations
{
    using Constants;
    using EF;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DataAccessLayer.EF.HotelModelContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(HotelModelContext context)
        {
            RoomType room = new RoomType
            {
                Id = Guid.NewGuid(),
                BaseRate = 300,
                Type = ROOM_TYPE.QueenRoom,
                Inventory = 100,
                Ameneties = "AMENETIES.",
                Description = "DESCRIPTION TEXT",
                ImageUrl = @"https://ritzcarlton-h.assetsadobe.com/is/image/content/dam/the-ritz-carlton/hotels/usa-and-canada/new-york/new-york-central-park/guest-rooms/supporting-images/RCNYCPK_00092_conversion.png?$XlargeViewport100pct$"
            };

            context.RoomTypes.AddOrUpdate(room);

            room = new RoomType
            {
                Id = Guid.NewGuid(),
                BaseRate = 500,
                Type = ROOM_TYPE.KingBedRoom,
                Inventory = 20,
                Ameneties = "AMENETIES.",
                Description = "DESCRIPTION TEXT",
                ImageUrl = @"https://ritzcarlton-h.assetsadobe.com/is/image/content/dam/the-ritz-carlton/hotels/usa-and-canada/new-york/new-york-central-park/guest-rooms/RCNYCPK_00102_conversion.png?$XlargeViewport100pct$"
            };
            context.RoomTypes.AddOrUpdate(room);
            context.SaveChanges();
        }
    }
}
