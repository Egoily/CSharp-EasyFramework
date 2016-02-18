using System;
using com.etak.core.model;
using com.etak.core.repository.crm.resource;
using NUnit.Framework;

namespace com.etak.core.repository.crm.Nhibernate.IntTests.resource
{
    [TestFixture]
    public class ImeiAssnHistTest : AbstractRepositoryTest<IImeiAssnHistRepository<ImeiAssnHist>, ImeiAssnHist, ImeiAssnHistPKInfo>
    {
        //[TestFixtureSetUp]
        //public static void Init()
        //{
        //    //var instance = RepositoryManagerSingleton.Instance;
        //}

        [Test]
        public void GetById2()
        {
            DoTransacted(repo =>
            {
                int resourceId = 1000024418;
                var response = repo.GetById(new ImeiAssnHistPKInfo
                {
                    Resourceid = resourceId,
                    StartDate = new DateTime(2014, 05, 31, 00, 21, 22)
                });

                Assert.AreEqual(response.PKInfo.Resourceid, resourceId);
            });
        }

        //[Test]
        //public override void GetById()
        //{
        //    DoTransacted(repo =>
        //    {
        //        var imeiAssnHist =
        //            repo.GetById(ExistingId);


        //        Console.Write("Checking ImeiAssn: ");
        //        Console.WriteLine(" Value " + imeiAssnHist.Imei);
        //    });
        //}

        //protected override int ExistingId
        //{
        //    get { return (1000024418); }
        //}

        protected override ImeiAssnHistPKInfo ExistingId
        {
            get { return new ImeiAssnHistPKInfo(); }
        }
    }
}
