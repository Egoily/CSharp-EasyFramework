using System;
using com.etak.core.model;
using com.etak.core.repository.crm.resource;
using NUnit.Framework;

namespace com.etak.core.repository.crm.Nhibernate.IntTests.resource
{
    [TestFixture]
    public class INumberPropertyInfoRepositoryTest :
        AbstractRepositoryTest<INumberPropertyInfoRepository<NumberPropertyInfo>, NumberPropertyInfo, String>
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
                NumberPropertyInfo npInfo =
                 repo.GetById(ExistingId);
                if (npInfo != null)
                {
                    Console.Write("Accessing NumberInfo: ");
                    Console.WriteLine(" Value " + npInfo.NumberInfo.UpdateUserID);
                }

            });
        }

        protected override string ExistingId
        {
            get { return ("34600000016"); }
        }
    }
}
