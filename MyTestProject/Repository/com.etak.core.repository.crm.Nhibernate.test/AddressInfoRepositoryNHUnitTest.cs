using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace com.etak.core.repository.crm.Nhibernate.UnitTests
{
    [TestClass]
    public class AddressInfoRepositoryNHUnitTest
    {

        [ClassInitialize()]
        public static void Intialize(TestContext testContext)
        {
            log4net.Config.XmlConfigurator.Configure();
            RepositoryManager.AddAssemby(typeof(com.etak.core.repository.crm.Nhibernate.Factory.SessionFactoryHelper).Assembly);
        }



        [TestMethod]
        public void GetById()
        {
            long id = 100001;
            using (var conn = RepositoryManager.GetNewConnection())
            {
                var repo = RepositoryManager.GetRepository<IAddressInfoRepository<AddressInfo>>();

                var value = repo.GetById(id);
            }
        }

    }
}
