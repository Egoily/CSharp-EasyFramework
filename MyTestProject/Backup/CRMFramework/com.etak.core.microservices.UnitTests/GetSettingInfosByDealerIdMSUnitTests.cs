using System;
using System.Collections.Generic;
using System.Linq;
using com.etak.core.microservices.messages.GetSettingInfosByDealerId;
using com.etak.core.microservices.microservices;
using com.etak.core.model;
using com.etak.core.model.operation.messages;
using com.etak.core.repository.crm;
using com.etak.core.test.utilities;
using com.etak.core.test.utilities.abstracts;
using NSubstitute;
using NUnit.Framework;

namespace com.etak.core.microservices.UniTests
{
    [TestFixture()]
    public class GetSettingInfosByDealerIdMSUnitTests : AbstractMicroServiceTest<GetSettingInfosByDealerIdMS,GetSettingInfosByDealerIdRequest,GetSettingInfosByDealerIdResponse>
    {
        private ISettingInfoRepository<SettingInfo> _settingInfoRepositoryMock;
        private List<SettingInfo> _settingInfos;
        private List<SettingInfo> _emptySettingInfos;


        [TestFixtureSetUp()]
        public void InitializeTest()
        {
            _settingInfos = new List<SettingInfo>()
            {
                CreateDefaultObject.Create<SettingInfo>()
            };
            _emptySettingInfos = new List<SettingInfo>();
            _settingInfoRepositoryMock = MockRepository<ISettingInfoRepository<SettingInfo>>();
            _settingInfoRepositoryMock.GetSettingInfoWithDetailByDealerId(1)
                .Returns(_settingInfos);
            _settingInfoRepositoryMock.GetSettingInfoWithDetailByDealerId(2)
                .Returns(_emptySettingInfos);
            _settingInfoRepositoryMock.GetSettingInfoWithDetailByDealerId(3).Returns(x => { throw new Exception("Error"); });
        }

        [Test()]
        public void Process_CorrectDealerId_ShouldReturnCorrectSettingInfos()
        {
            var request = new GetSettingInfosByDealerIdRequest()
            {
                DealerId = 1,
            };
            var response = CallMicroservice(request);
            Assert.AreEqual(response.SettingInfos.Count(),1);
            Assert.AreEqual(response.ResultType, ResultTypes.Ok);
        }

        [Test()]
        public void Process_DealerIdHasNoSettingInfo_ShouldReturnEmptySettingInfos()
        {
            var request = new GetSettingInfosByDealerIdRequest()
            {
                DealerId = 2,
            };
            var response = CallMicroservice(request);
            Assert.IsEmpty(response.SettingInfos);
            Assert.AreEqual(response.ResultType, ResultTypes.Ok);
        }

        [Test()]
        public void Process_RepoThrowException_ShouldThrowException()
        {
            var request = new GetSettingInfosByDealerIdRequest()
            {
                DealerId = 3,
            };
            Assert.Throws<Exception>(() => CallMicroservice(request));
        }
    }
}
