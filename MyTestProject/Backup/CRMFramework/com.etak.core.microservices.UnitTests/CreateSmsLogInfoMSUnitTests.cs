using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.etak.core.microservices.messages.CreateHLRRequestErrors;
using com.etak.core.microservices.messages.CreateSmsLogInfo;
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
    public class CreateSmsLogInfoMSUnitTests :
        AbstractMicroServiceTest<CreateSmsLogInfoMS, CreateSmsLogInfoRequest, CreateSmsLogInfoResponse>
    {
        private ISmsLogInfoRepository<SmsLogInfo> _mockRepository;
        private readonly DateTime _dummyDate = new DateTime(2015, 12, 24);

        [TestFixtureSetUp()]
        public void InitailizeTest()
        {
            _mockRepository = MockRepository<ISmsLogInfoRepository<SmsLogInfo>>();
        }

        [Test]
        public void CallMicroservice_CorrectRequestGiven_ShouldReturnResponseWithSameCreateDate()
        {
            var actualObj = CreateDummyCreateSmsLogInfoRequest();
            var expectedObj = CreateDummyCreateSmsLogInfoRequest();

            _mockRepository.Create(actualObj.SmsLogInfo).Returns(expectedObj.SmsLogInfo);

            var response = CallMicroservice(actualObj);
            Assert.AreEqual(response.ResultType, ResultTypes.Ok);
        }

        [Test]
        public void CallMicroservice_MockedErrorInput_ShouldThrownException()
        {
            var actualObj = CreateDummyCreateSmsLogInfoRequest();

            _mockRepository.Create(actualObj.SmsLogInfo).Returns(x => { throw new Exception("Error"); });

            Assert.Throws<Exception>(() => CallMicroservice(actualObj));

        }

        private CreateSmsLogInfoRequest CreateDummyCreateSmsLogInfoRequest()
        {
            var obj = CreateDefaultObject.Create<CreateSmsLogInfoRequest>();
            obj.SmsLogInfo.CreateDate = _dummyDate;
            return obj;
        }
    }
}
