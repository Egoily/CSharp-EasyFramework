using System;
using System.Linq;
using NUnit.Framework;
using com.etak.core.model.inventory;
using com.etak.core.model;
using System.Collections.Generic;
using com.etak.core.model.order;
using com.etak.core.repository.crm.customer;
using com.etak.core.repository.crm.revenueManagement;
using com.etak.core.model.revenueManagement;
using com.etak.core.repository.crm.operation;

namespace com.etak.core.repository.crm.Nhibernate.IntTests.inventory
{
    [TestFixture]
    public class ProductInventoryTest
    {
        [TestFixtureSetUp]
        public static void InitializeSessionFactoryAndLogging()
        {
            log4net.Config.XmlConfigurator.Configure();
            var instance = RepositoryManagerSingleton.Instance;
        }
        [Test]
        public void CreateProductInventoryTest()
        {

            using (var conn = RepositoryManager.GetNewConnection())
            {
                using (var trx = conn.BeginTransaction())
                {
                    Int32 dealerId = 170000;
                    IDealerInfoRepository<DealerInfo> dealerRepo = RepositoryManager.GetRepository<IDealerInfoRepository<DealerInfo>>();
                    DealerInfo dealer = dealerRepo.GetByDealerIdAndCache(dealerId).FirstOrDefault();

                    int prodId = 1020000632;
                    IProductRepository<Product> prodRepo = RepositoryManager.GetRepository<IProductRepository<Product>>();
                    var prod = prodRepo.GetById(prodId);

                    var repoCC = RepositoryManager.GetRepository<IProductInventoryRepository<ProductInventory>>();

                    ProductInventory cc = new ProductInventory()
                    {
                        Product = prod as PhysicalProduct,
                        AvailabeQuantity = 100,
                        BeginningQuantity = 200,
                    };
                    try
                    {
                        repoCC.Create(cc);
                        trx.Commit();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }
    }
}
