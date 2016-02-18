using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model;
using com.etak.core.model.provisioning;
using com.etak.core.repository.crm.customer;
using com.etak.core.repository.crm.provisioning;
using NUnit.Framework;

namespace com.etak.core.repository.crm.Nhibernate.IntTests
{
    [TestFixture]
    public class CarrierTest
    {
        [TestFixtureSetUp]
        public static void InitializeSessionFactoryAndLogging()
        {
            log4net.Config.XmlConfigurator.Configure();
            var instance = RepositoryManagerSingleton.Instance;
        }

        [Test]
        public void GetCarrier()
        {
            using (var conn = RepositoryManager.GetNewConnection())
            {
                var repo = RepositoryManager.GetRepository<ICarrierRepository<Carrier>>();

                var carrier = repo.GetById(1);

                Assert.AreEqual(carrier.Id, 1);
            }
        }
    }
}
