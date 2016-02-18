using System;
using System.Linq;
using com.etak.core.model;
using com.etak.core.repository.crm.portability;
using NUnit.Framework;

namespace com.etak.core.repository.crm.Nhibernate.IntTests.portability
{
    [TestFixture]
    public class IMNPPortabilityInfoRepositoryTest :
        AbstractRepositoryTest<IMNPPortabilityInfoRepository<MNPPortabilityInfo>, MNPPortabilityInfo, String>

    {
        [TestFixtureSetUp]
        public static void Init()
        {
            //InitializeSessionFactoryAndLogging();
        }

        [Test]
        public void GetByMSISDN()
        {
            DoTransacted(repo => { var prod = repo.GetByMSISDN("3456").FirstOrDefault() ; });
        }


        [Test]
        public void GetLatestIncomingByMsisdn()
        {
            DoTransacted(repo => { var prod = repo.GetLatestIncomingByMsisdn("34634063169"); });
        }
        

        protected override string ExistingId
        {
            get { return ("1234567"); }
        }
    }
}
