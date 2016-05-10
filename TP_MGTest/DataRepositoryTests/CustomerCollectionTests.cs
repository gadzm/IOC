using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using IOC.DataGens;
using IOC.Model;
using IOC.Repositories;

namespace IOCTests.DataRepositoryTests
{
    [TestClass]
    public class CustomerCollectionTests
    {
        private DataRepository testData;
        [TestInitialize]
        public void Initialize()
        {
            testData = new DataRepository(new SmallDataGenerator());
        }

        [TestMethod]
        public void addCustomerValidDataTest()
        {
            testData.addCustomer("Frank", "Zapp", category.vip);
            int expected = 8;
            int result = testData.getAllCustomers().Count();
            Assert.AreEqual(expected, result, 0.0);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void addCustomerNullFirstNameParameterTest()
        {
            testData.addCustomer(null, "Zappa", category.vip);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void addCustomerNullLastNameParameterTest()
        {
            testData.addCustomer("Frank", null, category.vip);
        }


        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void addCustomerEmptyFirstNameParameterTest()
        {
            testData.addCustomer("", "Zappa", category.vip);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void addCustomerEmptyLastNameParameterTest()
        {
            testData.addCustomer("Frank", "", category.vip);
        }

        [TestMethod]
        public void addCustomerNumberOfElementsCheck()
        {
            testData.addCustomer("Eric", "Dolphy", category.vip);
            int expected = 8;
            int result = testData.getAllCustomers().Count();
            Assert.AreEqual(expected, result, 0.0);
        }

        [TestMethod]
        public void deleteCustomerNonExistentTest()
        {
            int expected = 7;
            testData.deleteCustomer(10);
            int result = testData.getAllCustomers().Count();
            Assert.AreEqual(expected, result, 0.0);
        }

        [TestMethod]
        public void deleteCustomerExistentTest()
        {
            int expected = 6;
            testData.deleteCustomer(2);
            int result = testData.getAllCustomers().Count();
            Assert.AreEqual(expected, result, 0.0);
        }

        [TestMethod]
        public void getCustomersByLastNameTest()
        {
            int expected = 2;
            Dictionary<int, Customer> tmplist;
            tmplist = testData.getCustomersByLastName("Nixon");
            int result = tmplist.Count();
            Assert.AreEqual(expected, result, 0.0);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void getCustomersByLastNameNullParamaterTest()
        {
            testData.getCustomersByLastName(null);
        }

        [TestMethod]
        
        public void getCustomersByLastNameNonExistentTest()
        {
            int expected = 0;
            Dictionary<int,Customer> customers = testData.getCustomersByLastName("Zapp");
            int result = customers.Count();
            Assert.AreEqual(expected, result, 0.0);
        }
    

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void getCustomersByLastNameEmptyStringTest()
        {
            testData.getCustomersByLastName("");
        }

        [TestMethod]
        public void getCustomersByCategoryTest()
        {
            int expected = 2;
            Dictionary<int, Customer> tmplist;
            tmplist = testData.getCustomersByCategory(category.businesclass);
            int result = tmplist.Count();
            Assert.AreEqual(expected, result, 0.0);
        }

        [TestMethod]
        public void getCustomerByIdValidIdTest()
        {
            string expected = "Zappa";
            Customer customer = testData.getCustomerById(0);
            string result = customer.LastName;
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void getCustomerByIdInvalidIdTest()
        {
            testData.getCustomerById(18);
        }

        [TestMethod]
        public void getCustomerByFirstAndLastNameValidDataTest()
        {
            string expected = "Bowie";
            Customer customer = testData.getCustomerByFirstAndLastName("David", "Bowie");
            string result = customer.LastName;
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void getCustomerIdByFirstAndLastNameValidDataTest()
        {
            int expected = 4;
            int result = testData.getCustomerIdByFirstAndLastName("Fela", "Kuti");
            Assert.AreEqual(expected, result, 0.0);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void getCustomerIdByFirstAndLastNameEmptyFirstNameStringTest()
        {
            testData.getCustomerIdByFirstAndLastName("", "Kuti");
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void getCustomerIdByFirstAndLastNameEmptyLastNameStringTest()
        {
            testData.getCustomerIdByFirstAndLastName("Fela", "");
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void getCustomerIdByFirstAndLastNameEmptyStringsTest()
        {
            testData.getCustomerIdByFirstAndLastName("", "");
        }

        [TestMethod]
        public void deleteAllDataTest()
        {
            int result = 0;
            testData.deleteAllData();
            int expected = testData.getAllCustomers().Count();
            Assert.AreEqual(expected, result, 0.0);
        }

        [TestMethod]
        public void addCustomerFromObjectTest()
        {
            int expected = 8;
            Customer customer = new Customer("Jak", "Kowalski", category.regular);
            testData.addCustomer(customer);
            int result = testData.getAllCustomers().Count();
            Assert.AreEqual(expected, result, 0.0);
        }
    }
}
