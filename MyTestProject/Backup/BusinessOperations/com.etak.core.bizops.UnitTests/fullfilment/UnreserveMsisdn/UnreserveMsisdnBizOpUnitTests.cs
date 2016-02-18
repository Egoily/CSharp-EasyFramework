using System.Collections.Generic;
using System.Linq;
using com.etak.core.bizops.fullfilment.UnreserveMsisdn;
using com.etak.core.dealer.messages.CheckAuthorization;
using com.etak.core.dealer.messages.GetDealerInfoById;
using com.etak.core.model;
using com.etak.core.model.operation.messages;
using com.etak.core.operation;
using com.etak.core.operation.contract;
using com.etak.core.repository.crm.resource;
using com.etak.core.resource.msisdn.message.GetDealerNumberByResource;
using com.etak.core.resource.msisdn.message.UnReserveMsisdnMS;
using com.etak.core.test.utilities;
using com.etak.core.test.utilities.abstracts;
using com.etak.core.test.utilities.Helpers;
using NSubstitute;
using NUnit.Framework;

namespace com.etak.core.bizops.UnitTests.fullfilment.UnreserveMsisdn
{
    [TestFixture]
    public class UnreserveMsisdnBizOpUnitTests : AbstractSinglePhaseOrderProcessorTest<UnreserveMsisdnBizOp, UnreserveMsisdnRequestDTO, UnreserveMsisdnResponseDTO, UnreserveMsisdnRequestInternal, UnreserveMsisdnResponseInternal, UnreserveMsisdnOrder>
    {
        private IMicroService<CheckAuthorizationRequest, CheckAuthorizationResponse> mockMicroServiceCheckAuthorization;
        [TestFixtureSetUp]
        public void Initialize()
        {
            FakeSessionFactorySingleton.Init();
            mockMicroServiceCheckAuthorization = MockMicroService<CheckAuthorizationRequest, CheckAuthorizationResponse>();
        }

        [Test()]
        public void UnreserveMsisdnBizOp_CorrectRequestGiven_ShouldReturnModifiedMsisdnIntoUnreserved()
        {
            //Mock GetDealerNumberByResourceMS
            var getDealerNumberByResourceReq = Arg.Is<GetDealerNumberByResourceRequest>(x => x.Msisdn == "0819191929");
            var getDealerNumberByResourceMSMock =
                MockMicroServiceManager
                    .GetMockedMicroService
                    <GetDealerNumberByResourceRequest, GetDealerNumberByResourceResponse>();
            var getDealerNumberByResourceRes = new GetDealerNumberByResourceResponse()
            {
                DealerNumberInfo = new List<DealerNumberInfo> { CreateDefaultObject.Create<DealerNumberInfo>() }
            };
            getDealerNumberByResourceMSMock.Process(getDealerNumberByResourceReq,
                Arg.Any<RequestInvokationEnvironment>()).Returns(getDealerNumberByResourceRes);

            #region Mock CheckAuthorization
            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = true };

            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
                actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualCheckAuthorizationResponse);
            #endregion

            //Mock GetDealerInfoByIdMS
            var getDealerInfoByIdReq = Arg.Is<GetDealerInfoByIdRequest>(x => x.DealerId == getDealerNumberByResourceRes.DealerNumberInfo.FirstOrDefault().DealerID);
            var getDealerInfoByIdMSMock =
                MockMicroServiceManager
                    .GetMockedMicroService
                    <GetDealerInfoByIdRequest, GetDealerInfoByIdResponse>();
            var getDealerInfoByIdRes = new GetDealerInfoByIdResponse()
            {
                DealerInfo = CreateDefaultObject.Create<DealerInfo>()
            };
            getDealerInfoByIdMSMock.Process(getDealerInfoByIdReq,
                Arg.Any<RequestInvokationEnvironment>()).Returns(getDealerInfoByIdRes);

            //Mock UnReserveMsisdnMS
            var unReserveMsisdnReq = Arg.Is<UnReserveMsisdnRequest>(x => x.NumberPropertyInfo.Resource == "0819191929");
            var unReserveMsisdnMSMock =
                MockMicroServiceManager
                    .GetMockedMicroService
                    <UnReserveMsisdnRequest, UnReserveMsisdnResponse>();
            var numberPropertyInfo = CreateDefaultObject.Create<NumberPropertyInfo>();
            numberPropertyInfo.StatusID = (int)ResourceStatus.Reserved;

            var unReserveMsisdnRes = new UnReserveMsisdnResponse()
            {
                NumberPropertyInfo = numberPropertyInfo
            };
            unReserveMsisdnMSMock.Process(unReserveMsisdnReq,
                Arg.Any<RequestInvokationEnvironment>()).Returns(unReserveMsisdnRes);

            var unreserveMsisdnRequestDTO = new UnreserveMsisdnRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                MSISDN = "0819191929"
            };

            MockAbstractSinglePhaseOrderProcessor(unreserveMsisdnRequestDTO);

            //Remock Repository NumberInfo
            var mockedRepoNumberInfo =
                        MockRepositoryManager.GetMockedRepository<INumberInfoRepository<NumberInfo>>();
            var numberInfo = CreateDefaultObject.Create<NumberInfo>();
            numberInfo.NumberProperty = CreateDefaultObject.Create<NumberPropertyInfo>();
            numberInfo.NumberProperty.StatusID = (int)ResourceStatus.Reserved;
            numberInfo.Resource = "0819191929";
            mockedRepoNumberInfo.GetById("0819191929").Returns(numberInfo);

            var actualUnreserveMsisdnResponseDTO = CallBizOp(unreserveMsisdnRequestDTO);

            AssertExt.ObjectPropertiesAreEqual(ResultTypes.Ok, actualUnreserveMsisdnResponseDTO.resultType);
        }

        [Test()]
        public void UnreserveMsisdnBizOp_DealerNumberNull_ShouldThrowDataValidationException()
        {
            //Mock GetDealerNumberByResourceMS
            var getDealerNumberByResourceReq = Arg.Is<GetDealerNumberByResourceRequest>(x => x.Msisdn == "0819191929");
            var getDealerNumberByResourceMSMock =
                MockMicroServiceManager
                    .GetMockedMicroService
                    <GetDealerNumberByResourceRequest, GetDealerNumberByResourceResponse>();
            var getDealerNumberByResourceRes = new GetDealerNumberByResourceResponse()
            {
                DealerNumberInfo = new List<DealerNumberInfo> { }
            };
            getDealerNumberByResourceMSMock.Process(getDealerNumberByResourceReq,
                Arg.Any<RequestInvokationEnvironment>()).Returns(getDealerNumberByResourceRes);

            #region Mock CheckAuthorization
            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = true };

            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
                actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualCheckAuthorizationResponse);
            #endregion

            var unreserveMsisdnRequestDTO = new UnreserveMsisdnRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                MSISDN = "0819191929"
            };

            MockAbstractSinglePhaseOrderProcessor(unreserveMsisdnRequestDTO);

            //Remock Repository NumberInfo
            var mockedRepoNumberInfo =
                        MockRepositoryManager.GetMockedRepository<INumberInfoRepository<NumberInfo>>();
            var numberInfo = CreateDefaultObject.Create<NumberInfo>();
            numberInfo.NumberProperty = CreateDefaultObject.Create<NumberPropertyInfo>();
            numberInfo.NumberProperty.StatusID = (int)ResourceStatus.Reserved;
            numberInfo.Resource = "0819191929";
            mockedRepoNumberInfo.GetById("0819191929").Returns(numberInfo);

            var actualUnreserveMsisdnResponseDTO = CallBizOp(unreserveMsisdnRequestDTO);

            AssertExt.ObjectPropertiesAreEqual(ResultTypes.DataValidationError, actualUnreserveMsisdnResponseDTO.resultType);
        }

        [Test()]
        public void UnreserveMsisdnBizOp_DealerInfoNull_ShouldThrowBusinessLogicException()
        {
            //Mock GetDealerNumberByResourceMS
            var getDealerNumberByResourceReq = Arg.Is<GetDealerNumberByResourceRequest>(x => x.Msisdn == "0819191929");
            var getDealerNumberByResourceMSMock =
                MockMicroServiceManager
                    .GetMockedMicroService
                    <GetDealerNumberByResourceRequest, GetDealerNumberByResourceResponse>();
            var getDealerNumberByResourceRes = new GetDealerNumberByResourceResponse()
            {
                DealerNumberInfo = new List<DealerNumberInfo> { CreateDefaultObject.Create<DealerNumberInfo>() }
            };
            getDealerNumberByResourceMSMock.Process(getDealerNumberByResourceReq,
                Arg.Any<RequestInvokationEnvironment>()).Returns(getDealerNumberByResourceRes);

            #region Mock CheckAuthorization
            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = true };

            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
                actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualCheckAuthorizationResponse);
            #endregion

            //Mock GetDealerInfoByIdMS
            var getDealerInfoByIdReq = Arg.Is<GetDealerInfoByIdRequest>(x => x.DealerId == getDealerNumberByResourceRes.DealerNumberInfo.FirstOrDefault().DealerID);
            var getDealerInfoByIdMSMock =
                MockMicroServiceManager
                    .GetMockedMicroService
                    <GetDealerInfoByIdRequest, GetDealerInfoByIdResponse>();
            var getDealerInfoByIdRes = new GetDealerInfoByIdResponse()
            {
                DealerInfo = null
            };
            getDealerInfoByIdMSMock.Process(getDealerInfoByIdReq,
                Arg.Any<RequestInvokationEnvironment>()).Returns(getDealerInfoByIdRes);

            //Mock UnReserveMsisdnMS
            var unReserveMsisdnReq = Arg.Is<UnReserveMsisdnRequest>(x => x.NumberPropertyInfo.Resource == "0819191929");
            var unReserveMsisdnMSMock =
                MockMicroServiceManager
                    .GetMockedMicroService
                    <UnReserveMsisdnRequest, UnReserveMsisdnResponse>();
            var numberPropertyInfo = CreateDefaultObject.Create<NumberPropertyInfo>();
            numberPropertyInfo.StatusID = (int)ResourceStatus.Reserved;

            var unReserveMsisdnRes = new UnReserveMsisdnResponse()
            {
                NumberPropertyInfo = numberPropertyInfo
            };
            unReserveMsisdnMSMock.Process(unReserveMsisdnReq,
                Arg.Any<RequestInvokationEnvironment>()).Returns(unReserveMsisdnRes);

            var unreserveMsisdnRequestDTO = new UnreserveMsisdnRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                MSISDN = "0819191929"
            };

            MockAbstractSinglePhaseOrderProcessor(unreserveMsisdnRequestDTO);

            //Remock Repository NumberInfo
            var mockedRepoNumberInfo =
                        MockRepositoryManager.GetMockedRepository<INumberInfoRepository<NumberInfo>>();
            var numberInfo = CreateDefaultObject.Create<NumberInfo>();
            numberInfo.NumberProperty = CreateDefaultObject.Create<NumberPropertyInfo>();
            numberInfo.NumberProperty.StatusID = (int)ResourceStatus.Reserved;
            numberInfo.Resource = "0819191929";
            mockedRepoNumberInfo.GetById("0819191929").Returns(numberInfo);

            var actualUnreserveMsisdnResponseDTO = CallBizOp(unreserveMsisdnRequestDTO);

            AssertExt.ObjectPropertiesAreEqual(ResultTypes.BussinessLogicError, actualUnreserveMsisdnResponseDTO.resultType);
        }

        [Test()]
        public void UnreserveMsisdnBizOp_FiscalUnitIdNotMatch_ShouldThrowDataValidationException()
        {
            //Mock GetDealerNumberByResourceMS
            var getDealerNumberByResourceReq = Arg.Is<GetDealerNumberByResourceRequest>(x => x.Msisdn == "0819191929");
            var getDealerNumberByResourceMSMock =
                MockMicroServiceManager
                    .GetMockedMicroService
                    <GetDealerNumberByResourceRequest, GetDealerNumberByResourceResponse>();
            var getDealerNumberByResourceRes = new GetDealerNumberByResourceResponse()
            {
                DealerNumberInfo = new List<DealerNumberInfo> { CreateDefaultObject.Create<DealerNumberInfo>() }
            };
            getDealerNumberByResourceMSMock.Process(getDealerNumberByResourceReq,
                Arg.Any<RequestInvokationEnvironment>()).Returns(getDealerNumberByResourceRes);

            #region Mock CheckAuthorization
            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = true };

            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
                actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualCheckAuthorizationResponse);
            #endregion

            //Mock GetDealerInfoByIdMS
            var getDealerInfoByIdReq = Arg.Is<GetDealerInfoByIdRequest>(x => x.DealerId == getDealerNumberByResourceRes.DealerNumberInfo.FirstOrDefault().DealerID);
            var getDealerInfoByIdMSMock =
                MockMicroServiceManager
                    .GetMockedMicroService
                    <GetDealerInfoByIdRequest, GetDealerInfoByIdResponse>();
            var getDealerInfoByIdRes = new GetDealerInfoByIdResponse()
            {
                DealerInfo = CreateDefaultObject.Create<DealerInfo>()
            };
            getDealerInfoByIdRes.DealerInfo.FiscalUnitID = 300;
            getDealerInfoByIdMSMock.Process(getDealerInfoByIdReq,
                Arg.Any<RequestInvokationEnvironment>()).Returns(getDealerInfoByIdRes);

            //Mock UnReserveMsisdnMS
            var unReserveMsisdnReq = Arg.Is<UnReserveMsisdnRequest>(x => x.NumberPropertyInfo.Resource == "0819191929");
            var unReserveMsisdnMSMock =
                MockMicroServiceManager
                    .GetMockedMicroService
                    <UnReserveMsisdnRequest, UnReserveMsisdnResponse>();
            var numberPropertyInfo = CreateDefaultObject.Create<NumberPropertyInfo>();
            numberPropertyInfo.StatusID = (int)ResourceStatus.Reserved;

            var unReserveMsisdnRes = new UnReserveMsisdnResponse()
            {
                NumberPropertyInfo = numberPropertyInfo
            };
            unReserveMsisdnMSMock.Process(unReserveMsisdnReq,
                Arg.Any<RequestInvokationEnvironment>()).Returns(unReserveMsisdnRes);

            var unreserveMsisdnRequestDTO = new UnreserveMsisdnRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                MSISDN = "0819191929"
            };

            MockAbstractSinglePhaseOrderProcessor(unreserveMsisdnRequestDTO);

            //Remock Repository NumberInfo
            var mockedRepoNumberInfo =
                        MockRepositoryManager.GetMockedRepository<INumberInfoRepository<NumberInfo>>();
            var numberInfo = CreateDefaultObject.Create<NumberInfo>();
            numberInfo.NumberProperty = CreateDefaultObject.Create<NumberPropertyInfo>();
            numberInfo.NumberProperty.StatusID = (int)ResourceStatus.Reserved;
            numberInfo.Resource = "0819191929";
            mockedRepoNumberInfo.GetById("0819191929").Returns(numberInfo);

            var actualUnreserveMsisdnResponseDTO = CallBizOp(unreserveMsisdnRequestDTO);

            AssertExt.ObjectPropertiesAreEqual(ResultTypes.DataValidationError, actualUnreserveMsisdnResponseDTO.resultType);
        }

        [Test()]
        public void UnreserveMsisdnBizOp_StatusIsNotInReserved_ShouldThrowDataValidationException()
        {
            //Mock GetDealerNumberByResourceMS
            var getDealerNumberByResourceReq = Arg.Is<GetDealerNumberByResourceRequest>(x => x.Msisdn == "0819191929");
            var getDealerNumberByResourceMSMock =
                MockMicroServiceManager
                    .GetMockedMicroService
                    <GetDealerNumberByResourceRequest, GetDealerNumberByResourceResponse>();
            var getDealerNumberByResourceRes = new GetDealerNumberByResourceResponse()
            {
                DealerNumberInfo = new List<DealerNumberInfo> { CreateDefaultObject.Create<DealerNumberInfo>() }
            };
            getDealerNumberByResourceMSMock.Process(getDealerNumberByResourceReq,
                Arg.Any<RequestInvokationEnvironment>()).Returns(getDealerNumberByResourceRes);

            #region Mock CheckAuthorization
            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = true };

            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
                actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualCheckAuthorizationResponse);
            #endregion

            //Mock GetDealerInfoByIdMS
            var getDealerInfoByIdReq = Arg.Is<GetDealerInfoByIdRequest>(x => x.DealerId == getDealerNumberByResourceRes.DealerNumberInfo.FirstOrDefault().DealerID);
            var getDealerInfoByIdMSMock =
                MockMicroServiceManager
                    .GetMockedMicroService
                    <GetDealerInfoByIdRequest, GetDealerInfoByIdResponse>();
            var getDealerInfoByIdRes = new GetDealerInfoByIdResponse()
            {
                DealerInfo = CreateDefaultObject.Create<DealerInfo>()
            };
            getDealerInfoByIdMSMock.Process(getDealerInfoByIdReq,
                Arg.Any<RequestInvokationEnvironment>()).Returns(getDealerInfoByIdRes);

            //Mock UnReserveMsisdnMS
            var unReserveMsisdnReq = Arg.Is<UnReserveMsisdnRequest>(x => x.NumberPropertyInfo.Resource == "0819191929");
            var unReserveMsisdnMSMock =
                MockMicroServiceManager
                    .GetMockedMicroService
                    <UnReserveMsisdnRequest, UnReserveMsisdnResponse>();
            var numberPropertyInfo = CreateDefaultObject.Create<NumberPropertyInfo>();
            numberPropertyInfo.StatusID = (int)ResourceStatus.Init;

            var unReserveMsisdnRes = new UnReserveMsisdnResponse()
            {
                NumberPropertyInfo = numberPropertyInfo
            };
            unReserveMsisdnMSMock.Process(unReserveMsisdnReq,
                Arg.Any<RequestInvokationEnvironment>()).Returns(unReserveMsisdnRes);

            var unreserveMsisdnRequestDTO = new UnreserveMsisdnRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                MSISDN = "0819191929"
            };

            MockAbstractSinglePhaseOrderProcessor(unreserveMsisdnRequestDTO);

            //Remock Repository NumberInfo
            var mockedRepoNumberInfo =
                        MockRepositoryManager.GetMockedRepository<INumberInfoRepository<NumberInfo>>();
            var numberInfo = CreateDefaultObject.Create<NumberInfo>();
            numberInfo.NumberProperty = CreateDefaultObject.Create<NumberPropertyInfo>();
            numberInfo.NumberProperty.StatusID = (int)ResourceStatus.Reserved;
            numberInfo.Resource = "0819191929";
            mockedRepoNumberInfo.GetById("0819191929").Returns(numberInfo);

            var actualUnreserveMsisdnResponseDTO = CallBizOp(unreserveMsisdnRequestDTO);

            AssertExt.ObjectPropertiesAreEqual(ResultTypes.DataValidationError, actualUnreserveMsisdnResponseDTO.resultType);
        }

        [Test()]
        public void UnreserveMsisdnBizOp_StatusIdIsNull_ShouldThrowDataValidationException()
        {
            //Mock GetDealerNumberByResourceMS
            var getDealerNumberByResourceReq = Arg.Is<GetDealerNumberByResourceRequest>(x => x.Msisdn == "0819191929");
            var getDealerNumberByResourceMSMock =
                MockMicroServiceManager
                    .GetMockedMicroService
                    <GetDealerNumberByResourceRequest, GetDealerNumberByResourceResponse>();
            var getDealerNumberByResourceRes = new GetDealerNumberByResourceResponse()
            {
                DealerNumberInfo = new List<DealerNumberInfo> { CreateDefaultObject.Create<DealerNumberInfo>() }
            };
            getDealerNumberByResourceMSMock.Process(getDealerNumberByResourceReq,
                Arg.Any<RequestInvokationEnvironment>()).Returns(getDealerNumberByResourceRes);

            #region Mock CheckAuthorization
            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = true };

            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
                actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualCheckAuthorizationResponse);
            #endregion

            //Mock GetDealerInfoByIdMS
            var getDealerInfoByIdReq = Arg.Is<GetDealerInfoByIdRequest>(x => x.DealerId == getDealerNumberByResourceRes.DealerNumberInfo.FirstOrDefault().DealerID);
            var getDealerInfoByIdMSMock =
                MockMicroServiceManager
                    .GetMockedMicroService
                    <GetDealerInfoByIdRequest, GetDealerInfoByIdResponse>();
            var getDealerInfoByIdRes = new GetDealerInfoByIdResponse()
            {
                DealerInfo = CreateDefaultObject.Create<DealerInfo>()
            };
            getDealerInfoByIdMSMock.Process(getDealerInfoByIdReq,
                Arg.Any<RequestInvokationEnvironment>()).Returns(getDealerInfoByIdRes);

            //Mock UnReserveMsisdnMS
            var unReserveMsisdnReq = Arg.Is<UnReserveMsisdnRequest>(x => x.NumberPropertyInfo.Resource == "0819191929");
            var unReserveMsisdnMSMock =
                MockMicroServiceManager
                    .GetMockedMicroService
                    <UnReserveMsisdnRequest, UnReserveMsisdnResponse>();
            var numberPropertyInfo = CreateDefaultObject.Create<NumberPropertyInfo>();
            numberPropertyInfo.StatusID = null;

            var unReserveMsisdnRes = new UnReserveMsisdnResponse()
            {
                NumberPropertyInfo = numberPropertyInfo
            };
            unReserveMsisdnMSMock.Process(unReserveMsisdnReq,
                Arg.Any<RequestInvokationEnvironment>()).Returns(unReserveMsisdnRes);

            var unreserveMsisdnRequestDTO = new UnreserveMsisdnRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                MSISDN = "0819191929"
            };

            MockAbstractSinglePhaseOrderProcessor(unreserveMsisdnRequestDTO);

            //Remock Repository NumberInfo
            var mockedRepoNumberInfo =
                        MockRepositoryManager.GetMockedRepository<INumberInfoRepository<NumberInfo>>();
            var numberInfo = CreateDefaultObject.Create<NumberInfo>();
            numberInfo.NumberProperty = CreateDefaultObject.Create<NumberPropertyInfo>();
            numberInfo.NumberProperty.StatusID = (int)ResourceStatus.Reserved;
            numberInfo.Resource = "0819191929";
            mockedRepoNumberInfo.GetById("0819191929").Returns(numberInfo);

            var actualUnreserveMsisdnResponseDTO = CallBizOp(unreserveMsisdnRequestDTO);

            AssertExt.ObjectPropertiesAreEqual(ResultTypes.DataValidationError, actualUnreserveMsisdnResponseDTO.resultType);
        }

        [Test()]
        public void UnreserveMsisdnBizOp_NOK_authorizationfailed()
        {
            //Mock GetDealerNumberByResourceMS
            var getDealerNumberByResourceReq = Arg.Is<GetDealerNumberByResourceRequest>(x => x.Msisdn == "0819191929");
            var getDealerNumberByResourceMSMock =
                MockMicroServiceManager
                    .GetMockedMicroService
                    <GetDealerNumberByResourceRequest, GetDealerNumberByResourceResponse>();
            var getDealerNumberByResourceRes = new GetDealerNumberByResourceResponse()
            {
                DealerNumberInfo = new List<DealerNumberInfo> { CreateDefaultObject.Create<DealerNumberInfo>() }
            };
            getDealerNumberByResourceMSMock.Process(getDealerNumberByResourceReq,
                Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(getDealerNumberByResourceRes);

            #region Mock CheckAuthorization
            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = false };

            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
                actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualCheckAuthorizationResponse);
            #endregion

            var requestDto = new UnreserveMsisdnRequestDTO()
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