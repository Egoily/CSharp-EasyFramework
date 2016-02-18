using System;
using System.Collections.Generic;
using System.Reflection;
using com.etak.core.model;
using com.etak.core.model.operation.messages;
using com.etak.core.operation.contract.exceptions;
using com.etak.core.operation.implementation;
using com.etak.core.operation.IntTests.common;
using com.etak.core.operation.IntTests.operations.messages;
using com.etak.core.operation.util;
using com.etak.core.repository;
using com.etak.core.repository.crm;
using com.etak.core.repository.crm.Nhibernate.Factory;
using com.etak.core.test.utilities;
using log4net;
using NSubstitute;
using NUnit.Framework;

namespace com.etak.core.operation.IntTests
{
    /// <summary>
    /// Summary description for OrderCodeManagement
    /// </summary>
    [TestFixture()]
    public class OpLogFillerTest : RepositoryBasedUnitTest
    {
        protected static readonly ILog log = LogManager.GetLogger(typeof(OpLogFillerTest));
        [TestFixtureSetUp]
        public void Init()
        {
            MyClassInitialize();

        }


         [Test()]
        public void ExternalException()
        {
            FakeOrderRequestDTO req = GenerateRequest<FakeOrderRequestDTO>();
           
            var minfo = MethodBase.GetCurrentMethod();
            AlwaysErrorSinglePhaseOrderProcessor order = new AlwaysErrorSinglePhaseOrderProcessor(new StackOverflowException());
            
            var response = order.ProcessFromCustomerModel(new NullValidator<FakeOrderRequestDTO>(), 
                                                          new SameTypeConverter<FakeOrderRequestDTO>(),
                                                          new SameTypeConverter<FakeOrderResponseDTO>(), 
                                                          req, 
                                                          new RequestInvokationEnvironment { Invoker = minfo });

            Assert.AreEqual(response.orderCode, default(long), "generated orderCode is passed to the output");
            Assert.AreEqual(response.resultType, ResultTypes.UnknownError, "the response was not UnknownError");
            Assert.AreNotEqual(response.errorCode, 0, "error code was 0");
        }

         [Test()]
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
            log.DebugFormat("REsult types: {0}, {1}", response.resultType, ex.ResultType);

            Assert.AreEqual(response.resultType, ex.ResultType, "resultType was not correct");
            Assert.AreEqual(response.errorCode, ex.ErrorCode, "errorCode code was not correct");
        }
    }
}
