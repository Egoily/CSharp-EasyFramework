using System;
using System.Globalization;
using System.Reflection;
using com.etak.core.model;
using com.etak.core.model.operation.messages;
using com.etak.core.operation.contract.exceptions;
using com.etak.core.operation.implementation;
using com.etak.core.operation.test.common;
using com.etak.core.operation.test.operations;
using com.etak.core.operation.test.operations.messages;
using com.etak.core.repository;
using com.etak.core.test.utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace com.etak.core.operation.test
{
    /// <summary>
    /// Summary description for OrderCodeManagement
    /// </summary>
    [TestClass]
    public class AbstractOrderTest :RepositoryBasedUnitTest
    {
        [ClassInitialize]
        public static void Init(TestContext testContext)
        {
            MyClassInitialize(testContext);
        }
       

        [TestMethod]
        public void TestSucessFullOperation()
        {
            MethodBase minfo = MethodBase.GetCurrentMethod();
            AlwaysOkSinglePhaseOrderProcessor order = new AlwaysOkSinglePhaseOrderProcessor();
            FakeOrderRequestDTO req = GenerateRequest<FakeOrderRequestDTO>();
            req.CustomerId = 1660028729;
            using (RepositoryManager.GetConnection())
            {
                try
                {
                    var response = order.ProcessFromCustomerModel(new NullValidator<FakeOrderRequestDTO>(),
                        new SameTypeConverter<FakeOrderRequestDTO>(),
                        new SameTypeConverter<FakeOrderResponseDTO>(),
                        req, new RequestInvokationEnvironment {Invoker = minfo});

                    Assert.AreNotEqual(response.orderCode, default(long), "generated orderCode is not passed to the output");
                    Assert.AreEqual(response.resultType, ResultTypes.Ok, "the response was not ok");
                    Assert.AreEqual(response.errorCode, 0, "error code was not 0");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        [TestMethod]
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
        [TestMethod]
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

         [TestMethod]
        public void TestDirtyDataRollback()
        {
            var minfo = MethodBase.GetCurrentMethod();
            CreateDataAndThrowError order = new CreateDataAndThrowError(new DataValidationErrorException("TEST EXCEPCTION TEXT", -33));
            var req = GenerateRequest<FakeOrderRequestDTO>();

            order.ProcessFromCustomerModel
                (new NullValidator<FakeOrderRequestDTO>(), 
                new SameTypeConverter<FakeOrderRequestDTO>(),
                 new SameTypeConverter<FakeOrderResponseDTO>(),
                req, new RequestInvokationEnvironment { Invoker = minfo });

           
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

        [TestMethod]
        public void TestBusinessOperationExecutionFilled()
        {
            MethodBase minfo = MethodBase.GetCurrentMethod();
            AlwaysOkSinglePhaseOrderProcessor order = new AlwaysOkSinglePhaseOrderProcessor();
            
            FakeOrderRequest req = new FakeOrderRequest()
            {
                Customer = CreateDefaultObject.Create<CustomerInfo>(),
            };

            using (RepositoryManager.GetConnection())
            {
                try
                {
                    var response = order.Process(req, null);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
