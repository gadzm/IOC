using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IOC.DataBaseModel;
using IOC.Services;

namespace IOCTests.ServicesTests
{
    [TestClass]
    public class DataBaseServiceTests
    {

        [TestMethod]
        public void getProductNameByVendorNameLambdaTest()
        {
            List<string> res = DataBaseService.getProductNameByVendorNameLambda("Vision Cycles, Inc.");
            int result = res.Count();
            int expected = 3;
            Assert.AreEqual(expected, result, 0.0);
        }

        [TestMethod]
        public void getProductNameByVendorNameLinqTest()
        {
            List<string> res = DataBaseService.getProductNameByVendorNameLinq("Vision Cycles, Inc.");
            int result = res.Count();
            int expected = 3;
            Assert.AreEqual(expected, result, 0.0);
        }

        [TestMethod]
        public void getRecentlyReviewedProductsLambdaTest()
        {
            List<Product> res = DataBaseService.GetRecentlyReviewedProductsLambda(2);
            string expected = "Road-550-W Yellow, 40HL Mountain Pedal";
            string result = res[1].Name + res[0].Name;
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void getRecentlyReviewedProductsLinqTest()
        {
            List<Product> res = DataBaseService.GetRecentlyReviewedProductsLinq(2);
            string expected = "Road-550-W Yellow, 40HL Mountain Pedal";
            string result = res[1].Name + res[0].Name;
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetNRecentlyReviewedProductsLambdaTest()
        {
            List<Product> res = DataBaseService.GetNRecentlyReviewedProductsLambda(2);
            string expected = "Road-550-W Yellow, 40";
            string result = res[1].Name;
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetNRecentlyReviewedProductsLinqTest()
        {
            List<Product> res = DataBaseService.GetNRecentlyReviewedProductsLinq(2);
            string expected = "Road-550-W Yellow, 40";
            string result = res[1].Name;
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetProductsSortedByCategoryAndNameLambdaTest()
        {
            List<Product> res = DataBaseService.GetProductsSortedByCategoryAndNameLambda(4);
            string result = res[3].Name;
            string expected = "Mountain-100 Black, 48";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetProductsSortedByCategoryAndNameLinqTest()
        {
            List<Product> res = DataBaseService.GetProductsSortedByCategoryAndNameLinq(4);
            string result = res[3].Name;
            string expected = "Mountain-100 Black, 48";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void getTotalStandardCostByCategoryLambdaTest()
        {
            ProductCategory cat = new ProductCategory();
            cat.ProductCategoryID = 2;
            int expected = 62961;
            int result = DataBaseService.GetTotalStandardCostByCategoryLambda(cat);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void getTotalStandardCostByCategoryLinqTest()
        {
            ProductCategory cat = new ProductCategory();
            cat.ProductCategoryID = 2;
            int expected = 62961;
            int result = DataBaseService.GetTotalStandardCostByCategoryLinq(cat);
            Assert.AreEqual(expected, result);
        }
    }
}
