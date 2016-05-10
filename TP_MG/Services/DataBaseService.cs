using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IOC.DataBaseModel;

namespace IOC.Services
{
    public class DataBaseService
    {

        public static List<string> getProductNameByVendorNameLambda(string vendorName)
        {
            List<string> result = new List<string>();
            using (AdventureWorks2012Entities contex = new AdventureWorks2012Entities())
            {
                List<int> vendor = contex.Vendors.Where(a => a.Name == vendorName).Select(b => b.BusinessEntityID).ToList();
                List<int> pvendor = contex.ProductVendors.Where(a => vendor.Any(b => b == a.BusinessEntityID)).Select(c => c.ProductID).ToList();
                result = contex.Products.Where(a => pvendor.Any(b => b == a.ProductID)).Select(c => c.Name).ToList();
            }
            return result;
        }

        public static List<string> getProductNameByVendorNameLinq(string vendorName)
        {
            List<string> result;
            using (AdventureWorks2012Entities contex = new AdventureWorks2012Entities())
            {
                result = (from prod in contex.Products
                          from pven in contex.ProductVendors
                          from ven in contex.Vendors
                          where prod.ProductID == pven.ProductID
                          &&
                          pven.BusinessEntityID == ven.BusinessEntityID
                          &&
                          ven.Name == vendorName
                          select prod.Name).ToList();
            }
            return result;
        }

        public static List<Product> GetRecentlyReviewedProductsLambda(int howManyReviews)
        {
            List<Product> result = new List<Product>();
            using (AdventureWorks2012Entities contex = new AdventureWorks2012Entities())
            {
                List<int> rev = contex.ProductReviews.OrderByDescending(a => a.ReviewDate).Select(b => b.ProductID).Take(howManyReviews).ToList();
                result = contex.Products.Where(a => rev.Any(b => b == a.ProductID)).ToList();
            }

            return result;
        }

        public static List<Product> GetRecentlyReviewedProductsLinq(int howManyReviews)
        {
            List<Product> result;
            using (AdventureWorks2012Entities contex = new AdventureWorks2012Entities())
            {
                result = (from prod in contex.Products
                          from rev in contex.ProductReviews
                          where prod.ProductID == rev.ProductID
                          orderby rev.ReviewDate descending
                          select prod).Take(howManyReviews).ToList();
            }
            return result;
        }

        public static List<Product> GetNRecentlyReviewedProductsLambda(int howManyProducts)
        {
            List<Product> result;
            using (AdventureWorks2012Entities contex = new AdventureWorks2012Entities())
            {
                List<int> rev = contex.ProductReviews.OrderByDescending(a => a.ReviewDate).Select(b => b.ProductID).ToList();
                result = contex.Products.Distinct().Where(a => rev.Any(b => b == a.ProductID)).Take(howManyProducts).ToList();
            }
            return result;
        }

        public static List<Product> GetNRecentlyReviewedProductsLinq(int howManyProducts)
        {
            List<Product> result;
            using (AdventureWorks2012Entities contex = new AdventureWorks2012Entities())
            {
                result = (from prod in contex.Products
                          from rev in contex.ProductReviews
                          where prod.ProductID == rev.ProductID
                          orderby rev.ReviewDate descending
                          select prod).Distinct().Take(howManyProducts).ToList();
            }
            return result;
        }

        public static List<Product> GetProductsSortedByCategoryAndNameLambda(int n)
        {
            using (AdventureWorks2012Entities contex = new AdventureWorks2012Entities())
            {
                List<int> prodcat = contex.ProductCategories.OrderBy(a => a.ProductCategoryID).Select(b => b.ProductCategoryID).ToList();
                List<int> sub = contex.ProductSubcategories.Where(b => prodcat.Any(a => a == b.ProductCategoryID)).Select(c => c.ProductCategoryID).ToList();
                return contex.Products.Where(a => sub.Any(b => b == a.ProductSubcategoryID)).
                    OrderBy(c => c.ProductSubcategoryID).ThenBy(c => c.Name).Take(n).ToList();
            }
        }



        public static List<Product> GetProductsSortedByCategoryAndNameLinq(int n)
        {
            List<Product> result;
            using (AdventureWorks2012Entities contex = new AdventureWorks2012Entities())
            {
                result = (from prod in contex.Products
                          from sub in contex.ProductSubcategories
                          from cat in contex.ProductCategories
                          where
                          sub.ProductSubcategoryID == prod.ProductSubcategoryID
                          &&
                          cat.ProductCategoryID == sub.ProductCategoryID
                          orderby cat.ProductCategoryID, prod.Name ascending
                          select prod).Take(n).ToList();
            }
            return result;
        }

        public static int GetTotalStandardCostByCategoryLambda(ProductCategory category)
        {
            using (AdventureWorks2012Entities contex = new AdventureWorks2012Entities())
            {
                List<int> prodcat = contex.ProductCategories.Where(a => a.ProductCategoryID == category.ProductCategoryID).Select(b => b.ProductCategoryID).ToList();
                List<int> sub = contex.ProductSubcategories.Where(b => prodcat.Any(a => a == b.ProductCategoryID)).Select(b => b.ProductSubcategoryID).ToList();
                return (int)contex.Products.Where(a => sub.Any(b => b == a.ProductSubcategoryID)).Sum(a => a.ListPrice);
            }

        }

        public static int GetTotalStandardCostByCategoryLinq(ProductCategory category)
        {
            int result;
            using (AdventureWorks2012Entities contex = new AdventureWorks2012Entities())
            {
                int query = (int)(from prod in contex.Products
                                  from sub in contex.ProductSubcategories
                                  from cat in contex.ProductCategories
                                  where
                                  sub.ProductSubcategoryID == prod.ProductSubcategoryID
                                  &&
                                  cat.ProductCategoryID == sub.ProductCategoryID
                                  &&
                                  cat.ProductCategoryID == category.ProductCategoryID
                                  group prod by cat.ProductCategoryID into group1
                                  select group1.Sum(a => a.ListPrice)).First();
                result = query;
            }
            return result;
        }

    }
}
