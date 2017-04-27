using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessLogic.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Repositories;
using DataAccessLayer.Constants;
using DataAccessLayer;

namespace BusinessLogic.Handlers.Tests
{
    [TestClass()]
    public class RoomHandlerTests
    {
        RoomHandler handler;
        RoomRepository repo;
        List<ROOM_TYPE> types;
        RoomType room;
        int len = 365;
        string originalDesc;
        int originalInv;

        [TestInitialize]
        public void TestInit()
        {
            handler = new RoomHandler();
            repo = new RoomRepository(new DataAccessLayer.HotelDataModelContainer());
            types = handler.CheckAvailableTypeForDuration(DateTime.Today, DateTime.Today.AddDays(len));
            room = repo.GetRoomType(types.FirstOrDefault());
            originalDesc = room.Description;
            originalInv = room.Inventory;
        }

        [TestCleanup]
        public void TestCleanup()
        {
            room.Description = originalDesc;
            room.Inventory = originalInv;
            repo.UpdateRoom(room);
            repo.Save();
        }

        /// <summary>
        /// pass if no reservations in the database
        /// </summary>
        [TestMethod()]
        public void CheckAvailableTypeForDurationTest()
        {
            List<ROOM_TYPE> result = handler.CheckAvailableTypeForDuration(DateTime.Today, DateTime.Today.AddDays(365));
            Assert.AreEqual((new List<RoomType>(repo.GetRoomTypes()).Count), result.Count);
        }

        /// <summary>
        /// pass if throw ArgumentException or returns no available rooms
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void CheckAvailableTypeForDurationWithInvalidInputTest()
        {
            List<ROOM_TYPE> result = handler.CheckAvailableTypeForDuration(DateTime.Today.AddDays(365), DateTime.Today);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod()]
        public void GetRoomPriceListTest()
        {
            if (types.Count > 0)
            {
                List<int> prices = handler.GetRoomPriceList(types.FirstOrDefault(), DateTime.Today, DateTime.Today.AddDays(len));
                Assert.AreEqual(len, prices.Count);
                foreach (int price in prices)
                {
                    Assert.IsTrue(price >= repo.GetRoomType(types.FirstOrDefault()).BaseRate);
                }
            }
        }

        [TestMethod()]
        public void GetBookedRoomOnDateTest()
        {
            int result = handler.GetBookedRoomOnDate(types.FirstOrDefault(), DateTime.Today);
            Assert.IsTrue(result >= 0 && result <= room.Inventory);
        }

        [TestMethod()]
        public void GetRooomInventoryTest()
        {
            Assert.AreEqual(room.Inventory, handler.GetRoomInventory(room.Type));
        }

        [TestMethod()]
        public void UpdateRoomDescriptionTest()
        {
            string original = handler.GetRoomDescription(room.Type);
            string newDesc = "testDesc";
            handler.UpdateRoomDescription(room.Type, newDesc);
            Assert.AreEqual(handler.GetRoomDescription(room.Type), newDesc);
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
            int original = handler.GetRoomInventory(room.Type);
            int newInv = original + 10;
            handler.UpdateRoomInventory(room.Type, newInv);
            Assert.AreEqual(newInv, handler.GetRoomInventory(room.Type));
        }

        [TestMethod()]
        public void UpdateRoomInventoryNegativeTest()
        {
            int original = handler.GetRoomInventory(room.Type);
            int newInv = 0 - original;
            handler.UpdateRoomInventory(room.Type, newInv);
            Assert.AreEqual(newInv, handler.GetRoomInventory(room.Type));
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void UpdateRoomInventoryInvalidTest()
        {
            int original = handler.GetRoomInventory(room.Type);
            int newInv = repo.GetMaxRoomOccupanciesByRoomTypeAfterDate(room.Type, DateTime.Today) - 1;
            handler.UpdateRoomInventory(room.Type, newInv);
            Assert.AreEqual(original, handler.GetRoomInventory(room.Type));
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