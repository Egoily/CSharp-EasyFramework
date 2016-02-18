using System;
using System.Globalization;
using System.Reflection;
using com.etak.core.model.operation;
using com.etak.core.model.operation.messages;
using com.etak.core.operation.contract.exceptions;
using com.etak.core.operation.implementation;
using com.etak.core.operation.IntTests.common;
using com.etak.core.operation.IntTests.operations.messages;
using com.etak.core.repository;
using com.etak.core.repository.crm.operation;
using NUnit.Framework;

namespace com.etak.core.operation.IntTests
{
    /// <summary>
    /// Summary description for OrderCodeManagement
    /// </summary>
    [TestFixture()]
    public class NestedOperationsTest : RepositoryBasedUnitTest
    {
        [TestFixtureSetUp]
        public void Init()
        {
            MyClassInitialize();
        }

        //[Test]
        //public void TestRepoLoad()
        //{
        //    using (RepositoryManager.GetConnection())
        //    {
        //        IBusinessOperationExecutionRepository<BusinessOperationExecution> repo =
        //            RepositoryManager.GetRepository<IBusinessOperationExecutionRepository<BusinessOperationExecution>>();

        //        BusinessOperationExecution op = repo.GetById(6125757123793059840);
        //        Console.WriteLine("Order:" + op.OrderManaged);
        //        Console.Write(op);
        //    }
        //}

        [Test]
        public void NestedOperation()
        {

            //var temp = RepositoryManager.GetConnection().BeginTransaction();
            //ILoginInfoRepository<LoginInfo> userRepo = RepositoryManager.GetRepository<ILoginInfoRepository<LoginInfo>>();
            //IUserDealerInfoRepository<UserDealerInfo> userDealerRepo = RepositoryManager.GetRepository<IUserDealerInfoRepository<UserDealerInfo>>();

            //LoginInfo user = new LoginInfo
            //{
            //    FirstName = "Test",
            //    UserName = "Test",
            //    Email = "test@test.com",
            //    Password = MD5Utility.ComputeHash("123456"),

            //};
            //userRepo.Create(user);
            //UserDealerInfo permission = new UserDealerInfo();
            //permission.UserID = user.UserID;
            //permission.DealerID = 190000;
            //userDealerRepo.Create(permission);
            //temp.Commit();

            //AuthenticationHelper.Authenticate(request.user, request.password);

            MethodBase minfo = MethodBase.GetCurrentMethod();
            var bizOp = new NestedOperation();
            FakeBizOpRequestDTO req = GenerateRequest<FakeBizOpRequestDTO>();


            try
            {
                var response = bizOp.ProcessFromCustomerModel(new NullValidator<FakeBizOpRequestDTO>(),
                    new SameTypeConverter<FakeBizOpRequestDTO>(),
                    new SameTypeConverter<FakeBizOpResponseDTO>(),
                    req, new RequestInvokationEnvironment {Invoker = minfo});

                Assert.AreEqual(response.resultType, ResultTypes.Ok, "the response was not ok");
                Assert.AreEqual(response.errorCode, 0, "error code was not 0");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            RepositoryManager.CloseConnection();
            for (int i = 0; i < 10; i++)
            {
                RepositoryManager.GetNewConnection();
                IBusinessOperationRepository<NestedOperation> opRepo =
                    RepositoryManager.GetRepository<IBusinessOperationRepository<NestedOperation>>();

                NestedOperation operationDefinition = opRepo.Get();
                Console.WriteLine(operationDefinition.Id);
                RepositoryManager.CloseConnection();
            }
           


        }

        [Test]
        public void TestMethod4()
        {
            Int64 id = 6145408875504926721;
          
                IBusinessOperationExecutionRepository<BusinessOperationExecution> repo =
                    RepositoryManager.GetRepository<IBusinessOperationExecutionRepository<BusinessOperationExecution>>();
                var bizop = repo.GetById(id);
                if (bizop != null)
                {
                    Console.WriteLine(bizop.Id);

                }
                else
                {
                    Console.WriteLine("Caca");
                }
            
        }

        [Test]
        public void TestDuplicateReference()
        {
            MethodBase minfo = MethodBase.GetCurrentMethod();
            AlwaysOkSinglePhaseOrderProcessor order = new AlwaysOkSinglePhaseOrderProcessor();
            FakeOrderRequestDTO req = GenerateRequest<FakeOrderRequestDTO>();
            req.orderReference = DateTime.Now.ToString(CultureInfo.InvariantCulture);

            var response1 = order.ProcessFromCustomerModel(new NullValidator<FakeOrderRequestDTO>(),
                        new SameTypeConverter<FakeOrderRequestDTO>(),
                        new SameTypeConverter<FakeOrderResponseDTO>(),
                        req, new RequestInvokationEnvironment { Invoker = minfo });

            Assert.AreEqual(response1.resultType, ResultTypes.Ok, "the error type is not OK");
            Assert.AreEqual(response1.errorCode,0, "the errorCode type is not 0");

            var response2 = order.ProcessFromCustomerModel(new NullValidator<FakeOrderRequestDTO>(),
                        new SameTypeConverter<FakeOrderRequestDTO>(),
                        new SameTypeConverter<FakeOrderResponseDTO>(),
                        req, new RequestInvokationEnvironment { Invoker = minfo });

            Assert.AreEqual(response2.resultType, ResultTypes.DuplicatedReference, "the error type is not duplicated reference");
            Assert.AreEqual(response2.errorCode, OperationErrorCodes.DuplicatedReferenceWhileCreatingOrder, "the errorCode type is not DuplicatedReferenceWhileCreatingOrder");

        }

        [Test]
        public void ExternalException()
        {
            var minfo = MethodBase.GetCurrentMethod();
            AlwaysErrorSinglePhaseOrderProcessor order = new AlwaysErrorSinglePhaseOrderProcessor(new StackOverflowException());
            var req = GenerateRequest<FakeOrderRequestDTO>();

            var response = order.ProcessFromCustomerModel(new NullValidator<FakeOrderRequestDTO>(), 
                                            new SameTypeConverter<FakeOrderRequestDTO>(),
                                            new SameTypeConverter<FakeOrderResponseDTO>(), 
                                            req, new RequestInvokationEnvironment { Invoker = minfo });


            Assert.AreEqual(response.orderCode, default(long), "generated orderCode is passed to the output");
            Assert.AreEqual(response.resultType, ResultTypes.UnknownError, "the response was not UnknownError");
            Assert.AreNotEqual(response.errorCode, 0, "error code was 0");
        }

        [Test]
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
            var req = GenerateRequest<FakeOrderRequestDTO>();

            var response = order.ProcessFromCustomerModel(new NullValidator<FakeOrderRequestDTO>(), 
                                                           new SameTypeConverter<FakeOrderRequestDTO>(), 
                                                           new SameTypeConverter<FakeOrderResponseDTO>(),
                                                           req, new RequestInvokationEnvironment { Invoker = minfo });


            Assert.AreEqual(response.orderCode, default(long), "generated orderCode is passed to the output");
            Assert.AreEqual(response.resultType, ex.ResultType, "resultType was not correct");
            Assert.AreEqual(response.errorCode, ex.ErrorCode, "errorCode code was not correct");
        }

   
    }
}
