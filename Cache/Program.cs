using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICA6;
using BazzasBazaarExternalServiceProxyWCF;
using DodgeyDealersExternalServiceProxy;
using KhansKwikimartExternalServiceProxy;
using ExternalServiceProxy;
using System.Threading.Tasks;
using System.Data.Entity;
using System.IO;
using System.Web.Http;
using System.Net.Http.Formatting;
using System.Net.Http;


namespace cache
{
    class Program
    {
        static void Main(string[] args)
        {
            

            
            ExternalServiceProxy.ServiceProxy proxy = new ExternalServiceProxy.ServiceProxy();
            proxy.GetAllProducts();


            productsEntities2 db = new productsEntities2();
            IEnumerable<ExternalServiceProxy.DTO.ProductDTO> products = proxy.GetAllProducts(); 
            //Undercutters products

            try
            {
                foreach (ExternalServiceProxy.DTO.ProductDTO p in products)
                {
                    var test = new cache.products { productname = p.Name, productid = p.Id, productdescription = p.Description, categoryid = p.CategoryId, categoryname = p.CategoryName, brandid = p.BrandId, minprice = p.Price, maxprice = p.Price };

                    db.products.Add(test);
                    db.SaveChanges();
                }

            }

            catch (Exception e)
            {

                Console.WriteLine(e.ToString());

            }

            //bazzas
            //BazzasBazaarExternalServiceProxyWCF.ServiceProxy proxy2 = new BazzasBazaarExternalServiceProxyWCF.ServiceProxy();
            //Task<IEnumerable<BazzasBazaarExternalServiceProxyWCF.DTO.ProductDTO>> products6 = await proxy2.getAllCategories();
            //IEnumerable<BazzasBazaarExternalServiceProxyWCF.DTO.ProductDTO> products2 = await proxy2.getFilteredProducts();

            //try
            //{
            //    foreach (BazzasBazaarExternalServiceProxyWCF.DTO.ProductDTO p in products2)
            //    {
            //        var test = new cache.products { };

            //        db.products.Add(test);
            //        db.SaveChanges();
            //    }

            //}

            //catch (Exception e)
            //{

            //    Console.WriteLine(e.ToString());

            //}


            //DodgeyDealers
            DodgeyDealersExternalServiceProxy.ServiceProxy proxy3 = new DodgeyDealersExternalServiceProxy.ServiceProxy();
            IEnumerable<DodgeyDealersExternalServiceProxy.DTO.ProductDTO> products3 = proxy3.GetAllProducts();

            try
            {
                foreach (DodgeyDealersExternalServiceProxy.DTO.ProductDTO p in products3)
                {
                    var test = new cache.products { productname = p.Name, productid = p.Id, productdescription = p.Description, categoryid = p.CategoryId, categoryname = p.CategoryName, brandid = p.BrandId, minprice = p.Price, maxprice = p.Price };

                    db.products.Add(test);
                    db.SaveChanges();
                }

            }

            catch (Exception e)
            {

                Console.WriteLine(e.ToString());

            }

            //khansKwikimart
            KhansKwikimartExternalServiceProxy.ServiceProxy proxy4 = new KhansKwikimartExternalServiceProxy.ServiceProxy();
            IEnumerable<KhansKwikimartExternalServiceProxy.DTO.RangeDTO> products4 = proxy4.GetGiftWrappingByRange();
            IEnumerable<KhansKwikimartExternalServiceProxy.DTO.TypeDTO> products5 = proxy4.GetGiftWrappingByType();

            for (int i = 0; i < products4.ToList().Count; i++)
            {
                KhansKwikimartExternalServiceProxy.DTO.RangeDTO currentrange = products4.ToList()[i];
                for (var j = 0; j < products5.ToList().Count; j++)
                {
                    KhansKwikimartExternalServiceProxy.DTO.TypeDTO currenttype = products5.ToList()[i];

                    IEnumerable<KhansKwikimartExternalServiceProxy.DTO.ProductDTO> currentproduct = proxy4.GetGiftWrapping(currenttype.Id, currentrange.Id, 0, 100, 0, 100);

                    try
                    {
                        foreach (KhansKwikimartExternalServiceProxy.DTO.ProductDTO p in currentproduct)
                        {
                            KhansKwikimartExternalServiceProxy.DTO.ProductDTO current = currentproduct.ToList()[i];
                            var test = new cache.wrappings { id = current.Id, typeid = current.Id, typename = current.TypeName, rangeid = current.RangeId, rangename = current.RangeName, price = current.Price, size = current.Size };

                            db.wrappings.Add(test);
                            db.SaveChanges();
                        }

                    }

                    catch (Exception e)
                    {

                        Console.WriteLine(e.ToString());

                    }
                }
            }


        }

    }
}
