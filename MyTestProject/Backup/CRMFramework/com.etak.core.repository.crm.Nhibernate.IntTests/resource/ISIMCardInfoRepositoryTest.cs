using System;
using com.etak.core.model;
using NUnit.Framework;

namespace com.etak.core.repository.crm.Nhibernate.IntTests.resource
{
    [TestFixture]
    public class ISIMCardRepositoryTest :
        AbstractRepositoryTest<ISIMCardInfoRepository<SIMCardInfo>, SIMCardInfo, String>
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
                var simcard =
                    repo.GetById(ExistingId);

                if (simcard != null)
                {
                    Console.Write("Accessing SimcardInfo: ");
                    Console.WriteLine(" Value " + simcard.Status);
                }

            });
        }

        protected override string ExistingId
        {
            get { return ("8790000346343900085"); }
        }
    }
}
