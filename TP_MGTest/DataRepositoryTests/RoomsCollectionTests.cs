using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IOC.DataGens;
using IOC.Model;
using IOC.Repositories;

namespace IOCTests.DataRepositoryTests
{
    [TestClass]
    public class RoomsCollectionTests
    {
        private DataRepository testData;
        [TestInitialize]
        public void Initialize()
        {
            testData = new DataRepository(new SmallDataGenerator());
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void addRoomAboveTheRangeTest()
        {
            testData.addRoom(300);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void addRoomBelowTheRangeTest()
        {
            testData.addRoom(99);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void addRoomDoubleErrorTest()
        {
            testData.addRoom(200);
        }

        [TestMethod]
        public void addRoomValidDataTest()
        {
            testData.addRoom(150);
            int expected = 21;
            int result = testData.getAllRooms().Count();
            Assert.AreEqual(expected, result, 0.0);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void deleteRoomNonExistentTest()
        {
            int expected = 20;
            testData.deleteRoom(99);
            int result = testData.getAllRooms().Count();
            Assert.AreEqual(expected, result, 0.0);
        }

        [TestMethod]
        public void deleteRoomExistentTest()
        {
            int expected = 19;
            testData.deleteRoom(200);
            int result = testData.getAllRooms().Count();
            Assert.AreEqual(expected, result, 0.0);
        }

        [TestMethod]
        public void getRoomByNumberValidNoTest()
        {
            testData.getRoomByNumber(108);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void getRoomByNumberInvalidNoTest()
        {
            testData.getRoomByNumber(210);

        }

        [TestMethod]
        public void deleteAllDataTest()
        {
            int result = 0;
            testData.deleteAllData();
            int expected = testData.getAllRooms().Count();
            Assert.AreEqual(expected, result, 0.0);
        }

        [TestMethod]
        public void addRoomValidObjectTest()
        {
            int expected = 21;
            Room room = new Room(180);
            testData.addRoom(room);
            int result = testData.getAllRooms().Count();
            Assert.AreEqual(expected, result, 0.0);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void addRoomNumberOutOfRangeObjecTest()
        {
            Room room = new Room(80);
            testData.addRoom(room);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void addRoomSameNumberObjecTest()
        {
            Room room = new Room(101);
            testData.addRoom(room);
        }
    }
}
