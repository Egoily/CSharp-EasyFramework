using System;
using System.Linq;
using com.etak.core.model;
using NUnit.Framework;
using com.etak.core.repository.crm.Nhibernate.Factory;


namespace com.etak.core.repository.crm.Nhibernate.IntTests
{
    [TestFixture]
    public class IVoucherCardRepositoryTests
    {
        [TestFixtureSetUp]
        public static void InitializeSessionFactoryAndLogging()
        {
            log4net.Config.XmlConfigurator.Configure();
            var instance = RepositoryManagerSingleton.Instance;
            //SessionFactoryTest.BuildSessionFactory(SessionFactoryProviders.Mongo);
        }

        [Test]
        public void GetByMVNOIDAndCode()
        {
            using (var conn = RepositoryManager.GetNewConnection())
            {

                var opLogRepo = RepositoryManager.GetRepository<IVoucherCardInfoRepository<VoucherCardInfo>>();
                var list = opLogRepo.GetByCodeAndMvnoId("2853215543022119", 700000);
                var item = list.FirstOrDefault();
                Console.WriteLine(item.VcEncrypt);
            }

        }

    }
}
