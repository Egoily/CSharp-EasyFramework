using System;
using System.Collections.Generic;
using com.etak.core.microservices.messages.GetSystemConfigDataInfosByItem;
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
    public class GetSystemConfigDataInfoByItemMSTest : AbstractMicroServiceTest<GetSystemConfigDataInfosByItemMS, GetSystemConfigDataInfosByItemRequest, GetSystemConfigDataInfosByItemResponse>
    {
        private ISystemConfigDataInfoRepository<SystemConfigDataInfo> mockedRepository;

        [TestFixtureSetUp()]
        public void InitializeSetup()
        {
            mockedRepository = MockRepository<ISystemConfigDataInfoRepository<SystemConfigDataInfo>>();
            mockedRepository.GetSystemConfigDateInfoByItem("100").Returns(new List<SystemConfigDataInfo>() { CreateDefaultObject.Create<SystemConfigDataInfo>()});
            mockedRepository.GetSystemConfigDateInfoByItem("200").Returns(x => { throw new Exception("Error"); });
            mockedRepository.GetSystemConfigDateInfoByItem("300").Returns(new List<SystemConfigDataInfo>());
        }

        [TestCase(100)]
        public void Process_CorrectItem_ShouldReturnSystemConfigData(int testParameter)
        {
            var expectedObject = new List<SystemConfigDataInfo>() { CreateDefaultObject.Create<SystemConfigDataInfo>() };
            var request = new GetSystemConfigDataInfosByItemRequest()
            {
                 Item = testParameter.ToString(),
            };

            var response = CallMicroservice(request);

            Assert.AreEqual(response.ResultType, ResultTypes.Ok);
            AssertExt.ObjectPropertiesAreEqual(expectedObject, response.SystemConfigData);
        }

        [TestCase(200)]
        public void Process_RepoThrowException_ShouldThrowException(int testParameter)
        {

            var request = new GetSystemConfigDataInfosByItemRequest()
            {
                Item = testParameter.ToString(),
            };

            Assert.Throws<Exception>(() => CallMicroservice(request));
        }

        [TestCase(300)]
        public void Process_ItemHasNoSystemConfigData_ShouldReturnEmptySystemConfigData(int testParameter)
        {
            var request = new GetSystemConfigDataInfosByItemRequest()
            {
                Item = testParameter.ToString(),
            };

            var response = CallMicroservice(request);

            Assert.AreEqual(response.ResultType, ResultTypes.Ok);
            AssertExt.IsEmpty(response.SystemConfigData);
        }
    }
}
