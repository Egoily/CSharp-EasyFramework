using System;
using com.etak.core.microservices.messages.GetSystemConfigDataInfoById;
using com.etak.core.microservices.microservices;
using com.etak.core.model;
using com.etak.core.model.operation.messages;
using com.etak.core.repository.crm.configuration;
using com.etak.core.test.utilities;
using com.etak.core.test.utilities.abstracts;
using NSubstitute;
using NUnit.Framework;

namespace com.etak.core.microservices.UniTests
{

    [TestFixture]
    public class GetSystemConfigDataInfoByIdUnitMSTest :
        AbstractMicroServiceTest
            <GetSystemConfigDataInfoByIdMS, GetSystemConfigDataInfoByIdRequest, GetSystemConfigDataInfoByIdResponse>
    {
        private ISystemConfigDataInfoRepository<SystemConfigDataInfo> mockedRepository;
        private SystemConfigDataInfo expectedSysConfigData;

        [TestFixtureSetUp()]
        public void InitializeSetup()
        {
            expectedSysConfigData = CreateDefaultObject.Create<SystemConfigDataInfo>();
            mockedRepository = MockRepository<ISystemConfigDataInfoRepository<SystemConfigDataInfo>>();
            mockedRepository.GetById("100").Returns(expectedSysConfigData);
            mockedRepository.GetById("200").Returns(x => { throw new Exception("Error"); });
            mockedRepository.GetById("300").Returns((SystemConfigDataInfo) null);
        }

        [TestCase(100)]
        public void Process_CorrectSystemConfigDataId_ShouldReturnSystemConfigData(int testParameter)
        {
            var request = new GetSystemConfigDataInfoByIdRequest()
            {
                SystemConfigDataId = testParameter.ToString(),
            };

            var response = CallMicroservice(request);

            Assert.AreEqual(response.ResultType, ResultTypes.Ok);
            AssertExt.ObjectPropertiesAreEqual(expectedSysConfigData, response.SystemConfigData);
        }

        [TestCase(200)]
        public void Process_RepoThrowException_ShouldThrowException(int testParameter)
        {

            var request = new GetSystemConfigDataInfoByIdRequest()
            {
                SystemConfigDataId = testParameter.ToString(),
            };

            Assert.Throws<Exception>(() => CallMicroservice(request));
        }

        [TestCase(300)]
        public void Process_SystemConfigDataIdHasNoSystemConfigData_ShouldReturnEmptySystemConfigData(int testParameter)
        {
            var request = new GetSystemConfigDataInfoByIdRequest()
            {
                SystemConfigDataId = testParameter.ToString(),
            };

            var response = CallMicroservice(request);

            Assert.AreEqual(response.ResultType, ResultTypes.Ok);
            AssertExt.IsNull(response.SystemConfigData);
        }
    }
}


