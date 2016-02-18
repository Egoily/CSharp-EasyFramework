using System;
using System.Collections.Generic;
using System.Linq;
using com.etak.core.model;
using NUnit.Framework;
using com.etak.core.repository.crm.dealer;


namespace com.etak.core.repository.crm.Nhibernate.IntTests.dealer
{
    [TestFixture]
    public class IMVNORetailMarginRepository :
        AbstractRepositoryTest<IMVNORetailMarginRepository<MVNORetailMargin>, MVNORetailMargin, Int32>
    {
        [TestFixtureSetUp]
        public static void Init()
        {
            //InitializeSessionFactoryAndLogging();
        }

        [Test]
        public void GetAllTests()
        {
            DoTransacted(repo =>
            {
                var prod = repo.GetAll().ToList();

            });
        }

        protected override Int32 ExistingId
        {
            get { return (1); }
        }

    }
}
