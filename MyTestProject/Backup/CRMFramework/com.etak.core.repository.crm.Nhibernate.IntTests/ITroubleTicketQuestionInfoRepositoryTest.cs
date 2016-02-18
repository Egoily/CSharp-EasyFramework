using com.etak.core.model;
using com.etak.core.repository.crm.services;
using NUnit.Framework;

namespace com.etak.core.repository.crm.Nhibernate.IntTests
{
    [TestFixture]
    public class ITroubleTicketQuestionInfoRepositoryTest
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
                var ttrepo = RepositoryManager.GetRepository<ITroubleTicketQuestionInfoRepository<TroubleTicketQuestionInfo>>();
                ttrepo.GetById(1);
            }
        }

        [Test]
        public void GetByTypeAndSubTypeAndMvnoId()
        {
            using (var conn = RepositoryManager.GetNewConnection())
            {
                var ttrepo = RepositoryManager.GetRepository<ITroubleTicketQuestionInfoRepository<TroubleTicketQuestionInfo>>();
                var res = ttrepo.GetByTypeAndSubTypeAndMvnoId("1007", "2004", 140000);
            }
        }
    }
}
