using System;
using System.Collections.Generic;
using System.Linq;
using com.etak.core.app.BenifitsRenewalEngine.Actions;
using com.etak.core.model;
using com.etak.core.model.revenueManagement;
using com.etak.core.repository;
using com.etak.core.repository.crm.customer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace com.etak.core.app.BenifitsRenewalEngine.Tests
{
    [TestClass()]
    public class GenerateInformationalChargeActionTests
    {
        [ClassInitialize()]
        public static void InitializeSessionFactoryAndLogging(TestContext testContext)
        {
            log4net.Config.XmlConfigurator.Configure();
            RepositoryManager.AddAssemby(typeof(com.etak.core.repository.crm.Nhibernate.Factory.SessionFactoryHelper).Assembly);
        }

        [TestMethod()]
        public void RenewTest()
        {
            var renewAction = new GenerateInformationalChargeAction();

            IList<int> customerList = new List<int>{1673011229,1673011233,1674009621 };
            const int promotionPlanDetailId = 1858000002;

            foreach (var customerId in customerList)
            {
                using (var connection = RepositoryManager.GetNewConnection())
                {
                    var custRepo = RepositoryManager.GetRepository<ICustomerInfoRepository<CustomerInfo>>();
                    var customer = custRepo.LoadCustomerAndPromotionsByCustomerId(customerId).First();
                    foreach (var prom in customer.Promotions)
                    {
                        NHibernate.NHibernateUtil.Initialize(prom.PromotionDetail);
                    }

                    var promotionPlandetail = customer.Promotions.First(x => x.PromotionDetail.PromotionPlanDetailId == promotionPlanDetailId).PromotionDetail;
                    renewAction.RefferredPromotion = promotionPlandetail;

                    using (var tran = connection.BeginTransaction())
                    {
                        try
                        {
                            renewAction.Renew(customer, new BillRun()
                            {
                                StarteDate = new DateTime(2015, 5, 1, 0, 0, 0),
                                EndDate = new DateTime(2015, 5, 31, 23, 59, 59)
                            }, new BillRun()
                            {
                                StarteDate = new DateTime(2015, 6, 1, 0, 0, 0),
                                EndDate = new DateTime(2015, 6, 30, 23, 59, 59)
                            });

                            tran.Commit();
                        }
                        catch (Exception ex)
                        {
                            tran.Rollback();
                        }
                    }
                }
            }
        }
    }
}