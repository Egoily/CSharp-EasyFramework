using System;
using System.Linq;
using com.etak.core.model;
using com.etak.core.repository.crm.network;
using NUnit.Framework;

namespace com.etak.core.repository.crm.Nhibernate.IntTests.network
{
    [TestFixture]
    public class RMOperatorsInfoRepositoryTest : 
        AbstractRepositoryTest<IRMOperatorsInfoRepository<RMOperatorsInfo>, RMOperatorsInfo, String>
    {
        protected override String ExistingId
        {
            get { return "104"; }
        }

        [TestFixtureSetUp]
        public static void Init()
        {
            //InitializeSessionFactoryAndLogging();
        }

        [Test]
        public void GetByIdManual()
        {
            Action<IRMOperatorsInfoRepository<RMOperatorsInfo>> func = repo =>
            {
                RMOperatorsInfo opinfo = repo.GetById(ExistingId);
                Console.WriteLine(opinfo);
            };

            DoTransacted(func);
        }

        [Test]
        public void GetByOperatorCide()
        {
            Action<IRMOperatorsInfoRepository<RMOperatorsInfo>> func = repo =>
            {
                var opinfo = repo.GetByCNOperatorCode("989");
                Console.WriteLine(opinfo.Count());
            };

            DoTransacted(func);
        }
       
    }
}
