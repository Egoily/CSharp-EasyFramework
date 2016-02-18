using System;
using System.Linq;
using com.etak.core.app.BenifitsRenewalEngine.Actions;
using com.etak.core.model;
using com.etak.core.model.revenueManagement;
using com.etak.core.repository;
using com.etak.core.repository.crm;
using com.etak.core.repository.crm.customer;
using com.etak.core.repository.crm.Nhibernate.Factory;
using com.etak.core.repository.crm.promotion;
using log4net;
using log4net.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BenifitsRenewalEngineSessionFactoryHelper = com.etak.core.app.BenifitsRenewalEngine.Repository.SessionFactory.SessionFactoryHelper;

namespace com.etak.core.app.BenifitsRenewalEngine.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
        }


        [ClassInitialize()]
        public static void InitializeSessionFactoryAndLogging(TestContext testContext)
        {
            log4net.Config.XmlConfigurator.Configure();

            Level originalLevel = ((log4net.Repository.Hierarchy.Hierarchy)LogManager.GetRepository()).Root.Level;
            ((log4net.Repository.Hierarchy.Hierarchy)LogManager.GetRepository()).Root.Level = Level.Info;
            ((log4net.Repository.Hierarchy.Hierarchy)LogManager.GetRepository()).RaiseConfigurationChanged(EventArgs.Empty);

            RepositoryManager.AddAssemby(typeof(SessionFactoryHelper).Assembly);
            RepositoryManager.AddAssemby(typeof(BenifitsRenewalEngineSessionFactoryHelper).Assembly);
            ((log4net.Repository.Hierarchy.Hierarchy)LogManager.GetRepository()).Root.Level = originalLevel;
            ((log4net.Repository.Hierarchy.Hierarchy)LogManager.GetRepository()).RaiseConfigurationChanged(EventArgs.Empty);
        }

        [TestMethod()]
        public void RenewTest()
        {
            AbstractPromotionRenewAction renewAction = new AccumulativePromotionRenewAction();


            using (var Connection = RepositoryManager.GetNewConnection())
            {
                var crmPromotionRepo =
                    RepositoryManager.GetRepository<ICrmCustomersPromotionInfoRepository<CrmCustomersPromotionInfo>>();
               var  custRepo = RepositoryManager.GetRepository<ICustomerInfoRepository<CustomerInfo>>();
                var rmPromotionplandetailRepo =
                    RepositoryManager.GetRepository<IRmPromotionPlanDetailInfoRepository<RmPromotionPlanDetailInfo>>();
                var customer = custRepo.LoadCustomerAndPromotionsByCustomerId(1673012646).First();
                var promotionPlandetail = rmPromotionplandetailRepo.GetById(1858000001);
                renewAction.RefferredPromotion = promotionPlandetail;

                using (var conn = RepositoryManager.GetConnection().BeginTransaction())
                {

                    try
                    {
                        renewAction.Renew(customer, new BillRun()
                        {
                            StarteDate = new DateTime(2015, 2, 1, 0, 0, 0),
                            EndDate = new DateTime(2015, 2, 28, 23, 59, 59)
                        }, new BillRun()
                        {
                            StarteDate = new DateTime(2015, 3, 1, 0, 0, 0),
                            EndDate = new DateTime(2015, 3, 31, 23, 59, 59)
                        });

                        conn.Commit();
                    }
                    catch (Exception ex)
                    {
                        conn.Rollback();
                        throw;
                    }
                 

                }
            }
        }
    }
}
