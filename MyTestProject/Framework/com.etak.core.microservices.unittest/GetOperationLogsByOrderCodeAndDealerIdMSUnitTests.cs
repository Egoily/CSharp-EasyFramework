using System;
using System.Collections.Generic;
using com.etak.core.microservices.messages.GetOperationLogsByOrderCodeAndDealerId;
using com.etak.core.microservices.microservices;
using com.etak.core.model;
using com.etak.core.model.operation.messages;
using com.etak.core.repository.crm;
using com.etak.core.test.utilities;
using com.etak.core.test.utilities.abstracts;
using NSubstitute;
using NUnit.Framework;

namespace com.etak.core.microservices.unittest
{


    [TestFixture]
    public class GetOperationLogsByOrderCodeAndDealerIdMSTest :
        AbstractMicroServiceTest
            <GetOperationLogsByOrderCodeAndDealerIdMS, GetOperationLogsByOrderCodeAndDealerIdRequest,
                GetOperationLogsByOrderCodeAndDealerIdResponse>
    {
        private IOperationLogRepository<OperationLog> mockedRepository;
        private OperationLog expectedOpLog;

        [TestFixtureSetUp()]
        public void InitializeSetup()
        {
            expectedOpLog = CreateDefaultObject.Create<OperationLog>();
            mockedRepository = MockRepository<IOperationLogRepository<OperationLog>>();
            mockedRepository.GetByOrderCodeColumnAndDealerId(100, 100)
                .Returns(new List<OperationLog>() { expectedOpLog });
            mockedRepository.GetByOrderCodeColumnAndDealerId(200, 200).Returns(x => { throw new Exception("Error"); });
            mockedRepository.GetByOrderCodeColumnAndDealerId(300, 300).Returns(new List<OperationLog>());
        }

        [TestCase(100)]
        public void GetOperationLogsByOrderCodeAndDealerIdMSOk(int testParameter)
        {
            var expectedObject = new List<OperationLog>() { expectedOpLog };
            var request = new GetOperationLogsByOrderCodeAndDealerIdRequest()
            {
                OrderCode = testParameter,
                DealerId = testParameter,
            };

            var response = CallMicroservice(request);

            Assert.AreEqual(response.ResultType, ResultTypes.Ok);
            AssertExt.ObjectPropertiesAreEqual(expectedObject, response.OperationLogs);
        }

        [TestCase(200)]
        public void GetOperationLogsByOrderCodeAndDealerIdMSNOk(int testParameter)
        {

            var request = new GetOperationLogsByOrderCodeAndDealerIdRequest()
            {
                OrderCode = testParameter,
                DealerId = testParameter,
            };

            Assert.Throws<Exception>(() => CallMicroservice(request));
        }

        [TestCase(300)]
        public void GetOperationLogsByOrderCodeAndDealerIdMSOkNull(int testParameter)
        {
            var request = new GetOperationLogsByOrderCodeAndDealerIdRequest()
            {
                OrderCode = testParameter,
                DealerId = testParameter,
            };

            var response = CallMicroservice(request);

            Assert.AreEqual(response.ResultType, ResultTypes.Ok);
            AssertExt.IsEmpty(response.OperationLogs);
        }
    }
}


