using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model;
using com.etak.core.model.subscription.catalog;
using com.etak.core.repository.crm.subscription.catalog;
using com.etak.core.test.utilities;
using NUnit.Framework;

namespace com.etak.core.repository.crm.Nhibernate.IntTests.subscription.catalog
{
    [TestFixture]
    public class ProductOfferingCatalogTest
    {
        [TestFixtureSetUp]
        public static void InitializeSessionFactoryAndLogging()
        {
            log4net.Config.XmlConfigurator.Configure();
            var instance = RepositoryManagerSingleton.Instance;
        }

        [Test]
        public void CreateProductOfferingCatalog()
        {
            using (var conn = RepositoryManager.GetNewConnection())
            {
                using (var trx = conn.BeginTransaction())
                {
                    var repoProductOfferingCatalog =
                        RepositoryManager.GetRepository<IProductOfferingCatalogRepository<ProductOfferingCatalog>>();

                    var productOfferingCatalog = CreateDefaultObject.Create<ProductOfferingCatalog>();
                    var repoProductOffering =
                        RepositoryManager.GetRepository<IProductOfferingRepository<ProductOffering>>();
                    var producOffering1 = repoProductOffering.GetById(1020000001);
                    var producOffering2 = repoProductOffering.GetById(1020000002);
                    productOfferingCatalog.ProductOfferingsInCatalog = new List<ProductOffering>() { producOffering1, producOffering2};

                    var repoDealerInfo =
                       RepositoryManager.GetRepository<IDealerInfoRepository<DealerInfo>>();
                    var dealerInfo = repoDealerInfo.GetById(1);
                    productOfferingCatalog.MNO = dealerInfo;

                    try
                    {
                        repoProductOfferingCatalog.Create(productOfferingCatalog);
                        trx.Commit();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }

        [Test]
        public void QueryProductOfferingCatalog()
        {
            using (var conn = RepositoryManager.GetNewConnection())
            {
                var repoProductOfferingCatalog =
                        RepositoryManager.GetRepository<IProductOfferingCatalogRepository<ProductOfferingCatalog>>();

                int id = 1;
                try
                {
                    var css = repoProductOfferingCatalog.GetById(id);
                    if (css != null)
                    {
                        Console.Write(css.Id);
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw ex;
                }

            }
        }

    }
}
