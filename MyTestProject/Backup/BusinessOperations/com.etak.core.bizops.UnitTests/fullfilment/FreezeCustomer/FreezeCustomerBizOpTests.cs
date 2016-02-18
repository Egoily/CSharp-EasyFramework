

using com.etak.core.bizops.fullfilment.FreezeCustomer;
using com.etak.core.customer.message.FreezeCustomerInfo;
using com.etak.core.dealer.messages.CheckAuthorization;
using com.etak.core.model.operation.messages;
using com.etak.core.operation;
using com.etak.core.operation.contract;
using com.etak.core.test.utilities;
using com.etak.core.test.utilities.abstracts;
using com.etak.core.test.utilities.Helpers;
using NSubstitute;
using NUnit.Framework;

namespace com.etak.core.bizops.UnitTests.fullfilment.FreezeCustomer
{
    [TestFixture]
    public class FreezeCustomerBizOpTests : AbstractSinglePhaseOrderProcessorTest<FreezeCustomerBizOp, FreezeCustomerRequestDTO,FreezeCustomerResponseDTO, FreezeCustomerRequestInternal,
                FreezeCustomerResponseInternal, FreezeCustomerOrder>
    {
        private IMicroService<CheckAuthorizationRequest, CheckAuthorizationResponse> mockMicroServiceCheckAuthorization;
        [TestFixtureSetUp]
        public void Initialize()
        {
            FakeSessionFactorySingleton.Init();
            mockMicroServiceCheckAuthorization = MockMicroService<CheckAuthorizationRequest, CheckAuthorizationResponse>();
        }

        [Test()]
        public void FreezeCustomerBizOp_CorrectFreezeCustomerGiven_ShouldFreezeCustomerOK()
        {
            #region Mock CheckAuthorization
            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = true };

            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
                actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualCheckAuthorizationResponse);
            #endregion

            var request = new FreezeCustomerRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                MSISDN = "1000000000",
            };


            var mockedFreezeCustomerInfoMS = MockMicroService<FreezeCustomerInfoRequest, FreezeCustomerInfoResponse>();
            var actualFreezeCustomerInfoMS = new FreezeCustomerInfoResponse()
            {
                ResultType = ResultTypes.Ok,
            };
            mockedFreezeCustomerInfoMS.Process(Arg.Any<FreezeCustomerInfoRequest>(), null).Returns(actualFreezeCustomerInfoMS);

            MockAbstractSinglePhaseOrderProcessor(request);

            var response = CallBizOp(request);

            Assert.IsTrue(response.resultType == ResultTypes.Ok);
        }

        [Test()]
        public void FreezeCustomerBizOp_NullMsisdnGiven_ShouldThrowException()
        {
            #region Mock CheckAuthorization
            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = true };

            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
                actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualCheckAuthorizationResponse);
            #endregion

            var request = new FreezeCustomerRequestDTO()
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
        public void FreezeCustomerBizOp_NOK_authorizationfailed()
        {
            #region Mock CheckAuthorization

            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse {IsAuthorized = false};

            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
                actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>())
                .ReturnsForAnyArgs(actualCheckAuthorizationResponse);

            #endregion

            var requestDto = new FreezeCustomerRequestDTO()
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
