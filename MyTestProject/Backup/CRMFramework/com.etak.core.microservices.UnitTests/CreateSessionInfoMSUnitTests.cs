using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.etak.core.microservices.messages.CreateHLRRequestErrors;
using com.etak.core.microservices.messages.CreateSessionInfo;
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
    public class CreateSessionInfoMSUnitTests :
        AbstractMicroServiceTest<CreateSessionInfoMS, CreateSessionInfoRequest, CreateSessionInfoResponse>
    {
        private ISessionInfoRepository<SessionInfo> _mockRepository;

        private readonly DateTime _dummyIdleTimeoutDate = new DateTime(2015, 04, 02);
        private const int DummyIdelTimeoutMinutes = 10;

        [TestFixtureSetUp()]
        public void InitailizeTest()
        {
            _mockRepository = MockRepository<ISessionInfoRepository<SessionInfo>>();
        }

        [Test]
        public void CallMicroservice_CorrectRequestGiven_ShouldReturnResponseWithSessionId()
        {
            var actualObj = CreateDummyCreateSessionInfoRequest();
            var expectedObj = CreateDummyCreateSessionInfoRequest();
            expectedObj.SessionInfo.SessionID = "123123";

            _mockRepository.Create(actualObj.SessionInfo).Returns(expectedObj.SessionInfo);

            var response = CallMicroservice(actualObj);
            Assert.AreEqual(response.ResultType, ResultTypes.Ok);
            Assert.AreEqual("123123", expectedObj.SessionInfo.SessionID);
        }

        [Test]
        public void CallMicroservice_MockedErrorInput_ShouldThrownException()
        {
            var actualObj = CreateDummyCreateSessionInfoRequest();

            _mockRepository.Create(actualObj.SessionInfo).Returns(x => { throw new Exception("Error"); });

            Assert.Throws<Exception>(() => CallMicroservice(actualObj));

        }

        private CreateSessionInfoRequest CreateDummyCreateSessionInfoRequest()
        {
            var obj = CreateDefaultObject.Create<CreateSessionInfoRequest>();
            obj.SessionInfo.IdleTimeoutDate = _dummyIdleTimeoutDate;
            obj.SessionInfo.IdleTimoutMinutes = DummyIdelTimeoutMinutes;
            return obj;
        }
    }
}
