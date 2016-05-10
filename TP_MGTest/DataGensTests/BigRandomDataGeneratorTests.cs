using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using IOC.DataGens;
using IOC.Model;
using IOC.Repositories;

namespace IOCTests.DataGensTests
{
    [TestClass]
    public class BigRandomDataGeneratorTests
    {
        private BigRandomDataGenerator dataGenerator;
        private Dictionary<int, Customer> customers = new Dictionary<int, Customer>();
        private List<Room> rooms = new List<Room>();
        private ObservableCollection<Reservation> reservations = new ObservableCollection<Reservation>();

        [TestInitialize]
        public void Initialize()
        {
            customers.Clear();
            rooms.Clear();
            reservations.Clear();
        }

        [TestMethod]
        public void fillCustomerTest()
        {
            int expected = 10;
            dataGenerator = new BigRandomDataGenerator(10);
            dataGenerator.fillCustomers(customers);
            int result = customers.Count();
            Assert.AreEqual(expected, result, 0.0);
        }

        [TestMethod]
        public void fillRoomsTest()
        {
            int expected = 10;
            dataGenerator = new BigRandomDataGenerator(10);
            dataGenerator.fillRooms(rooms);
            int result = rooms.Count();
            Assert.AreEqual(expected, result, 0.0);
        }

        [TestMethod]
        public void fillReservationTest()
        {
            int expected = 10;
            dataGenerator = new BigRandomDataGenerator(10);
            dataGenerator.fillRooms(rooms);
            dataGenerator.fillCustomers(customers);
            dataGenerator.fillReservations(customers, rooms, reservations);
            int result = reservations.Count();
            Assert.AreEqual(expected, result, 0.0);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void fillReservationWithEmptyCustomersListTest()
        {
            dataGenerator = new BigRandomDataGenerator(10);
            dataGenerator.fillRooms(rooms);
            dataGenerator.fillReservations(customers, rooms, reservations);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void fillReservationWithEmptyRoomsListTest()
        {
            dataGenerator = new BigRandomDataGenerator(10);
            dataGenerator.fillCustomers(customers);
            dataGenerator.fillReservations(customers, rooms, reservations);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void fillReservationWithEmptyListsTest()
        {
            dataGenerator = new BigRandomDataGenerator(10);
            dataGenerator.fillReservations(customers, rooms, reservations);
        }
    }
}
