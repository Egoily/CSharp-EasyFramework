using System;
using System.Linq;
using com.etak.core.model;
using com.etak.core.model.revenueManagement;
using com.etak.core.model.subscription.catalog;
using com.etak.core.repository.crm.customer;
using com.etak.core.repository.crm.Nhibernate.Factory;
using com.etak.core.repository.crm.revenueManagement;
using com.etak.core.repository.crm.subscription.catalog;
using NUnit.Framework;

namespace com.etak.core.repository.crm.Nhibernate.IntTests.reveneu
{
    [TestFixture]
    public class CustomerProductAssignmentRepositoryTest
    {
        [TestFixtureSetUp]
        public static void InitializeSessionFactoryAndLogging()
        {
            log4net.Config.XmlConfigurator.Configure();
            var instance = RepositoryManagerSingleton.Instance;
        }

        [Test]
        public void GetById()
        {
            using (var conn = RepositoryManager.GetNewConnection())
            {
                var prodRepo = RepositoryManager.GetRepository<ICustomerProductAssignmentRepository<CustomerProductAssignment>>();
                CustomerProductAssignment ProductId1 = prodRepo.GetById(1);
            }
        }

        [Test]
        public void GetByCustomerId()
        {
            using (var conn = RepositoryManager.GetNewConnection())
            {
                Int32 CustomerId = 1222;

                var prodRepo = RepositoryManager.GetRepository<ICustomerProductAssignmentRepository<CustomerProductAssignment>>();
                var assn = prodRepo.GetByCustomerId(CustomerId);
            }
        }

        [Test]
        public void GetByCustomerIdWithRange()
        {
            using (var conn = RepositoryManager.GetNewConnection())
            {
                Int32 CustomerId = 1222;
                DateTime DateStart = DateTime.Now.AddDays(-100);
                DateTime DateEnd = DateTime.Now.AddDays(-100);

                var prodRepo = RepositoryManager.GetRepository<ICustomerProductAssignmentRepository<CustomerProductAssignment>>();
                var assn = prodRepo.GetByCustomerIdWithRange(CustomerId, DateStart, DateEnd);
            }
        }

        [Test]
        public void GetByCustomerWhereDateRangesInDate()
        {
            using (var conn = RepositoryManager.GetNewConnection())
            {
                Int32 CustomerId = 1222;

                var prodRepo = RepositoryManager.GetRepository<ICustomerProductAssignmentRepository<CustomerProductAssignment>>();
                var assn = prodRepo.GetByCustomerWhereDateRangesInDate(CustomerId, DateTime.Now).FirstOrDefault();
            }
        }


        [Test]
        public void Update()
        {
            using (var conn = RepositoryManager.GetNewConnection())
            {
                var prodRepo = RepositoryManager.GetRepository<ICustomerProductAssignmentRepository<CustomerProductAssignment>>();
                CustomerProductAssignment ProductId1 = prodRepo.GetById(1);
                if (ProductId1 != null)
                {
                    ProductId1.EndDate = DateTime.Now;
                    prodRepo.Update(ProductId1);
                }

            }
        }

        [Test]
        public void CreatecustomerProductAssociation()
        {
            using (var conn = RepositoryManager.GetNewConnection())
            {
                using (var trx = conn.BeginTransaction())
                {
                    var repoCPA =
                        RepositoryManager.GetRepository<ICustomerProductAssignmentRepository<CustomerProductAssignment>>
                            ();
                    var repoCust = RepositoryManager.GetRepository<ICustomerInfoRepository<CustomerInfo>>();
                    var prodRepo = RepositoryManager.GetRepository<IProductOfferingRepository<ProductOffering>>();

                    ProductOffering productOffering = prodRepo.GetById(1003000000);
                    CustomerInfo cust = repoCust.GetById(5000204);

                    if (productOffering != null && cust != null)
                    {
                        CustomerProductAssignment CPA = new CustomerProductAssignment()
                        {
                            PurchasingCustomer = cust,
                            ProductChargePurchased = productOffering.ChargingOptions.First(),
                            PurchasedProduct = productOffering.OfferedProduct,
                            EndDate = DateTime.Now,
                            StartDate = DateTime.Now,
                            CreateDate = DateTime.Now

                        };

                        repoCPA.Create(CPA);

                        trx.Commit();
                    }



                }
            }
        }

    }
}
