using System;
using com.etak.core.model;
using com.etak.core.repository.crm.network;
using com.etak.core.repository.crm.Nhibernate.Factory;
using NUnit.Framework;

namespace com.etak.core.repository.crm.Nhibernate.IntTests
{
    [TestFixture]
    public class IRMOperatorsInfoRepositoryTest
    {
        [TestFixtureSetUp]
        public static void InitializeSessionFactoryAndLogging()
        {
            log4net.Config.XmlConfigurator.Configure();
            var instance = RepositoryManagerSingleton.Instance;
        }    

        [Test]
        public void OperatorQuery()
        {
            using (var conn = RepositoryManager.GetNewConnection())
            {               
                var optRepo = RepositoryManager.GetRepository<IRMOperatorsInfoRepository<RMOperatorsInfo>>();
                var opt = optRepo.GetById("124");
                if (opt != null)
                {
                    Console.Write(opt.OperaName);
                }
            }
        }


    }
}
