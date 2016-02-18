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
using System.Linq.Expressions;

namespace com.etak.core.repository.crm.Nhibernate.IntTests.inventory
{
    [TestFixture]
    public class CustomerOrderTest
    {
        [TestFixtureSetUp]
        public static void InitializeSessionFactoryAndLogging()
        {
            log4net.Config.XmlConfigurator.Configure();
            var instance = RepositoryManagerSingleton.Instance;
        }
        [Test]
        public void CreateCustomerOrderTest()
        {

            using (var conn = RepositoryManager.GetNewConnection())
            {
                using (var trx = conn.BeginTransaction())
                {
                    Int32 dealerId = 170000;
                    IDealerInfoRepository<DealerInfo> dealerRepo = RepositoryManager.GetRepository<IDealerInfoRepository<DealerInfo>>();
                    DealerInfo dealer = dealerRepo.GetByDealerIdAndCache(dealerId).FirstOrDefault();

                    int customerId = 1020000005;
                    ICustomerInfoRepository<CustomerInfo> ctmRepo = RepositoryManager.GetRepository<ICustomerInfoRepository<CustomerInfo>>();
                    CustomerInfo cust = ctmRepo.GetById(customerId);

                    int prodId = 1020000632;
                    IProductRepository<Product> prodRepo = RepositoryManager.GetRepository<IProductRepository<Product>>();
                    var prod = prodRepo.GetById(prodId);

                    var repoCC = RepositoryManager.GetRepository<IOrderRepository<CustomerOrder>>();

                    CustomerOrder cc = new CustomerOrder()
                    {
                        Dealer = dealer,
                        Customer = cust,
                        CreationDate =  System.DateTime.Now,
                        CompletitionDate = System.DateTime.Now,
                        LastUpdateDate = System.DateTime.Now,
                    };
                    cc.OrderItems = new List<OrderItem>();
                    for (int i = 0; i < 3; i++)
                    {
                        OrderItem item = new OrderItem()
                        {
                            Order = cc,
                            Price = i * 100,
                            Quantity = i * 10,
                            Product = prod
                        };
                        cc.OrderItems.Add(item);
                    }

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

        [Test]
        public void GetCustomerOrderByDealerTest()
        {
            using (var conn = RepositoryManager.GetNewConnection())
            {
                Int32 dealerId = 170000;
                IDealerInfoRepository<DealerInfo> dealerRepo = RepositoryManager.GetRepository<IDealerInfoRepository<DealerInfo>>();
                DealerInfo dealer = dealerRepo.GetByDealerIdAndCache(dealerId).FirstOrDefault();

                var repoCC = RepositoryManager.GetRepository<IOrderRepository<CustomerOrder>>();
                var list = new List<Expression<Func<CustomerOrder, bool>>>();
                list.Add(x => x.Customer.CustomerID == 1);

                var listOfOrder = repoCC.GetByDealerAndFilters(dealer, list,2, 10).ToList();
                                
            }
        }
    }
}
