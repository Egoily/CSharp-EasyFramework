using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using com.etak.core.model;
using com.etak.core.model.operation;
using com.etak.core.model.operation.messages;
using com.etak.core.operation.contract.exceptions;
using com.etak.core.operation.implementation;
using com.etak.core.operation.IntTests.common;
using com.etak.core.operation.IntTests.operations;
using com.etak.core.operation.IntTests.operations.messages;
using com.etak.core.repository;
using com.etak.core.repository.crm;
using com.etak.core.repository.crm.Nhibernate.Factory;
using com.etak.core.test.utilities;
using NHibernate;
using NSubstitute;
using NUnit.Framework;

namespace com.etak.core.operation.IntTests
{
    /// <summary>
    /// Summary description for OrderCodeManagement
    /// </summary>
    [TestFixture()]
    public class AbstractOrderTest :RepositoryBasedUnitTest
    {
        [TestFixtureSetUp]
        public void Init()
        {
            MyClassInitialize();
        }

        //[Test]
        //public void DirtyTest()
        //{
        //    ISession ses = SessionManagement.GetInstance().GetSession("CRM");
        //    //BusinessOperation op = new CreateDataAndThrowError();
        //    //ses.Save(op);
        //    //ses.Flush();
        //    BusinessOperation test = ses.Get<BusinessOperation>(1214114367);
        //    Console.Write(test.Id);
        //}
        [Test]
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

         [Test]
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

        [Test]
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



        /// <summary>
        /// Checks if filling Response is working (only for CustomerBasedResponse)
        /// </summary>
        [Test]
        public void TestBusinessOperationExecutionResponseFilled()
        {
            MethodBase minfo = MethodBase.GetCurrentMethod();
            AlwaysOkSinglePhaseOrderCustomerBasedProcessor order = new AlwaysOkSinglePhaseOrderCustomerBasedProcessor();

            FakeOrderNoCustomerbasedRequest req = new FakeOrderNoCustomerbasedRequest()
            {
                Customer = CreateDefaultObject.Create<CustomerInfo>(),
            };

            FakeOrderCustomerbasedResponse response = null;

            using (RepositoryManager.GetConnection())
            {
                try
                {
                    response = order.Process(req, null);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            Assert.IsNotNull(response.CreatedOrder.OperationsForOrder.FirstOrDefault().Customer);
        }

        [Test]
        public void TestMVNOFilledInBusinessOperationExecutionFromInternalRequest()
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

        [Test]
        public void TestMVNOFilledInBusinessOperationExecutionFromExternalRequest()
        {
            var minfo = MethodBase.GetCurrentMethod();
            CreateDataAndThrowError order = new CreateDataAndThrowError(new DataValidationErrorException("TEST EXCEPCTION TEXT", -33));
            var req = GenerateRequest<FakeOrderRequestDTO>();

            var response = order.ProcessFromCustomerModel
                (new AlwaysErrorValidator<FakeOrderRequestDTO>(),
                new SameTypeConverter<FakeOrderRequestDTO>(),
                 new SameTypeConverter<FakeOrderResponseDTO>(),
                req, new RequestInvokationEnvironment { Invoker = minfo });
        }

        [Test]
        public void TestRepoDealerNullThrowException()
        {
            var minfo = MethodBase.GetCurrentMethod();
            CreateDataAndThrowError order = new CreateDataAndThrowError(new DataValidationErrorException("TEST EXCEPCTION TEXT", -33));
            var req = GenerateRequest<FakeOrderRequestDTO>();
            req.vmno = "1000";
            IMVNOPropertiesRepository<MVNOPropertiesInfo> mockrepo = Substitute.For<IMVNOPropertiesRepository<MVNOPropertiesInfo>>();
            mockrepo.GetByVMNOId(Arg.Is<String>(x => x == "1000")).Returns(new List<MVNOPropertiesInfo>());

            var response = order.ProcessFromCustomerModel
                (new NullValidator<FakeOrderRequestDTO>(),
                new SameTypeConverter<FakeOrderRequestDTO>(),
                 new SameTypeConverter<FakeOrderResponseDTO>(),
                req, new RequestInvokationEnvironment { Invoker = minfo });

            Assert.AreEqual(response.resultType, ResultTypes.AuthenticationError);
        }
    }
}
