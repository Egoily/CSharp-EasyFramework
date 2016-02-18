using System;
using System.Linq;
using com.etak.core.app.BenifitsRenewalEngine.Actions;
using com.etak.core.model;
using com.etak.core.model.revenueManagement;
using com.etak.core.repository;
using com.etak.core.repository.crm.customer;
using com.etak.core.repository.crm.Nhibernate.Factory;
using log4net;
using log4net.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace com.etak.core.app.BenifitsRenewalEngine.Tests
{
    [TestClass()]
    public class PromotionRenewInAdvanceActionTests
    {
        [ClassInitialize()]
        public static void InitializeSessionFactoryAndLogging(TestContext testContext)
        {
            log4net.Config.XmlConfigurator.Configure();

            Level originalLevel = ((log4net.Repository.Hierarchy.Hierarchy)LogManager.GetRepository()).Root.Level;
            ((log4net.Repository.Hierarchy.Hierarchy)LogManager.GetRepository()).Root.Level = Level.Info;
            ((log4net.Repository.Hierarchy.Hierarchy)LogManager.GetRepository()).RaiseConfigurationChanged(EventArgs.Empty);

            RepositoryManager.AddAssemby(typeof(SessionFactoryHelper).Assembly);
            RepositoryManager.AddAssemby(typeof(Repository.SessionFactory.SessionFactoryHelper).Assembly);
            ((log4net.Repository.Hierarchy.Hierarchy)LogManager.GetRepository()).Root.Level = originalLevel;
            ((log4net.Repository.Hierarchy.Hierarchy)LogManager.GetRepository()).RaiseConfigurationChanged(EventArgs.Empty);
        }

        [TestMethod()]
        public void RunRenewal()
        {
            var preRenewAction = new PromotionRenewInAdvanceAction();

            const int customerId = 1676009514;
            const int promotionPlanDetailId = 1858000001;

            using (var connection = RepositoryManager.GetNewConnection())
            {
                var custRepo = RepositoryManager.GetRepository<ICustomerInfoRepository<CustomerInfo>>();
                var customer = custRepo.GetById(customerId);

                var promotionPlandetail = customer.Promotions.First(x => x.PromotionDetail.PromotionPlanDetailId == promotionPlanDetailId).PromotionDetail;
                preRenewAction.PreRefferredPromotion = promotionPlandetail;

                using (var conn = connection.BeginTransaction())
                {
                    preRenewAction.Renew(customer, new BillRun()
                    {
                        StarteDate = new DateTime(2015, 1, 15, 0, 0, 0),
                        EndDate = new DateTime(2015, 1, 31, 23, 59, 59)
                    }, new BillRun()
                    {   
                        StarteDate = new DateTime(2015, 2, 1, 0, 0, 0),
                        EndDate = new DateTime(2015, 2, 28, 23, 59, 59)
                    });
                }
            }
        }
    }
}
