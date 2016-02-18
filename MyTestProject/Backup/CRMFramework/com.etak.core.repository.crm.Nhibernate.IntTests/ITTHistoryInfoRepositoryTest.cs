using com.etak.core.model;
using com.etak.core.repository.crm.services;
using NUnit.Framework;

namespace com.etak.core.repository.crm.Nhibernate.IntTests
{
    [TestFixture]
    public class ITTHistoryInfoRepositoryTest
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
                var ttHistoryInfoRepo = RepositoryManager.GetRepository<ITTHistoryInfoRepository<TTHistoryInfo>>();
                ttHistoryInfoRepo.GetById(1);
            }
        }
    }
}
