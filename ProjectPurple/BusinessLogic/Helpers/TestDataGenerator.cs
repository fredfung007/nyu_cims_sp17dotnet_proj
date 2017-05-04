﻿using BusinessLogic.Handlers;
using DataAccessLayer.Constants;
using DataAccessLayer.EF;
using DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;

namespace BusinessLogic.Helpers
{
    public class TestDataGenerator
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IRoomRepository _roomRepository;
        private RoomHandler _roomHandler;
        private ReservationHandler _reservationHandler;
        private RoomType room;
        private List<string> firstnames;
        private List<string> lastnames;

        public TestDataGenerator()
        {
            _reservationRepository = new ReservationRepository(new CodeFirstHotelModel());
            _roomRepository = new RoomRepository(new CodeFirstHotelModel());
            _reservationHandler = new ReservationHandler();
            _roomHandler = new RoomHandler();

            room = new RoomType
            {
                Id = Guid.NewGuid(),
                BaseRate = 300,
                Type = ROOM_TYPE.QueenRoom,
                Inventory = 30
            };

            firstnames = new List<string>
            {
                "Maryalice",
                "Dick",
                "Ivonne",
                "Desiree",
                "Jewel",
                "Brandie",
                "Rema",
                "Kiara",
                "Edgardo",
                "Serina",
                "Miles",
                "Rosendo",
                "Jesse",
                "Loria",
                "Gretchen",
                "Brad",
                "Broderick",
                "Columbus",
                "Summer",
                "Karma",
                "Steven",
                "Julieta",
                "Delana",
                "Clint",
                "Lakenya",
                "Peter",
                "Connie",
                "Lucretia",
                "Joane",
                "Rosia",
                "Denese",
                "Fermina",
                "Elizabet",
                "Joe",
                "Eduardo",
                "Seth",
                "Lise",
                "Johana",
                "Colleen",
                "Robin",
                "Rufus",
                "Ofelia",
                "Trudi",
                "Tona",
                "Miss",
                "Rosy",
                "Melvin",
                "Cleora",
                "Vannessa",
                "Delphine",
                "Wilson",
                "Delsie",
                "Jessika",
                "Priscila",
                "Dawna",
                "Christian",
                "Matt",
                "Rhoda",
                "Corrinne",
                "Felicidad",
                "Rosaline",
                "Gena",
                "Nelda",
                "Yetta",
                "Blossom",
                "Emerita",
                "Gwen",
                "Flora",
                "Terisa",
                "Shondra",
                "Justa",
                "Janine",
                "Jo",
                "Maricruz",
                "Kerrie",
                "Art",
                "Dani",
                "Priscilla",
                "Izetta",
                "Racquel",
                "Mack",
                "Yuriko",
                "Rayna",
                "William",
                "Buck",
                "Loretta",
                "Tama",
                "Madalene",
                "Sheron",
                "Ramon",
                "Melania",
                "Maudie",
                "Shannan",
                "Shalanda",
                "Ahmad",
                "Lurlene",
                "Quinton",
                "Melani",
                "Londa",
                "Jamie"
            };

            lastnames = new List<string>
            {
                "Stiegemeier",
                "Coar",
                "Gaus",
                "Jandrin",
                "Evanich",
                "Kelder",
                "Kieser",
                "Whetstone",
                "Lage",
                "Haraway",
                "Sweitzer",
                "Eschrich",
                "Archibald",
                "Tsosie",
                "Candella",
                "Pesick",
                "Ziems",
                "Petronis",
                "Kuemmerle",
                "Pharris",
                "Budden",
                "Vacha",
                "Stiegman",
                "Longwith",
                "Widdison",
                "Goodman",
                "Glenna",
                "Boies",
                "Zarin",
                "Massey",
                "Stemen",
                "Thorn",
                "Layne",
                "Derentis",
                "Kozma",
                "Rizor",
                "Ziebart",
                "Comiso",
                "Zien",
                "Rager",
                "Bruna",
                "Stanick",
                "Velky",
                "Bleggi",
                "Weisinger",
                "Boronat",
                "Rekas",
                "Gelber",
                "Arre",
                "Tarshis",
                "Kantis",
                "Seib",
                "Betz",
                "Dottery",
                "Nason",
                "Evins",
                "Vannatten",
                "Mansker",
                "Labreche",
                "Pinelo",
                "Pillitteri",
                "Bollig",
                "Heagle",
                "Survant",
                "Pobre",
                "Thobbs",
                "Mahlman",
                "Spitale",
                "Pust",
                "Seawood",
                "Conliffe",
                "Viars",
                "Woodburn",
                "Gomzalez",
                "Malouff",
                "Benner",
                "Schmied",
                "Garibaldi",
                "Dockus",
                "Brazinski",
                "Javens",
                "Zarriello",
                "Tolomeo",
                "Surita",
                "Weather",
                "Munster",
                "Galeano",
                "Volbrecht",
                "Galassi",
                "Castanado",
                "Housey",
                "Bruson",
                "Kaja",
                "Stlaurent",
                "Merksamer",
                "Angelbeck",
                "Laeger",
                "Reinitz",
                "Kochanek",
                "Huisenga"
            };
        }

        public Tuple<List<AspNetUser>, List<Profile>> GenerateUsers(int amount)
        {
            Random rand = new Random();
            List<AspNetUser> users = new List<AspNetUser>();
            List<Profile> profiles = new List<Profile>();
            for (int i = 0; i < amount; i++)
            {
                string firstname = firstnames[rand.Next(0, firstnames.Count)];
                string lastname = lastnames[rand.Next(0, lastnames.Count)];

                Profile profile = new Profile
                {
                    Id = Guid.NewGuid(),
                    FirstName = firstname,
                    LastName = lastname,
                    Email = firstname + "@" + lastname + ".me",
                };

                AspNetUser user = new AspNetUser
                {
                    Id = Guid.NewGuid().ToString(),
                    Profile = profile,
                    Email = profile.Email,
                    ProfileGuid = profile.Id,
                    LoyaltyProgress = 0,
                    LoyaltyYear = DateTime.Now
                };

                profiles.Add(profile);
                users.Add(user);
            }
            return new Tuple<List<AspNetUser>, List<Profile>>(users, profiles);
        }

        public List<Reservation> GenerateReservationsWithCheckInDate(List<AspNetUser> users, DateTime checkIn, bool checkedIn)
        {
            Random rand = new Random();
            List<Reservation> reservations = new List<Reservation>();
            for (int i = 0; i < users.Count; i++)
            {
                DateTime checkOut = checkIn.AddDays(rand.Next(1, users.Count));
                Reservation reservation = new Reservation
                {
                    AspNetUser = users[i],
                    AspNetUsersId = users[i].Id,
                    StartDate = checkIn,
                    EndDate = checkOut,
                    RoomType = room
                };

                if (checkedIn)
                {
                    reservation.CheckInDate = checkIn;
                }

                List<DailyPrice> dailyPrices = new List<DailyPrice>();
                for (int j = 0;j < (checkOut - checkIn).Days;j++)
                {
                    dailyPrices.Add(new DailyPrice
                    {
                        Id = Guid.NewGuid(),
                        BillingPrice = room.BaseRate + rand.Next(0, 100),
                        Date = checkIn.AddDays(j),
                        Reservation = reservation,
                        ReservationId = reservation.Id
                    });
                }

                reservation.DailyPrices = dailyPrices;
                reservations.Add(reservation);
            }
            return reservations;
        }

        public List<Reservation> GenerateReservationsWithCheckOutDate(List<AspNetUser> users, DateTime checkOut, bool checkedOut)
        {
            Random rand = new Random();
            List<Reservation> reservations = new List<Reservation>();
            for (int i = 0; i < users.Count; i++)
            {
                DateTime checkIn = checkOut.Subtract(new TimeSpan(rand.Next(1, users.Count), 0, 0, 0));
                Reservation reservation = new Reservation
                {
                    AspNetUser = users[i],
                    AspNetUsersId = users[i].Id,
                    StartDate = checkIn,
                    EndDate = checkOut,
                    CheckInDate = checkIn,
                    RoomType = room
                };

                if (checkedOut)
                {
                    reservation.CheckOutDate = checkOut;
                }

                List<DailyPrice> dailyPrices = new List<DailyPrice>();
                for (int j = 0;j < (checkOut - checkIn).Days;j++)
                {
                    dailyPrices.Add(new DailyPrice
                    {
                        Id = Guid.NewGuid(),
                        BillingPrice = room.BaseRate + rand.Next(0, 100),
                        Date = checkIn.AddDays(j),
                        Reservation = reservation,
                        ReservationId = reservation.Id
                    });
                }

                reservation.DailyPrices = dailyPrices;
                reservations.Add(reservation);
            }
            return reservations;
        }
    }
}
