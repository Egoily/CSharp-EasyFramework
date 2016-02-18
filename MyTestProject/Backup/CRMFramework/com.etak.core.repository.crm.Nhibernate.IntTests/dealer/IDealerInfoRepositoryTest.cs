using System;
using System.Collections.Generic;
using System.Linq;
using com.etak.core.model;
using NUnit.Framework;


namespace com.etak.core.repository.crm.Nhibernate.IntTests.dealer
{
    [TestFixture]
    public class IDealerInfoRepositoryTest :
        AbstractRepositoryTest<IDealerInfoRepository<DealerInfo>, DealerInfo, Int32>
    {
        [TestFixtureSetUp]
        public static void Init()
        {
            //InitializeSessionFactoryAndLogging();
        }

        [Test]
        public void GetByDealerId()
        {
            DoTransacted(repo =>
            {
                var prod = repo.GetById(ExistingId);
            });
        }

         [Test]
        public void Cose()
        {
            foreach (var counter in new List<Int32> { 1, 2, 3 })
            {
                Log.InfoFormat("Running {0} iteration", counter);

                DoTransacted(repo =>
                {
                    var prod = repo.GetDealersByFiscalUnitId(ExistingId).ToList();
                });
            }
        }


        protected override Int32 ExistingId
        {
            get { return (190000); }
        }
    }
}
