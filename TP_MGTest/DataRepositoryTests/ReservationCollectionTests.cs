using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using IOC.DataGens;
using IOC.Model;
using IOC.Repositories;

namespace IOCTests.DataRepositoryTests
{
    [TestClass]
    public class ReservationCollectionTests
    {
        private DataRepository testData;
        [TestInitialize]
        public void Initialize()
        {
            testData = new DataRepository(new SmallDataGenerator());
        }

        [TestMethod]
        public void addReservationValidDataTest()
        {
            int expected = 10;
            Customer validCustomer = testData.getCustomerById(0);
            Room validRoom = testData.getRoomByNumber(200);
            testData.addReservation(validCustomer, validRoom, new DateTime(2016, 10, 10), new DateTime(2016, 10, 12));
            int result = testData.getAllReservations().Count();
            Assert.AreEqual(expected, result, 0.0);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void addReservationInvalidCustomerTest()
        {
            Customer invalidCustomer = new Customer("Jan", "Kowalski", category.vip);
            Room validRoom = testData.getRoomByNumber(200);
            testData.addReservation(invalidCustomer, validRoom, new DateTime(2016, 10, 10), new DateTime(2016, 10, 12));
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void addReservationInvalidRoomTest()
        {
            Customer validCustomer = testData.getCustomerById(0);
            Room invalidRoom = new Room(200);
            testData.addReservation(validCustomer, invalidRoom, new DateTime(2016, 10, 10), new DateTime(2016, 10, 12));
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void addReservationInvalidArrivalDateTest()
        {
            Customer validCustomer = testData.getCustomerById(0);
            Room validRoom = testData.getRoomByNumber(200);
            testData.addReservation(validCustomer, validRoom, new DateTime(2014, 10, 10), new DateTime(2016, 10, 12));
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void addReservationInvalidDepartureDateTest()
        {
            Customer validCustomer = testData.getCustomerById(0);
            Room validRoom = testData.getRoomByNumber(200);
            testData.addReservation(validCustomer, validRoom, new DateTime(2016, 10, 10), new DateTime(2016, 10, 10));
        }

        [TestMethod]
        public void getReservationByIdValidIdTest()
        {
            Reservation reservation=  testData.getReservationById(0);
            string result = "Zappa";
            string expected = reservation.Customer.LastName;
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void getReservationByIdInvalidIdTest()
        {
            Reservation reservation = testData.getReservationById(15);
        }

        [TestMethod]
        public void getReservationByCustomerNameValidNameIdTest()
        {
            ObservableCollection<Reservation> reservation = testData.getReservationsByCustomerName("Zappa");
            int result = 2;
            int expected = reservation.Count();
            Assert.AreEqual(expected, result, 0.0);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void getReservationByCustomerNameInvalidNameIdTest()
        {
            ObservableCollection<Reservation> reservation = testData.getReservationsByCustomerName("Zagłoba");
        }

        [TestMethod]
        public void getReservationByCustomerIdValidIdTest()
        {
            ObservableCollection<Reservation> reservation = testData.getReservationsByCustomerId(0);
            int result = 2;
            int expected = reservation.Count();
            Assert.AreEqual(expected, result, 0.0);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void getReservationByCustomerIdInvalidIdTest()
        {
            ObservableCollection<Reservation> reservation = testData.getReservationsByCustomerId(18);
        }

        [TestMethod]
        public void getReservationByRoomNumberValidNumberTest()
        {
            ObservableCollection<Reservation> reservation = testData.getReservationsByRoomNumber(100);
            int result = 3;
            int expected = reservation.Count();
            Assert.AreEqual(expected, result, 0.0);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void getReservationByRoomNumberInvalidNumberTest()
        {
            ObservableCollection<Reservation> reservation = testData.getReservationsByRoomNumber(210);
        }

        [TestMethod]
        public void getReservationByTimeIntervalValidIntervalTest()
        {
            ObservableCollection<Reservation> reservation = testData.getReservationsByTimeInterval(new DateTime(2015,10,10), new DateTime(2016,1,1));
            int result = 4;
            int expected = reservation.Count();
            Assert.AreEqual(expected, result, 0.0);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void getReservationByTimeIntervalInvalidIntervalTest()
        {
            ObservableCollection<Reservation> reservation = testData.getReservationsByTimeInterval(new DateTime(2016, 10, 10), new DateTime(2015, 1, 1));
        }

        [TestMethod]
        public void deleteReservationByIdValidIdTest()
        {
            int expected = 8;
            testData.deleteReservationById(4);
            int result = testData.getAllReservations().Count;
            Assert.AreEqual(expected, result, 0.0);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void deleteReservationByIdInvalidIdTest()
        {
            testData.deleteReservationById(18);
        }

        [TestMethod]
        public void deleteAllDataTest()
        {
            int result = 0;
            testData.deleteAllData();
            int expected = testData.getAllReservations().Count();
            Assert.AreEqual(expected, result, 0.0);
        }

    }

}
