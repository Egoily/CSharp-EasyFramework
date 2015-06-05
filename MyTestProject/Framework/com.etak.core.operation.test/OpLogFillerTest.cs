using System;
using System.Reflection;
using com.etak.core.model.operation.messages;
using com.etak.core.operation.contract.exceptions;
using com.etak.core.operation.implementation;
using com.etak.core.operation.test.common;
using com.etak.core.operation.test.operations.messages;
using com.etak.core.repository;
using com.etak.core.repository.crm.Nhibernate.Factory;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace com.etak.core.operation.test
{
    /// <summary>
    /// Summary description for OrderCodeManagement
    /// </summary>
    [TestClass]
    public class OpLogFillerTest
    {
        static ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public OpLogFillerTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
         [ClassInitialize()]
         public static void MyClassInitialize(TestContext testContext) 
         {
             try
             {
                 log4net.Config.XmlConfigurator.Configure();
                 RepositoryManager.AddAssemby(typeof(SessionFactoryHelper).Assembly);
             }
             catch(Exception)
             {
                 testContext.WriteLine("Error initializing session");
             }
             
         }
        
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
         [TestInitialize()]
         public void MyTestInitialize() 
         {
             try
             {
                 RepositoryManager.GetNewConnection();
             }
             catch (ConnectionAlreadyOpened) { }

             
         }
        
        // Use TestCleanup to run code after each test has run
         [TestCleanup()]
         public void MyTestCleanup() 
         {
             try
             {
                 RepositoryManager.CloseConnection();
             }
             catch (ConnectionNotOpened) { }
         }

         private FakeOrderRequestDTO GenerateRequest()
        {
             var req = new FakeOrderRequestDTO
                            {
                                user = "1755000001",
                                password = "123456",
                                vmno = "970100",
                                channel = "Unit test",
                                comments = testContextInstance.TestName,
                                orderReference = DateTime.Now.ToString()
                            };
            return req;
        }
        #endregion

         
        [TestMethod]
        public void ExternalException()
        {
            var minfo = MethodBase.GetCurrentMethod();
            AlwaysErrorSinglePhaseOrderProcessor order = new AlwaysErrorSinglePhaseOrderProcessor(new StackOverflowException());
            FakeOrderRequestDTO req = GenerateRequest();

            var response = order.ProcessFromCustomerModel(new NullValidator<FakeOrderRequestDTO>(), 
                                                          new SameTypeConverter<FakeOrderRequestDTO>(),
                                                          new SameTypeConverter<FakeOrderResponseDTO>(), 
                                                          req, 
                                                          new RequestInvokationEnvironment { Invoker = minfo });

            Assert.AreEqual(response.orderCode, default(long), "generated orderCode is passed to the output");
            Assert.AreEqual(response.resultType, ResultTypes.UnknownError, "the response was not UnknownError");
            Assert.AreNotEqual(response.errorCode, 0, "error code was 0");
        }

        [TestMethod]
        public void TestEtakExceptions()
        {
            TestEtakException(new AuthenticationErrorException("TEST EXCEPCTION TEXT", -33));
            TestEtakException(new AuthorizationErrorException("TEST EXCEPCTION TEXT", -33));
            TestEtakException(new BusinessLogicErrorException("TEST EXCEPCTION TEXT", -33));
            TestEtakException(new DataValidationErrorException("TEST EXCEPCTION TEXT", -33));
            TestEtakException(new DuplicatedReferenceException("TEST EXCEPCTION TEXT", -33));
            TestEtakException(new InternalErrorException("TEST EXCEPCTION TEXT", -33));
        }
    
        public void TestEtakException(ElephantTalkBaseException ex)
        {
            var minfo = MethodBase.GetCurrentMethod();
            AlwaysErrorSinglePhaseOrderProcessor order = new AlwaysErrorSinglePhaseOrderProcessor(ex);
            var req = GenerateRequest();

            var response = order.ProcessFromCustomerModel(new NullValidator<FakeOrderRequestDTO>(), 
                                    new SameTypeConverter<FakeOrderRequestDTO>(),
                                     new SameTypeConverter<FakeOrderResponseDTO>(), 
                                     req, new RequestInvokationEnvironment { Invoker = minfo });

            Assert.AreEqual(response.orderCode, default(long), "generated orderCode is passed to the output");
            Log.DebugFormat("REsult types: {0}, {1}", response.resultType, ex.ResultType);

            Assert.AreEqual(response.resultType, ex.ResultType, "resultType was not correct");
            Assert.AreEqual(response.errorCode, ex.ErrorCode, "errorCode code was not correct");
        }
    }
}
