using System;
using com.etak.core.model;
using com.etak.core.model.operation;
using com.etak.core.repository.crm.configuration;
using Newtonsoft.Json;
using NUnit.Framework;

namespace com.etak.core.repository.crm.Nhibernate.IntTests.operation
{


    [TestFixture]
    public class OperationConfigurationTest :
        AbstractRepositoryTest<IOperationConfigurationRepository<OperationConfiguration>, OperationConfiguration, Int32>
    {
        public class TestConfig
        {
            public String Var1 { get; set; }
            public String Var2 { get; set; }
        }

        protected override Int32 ExistingId
        {
            get { return 1; }
        }

        [TestFixtureSetUp]
        public static void EnsureInitializeSessionFactoryAndLogging()
        {
            //InitializeSessionFactoryAndLogging();
        }

       

        [Test]
        public void PersistConfig()
        {
            Action<IOperationConfigurationRepository<OperationConfiguration>> func = repo =>
            {
                IDealerInfoRepository<DealerInfo> dealerRepo = RepositoryManager.GetRepository<IDealerInfoRepository<DealerInfo>>();
                var dealer = dealerRepo.GetById(1234567);
                OperationConfiguration configToPersist = new OperationConfiguration
                {
                    StarTime = DateTime.Now,
                    EndDate = DateTime.Now.AddYears(1),
                    //OperationCode = "TEST",
                    //OperationDiscriminator = "TEST",
                    MVNO = dealer,
                    JSonConfig = JsonConvert.SerializeObject(new TestConfig { Var1 = "123", Var2 = "1234"}),

                };
                repo.Create(configToPersist);
            };

            DoTransacted(func);
        }
    }
}
