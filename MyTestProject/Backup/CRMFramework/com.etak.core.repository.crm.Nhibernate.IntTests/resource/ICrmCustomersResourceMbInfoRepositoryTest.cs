using System;
using System.Linq;
using com.etak.core.model;
using com.etak.core.repository.crm.subscription;
using FluentNHibernate.Conventions;
using NUnit.Framework;

namespace com.etak.core.repository.crm.Nhibernate.IntTests.resource
{

    [TestFixture]
    public class ICrmCustomersResourceMbInfoRepositoryTest : AbstractRepositoryTest<ICrmCustomersResourceMbInfoRepository<CrmCustomersResourceMbInfo>, CrmCustomersResourceMbInfo, Int32>
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
                CrmCustomersResourceMbInfo resource = repo.GetById(ExistingId);

                if (resource != null)
                {
                    Console.Write("Accessing ResourceMBInfo: ");
                    Console.WriteLine(" Value " + resource.RESOURCE);
                }
            });
        }

        //[Test]
        //public void GetActiveSubscriptionByMsisdn()
        //{
        //    DoTransacted(repo =>
        //    {
        //        var resources = repo.GetActiveSubscriptionByMsisdn("34611450000").ToList();

        //        if (resources.IsNotEmpty())
        //        {
        //            Console.Write("Accessing ResourceMBInfo: ");
        //            Console.WriteLine(" Value " + resources.FirstOrDefault().RESOURCE);
        //        }
        //    });
        //}


        protected override int ExistingId
        {
            get { return 1656000286; }
        }
    }
}
