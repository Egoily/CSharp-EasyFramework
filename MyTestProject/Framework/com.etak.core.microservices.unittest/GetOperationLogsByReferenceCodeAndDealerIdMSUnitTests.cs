using System;
using System.Collections.Generic;
using com.etak.core.microservices.messages.GetOperationLogsByReferenceCodeAndDealerId;
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
    public class GetOperationLogsByReferenceCodeAndDealerIdMSTest :
        AbstractMicroServiceTest
            <GetOperationLogsByReferenceCodeAndDealerIdMS, GetOperationLogsByReferenceCodeAndDealerIdRequest,
                GetOperationLogsByReferenceCodeAndDealerIdResponse>
    {
        private IOperationLogRepository<OperationLog> mockedRepository;
        private OperationLog expectedOpLog;

        [TestFixtureSetUp()]
        public void InitializeSetup()
        {
            expectedOpLog = CreateDefaultObject.Create<OperationLog>();
            mockedRepository = MockRepository<IOperationLogRepository<OperationLog>>();
            mockedRepository.GetByOrderCodeAndDealerId("100", 100)
                .Returns(new List<OperationLog>() { expectedOpLog });
            mockedRepository.GetByOrderCodeAndDealerId("200", 200).Returns(x => { throw new Exception("Error"); });
            mockedRepository.GetByOrderCodeAndDealerId("300", 300).Returns(new List<OperationLog>());
        }

        [TestCase(100)]
        public void GetOperationLogsByReferenceCodeAndDealerIdMSOk(int testParameter)
        {
            var expectedObject = new List<OperationLog>() { expectedOpLog };
            var request = new GetOperationLogsByReferenceCodeAndDealerIdRequest()
            {
                ReferenceCode = testParameter.ToString(),
                DealerId = testParameter,
            };

            var response = CallMicroservice(request);

            Assert.AreEqual(response.ResultType, ResultTypes.Ok);
            AssertExt.ObjectPropertiesAreEqual(expectedObject, response.OperationLogs);
        }

        [TestCase(200)]
        public void GetOperationLogsByReferenceCodeAndDealerIdMSNOk(int testParameter)
        {

            var request = new GetOperationLogsByReferenceCodeAndDealerIdRequest()
            {
                ReferenceCode = testParameter.ToString(),
                DealerId = testParameter,
            };

            Assert.Throws<Exception>(() => CallMicroservice(request));
        }

        [TestCase(300)]
        public void GetDealerInfoByIdMSNOkNull(int testParameter)
        {
            var request = new GetOperationLogsByReferenceCodeAndDealerIdRequest()
            {
                ReferenceCode = testParameter.ToString(),
                DealerId = testParameter,
            };

            var response = CallMicroservice(request);

            Assert.AreEqual(response.ResultType, ResultTypes.Ok);
            AssertExt.IsEmpty(response.OperationLogs);
        }
    }
}


