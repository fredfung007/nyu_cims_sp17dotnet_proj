using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogic.Handlers;
using DataAccessLayer.Constants;
using DataAccessLayer.EF;
using DataAccessLayer.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BusinessLogicTest
{
    [TestClass()]
    public class RoomHandlerTests
    {
        private const int _LEN = 5;
        private const int _RATE = 300;
        private const int _INV = 30;
        private const ROOM_TYPE _TYPE = ROOM_TYPE.QueenRoom;

        private RoomHandler _handler;
        private RoomType _room;
        private Reservation _reservation;
        private Guid _confirmationNum = Guid.NewGuid();
        private Mock<IRoomRepository> _mockRoomRepo;
        private Mock<IReservationRepository> _mockReservationRepo;

        [TestInitialize]
        public void TestInit()
        {
            // mock the RoomRepository
            _mockRoomRepo = new Mock<IRoomRepository>();
            _room = new RoomType
            {
                Id = Guid.NewGuid(),
                BaseRate = _RATE,
                Ameneties = "amenety",
                Description = "desc",
                ImageUrl = "imgUrl",
                Inventory = _INV,
                RoomOccupancies = new List<RoomOccupancy>(),
                Type = _TYPE
            };
            _mockRoomRepo.Setup(m => m.GetRoomTypes()).Returns(new List<RoomType> { _room });
            _mockRoomRepo.Setup(m => m.GetRoomType(_TYPE)).Returns(_room);
            _mockRoomRepo.Setup(m => m.GetRoomTotalAmount(_room)).Returns(_INV);
            for (int i = 0; i <= _LEN; i++)
            {
                _mockRoomRepo.Setup(m => m.GetMaxRoomOccupanciesByRoomTypeAfterDate(_TYPE, DateTime.Now.AddDays(i))).Returns(_INV);
                _mockRoomRepo.Setup(m => m.GetRoomReservationAmount(_room, DateTime.Now.AddDays(i))).Returns(_INV - 1);
            }

            // mock the ReservationRepository
            _mockReservationRepo = new Mock<IReservationRepository>();
            _reservation = new Reservation {
                AspNetUser = new AspNetUser {  },

            };

            // generate the handler
            var roomRepo = _mockRoomRepo.Object;
            var reservationRepo = _mockReservationRepo.Object;
            _handler = new RoomHandler(roomRepo, reservationRepo);
        }

        /// <summary>
        /// pass if no reservations in the database
        /// </summary>
        [TestMethod()]
        public void CheckAvailableTypeForDurationTest()
        {
            List<ROOM_TYPE> result = _handler.CheckAvailableTypeForDuration(DateTime.Today, DateTime.Today.AddDays(_LEN));
            Assert.AreEqual(1, result.Count);
        }

        /// <summary>
        /// pass if throw ArgumentException or returns no available rooms
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void CheckAvailableTypeForDurationWithInvalidInputTest()
        {
            List<ROOM_TYPE> result = _handler.CheckAvailableTypeForDuration(DateTime.Today.AddDays(365), DateTime.Today);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod()]
        public void GetRoomPriceListTest()
        {
            List<int> prices = _handler.GetRoomPriceList(_TYPE, DateTime.Today, DateTime.Today.AddDays(_LEN));
            Assert.AreEqual(_LEN, prices.Count);
            foreach (int price in prices)
            {
                Assert.IsTrue(price >= _RATE);
            }
        }

        [TestMethod()]
        public void GetBookedRoomOnDateTest()
        {
            int result = _handler.GetBookedRoomOnDate(_TYPE, DateTime.Today);
            Assert.IsTrue(result >= 0 && result <= _room.Inventory);
        }

        [TestMethod()]
        public void GetRooomInventoryTest()
        {
            Assert.AreEqual(_room.Inventory, _handler.GetRoomInventory(_room.Type));
        }

        [TestMethod()]
        public void UpdateRoomDescriptionTest()
        {
            string original = _handler.GetRoomDescription(_room.Type);
            string newDesc = "testDesc";
            _handler.UpdateRoomDescription(_room.Type, newDesc);
            Assert.AreEqual(_handler.GetRoomDescription(_room.Type), newDesc);
        }

        [TestMethod()]
        public void UpdateRoomInventoryTest()
        {
            int original = _handler.GetRoomInventory(_room.Type);
            int newInv = original + 10;
            _handler.UpdateRoomInventory(_room.Type, newInv);
            Assert.AreEqual(newInv, _handler.GetRoomInventory(_room.Type));
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void UpdateRoomInventoryNegativeTest()
        {
            int original = _handler.GetRoomInventory(_room.Type);
            int newInv = -10;
            _handler.UpdateRoomInventory(_room.Type, newInv);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void UpdateRoomInventoryInvalidTest()
        {
            int original = _handler.GetRoomInventory(_room.Type);
            var repo = _mockRoomRepo.Object;
            int maxOccu = repo.GetMaxRoomOccupanciesByRoomTypeAfterDate(_TYPE, DateTime.Today);
            _handler.UpdateRoomInventory(_room.Type, maxOccu - 1);
        }

        [TestMethod()]
        public void CheckInTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void CheckOutTest()
        {
            Assert.Fail();
        }
    }
}