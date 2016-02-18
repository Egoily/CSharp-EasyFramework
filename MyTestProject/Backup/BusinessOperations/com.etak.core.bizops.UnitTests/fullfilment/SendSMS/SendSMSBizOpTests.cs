using com.etak.core.bizops.fullfilment.SendSMS;
using com.etak.core.microservices.messages.CreateSmsLogInfo;
using com.etak.core.model.operation.messages;
using com.etak.core.test.utilities.abstracts;
using com.etak.core.test.utilities.Helpers;
using NSubstitute;
using NUnit.Framework;

namespace com.etak.core.bizops.UnitTests.fullfilment.UnFreezeCustomer
{
    public class SendSMSBizOpTests : AbstractSinglePhaseOrderProcessorTest<SendSMSBizOp, SendSMSRequestDTO, SendSMSResponseDTO, SendSMSRequestInternal,
        SendSMSResponseInternal, SendSMSOrder>
    {
        [TestFixtureSetUp]
        public void Initialize()
        {
            FakeSessionFactorySingleton.Init();
        }

        [Test()]
        public void SendSMSBizOp_CorrectSendSMSGiven_ShouldOK()
        {
            var request = new SendSMSRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                MSISDNs = "1000000000",
            };
            
            var createSmsLogInfoMS = MockMicroService<CreateSmsLogInfoRequest, CreateSmsLogInfoResponse>();
            var mockCreateSmsLogInfoResponse = new CreateSmsLogInfoResponse()
            {
                ResultType = ResultTypes.Ok,
            };
            createSmsLogInfoMS.Process(Arg.Any<CreateSmsLogInfoRequest>(), null).Returns(mockCreateSmsLogInfoResponse);

            MockAbstractSinglePhaseOrderProcessor(request);

            var response = CallBizOp(request);

            Assert.IsTrue(response.resultType == ResultTypes.Ok);
        }

        [Test()]
        public void SendSMSBizOp_NullMsisdnGiven_ShouldThrowException()
        {
            var request = new SendSMSRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                MSISDNs = null,
            };

            MockAbstractSinglePhaseOrderProcessor(request);

            var response = CallBizOp(request);

            Assert.IsTrue(response.resultType == ResultTypes.BussinessLogicError);
            Assert.IsTrue(response.errorCode == BizOpsErrors.MSISDNNotFound);
        }
    }
}
