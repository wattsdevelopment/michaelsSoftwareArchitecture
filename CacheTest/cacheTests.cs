using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Data;
using System.Data.Entity;

namespace CacheTest
{
    [TestClass]
    public class cacheTests
    {

        [TestMethod]
            public void GetConnStringFromAppConfig()
        {
                cache.productsEntities2 db = new cache.productsEntities2();
                string actualString = db.Database.Connection.ConnectionString;
                string expectedString = ConfigurationManager.ConnectionStrings["DatabaseConnection"].ConnectionString;
                Assert.AreEqual(expectedString, actualString);
            }

        //[TestMethod]
        //    public void ConnectAndDisconnectFromDatabase()
        //{
        //        cache.productsEntities2 db = new cache.productsEntities2();
        //        bool connected = db.Connect();
        //        bool disconnected = db.Disconnect();
        //        Assert.IsTrue(connected);
        //        Assert.IsTrue(disconnected);
        //    }


        [TestMethod]
        public void productsTest()
        {
            cache.productsEntities2 db = new cache.productsEntities2();
            //db.products.Select();

            var test1 = db.products;

            IEnumerable<testProducts> testing = test1.Select(o => new testProducts
            {
                productid = o.productid,

                productname = o.productname,

                productdescription = o.productdescription,

                categoryid = o.categoryid ?? 0,

                categoryname = o.categoryname,

                brandid = o.brandid ?? 0,

                minprice = o.minprice,

                maxprice = o.maxprice,
            });

            Assert.AreEqual(testing.Count(), 17);

        }

        [TestMethod]
        public void giftwrappingTest()
        {
            cache.productsEntities2 db = new cache.productsEntities2();
            //db.wrappings.Select();

            var test1 = db.wrappings;

            IEnumerable<testWrapping> testing = test1.Select(o => new testWrapping
            {
                id = o.id,

                typeid = o.typeid ?? 0,

                typename = o.typename,

                rangeid = o.rangeid ?? 0,

                rangename = o.rangename,

                price = o.price,

                size = o.size,
            });

            Assert.AreEqual(testing.Count(), 1);


        }
    }
}
