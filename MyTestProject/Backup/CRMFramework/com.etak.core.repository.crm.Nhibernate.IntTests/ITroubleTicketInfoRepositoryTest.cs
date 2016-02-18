using com.etak.core.model;
using com.etak.core.repository.crm.services;
using NUnit.Framework;

namespace com.etak.core.repository.crm.Nhibernate.IntTests
{
    [TestFixture]
    public class ITroubleTicketInfoRepositoryTest
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
                var ttrepo = RepositoryManager.GetRepository<ITroubleTicketInfoRepository<TroubleTicketInfo>>();
                ttrepo.GetById(1);
            }
        }

        [Test]
        public void GetByCustomerId()
        {
            using (var conn = RepositoryManager.GetNewConnection())
            {
                var ttrepo = RepositoryManager.GetRepository<ITroubleTicketInfoRepository<TroubleTicketInfo>>();
                var res = ttrepo.GetByCustomerId(1674014738);
            }
        }

        [Test]
        public void GetByTicketNumber()
        {
            using (var conn = RepositoryManager.GetNewConnection())
            {
                var ttrepo = RepositoryManager.GetRepository<ITroubleTicketInfoRepository<TroubleTicketInfo>>();
                var res = ttrepo.GetByTicketNumber("12345");
            }
        }
    }
}
