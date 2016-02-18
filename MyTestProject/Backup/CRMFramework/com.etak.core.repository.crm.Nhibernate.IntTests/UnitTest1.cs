using System;
using com.etak.core.model;
using com.etak.core.repository.crm.Nhibernate.Factory;
using log4net;
using log4net.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace com.etak.core.repository.crm.Nhibernate.test
{
    [TestClass]
    public class UnitTest1
    {
        [ClassInitialize()]
        public static void InitializeSessionFactoryAndLogging(TestContext testContext) 
        {
            log4net.Config.XmlConfigurator.Configure();
            Level originalLevel = ((log4net.Repository.Hierarchy.Hierarchy)LogManager.GetRepository()).Root.Level;
            ((log4net.Repository.Hierarchy.Hierarchy)LogManager.GetRepository()).Root.Level = Level.Info;
            ((log4net.Repository.Hierarchy.Hierarchy)LogManager.GetRepository()).RaiseConfigurationChanged(EventArgs.Empty);
            RepositoryManager.AddAssemby(typeof(SessionFactoryHelper).Assembly);
            ((log4net.Repository.Hierarchy.Hierarchy)LogManager.GetRepository()).Root.Level = originalLevel;
            ((log4net.Repository.Hierarchy.Hierarchy)LogManager.GetRepository()).RaiseConfigurationChanged(EventArgs.Empty);
        }

        [ClassCleanup()]
        public static void MyClassCleanup() { }

        [TestMethod]
        public void TestMethod1()
        {
            using (var Connection = RepositoryManager.GetNewConnection())
            {
                var custRepo = RepositoryManager.GetRepository<ICustomerInfoRepository<CustomerInfo>>();
                var customer = custRepo.GetById(1121000024);
            }
        }

        [TestMethod]
        public void TestMethod2()
        {
            using (var Connection = RepositoryManager.GetNewConnection())
            {
                var repo = RepositoryManager.GetRepository<IOperationLogRepository<OperationLog>>();
                //var dealerNumberInfo = new DealerNumberInfo() { DealerID = 210003 };
                repo.Create(new OperationLog() { });
            }
        }
    }
}
