using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.repository.crm.Nhibernate.Factory;
using NUnit.Framework;

namespace com.etak.core.repository.crm.Nhibernate.IntTests
{
    [TestFixture]
    public class ILoginInfoRepositoryTest
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
                var prodRepo = RepositoryManager.GetRepository<ILoginInfoRepository<LoginInfo>>();
                LoginInfo ProductId1 = prodRepo.GetById(14);
            }
        }

        [Test]
        public void GetByUserId()
        {
            using (var conn = RepositoryManager.GetNewConnection())
            {
                var prodRepo = RepositoryManager.GetRepository<ILoginInfoRepository<LoginInfo>>();
                IEnumerable<LoginInfo> ProductId1 = prodRepo.GetByUserId(14);
            }
        }

       
    }
}
