using System;
using System.Linq;
using com.etak.core.model;
using com.etak.core.repository.crm.resource;
using NUnit.Framework;

namespace com.etak.core.repository.crm.Nhibernate.IntTests.resource
{
    [TestFixture]
    public class IMVNOAPNIPPoolInfoRepositoryTest :
        AbstractRepositoryTest<IMVNOAPNIPPoolInfoRepository<MVNOAPNIPPoolInfo>, MVNOAPNIPPoolInfo, Int32>

    {
        [TestFixtureSetUp]
        public static void Init()
        {
            //InitializeSessionFactoryAndLogging();
        }

        [Test]
        public override void GetById()
        {
            DoTransacted(repo =>
            {
                MVNOAPNIPPoolInfo nInfo = 
                repo.GetById(ExistingId);

                Console.Write("Accessing NumberInfo: ");
                Console.WriteLine(" Value " + nInfo.MSISDN);
            });
        }

        [Test]
        public void GetByMsisdn()
        {
            DoTransacted(repo =>
            {
                MVNOAPNIPPoolInfo nInfo =
                repo.GetByMsisdn("34692999951").FirstOrDefault();

                Console.Write("Accessing NumberInfo: ");
                Console.WriteLine(" Value " + nInfo.MSISDN);
            });
        }

        protected override Int32 ExistingId
        {
            get { return (1310000075); }
        }
    }
}
