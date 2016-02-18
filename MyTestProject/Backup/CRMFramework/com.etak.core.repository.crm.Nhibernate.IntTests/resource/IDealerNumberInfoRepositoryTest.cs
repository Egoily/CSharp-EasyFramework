using System;
using System.Linq;
using com.etak.core.model;
using com.etak.core.repository.crm.resource;
using NUnit.Framework;

namespace com.etak.core.repository.crm.Nhibernate.IntTests.resource
{
    [TestFixture]
    public class IDealerNumberInfoRepositoryTest :
        AbstractRepositoryTest<IDealerNumberInfoRepository<DealerNumberInfo>, DealerNumberInfo, Int32>
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
                DealerNumberInfo nInfo =
                repo.GetById(ExistingId);
                if (nInfo != null)
                {
                    Console.Write("Accessing NumberInfo: ");

                    Console.WriteLine(" Value " + nInfo.Resource.TrafficTypeID);
                }
            });
        }

        [Test]
        public void GetByResource()
        {
            DoTransacted(repo =>
            {
                DealerNumberInfo nInfo =
                repo.GetByResource("34600000016").FirstOrDefault();
                if (nInfo != null)
                {
                    Console.Write("Accessing NumberInfo: ");
                    Console.WriteLine(" Value " + nInfo.Resource.TrafficTypeID);
                }
            });
        }

        protected override Int32 ExistingId
        {
            get { return (1955000923); }
        }
    }
}
