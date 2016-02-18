using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model;
using com.etak.core.repository.crm.subscription;
using NHibernate.Mapping;
using NUnit.Framework;

namespace com.etak.core.repository.crm.Nhibernate.IntTests.resource
{

    [TestFixture]
    public class IResourceMBRepositoryTest : AbstractRepositoryTest<IResourceMBRepository<ResourceMBInfo>, ResourceMBInfo, Int32>
    {
        [TestFixtureSetUp]
        public static void InitTest()
        {
            //InitializeSessionFactoryAndLogging();
        }

        [Test]
        public override void GetById()
        {
            DoTransacted(repo =>
            {
                ResourceMBInfo resource = repo.GetById(ExistingId);

                Console.Write("Accessing ResourceMBInfo: ");
                Console.WriteLine(" Value " + resource.Resource);
            });
        }

        [Test]
        public void GetLastByDealerIdAndMSISDNAndStatusNotIn()
        {
            DoTransacted(repo =>
            {
                String msisdn = "34602623025";
                DealerInfo dealer = RepositoryManager.GetRepository<IDealerInfoRepository<DealerInfo>>().GetMVNOByDealerId(700000);
                IEnumerable<ResourceMBInfo> resourceMbInfos = repo.GetLastByDealerIdAndMSISDNAndStatusNotIn(dealer,msisdn, new List<int>());
                var resource = resourceMbInfos.FirstOrDefault();
                
                Assert.AreEqual(resource.Resource, msisdn);
            });
        }

        [Test]
        public void GetByMSISDNAndStatusNotIn()
        {
            DoTransacted(repo =>
            {
                String msisdn = "34602623025";
                IEnumerable<ResourceMBInfo> resourceMbInfos = repo.GetByMSISDNAndStatusNotIn(msisdn, new List<int>(){20});
                var resource = resourceMbInfos.FirstOrDefault();

                Assert.AreEqual(resource.Resource, msisdn);
            });
        }

        protected override int ExistingId
        {
            get { return 1656000286; }
        }
    }
}
