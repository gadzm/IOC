using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IOC.DataGens;
using IOC.Model;
using IOC.Repositories;
using IOC.Services;

namespace IOCTests.DataServiceTests
{


    [TestClass]
    public class DataServiceTest
    {
        private Dictionary<int, Customer> customersList;
        private List<Room> roomList1;
        private List<Room> roomList2;
        private List<Room> roomsList;
        private ObservableCollection<Reservation> reservationList;

        [TestInitialize]
        public void Initialize()
        {
            DataRepository testData = new DataRepository(new SmallDataGenerator());
            customersList = testData.getAllCustomers();
            roomsList = testData.getAllRooms();
            reservationList = testData.getAllReservations();
            roomList1 = new List<Room>();
            roomList2 = new List<Room>();
            roomList1.Add(new Room(101)); roomList2.Add(new Room(105));
            roomList1.Add(new Room(201)); roomList2.Add(new Room(200));
            roomList1.Add(new Room(150)); roomList2.Add(new Room(151));
            roomList1.Add(new Room(150)); roomList2.Add(new Room(160));
        }

        [TestMethod]
        public void GetRoomAtFloorLambdaTest()
        {
            int expected = 10;
            List<Room> rooms = DataService.GetRoomsAtFloorLambda(roomsList, 1);
            int result = rooms.Count();
            Assert.AreEqual(expected, result, 0.0);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void GetRoomAtFloorLambdaNullListTest()
        {
            DataService.GetRoomsAtFloorLambda(null, 1);
        }

        [TestMethod]
        public void GetRoomAtFloorLinqTest()
        {
            int expected = 10;
            List<Room> rooms = DataService.GetRoomsAtFloorLinq(roomsList, 1);
            int result = rooms.Count();
            Assert.AreEqual(expected, result, 0.0);
        }


        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void GetRoomAtFloorLinqNullListTest()
        {
            DataService.GetRoomsAtFloorLinq(null, 1);
        }

        [TestMethod]
        public void GetReservationsByTimeIntervalLambdaTest()
        {
            List<Reservation> res = DataService.GetReservationsByTimeIntervalLambda(reservationList, new DateTime(2015, 10, 10), new DateTime(2016, 1, 1));
            int result = 4;
            int expected = res.Count();
            Assert.AreEqual(expected, result, 0.0);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void GetReservationsByTimeIntervalLambdaNullListTest()
        {
            DataService.GetReservationsByTimeIntervalLambda(null, new DateTime(2015, 10, 10), new DateTime(2016, 1, 1));
        }

        [TestMethod]
        public void GetReservationsByTimeIntervalLinqTest()
        {
            List<Reservation> res = DataService.GetReservationsByTimeIntervalLinq(reservationList, new DateTime(2015, 10, 10), new DateTime(2016, 1, 1));
            int result = 4;
            int expected = res.Count();
            Assert.AreEqual(expected, result, 0.0);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void GetReservationsByTimeIntervalLinqNullListTest()
        {
            DataService.GetReservationsByTimeIntervalLinq(null, new DateTime(2015, 10, 10), new DateTime(2016, 1, 1));
        }

        [TestMethod]
        public void GetLastNamesFromReservationsLambdaTest()
        {
            int expected = 6;
            List<String> res = DataService.GetLastNamesFromReservationsLambda(reservationList);
            int result = res.Count();
            Assert.AreEqual(expected, result, 0.0);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void GetLastNamesFromReservationsTestLambdNullListTest()
        {
            DataService.GetLastNamesFromReservationsLambda(null);
        }

        [TestMethod]
        public void GetLastNamesFromReservationsLinqTest()
        {
            int expected = 6;
            List<String> res = DataService.GetLastNamesFromReservationsLinq(reservationList);
            int result = res.Count();
            Assert.AreEqual(expected, result, 0.0);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void GetLastNamesFromReservationsTestLinqNullListTest()
        {
            DataService.GetLastNamesFromReservationsLinq(null);
        }

        [TestMethod]
        public void CompareListsTestLambda()
        {
            Trace.WriteLine(Console.Out);
            string expected = "101 105\n150 151\n150 160\n";
            StringWriter sw = new StringWriter();
            TextWriter tw = Console.Out;
            Console.SetOut(sw);
            DataService.CompareListsLambda(roomList1, roomList2);
            string result = sw.ToString();
            Console.SetOut(tw);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void CompareListsNullListTestLambda()
        {
            DataService.CompareListsLambda(roomList1, null);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void CompareListsNullBothListTestLambda()
        {
            DataService.CompareListsLambda(null, null);
        }

        [TestMethod]
        public void CompareListsTestLinq()
        {
            Trace.WriteLine(Console.Out);
            string expected = "101 105\n150 151\n150 160\n";
            StringWriter sw = new StringWriter();
            TextWriter tw = Console.Out;
            Console.SetOut(sw);
            DataService.CompareListsLinq(roomList1, roomList2);
            string result = sw.ToString();
            Console.SetOut(tw);
            Assert.AreEqual(expected, result);

        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void CompareListsNullListTestLinq()
        {
            DataService.CompareListsLinq(roomList1, null);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void CompareListsNullBothListTestLinq()
        {
            DataService.CompareListsLinq(null, null);
        }

        [TestMethod]
        public void GetLeastNumberRoomLambdaTest()
        {
            int expected = 100;
            Room res = DataService.GetLeastNumberRoomLambda(roomsList);
            int result = res.RoomNumber;
            Assert.AreEqual(expected, result, 0.0);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void GetLeastNumberRoomNullLambdaTest()
        {
            DataService.GetLeastNumberRoomLambda(null);
        }
        [TestMethod]
        public void GetLeastNumberRoomLinqTest()
        {
            int expected = 100;
            Room res = DataService.GetLeastNumberRoomLinq(roomsList);
            int result = res.RoomNumber;
            Assert.AreEqual(expected, result, 0.0);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void GetLeastNumberRoomNullLinqTest()
        {
            DataService.GetLeastNumberRoomLinq(null);
        }


        [TestMethod]
        public void GetClientsWithReservationsLambdaTest()
        {
            string lastPos = "Nixon";
            Customer[] res = DataService.GetClientsWithReservationsLambda(reservationList);
            string lastPosRes = res[2].LastName;
            Assert.AreEqual(lastPos, lastPosRes);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void GetClientsWithReservationsLambdaNullTest()
        {
            DataService.GetClientsWithReservationsLambda(null);
        }
        [TestMethod]
        public void GetClientsWithReservationsLinqTest()
        {
            string lastPos = "Nixon";
            Customer[] res = DataService.GetClientsWithReservationsLinq(reservationList);
            string lastPosRes = res[2].LastName;
            Assert.AreEqual(lastPos, lastPosRes);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void GetClientsWithReservationsLinqNullTest()
        {
            DataService.GetClientsWithReservationsLinq(null);
        }


        [TestMethod]
        public void GetDistinctReservationsLambdaTest()
        {
            int lastPos = 5;
            List<Reservation> res = DataService.GetDistinctReservationsLambda(reservationList);
            int lastPosRes = res.Count();
            Assert.AreEqual(lastPos, lastPosRes, 0.0);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void GetDistinctReservationsLambdaNullTest()
        {
            DataService.GetDistinctReservationsLambda(null);
        }

        [TestMethod]
        public void GetDistinctReservationsLinqTest()
        {
            int lastPos = 5;
            List<Reservation> res = DataService.GetDistinctReservationsLinq(reservationList);
            int lastPosRes = res.Count();
            Assert.AreEqual(lastPos, lastPosRes, 0.0);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void GetDistinctReservationsLinqNullTest()
        {
            DataService.GetDistinctReservationsLinq(null);
        }

        [TestMethod]
        public void GetReservationsByTimeIntervalLambdaAsSClassTest()
        {
            List<SimpleClass> res = DataService.GetReservationsByTimeIntervalAsSClassLambda(reservationList, new DateTime(2015, 10, 10), new DateTime(2016, 1, 1));
            string result = "104";
            string expected = res[2].stringValue;
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void GetReservationsByTimeIntervalLambdaAsSClassNullListTest()
        {
            DataService.GetReservationsByTimeIntervalAsSClassLambda(null, new DateTime(2015, 10, 10), new DateTime(2016, 1, 1));
        }

        [TestMethod]
        public void GetReservationsByTimeIntervalAsSClassLinqTest()
        {
            
            List<SimpleClass> res = DataService.GetReservationsByTimeIntervalAsSClassLinq(reservationList, new DateTime(2015, 10, 10), new DateTime(2016, 1, 1));
            string result = "104";
            string expected = res[2].stringValue;
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void GetReservationsByTimeIntervalAsSClassLinqNullListTest()
        {
            DataService.GetReservationsByTimeIntervalAsSClassLinq(null, new DateTime(2015, 10, 10), new DateTime(2016, 1, 1));
        }

    }
}
