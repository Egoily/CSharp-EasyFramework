using System;
using System.Linq;
using com.etak.core.model;
using com.etak.core.repository.crm.portability;
using NUnit.Framework;

namespace com.etak.core.repository.crm.Nhibernate.IntTests.portability
{
    [TestFixture]
    public class IMNPPortabilityCustomerInfoRepositoryTest :
        AbstractRepositoryTest<IMNPPortabilityCustomerInfoRepository<MNPPortabilityCustomerInfo>, MNPPortabilityCustomerInfo, Int32>

    {
        [TestFixtureSetUp]
        public static void Init()
        {
            //InitializeSessionFactoryAndLogging();
        }

        [Test]
        public void GetByCustomerId()
        {
            DoTransacted(repo => 
            { 
                var prod = repo.GetByCustomerId(12345).FirstOrDefault() ; 
            });
        }


        protected override Int32 ExistingId
        {
            get { return (123456); }
        }
    }
}
