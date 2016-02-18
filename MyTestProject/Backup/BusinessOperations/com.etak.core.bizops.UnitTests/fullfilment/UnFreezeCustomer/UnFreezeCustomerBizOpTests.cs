

using com.etak.core.bizops.fullfilment.UnFreezeCustomer;
using com.etak.core.customer.message.UnFreezeCustomerInfo;
using com.etak.core.dealer.messages.CheckAuthorization;
using com.etak.core.model.operation.messages;
using com.etak.core.operation;
using com.etak.core.operation.contract;
using com.etak.core.test.utilities;
using com.etak.core.test.utilities.abstracts;
using com.etak.core.test.utilities.Helpers;
using NSubstitute;
using NUnit.Framework;

namespace com.etak.core.bizops.UnitTests.fullfilment.UnFreezeCustomer
{
    [TestFixture]
    public class UnFreezeCustomerBizOpTests : AbstractSinglePhaseOrderProcessorTest<UnFreezeCustomerBizOp, UnFreezeCustomerRequestDTO, UnFreezeCustomerResponseDTO, UnFreezeCustomerRequestInternal,
        UnFreezeCustomerResponseInternal, UnFreezeCustomerOrder>
    {
        private IMicroService<CheckAuthorizationRequest, CheckAuthorizationResponse> mockMicroServiceCheckAuthorization;
        [TestFixtureSetUp]
        public void Initialize()
        {
            FakeSessionFactorySingleton.Init();
            mockMicroServiceCheckAuthorization = MockMicroService<CheckAuthorizationRequest, CheckAuthorizationResponse>();
        }

        [Test()]
        public void UnFreezeCustomerBizOp_CorrectUnFreezeCustomerGiven_ShouldUnFreezeCustomerOK()
        {
            #region Mock CheckAuthorization
            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = true };

            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
                actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualCheckAuthorizationResponse);
            #endregion
            var request = new UnFreezeCustomerRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                MSISDN = "1000000000",
            };


            var mockedUnFreezeCustomerInfoMS = MockMicroService<UnFreezeCustomerInfoRequest, UnFreezeCustomerInfoResponse>();
            var actualUnFreezeCustomerInfoMS = new UnFreezeCustomerInfoResponse()
            {
                ResultType = ResultTypes.Ok,
            };
            mockedUnFreezeCustomerInfoMS.Process(Arg.Any<UnFreezeCustomerInfoRequest>(), null).Returns(actualUnFreezeCustomerInfoMS);

            MockAbstractSinglePhaseOrderProcessor(request);

            var response = CallBizOp(request);

            Assert.IsTrue(response.resultType == ResultTypes.Ok);
        }

        [Test()]
        public void UnFreezeCustomerBizOp_NullMsisdnGiven_ShouldThrowException()
        {
            #region Mock CheckAuthorization
            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = true };

            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
                actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualCheckAuthorizationResponse);
            #endregion
            var request = new UnFreezeCustomerRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                MSISDN = null,
            };

            MockAbstractSinglePhaseOrderProcessor(request);

            var response = CallBizOp(request);

            Assert.IsTrue(response.resultType == ResultTypes.BussinessLogicError);
        }
        [Test()]
        public void UnFreezeCustomerBizOp_NOK_authorizationfailed()
        {
            #region Mock CheckAuthorization
            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = false };

            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
                actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualCheckAuthorizationResponse);
            #endregion

            var requestDto = new UnFreezeCustomerRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                MSISDN = "1000000000"
            };

            MockAbsctractBusinessOperation(requestDto);

            var res = CallBizOp(requestDto);
            Assert.AreEqual(res.resultType, ResultTypes.AuthorizationError);
            Assert.AreEqual(res.errorCode, BizOpsErrors.AuthorizeErrorUser);
        }
    }
}
