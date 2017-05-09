using System;
using System.Collections.Generic;
using BusinessLogic.Handlers;
using DataAccessLayer.Constants;
using DataAccessLayer.EF;
using DataAccessLayer.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BusinessLogicTest
{
    [TestClass]
    public class RoomHandlerTests
    {
        private const int Len = 5;
        private const int Rate = 300;
        private const int Inv = 30;
        private const ROOM_TYPE Type = ROOM_TYPE.QueenRoom;
        private Guid _confirmationNum = Guid.NewGuid();

        private RoomHandler _handler;
        private Mock<IReservationRepository> _mockReservationRepo;
        private Mock<IRoomRepository> _mockRoomRepo;
        private Reservation _reservation;
        private RoomType _room;

        [TestInitialize]
        public void TestInit()
        {
            // mock the RoomRepository
            _mockRoomRepo = new Mock<IRoomRepository>();
            _room = new RoomType
            {
                Id = Guid.NewGuid(),
                BaseRate = Rate,
                Ameneties = "amenety",
                Description = "desc",
                ImageUrl = "imgUrl",
                Inventory = Inv,
                Type = Type
            };
            _mockRoomRepo.Setup(m => m.GetRoomTypes()).Returns(new List<RoomType> {_room});
            _mockRoomRepo.Setup(m => m.GetRoomType(Type)).Returns(_room);
            _mockRoomRepo.Setup(m => m.GetRoomTotalAmount(_room.Type)).Returns(Inv);
            for (var i = 0; i <= Len; i++)
            {
                _mockRoomRepo.Setup(m => m.GetMaxRoomOccupanciesByRoomTypeAfterDate(Type, DateTime.Now.AddDays(i)))
                    .Returns(Inv);
                _mockRoomRepo.Setup(m => m.GetRoomOccupancyByDate(_room.Type, DateTime.Now.AddDays(i)))
                    .Returns(Inv - 1);
            }

            // mock the ReservationRepository
            _mockReservationRepo = new Mock<IReservationRepository>();
            _reservation = new Reservation
            {
                AspNetUser = new AspNetUser()
            };

            // generate the handler
            IRoomRepository roomRepo = _mockRoomRepo.Object;
            IReservationRepository reservationRepo = _mockReservationRepo.Object;
            _handler = new RoomHandler(roomRepo);
        }

        /// <summary>
        ///     pass if no reservations in the database
        /// </summary>
        [TestMethod]
        public void CheckAvailableTypeForDurationTest()
        {
            var result = _handler.CheckAvailableTypeForDuration(DateTime.Today, DateTime.Today.AddDays(Len));
            Assert.AreEqual(1, result.Count);
        }

        /// <summary>
        ///     pass if throw ArgumentException or returns no available rooms
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CheckAvailableTypeForDurationWithInvalidInputTest()
        {
            var result = _handler.CheckAvailableTypeForDuration(DateTime.Today.AddDays(365), DateTime.Today);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void GetRoomPriceListTest()
        {
            var prices = _handler.GetRoomPriceList(Type, DateTime.Today, DateTime.Today.AddDays(Len));
            Assert.AreEqual(Len, prices.Count);
            foreach (var price in prices)
            {
                Assert.IsTrue(price >= Rate);
            }
        }

        [TestMethod]
        public void GetBookedRoomOnDateTest()
        {
            var result = _handler.GetBookedRoomOnDate(Type, DateTime.Today);
            Assert.IsTrue(result >= 0 && result <= _room.Inventory);
        }

        [TestMethod]
        public void GetRooomInventoryTest()
        {
            Assert.AreEqual(_room.Inventory, _handler.GetRoomInventory(_room.Type));
        }

        [TestMethod]
        public void UpdateRoomDescriptionTest()
        {
            var original = _handler.GetRoomDescription(_room.Type);
            var newDesc = "testDesc";
            _handler.UpdateRoomDescription(_room.Type, newDesc);
            Assert.AreEqual(_handler.GetRoomDescription(_room.Type), newDesc);
        }

        [TestMethod]
        public void UpdateRoomInventoryTest()
        {
            var original = _handler.GetRoomInventory(_room.Type);
            var newInv = original + 10;
            _handler.UpdateRoomInventory(_room.Type, newInv);
            Assert.AreEqual(newInv, _handler.GetRoomInventory(_room.Type));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void UpdateRoomInventoryNegativeTest()
        {
            var original = _handler.GetRoomInventory(_room.Type);
            var newInv = -10;
            _handler.UpdateRoomInventory(_room.Type, newInv);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void UpdateRoomInventoryInvalidTest()
        {
            var original = _handler.GetRoomInventory(_room.Type);
            IRoomRepository repo = _mockRoomRepo.Object;
            var maxOccu = repo.GetMaxRoomOccupanciesByRoomTypeAfterDate(Type, DateTime.Today);
            _handler.UpdateRoomInventory(_room.Type, maxOccu - 1);
        }
    }
}