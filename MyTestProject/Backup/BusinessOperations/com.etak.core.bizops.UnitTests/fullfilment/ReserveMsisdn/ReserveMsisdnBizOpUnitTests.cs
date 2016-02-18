using com.etak.core.bizops.fullfilment.ReserveMsisdn;
using com.etak.core.dealer.messages.CheckAuthorization;
using com.etak.core.dealer.messages.GetDealerInfoById;
using com.etak.core.model;
using com.etak.core.model.operation.messages;
using com.etak.core.operation;
using com.etak.core.operation.contract;
using com.etak.core.repository.crm.resource;
using com.etak.core.resource.msisdn.message.ReserveMsisdnMS;
using com.etak.core.test.utilities;
using com.etak.core.test.utilities.abstracts;
using com.etak.core.test.utilities.Helpers;
using NSubstitute;
using NUnit.Framework;

namespace com.etak.core.bizops.UnitTests.fullfilment.ReserveMsisdn
{
    [TestFixture()]
    public class ReserveMsisdnBizOpUnitTests : AbstractSinglePhaseOrderProcessorTest<ReserveMsisdnBizOp, ReserveMsisdnRequestDTO, ReserveMsisdnResponseDTO, ReserveMsisdnRequestInternal, ReserveMsisdnResponseInternal, ReserveMsisdnOrder>
    {
        private IMicroService<CheckAuthorizationRequest, CheckAuthorizationResponse> mockMicroServiceCheckAuthorization;

        [TestFixtureSetUp]
        public void Initialize()
        {
            FakeSessionFactorySingleton.Init();
            mockMicroServiceCheckAuthorization = MockMicroService<CheckAuthorizationRequest, CheckAuthorizationResponse>();
        }

        [Test()]
        public void ReserveMsisdnBizOp_CorrectRequestGiven_ShouldReserveMsisdnOk()
        {
        #region Mock CheckAuthorization
            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = true };
            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
                actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualCheckAuthorizationResponse);
        # endregion

            #region GetDealerInfoByIdMS mock

            var getDealerRequest = Arg.Is<GetDealerInfoByIdRequest>(x => x.DealerId == 1);
            var getDealerResponse = CreateDefaultObject.Create<GetDealerInfoByIdResponse>();
            getDealerResponse.DealerInfo = CreateDefaultObject.Create<DealerInfo>();

            var mockGetDealerInfo = MockMicroService<GetDealerInfoByIdRequest, GetDealerInfoByIdResponse>();
            mockGetDealerInfo.Process(getDealerRequest, Arg.Any<RequestInvokationEnvironment>()).Returns(getDealerResponse);

            #endregion

            #region ReserveMsisdnMS mock

            var reserveMsisdnRequest = Arg.Is<ReserveMsisdnRequest>(x => x.NumberPropertyInfo.Resource == "Resource1");
            var reserveMsisdnResponse = CreateDefaultObject.Create<ReserveMsisdnResponse>();
            reserveMsisdnResponse.ResultType = ResultTypes.Ok;
            reserveMsisdnResponse.NumberPropertyInfo = CreateDefaultObject.Create<NumberPropertyInfo>();

            var mockReserveMsisdn = MockMicroService<ReserveMsisdnRequest, ReserveMsisdnResponse>();
            mockReserveMsisdn.Process(reserveMsisdnRequest, Arg.Any<RequestInvokationEnvironment>()).Returns(reserveMsisdnResponse);

            #endregion

            var request = new ReserveMsisdnRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                MSISDN = "1",
            };

            MockAbstractSinglePhaseOrderProcessor(request);

            #region Mock INumberInfoRepository

            var expectedNumberInfo = CreateDefaultObject.Create<NumberInfo>();
            expectedNumberInfo.NumberProperty = CreateDefaultObject.Create<NumberPropertyInfo>();
            expectedNumberInfo.NumberProperty.StatusID = (int)ResourceStatus.Init;

            var mockRepo = MockRepositoryManager.GetMockedRepository<INumberInfoRepository<NumberInfo>>();
            mockRepo.GetById(Arg.Any<string>()).Returns(expectedNumberInfo);

            #endregion

            var response = CallBizOp(request);
            Assert.AreEqual(response.resultType, ResultTypes.Ok);
        }

        [Test()]
        public void ReserveMsisdnBizOp_CorrectRequestGivenNullMsisdn_ShouldFailedReserveMsisdn()
        {
            #region Mock CheckAuthorization
            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = true };
            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
                actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualCheckAuthorizationResponse);
            # endregion

            #region GetDealerInfoByIdMS mock

            var getDealerRequest = Arg.Is<GetDealerInfoByIdRequest>(x => x.DealerId == 1);
            var getDealerResponse = CreateDefaultObject.Create<GetDealerInfoByIdResponse>();
            getDealerResponse.DealerInfo = CreateDefaultObject.Create<DealerInfo>();

            var mockGetDealerInfo = MockMicroService<GetDealerInfoByIdRequest, GetDealerInfoByIdResponse>();
            mockGetDealerInfo.Process(getDealerRequest, Arg.Any<RequestInvokationEnvironment>()).Returns(getDealerResponse);

            #endregion

            #region ReserveMsisdnMS mock

            var reserveMsisdnRequest = Arg.Is<ReserveMsisdnRequest>(x => x.NumberPropertyInfo.Resource == "Resource1");
            var reserveMsisdnResponse = CreateDefaultObject.Create<ReserveMsisdnResponse>();
            reserveMsisdnResponse.ResultType = ResultTypes.Ok;
            reserveMsisdnResponse.NumberPropertyInfo = (NumberPropertyInfo)null;

            var mockReserveMsisdn = MockMicroService<ReserveMsisdnRequest, ReserveMsisdnResponse>();
            mockReserveMsisdn.Process(reserveMsisdnRequest, Arg.Any<RequestInvokationEnvironment>()).Returns(reserveMsisdnResponse);

            #endregion

            var request = new ReserveMsisdnRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                MSISDN = "1",
            };

            MockAbstractSinglePhaseOrderProcessor(request);

            #region Mock INumberInfoRepository

            var mockRepo = MockRepositoryManager.GetMockedRepository<INumberInfoRepository<NumberInfo>>();
            mockRepo.GetById(Arg.Any<string>()).Returns(((NumberInfo)null));

            #endregion

            var response = CallBizOp(request);
            Assert.AreEqual(response.resultType, ResultTypes.BussinessLogicError);
        }

        [Test()]
        public void ReserveMsisdnBizOp_IncorrectRequestGiven_ShouldThrowException()
        {
            #region Mock CheckAuthorization
            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = true };
            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
                actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualCheckAuthorizationResponse);
            # endregion

            #region GetDealerInfoByIdMS mock

            var getDealerRequest = Arg.Is<GetDealerInfoByIdRequest>(x => x.DealerId == 1);
            var getDealerResponse = CreateDefaultObject.Create<GetDealerInfoByIdResponse>();
            getDealerResponse.DealerInfo = CreateDefaultObject.Create<DealerInfo>();

            var mockGetDealerInfo = MockMicroService<GetDealerInfoByIdRequest, GetDealerInfoByIdResponse>();
            mockGetDealerInfo.Process(getDealerRequest, Arg.Any<RequestInvokationEnvironment>()).Returns(getDealerResponse);

            #endregion

            #region ReserveMsisdnMS mock

            var reserveMsisdnRequest = Arg.Is<ReserveMsisdnRequest>(x => x.NumberPropertyInfo.Resource == "Resource1");
            var reserveMsisdnResponse = CreateDefaultObject.Create<ReserveMsisdnResponse>();
            reserveMsisdnResponse.ResultType = ResultTypes.Ok;
            reserveMsisdnResponse.NumberPropertyInfo = CreateDefaultObject.Create<NumberPropertyInfo>();

            var mockReserveMsisdn = MockMicroService<ReserveMsisdnRequest, ReserveMsisdnResponse>();
            mockReserveMsisdn.Process(reserveMsisdnRequest, Arg.Any<RequestInvokationEnvironment>()).Returns(reserveMsisdnResponse);

            #endregion

            var request = new ReserveMsisdnRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                MSISDN = "1",
            };

            MockAbstractSinglePhaseOrderProcessor(request);

            #region Mock INumberInfoRepository

            var expectedNumberInfo = CreateDefaultObject.Create<NumberInfo>();
            expectedNumberInfo.NumberProperty = CreateDefaultObject.Create<NumberPropertyInfo>();
            expectedNumberInfo.NumberProperty.StatusID = (int)ResourceStatus.Reserved;

            var mockRepo = MockRepositoryManager.GetMockedRepository<INumberInfoRepository<NumberInfo>>();
            mockRepo.GetById(Arg.Any<string>()).Returns(expectedNumberInfo);

            #endregion

            var response = CallBizOp(request);
            Assert.AreNotEqual(response.resultType, ResultTypes.Ok);
        }
        [Test()]
        public void ReserveMsisdnBizOp_NOK_authorizationfailed()
        {
            #region Mock CheckAuthorization
            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = false };

            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
                actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualCheckAuthorizationResponse);
            #endregion

            var requestDto = new ReserveMsisdnRequestDTO()
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