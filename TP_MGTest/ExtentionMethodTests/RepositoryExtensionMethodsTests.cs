using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IOC.DataGens;
using IOC.Model;
using IOC.Repositories;
using IOC.Services;
using IOC.Exstensions;

namespace IOCTests.ExtentionMethodTests
{
    [TestClass]
    public class RepositoryExtensionMethodsTests
    {
        DataRepository testData;
        DataService dataService;

        [TestInitialize]
        public void Initialize()
        {
            testData = new DataRepository(new SmallDataGenerator());
            dataService = new DataService(testData);
        }

        [TestMethod]
        public void GetCustomerWithMostReservationsTest()
        {
            string expected = "ZappaBowie";
            List<Customer> cust = dataService.GetBestCustomer();
            string result = cust[0].LastName + cust[1].LastName;
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetCustomerInSublists()
        {
            List<Customer>[] res;
            res = dataService.GetCustomerInSublists();
            string result = "Richard";
            string expected = res[2][0].FirstName;
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetUnusedRooms()
        {
            List<Room> rooms;
            rooms = dataService.GetUnusedRooms();
            int result = rooms.Count();
            int expected = 13;
            Assert.AreEqual(expected, result, 0.0);
        }
    }
}
