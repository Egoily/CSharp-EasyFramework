using System;
using System.Linq;
using com.etak.core.model;
using com.etak.core.repository.crm.portability;
using NUnit.Framework;


namespace com.etak.core.repository.crm.Nhibernate.IntTests.portability
{
    [TestFixture]
    public class IMNPPortabilityMultiPortInInfoRepositoryTest :
        AbstractRepositoryTest<IMNPPortabilityMultiPortInInfoRepository<MNPPortabilityMultiPortInInfo>, MNPPortabilityMultiPortInInfo, Int64>

    {
        [TestFixtureSetUp]
        public static void Init()
        {
            //InitializeSessionFactoryAndLogging();
        }

        [Test]
        public void GetByMSISDN()
        {
            DoTransacted(repo => { var prod = repo.GetByMsisdn("3456").FirstOrDefault() ; });
        }


        protected override Int64 ExistingId
        {
            get { return (123456); }
        }
    }
}
