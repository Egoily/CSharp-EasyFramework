using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.bizops.fullfilment.CreateSession;
using com.etak.core.microservices.messages.CreateSessionInfo;
using com.etak.core.model;
using com.etak.core.model.operation.messages;
using com.etak.core.operation;
using com.etak.core.operation.contract;
using com.etak.core.test.utilities;
using com.etak.core.test.utilities.abstracts;
using com.etak.core.test.utilities.Helpers;
using NSubstitute;
using NUnit.Framework;

namespace com.etak.core.bizops.UnitTests.fullfilment.CreateSession
{
    [TestFixture]
    public class CreateSessionBizOpUnitTests : AbstractSinglePhaseOrderProcessorTest<CreateSessionBizOp, CreateSessionRequestDTO, CreateSessionResponseDTO, CreateSessionRequestInternal,
        CreateSessionResponseInternal, CreateSessionOrder>
    {
        private IMicroService<CreateSessionInfoRequest, CreateSessionInfoResponse> mockMicroServiceCreateSessionInfo;
        [TestFixtureSetUp]
        public void Initialize()
        {
            FakeSessionFactorySingleton.Init();
            mockMicroServiceCreateSessionInfo = MockMicroService<CreateSessionInfoRequest, CreateSessionInfoResponse>();
        }

        [Test()]
        public void CallBizOp_CorrectCreateSessionGiven_ShouldCreateSessionOK()
        {
            #region Mock CreateSession
            var actualSessionIfno = CreateDefaultObject.Create<SessionInfo>();
            actualSessionIfno.SessionID = "123454567890";
            var actualCreateSessionResponse = new CreateSessionInfoResponse { SessionInfo = actualSessionIfno };

            var actualCreateSessionRequest = CreateDefaultObject.Create<CreateSessionInfoRequest>();
            mockMicroServiceCreateSessionInfo.Process(
                actualCreateSessionRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualCreateSessionResponse);
            #endregion

            var request = new CreateSessionRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "1644000204",
            };

            MockAbstractSinglePhaseOrderProcessor(request);

            var response = CallBizOp(request);

            Assert.IsTrue(response.resultType == ResultTypes.Ok);
        }

        
    }
}
