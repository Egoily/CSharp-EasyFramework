using System;
using com.etak.core.model.operation.messages;
using com.etak.core.repository;
using com.etak.core.repository.crm.Nhibernate.Factory;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace com.etak.core.operation.test.common
{
    [TestClass]
    public class RepositoryBasedUnitTest
    {
        protected TestContext TestContextInstance;

        protected TRequest GenerateRequest<TRequest>() where TRequest : RequestBaseDTO, new()
        {
            var req = new TRequest
            {
                user = "1755000001",
                password = "123456",
                vmno = "970100"
            };
            return req;
        }

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return TestContextInstance;
            }
            set
            {
                TestContextInstance = value;
            }
        }

        // Use TestInitialize to run code before running each test 
        [TestInitialize]
        public void TestInitialize()
        {
            try
            {
                RepositoryManager.GetNewConnection();
            }
            catch (ConnectionAlreadyOpened)
            {
                
            }
        }

        // Use TestCleanup to run code after each test has run
        [TestCleanup]
        public void MyTestCleanup()
        {
            try
            {
                RepositoryManager.CloseConnection();
            }
            catch (ConnectionNotOpened) { }
        }

        [ClassInitialize]
        public static void MyClassInitialize(TestContext testContext)
        {
            try
            {
                log4net.Config.XmlConfigurator.Configure();
                RepositoryManager.AddAssemby(typeof(SessionFactoryHelper).Assembly);
            }
            catch (Exception)
            {
                testContext.WriteLine("Error initializing session");
            }
        }

        [TestMethod]
        public void GenerateSchema()
        {
            SessionManagement.GetInstance().GenerateSchemaToFile("CRM", @"CRM_SQUEMA.SQL");
        }
    }
}
