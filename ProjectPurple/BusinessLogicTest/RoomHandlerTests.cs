using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogic.Handlers;
using DataAccessLayer.Constants;
using DataAccessLayer.EF;
using DataAccessLayer.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessLogicTest
{
    [TestClass()]
    public class RoomHandlerTests
    {
        private RoomHandler _handler;
        private RoomRepository _repo;
        private List<ROOM_TYPE> _types;
        private RoomType _room;
        private const int Len = 365;
        private string _originalDesc;
        private int _originalInv;

        [TestInitialize]
        public void TestInit()
        {
            _handler = new RoomHandler();
            _repo = new RoomRepository(new HotelModelContext());
            _types = _handler.CheckAvailableTypeForDuration(DateTime.Today, DateTime.Today.AddDays(Len));
            _room = _repo.GetRoomType(_types.FirstOrDefault());
            _originalDesc = _room.Description;
            _originalInv = _room.Inventory;
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _room.Description = _originalDesc;
            _room.Inventory = _originalInv;
            _repo.UpdateRoom(_room);
            _repo.Save();
        }

        /// <summary>
        /// pass if no reservations in the database
        /// </summary>
        [TestMethod()]
        public void CheckAvailableTypeForDurationTest()
        {
            List<ROOM_TYPE> result = _handler.CheckAvailableTypeForDuration(DateTime.Today, DateTime.Today.AddDays(365));
            Assert.AreEqual((new List<RoomType>(_repo.GetRoomTypes()).Count), result.Count);
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
            if (_types.Count > 0)
            {
                List<int> prices = _handler.GetRoomPriceList(_types.FirstOrDefault(), DateTime.Today, DateTime.Today.AddDays(Len));
                Assert.AreEqual(Len, prices.Count);
                foreach (int price in prices)
                {
                    Assert.IsTrue(price >= _repo.GetRoomType(_types.FirstOrDefault()).BaseRate);
                }
            }
        }

        [TestMethod()]
        public void GetBookedRoomOnDateTest()
        {
            int result = _handler.GetBookedRoomOnDate(_types.FirstOrDefault(), DateTime.Today);
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
        public void UpdateRoomPictureUrlsTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void InsertPictureUrlTest()
        {
            Assert.Fail();
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
        public void UpdateRoomInventoryNegativeTest()
        {
            int original = _handler.GetRoomInventory(_room.Type);
            int newInv = 0 - original;
            _handler.UpdateRoomInventory(_room.Type, newInv);
            Assert.AreEqual(newInv, _handler.GetRoomInventory(_room.Type));
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void UpdateRoomInventoryInvalidTest()
        {
            int original = _handler.GetRoomInventory(_room.Type);
            int newInv = _repo.GetMaxRoomOccupanciesByRoomTypeAfterDate(_room.Type, DateTime.Today) - 1;
            _handler.UpdateRoomInventory(_room.Type, newInv);
            Assert.AreEqual(original, _handler.GetRoomInventory(_room.Type));
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

        [TestMethod()]
        public void GetReservationsCheckInTodayTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetAllCheckedInReservationsTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetAveragePriceTest()
        {
            Assert.Fail();
        }
    }
}