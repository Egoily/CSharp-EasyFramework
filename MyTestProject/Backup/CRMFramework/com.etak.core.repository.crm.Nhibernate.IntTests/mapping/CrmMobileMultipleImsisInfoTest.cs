using System;
using com.etak.core.model;
using com.etak.core.repository.crm.subscription;
using NUnit.Framework;

namespace com.etak.core.repository.crm.Nhibernate.IntTests.mapping
{
    [TestFixture]
    public class CrmMobileMultipleImsisInfoTest : AbstractRepositoryTest<ICrmMobileMultipleImsiInfoRepository<CrmMobileMultipleImsiInfo>, CrmMobileMultipleImsiInfo, int>
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
                var crmMobileNetworkInfo =
                    repo.GetById(ExistingId);


                Console.Write("Accessing CrmMobileMultipleImsis: ");
                if (crmMobileNetworkInfo != null)
                    Console.WriteLine(" Value " + crmMobileNetworkInfo.IMSIID);
            });
        }


        protected override int ExistingId
        {
            get { return (1053002275); }
        }

    }
}
