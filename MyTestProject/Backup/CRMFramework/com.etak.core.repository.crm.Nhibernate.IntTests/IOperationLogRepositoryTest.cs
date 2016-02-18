using System;
using com.etak.core.model;
using NUnit.Framework;
using com.etak.core.repository.crm.Nhibernate.Factory;


namespace com.etak.core.repository.crm.Nhibernate.IntTests
{
    [TestFixture]
    public class IOperationLogRepositoryTest
    {
        [TestFixtureSetUp]
        public static void InitializeSessionFactoryAndLogging()
        {
            log4net.Config.XmlConfigurator.Configure();
            var instance = RepositoryManagerSingleton.Instance;
            //SessionFactoryTest.BuildSessionFactory(SessionFactoryProviders.Mongo);
        }

        [Test]
        public void GetById()
        {
            using (var conn = RepositoryManager.GetNewConnection())
            {

                var opLogRepo = RepositoryManager.GetRepository<IOperationLogRepository<OperationLog>>();
                opLogRepo.GetById(1);
            }

        }

        [Test]
        public void GetByOrderCodeAndDealerId()
        {
            using (RepositoryManager.GetNewConnection())
            {
                var opLogRepo = RepositoryManager.GetRepository<IOperationLogRepository<OperationLog>>();
                opLogRepo.GetByOrderCodeAndDealerId("orderCode", -1);
            }
        }


        [Test]
        public void Create()
        {
            using (var conn = RepositoryManager.GetNewConnection())
            {
                using (var trx = conn.BeginTransaction())
                {
                    var opLogRepo = RepositoryManager.GetRepository<IOperationLogRepository<OperationLog>>();
                    try
                    {
                        OperationLog log = CreateOperationLog();
                        log.Remark = null;
                        opLogRepo.Create(log);
                        trx.Commit();
                    }
                    catch (Exception ex)
                    {
                        
                    }
                    
                }
            }
        }

        static OperationLog CreateOperationLog()
        {
            OperationLog opLog = new OperationLog
            {
                Channel = "TestChannel",
                //Code = DateTime.UtcNow.Ticks,
                DealerID = -1,
                Description = "Unit Test Channel",
                ExternalCode = "externalCode",
                ForeignCode = "123556",
                InvokeParams = new byte[] { },
                Invoker = -1,
                LOGID = 145,
                Messages = "Unit Test Message",
                Name1 = "NAME1",
                Name2 = "NAME2",
                Name3 = "NAME3",
                Name4 = "NAME4",
                Name5 = "NAME5",
                Name6 = "NAME6",
                Name7 = "NAME7",
                Name8 = "NAME8",
                Name9 = "NAME9",
                Name10 = "NAME10",
                UserID = 1,
            };
            return opLog;
        }
    }
}
