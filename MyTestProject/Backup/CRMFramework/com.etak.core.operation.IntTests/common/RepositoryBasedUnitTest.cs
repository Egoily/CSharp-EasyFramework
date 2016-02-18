using System;
using com.etak.core.model.operation.messages;
using com.etak.core.repository;
using com.etak.core.repository.crm.Nhibernate.Factory;
using log4net;
using NHibernate.Engine;
using NUnit.Framework;

namespace com.etak.core.operation.IntTests.common
{
    [TestFixture()]
    public class RepositoryBasedUnitTest
    {
        protected TestContext TestContextInstance;
        protected static readonly ILog log = LogManager.GetLogger(typeof(RepositoryBasedUnitTest));

        protected TRequest GenerateRequest<TRequest>() where TRequest : RequestBaseDTO, new()
        {
            var req = new TRequest
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100"
            };
            return req;
        }

        ///// <summary>
        /////Gets or sets the test context which provides
        /////information about and functionality for the current test run.
        /////</summary>
        //public TestContext TestContext
        //{
        //    get
        //    {
        //        return TestContextInstance;
        //    }
        //    set
        //    {
        //        TestContextInstance = value;
        //    }
        //}

        // Use TestInitialize to run code before running each test 
        [SetUp]
        public void TestInitialize()
        {
            try
            {
                RepositoryManager.GetNewConnection();
            }
            catch (ConnectionAlreadyOpened ex)
            {
                log.Error("Error connection already opened");
                Console.WriteLine(ex.Message);
            }
        }

        // Use TestCleanup to run code after each test has run
        	
        [TearDown]
        public void MyTestCleanup()
        {
            try
            {
                RepositoryManager.CloseConnection();
            }
            catch (ConnectionNotOpened ex)
            {
                log.Error("Error connection not opened");
                Console.WriteLine(ex.Message);
            }
        }

        [TestFixtureSetUp]
        public void MyClassInitialize()
        {
            try
            {
                log4net.Config.XmlConfigurator.Configure();
                var instance = RepositoryManagerSingleton.Instance;
            }
            catch (Exception ex)
            {
                log.Error("Error initializing session");
                Console.WriteLine(ex.Message);
                //testContext.WriteLine("Error initializing session");
            }
        }

        [Test]
        public void GenerateSchema()
        {
            SessionManagement.GetInstance().GenerateSchemaToFile("CRM", @"CRM_SQUEMA.SQL");
        }
    }
}
