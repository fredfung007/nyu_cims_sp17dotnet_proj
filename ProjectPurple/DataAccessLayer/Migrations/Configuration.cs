namespace DataAccessLayer.Migrations
{
    using Constants;
    using EF;
    using System;
    using System.Data.Entity.Migrations;

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
                Id = new Guid("0805a77d-4952-44a3-9e32-606a4f418e99"),
                BaseRate = 200,
                Type = ROOM_TYPE.QueenRoom,
                Inventory = 30,
                Ameneties = @"Room Features

Air-conditioned
Non-smoking
Connecting rooms are not available
Soundproof windows
In-room safe that can accommodate a laptop
Entertainment

TV features: remote control, 27in/69cm, and flat screen
DVD player with movies for rental
Premium movie channels
Cable/satellite
CNN, ESPN, and HBO
Movies, pay-per-view
Radio
Stereo
CD Stereo
Internet Access

High-Speed Internet Access: Wireless, Wired for $14.95 USD/day",
                Description = @"QUIET COURTYARD VIEW

Comfortable spacious accommodations with a quiet courtyard view
LUXURIOUS ROOM AMENITIES

400-thread-count fine linen
Choice of seven types of pillows for optimal sleeping experience
SPACIOUS MARBLE BATH

Luxurious bath with soaking tub and separate stall shower",

                ImageUrl = @"https://ritzcarlton-h.assetsadobe.com/is/image/content/dam/the-ritz-carlton/hotels/usa-and-canada/new-york/new-york-central-park/guest-rooms/supporting-images/RCNYCPK_00092_conversion.png?$XlargeViewport100pct$"
            };
            context.RoomTypes.AddOrUpdate(room);

            room = new RoomType
            {
                Id = new Guid("7b7cf8fe-d455-450b-80c9-e37f68604570"),
                BaseRate = 350,
                Type = ROOM_TYPE.KingBedRoom,
                Inventory = 20,
                Ameneties = @"Internet Access

Wired and wireless internet access available
Connecting Room

This room adjoins to The Park View Suites and connects to The Premiere Suites
Accessibility

This room type offers mobility accessible rooms
This room type offers accessible rooms with roll in showers
Accessible City View rooms must be requested ahead of time
All accessible City View rooms do not have separate bathtub and shower features",
                Description = @"CENTRAL PARK VIEW

Comfortable accommodations with views of Central Park from floors 3 through 9
LUXURIOUS ROOM AMENITIES

400 - thread count fine linen
Exclusive Asprey amenities
27 in High Definition Flat Screen Television
SPACIOUS MARBLE BATHROOM

Luxurious bath with soaking tub and separate stall shower",
                ImageUrl = @"https://ritzcarlton-h.assetsadobe.com/is/image/content/dam/the-ritz-carlton/hotels/usa-and-canada/new-york/new-york-central-park/guest-rooms/RCNYCPK_00102_conversion.png?$XlargeViewport100pct$"
            };
            context.RoomTypes.AddOrUpdate(room);

            room = new RoomType
            {
                Id = new Guid("dd1b42a6-56db-4474-972c-a40607f7398c"),
                BaseRate = 150,
                Type = ROOM_TYPE.DoubleBedRoom,
                Inventory = 50,
                Ameneties = @"Entertainment

Flat Panel HD television with DVD player
Cable/satellite with premium movie channels
CD Stereo
Internet Access

High-Speed Internet Access: Wireless, Wired for $14.95 USD/day
ACCESSIBILITY

This room type does not offer accessible rooms with roll in showers
This room type offers hearing accessible rooms with visual alarms and visual notification devices for door and phone",
                Description = @"SUPERB VIEWS

Exciting views of New York City and partial views of Central Park
LUXURIOUS ROOM AMENITIES

400-thread count fine linen 
Exclusive Asprey amenities 
DVD player and LG High Definition Flat Screen Television
SPACIOUS MARBLE BATH

Luxurious bath with soaking tub and separate stall shower",
                ImageUrl = @"https://ritzcarlton-h.assetsadobe.com/is/image/content/dam/the-ritz-carlton/hotels/usa-and-canada/new-york/new-york-central-park/guest-rooms/RCNYCPK_00102_conversion.png?$XlargeViewport100pct$"
            };
            context.RoomTypes.AddOrUpdate(room);

            room = new RoomType
            {
                Id = new Guid("328c8536-c80c-4267-ad9e-a41de9a092b4"),
                BaseRate = 900,
                Type = ROOM_TYPE.Suite,
                Inventory = 10,
                Ameneties = @"Entertainment

Four Flat Panel HD television with DVD player
Cable/satellite with premium movie channels
Bang & Olufsen stereo system
Internet Access

High-Speed Internet Access: Wireless, Wired
Services and Amenities

Exclusive Club Lounge access
Complimentary bottled water daily
Day and evening turndown service
Mini bar
24-hour in-suite dining available
ACCESSIBILITY

This room type does not offer accessible rooms with roll in showers.
This room type offers hearing accessible rooms with visual alarms and visual notification devices for door and phone.",
                Description = @"AN ELEVATED SUITE EXPERIENCE

Our largest suite featuring 2 king bedrooms and 2.5 bathrooms
Panoramic views of Central Park from the 22nd floor
Complimentary Club Lounge access  
EXPANSIVE LIVING QUARTERS

Separate living, dining and working areas 
Dining room with seating for eight guests
Large foyer and living area, ideal for entertaining
MAGNIFICENT MARBLE BATHS

Two full marble bathrooms with separate tub and shower
Additional half bath by living room
Two 12-inch flat screen televisions in bathroom",
                ImageUrl = @"https://ritzcarlton-h.assetsadobe.com/is/image/content/dam/the-ritz-carlton/hotels/usa-and-canada/new-york/new-york-central-park/assetmigration/png-files/RCNYCPK_00166_conversion1.png?$XlargeViewport100pct$"
            };
            context.RoomTypes.AddOrUpdate(room);

            context.SaveChanges();
        }
    }
}
