using System;
using System.Collections.Generic;
using com.etak.core.bizops.fullfilment.SwapSimCard;
using com.etak.core.dealer.messages.CheckAuthorization;
using com.etak.core.GSMSubscription.messages.CreateHLRRequestErrors;
using com.etak.core.GSMSubscription.messages.GetResourceMBInfosByICCID;
using com.etak.core.GSMSubscription.messages.UpdateSubscription;
using com.etak.core.model;
using com.etak.core.model.dto;
using com.etak.core.model.operation.messages;
using com.etak.core.model.provisioning;
using com.etak.core.model.resource;
using com.etak.core.model.subscription;
using com.etak.core.operation;
using com.etak.core.operation.contract;
using com.etak.core.resource.simCard.message.ActiveSimCard;
using com.etak.core.resource.simCard.message.ExpirateSimCard;
using com.etak.core.resource.simCard.message.GetSimCardByICCId;
using com.etak.core.resource.simCard.message.HLRSwapResources;
using com.etak.core.test.utilities;
using com.etak.core.test.utilities.abstracts;
using com.etak.core.test.utilities.Helpers;
using FrontEndServiceContract;
using FrontEndServiceContract.messages;
using FrontEndServiceContract.messages.provision;
using Network3GPPModel;
using NSubstitute;
using NUnit.Framework;

namespace com.etak.core.bizops.UnitTests.fullfilment.SwapSimCard
{
    [TestFixture()]
    public class SwapSimCardBizOpTests : AbstractSinglePhaseOrderProcessorTest<SwapSimCardBizOp, SwapSimCardRequestDTO, SwapSimCardResponseDTO, SwapSimCardRequestInternal, SwapSimCardResponseInternal, SwapSimCardOrder>
    {
        private IProvisionService mockedProvisionService = null;
        private IMicroService<CheckAuthorizationRequest, CheckAuthorizationResponse> mockMicroServiceCheckAuthorization;
        [TestFixtureSetUp]
        public void Initialize()
        {
            FakeSessionFactorySingleton.Init();
            mockMicroServiceCheckAuthorization = MockMicroService<CheckAuthorizationRequest, CheckAuthorizationResponse>();
        }

        [Test()]
        public void SwapSimCardBizOpOk_CorrectRequestResource_ShouldReturnSimCardInfo()
        {
            #region Mock CheckAuthorization
            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = true };

            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
                actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualCheckAuthorizationResponse);
            #endregion

            var simCardInfoMock = CreateDefaultObject.Create<SIMCardInfo>();
            var resourceMBInfoMock = CreateDefaultObject.Create<ResourceMBInfo>();
            var hlrRequestErrorsMock = CreateDefaultObject.Create<HLRRequestErrors>();

            var getResourceMBInfosByICCIDMsMock =
                MockMicroService<GetResourceMBInfosByICCIDRequest, GetResourceMBInfosByICCIDResponse>();
            var getResourceMBInfosByICCIDRequest = Arg.Is<GetResourceMBInfosByICCIDRequest>(x => x.ICCID == "1000");
            var getResourceMBInfos = CreateDefaultObject.Create<ResourceMBInfo>();
            getResourceMBInfos.CustomerInfo = CreateDefaultObject.Create<CustomerInfo>();
            getResourceMBInfos.CustomerInfo.CustomerID = 5;
            getResourceMBInfos.CustomerInfo = CreateDefaultObject.Create<CustomerInfo>();
            var getResourceMBInfosByICCIDResponse = new GetResourceMBInfosByICCIDResponse()
            {

                ResourceMbInfos = new List<ResourceMBInfo>() { getResourceMBInfos }
            };
            getResourceMBInfosByICCIDMsMock.Process(getResourceMBInfosByICCIDRequest,
                Arg.Any<RequestInvokationEnvironment>()).Returns(getResourceMBInfosByICCIDResponse);

            var getSimCardByICCIdMSMock = MockMicroService<GetSimCardByICCIdRequest, GetSimCardByICCIdResponse>();
            var getSimCardByICCIdRequest = Arg.Is<GetSimCardByICCIdRequest>(x => x.IccId == "1000A");
            var getSimCardByICCIdResponse = new GetSimCardByICCIdResponse()
            {
                SimCardInfo = CreateDefaultObject.Create<SIMCardInfo>()
            };
            getSimCardByICCIdResponse.SimCardInfo.Status = 0;
            getSimCardByICCIdMSMock.Process(getSimCardByICCIdRequest, Arg.Any<RequestInvokationEnvironment>())
                .Returns(getSimCardByICCIdResponse);

            
            var expirateSimCardMSMock = MockMicroService<ExpirateSimCardRequest, ExpirateSimCardResponse>();
            var expirateSimCardRequest =
                Arg.Is<ExpirateSimCardRequest>(x => x.SimCardInfo == simCardInfoMock);
            var expirateSimCardResponse = new ExpirateSimCardResponse()
            {
                SimCardInfo = CreateDefaultObject.Create<SIMCardInfo>()
            };
            expirateSimCardMSMock.Process(expirateSimCardRequest, Arg.Any<RequestInvokationEnvironment>())
                .Returns(expirateSimCardResponse);


            var activeSimCardMSMock = MockMicroService<ActiveSimCardRequest, ActiveSimCardResponse>();
            var activeSimCardRequest = Arg.Is<ActiveSimCardRequest>(x => x.SimCardInfo == simCardInfoMock);
            var activeSimCardResponse = new ActiveSimCardResponse()
            {
                SimCardInfo = CreateDefaultObject.Create<SIMCardInfo>()
            };
            activeSimCardMSMock.Process(activeSimCardRequest, Arg.Any<RequestInvokationEnvironment>())
                .Returns(activeSimCardResponse);


            var updateSubscriptionMSMock = MockMicroService<UpdateSubscriptionRequest, UpdateSubscriptionResponse>();
            var updateSubscriptionRequest = Arg.Is<UpdateSubscriptionRequest>(x => x.SIMCardSubcriber == simCardInfoMock && x.SubcriberInfo == resourceMBInfoMock);
            var updateSubscriptionResponse = new UpdateSubscriptionResponse()
            {
                SubscriberResourceMB = CreateDefaultObject.Create<ResourceMBInfo>()
            };
            updateSubscriptionMSMock.Process(updateSubscriptionRequest, Arg.Any<RequestInvokationEnvironment>())
                .Returns(updateSubscriptionResponse);

            var createHLRRequestErrorsMSMock =
                MockMicroService<CreateHLRRequestErrorsRequest, CreateHLRRequestErrorsResponse>();
            var createHLRRequestErrorsRequest =
                Arg.Is<CreateHLRRequestErrorsRequest>(x => x.HLRRequestErrorsObj == hlrRequestErrorsMock);
            var createHLRRequestErrorsResponse = new CreateHLRRequestErrorsResponse()
            {
                HLRRequestErrorsObj = CreateDefaultObject.Create<HLRRequestErrors>()
            };
            createHLRRequestErrorsMSMock.Process(createHLRRequestErrorsRequest, Arg.Any<RequestInvokationEnvironment>())
                .Returns(createHLRRequestErrorsResponse);

            mockedProvisionService = Substitute.For<IProvisionService>();
            mockedProvisionService.SwapResources(Arg.Any<SwapResourcesRequest>())
                .Returns(new SwapResourcesResponse() { result = new HPSResult() { ErrorCode = HLRErrorCodes.Ok } });

            var hLRSwapResourcesMS = MockMicroService<HLRSwapResourcesRequest, HLRSwapResourcesResponse>();
            var hLRSwapResourcesMSRequest =
                Arg.Is<HLRSwapResourcesRequest>(
                    x => x.Msisdn == "1000" && x.SourceSim == simCardInfoMock && x.DestinationSim == simCardInfoMock);

            

            var hLRSwapResourcesMSResponse = new HLRSwapResourcesResponse()
            {
                SimCardInfo = simCardInfoMock
            };
            hLRSwapResourcesMS.Process(hLRSwapResourcesMSRequest, Arg.Any<RequestInvokationEnvironment>()).Returns(hLRSwapResourcesMSResponse);

            var SwapSimCardRequestDTO = new SwapSimCardRequestDTO()
            {
                ICCID = "1000",
                vmno = "970100",
                Msisdn = "Resource1",
                password = "123456",
                user = "1644000204",
                NewIccId = "1000A",
                OldIccId = "1000B",
           };

            MockAbstractSinglePhaseOrderProcessor(SwapSimCardRequestDTO);
            var swapSimCardResponseDTO = CallBizOp(SwapSimCardRequestDTO);
            swapSimCardResponseDTO.SimCard.DealerID = 1;
            var expectedSwapSimCard = new SwapSimCardResponseDTO()
            {
                SimCard = CreateDefaultObject.Create<SimCardDTO>(),
                Subscription = new SubscriptionDTO { CustomerId = getResourceMBInfos.CustomerInfo.CustomerID.Value },
                Customer = new CustomerDTO { CustomerId = getResourceMBInfos.CustomerInfo.CustomerID.Value }
            };
            expectedSwapSimCard.SimCard.Status = 0;
            AssertExt.ObjectPropertiesAreEqual(swapSimCardResponseDTO.SimCard, expectedSwapSimCard.SimCard);
            Assert.IsTrue(expectedSwapSimCard.resultType == ResultTypes.Ok);
            Assert.IsTrue(expectedSwapSimCard.Customer.CustomerId == swapSimCardResponseDTO.Customer.CustomerId);
            Assert.IsTrue(expectedSwapSimCard.Subscription.CustomerId == swapSimCardResponseDTO.Subscription.CustomerId);
        }

        [Test()]
        public void SwapSimCardBizOp_CorrectRequestResource_ShouldReturnErrorSimCardInfo()
        {
            #region Mock CheckAuthorization
            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = true };

            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
                actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualCheckAuthorizationResponse);
            #endregion

            var simCardInfoMock = CreateDefaultObject.Create<SIMCardInfo>();
            var resourceMBInfoMock = CreateDefaultObject.Create<ResourceMBInfo>();
            var hlrRequestErrorsMock = CreateDefaultObject.Create<HLRRequestErrors>();

            var getResourceMBInfosByICCIDMsMock =
                MockMicroService<GetResourceMBInfosByICCIDRequest, GetResourceMBInfosByICCIDResponse>();
            var getResourceMBInfosByICCIDRequest = Arg.Is<GetResourceMBInfosByICCIDRequest>(x => x.ICCID == "2000");
            var getResourceMBInfos = CreateDefaultObject.Create<ResourceMBInfo>();
            getResourceMBInfos.CustomerInfo = CreateDefaultObject.Create<CustomerInfo>();
            getResourceMBInfosByICCIDMsMock.Process(getResourceMBInfosByICCIDRequest,
                Arg.Any<RequestInvokationEnvironment>()).Returns(x=>{throw new Exception("Error");});

            var getSimCardByICCIdMSMock = MockMicroService<GetSimCardByICCIdRequest, GetSimCardByICCIdResponse>();
            var getSimCardByICCIdRequest = Arg.Is<GetSimCardByICCIdRequest>(x => x.IccId == "2000A");
            getSimCardByICCIdMSMock.Process(getSimCardByICCIdRequest, Arg.Any<RequestInvokationEnvironment>())
                .Returns(x => { throw new Exception("Error"); });


            var expirateSimCardMSMock = MockMicroService<ExpirateSimCardRequest, ExpirateSimCardResponse>();
            var expirateSimCardRequest =
                Arg.Is<ExpirateSimCardRequest>(x => x.SimCardInfo == simCardInfoMock);
            expirateSimCardMSMock.Process(expirateSimCardRequest, Arg.Any<RequestInvokationEnvironment>())
                .Returns(x => { throw new Exception("Error"); });


            var activeSimCardMSMock = MockMicroService<ActiveSimCardRequest, ActiveSimCardResponse>();
            var activeSimCardRequest = Arg.Is<ActiveSimCardRequest>(x => x.SimCardInfo == simCardInfoMock);
            activeSimCardMSMock.Process(activeSimCardRequest, Arg.Any<RequestInvokationEnvironment>())
                .Returns(x => { throw new Exception("Error"); });


            var updateSubscriptionMSMock = MockMicroService<UpdateSubscriptionRequest, UpdateSubscriptionResponse>();
            var updateSubscriptionRequest = Arg.Is<UpdateSubscriptionRequest>(x => x.SIMCardSubcriber == simCardInfoMock && x.SubcriberInfo == resourceMBInfoMock);
            updateSubscriptionMSMock.Process(updateSubscriptionRequest, Arg.Any<RequestInvokationEnvironment>())
                .Returns(x => { throw new Exception("Error"); });

            var createHLRRequestErrorsMSMock =
                MockMicroService<CreateHLRRequestErrorsRequest, CreateHLRRequestErrorsResponse>();
            var createHLRRequestErrorsRequest =
                Arg.Is<CreateHLRRequestErrorsRequest>(x => x.HLRRequestErrorsObj == hlrRequestErrorsMock);
            createHLRRequestErrorsMSMock.Process(createHLRRequestErrorsRequest, Arg.Any<RequestInvokationEnvironment>())
                .Returns(x => { throw new Exception("Error"); });

            mockedProvisionService = Substitute.For<IProvisionService>();
            mockedProvisionService.SwapResources(Arg.Any<SwapResourcesRequest>())
                .Returns(new SwapResourcesResponse() { result = new HPSResult() { ErrorCode = HLRErrorCodes.Ok } });

            var hLRSwapResourcesMS = MockMicroService<HLRSwapResourcesRequest, HLRSwapResourcesResponse>();
            var hLRSwapResourcesMSRequest =
                Arg.Is<HLRSwapResourcesRequest>(
                    x => x.Msisdn == "2000" && x.SourceSim == simCardInfoMock && x.DestinationSim == simCardInfoMock);

            hLRSwapResourcesMS.Process(hLRSwapResourcesMSRequest, Arg.Any<RequestInvokationEnvironment>()).Returns(x => { throw new Exception("Error"); });

            var SwapSimCardRequestDTO = new SwapSimCardRequestDTO()
            {
                ICCID = "2000",
                vmno = "970100",
                Msisdn = "Resource1",
                password = "123456",
                user = "1644000204",
                NewIccId = "2000A",
                OldIccId = "2000B",
            };

            MockAbstractSinglePhaseOrderProcessor(SwapSimCardRequestDTO);
            var swapSimCardResponseDTO = CallBizOp(SwapSimCardRequestDTO);
            Assert.IsTrue(swapSimCardResponseDTO.resultType == ResultTypes.UnknownError);
        }

        [Test()]
        public void SwapSimCardBizOpOk_CorrectRequestResource_ShouldReturnNullSimCardInfo()
        {
            #region Mock CheckAuthorization
            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = true };

            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
                actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualCheckAuthorizationResponse);
            #endregion

            var simCardInfoMock = CreateDefaultObject.Create<SIMCardInfo>();
            var resourceMBInfoMock = CreateDefaultObject.Create<ResourceMBInfo>();
            var hlrRequestErrorsMock = CreateDefaultObject.Create<HLRRequestErrors>();

            var getResourceMBInfosByICCIDMsMock =
                MockMicroService<GetResourceMBInfosByICCIDRequest, GetResourceMBInfosByICCIDResponse>();
            var getResourceMBInfosByICCIDRequest = Arg.Is<GetResourceMBInfosByICCIDRequest>(x => x.ICCID == "3000");
            var getResourceMBInfos = CreateDefaultObject.Create<ResourceMBInfo>();
            getResourceMBInfos.CustomerInfo = CreateDefaultObject.Create<CustomerInfo>();
            getResourceMBInfosByICCIDMsMock.Process(getResourceMBInfosByICCIDRequest,
                Arg.Any<RequestInvokationEnvironment>()).Returns(new GetResourceMBInfosByICCIDResponse()
                {
                    ResourceMbInfos = new List<ResourceMBInfo>()
                });

            var getSimCardByICCIdMSMock = MockMicroService<GetSimCardByICCIdRequest, GetSimCardByICCIdResponse>();
            var getSimCardByICCIdRequest = Arg.Is<GetSimCardByICCIdRequest>(x => x.IccId == "3000A");
            getSimCardByICCIdMSMock.Process(getSimCardByICCIdRequest, Arg.Any<RequestInvokationEnvironment>())
                .Returns(new GetSimCardByICCIdResponse()
                {
                    SimCardInfo = new SIMCardInfo()
                });


            var expirateSimCardMSMock = MockMicroService<ExpirateSimCardRequest, ExpirateSimCardResponse>();
            var expirateSimCardRequest =
                Arg.Is<ExpirateSimCardRequest>(x => x.SimCardInfo == simCardInfoMock);
            expirateSimCardMSMock.Process(expirateSimCardRequest, Arg.Any<RequestInvokationEnvironment>())
                .Returns(new ExpirateSimCardResponse()
                {
                    SimCardInfo = new SIMCardInfo()
                });


            var activeSimCardMSMock = MockMicroService<ActiveSimCardRequest, ActiveSimCardResponse>();
            var activeSimCardRequest = Arg.Is<ActiveSimCardRequest>(x => x.SimCardInfo == simCardInfoMock);
            activeSimCardMSMock.Process(activeSimCardRequest, Arg.Any<RequestInvokationEnvironment>())
                .Returns(new ActiveSimCardResponse()
                {
                    SimCardInfo = new SIMCardInfo()
                });


            var updateSubscriptionMSMock = MockMicroService<UpdateSubscriptionRequest, UpdateSubscriptionResponse>();
            var updateSubscriptionRequest = Arg.Is<UpdateSubscriptionRequest>(x => x.SIMCardSubcriber == simCardInfoMock && x.SubcriberInfo == resourceMBInfoMock);
            updateSubscriptionMSMock.Process(updateSubscriptionRequest, Arg.Any<RequestInvokationEnvironment>())
                .Returns(new UpdateSubscriptionResponse()
                {
                    SubscriberResourceMB = new ResourceMBInfo()
                });

            var createHLRRequestErrorsMSMock =
                MockMicroService<CreateHLRRequestErrorsRequest, CreateHLRRequestErrorsResponse>();
            var createHLRRequestErrorsRequest =
                Arg.Is<CreateHLRRequestErrorsRequest>(x => x.HLRRequestErrorsObj == hlrRequestErrorsMock);
            createHLRRequestErrorsMSMock.Process(createHLRRequestErrorsRequest, Arg.Any<RequestInvokationEnvironment>())
                .Returns(new CreateHLRRequestErrorsResponse()
                {
                    HLRRequestErrorsObj = new HLRRequestErrors()
                });

            mockedProvisionService = Substitute.For<IProvisionService>();
            mockedProvisionService.SwapResources(Arg.Any<SwapResourcesRequest>())
                .Returns(new SwapResourcesResponse() { result = new HPSResult() { ErrorCode = HLRErrorCodes.Ok } });

            var hLRSwapResourcesMS = MockMicroService<HLRSwapResourcesRequest, HLRSwapResourcesResponse>();
            var hLRSwapResourcesMSRequest =
                Arg.Is<HLRSwapResourcesRequest>(
                    x => x.Msisdn == "3000" && x.SourceSim == simCardInfoMock && x.DestinationSim == simCardInfoMock);

            hLRSwapResourcesMS.Process(hLRSwapResourcesMSRequest, Arg.Any<RequestInvokationEnvironment>()).Returns(new HLRSwapResourcesResponse()
            {
                SimCardInfo = new SIMCardInfo()
            });

            var SwapSimCardRequestDTO = new SwapSimCardRequestDTO()
            {
                ICCID = "3000",
                vmno = "970100",
                Msisdn = "Resource1",
                password = "123456",
                user = "1644000204",
                NewIccId = "3000A",
                OldIccId = "3000B",
            };

            MockAbstractSinglePhaseOrderProcessor(SwapSimCardRequestDTO);
            var swapSimCardResponseDTO = CallBizOp(SwapSimCardRequestDTO);
            Assert.IsNull(swapSimCardResponseDTO.SimCard);
        }
        [Test()]
        public void SwapSimCardBizOp_NOK_authorizationfailed()
        {
            #region Mock CheckAuthorization
            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = false };

            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
                actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(actualCheckAuthorizationResponse);
            #endregion

            var getResourceMBInfosByICCIDMsMock =
                MockMicroService<GetResourceMBInfosByICCIDRequest, GetResourceMBInfosByICCIDResponse>();
            var getResourceMBInfosByICCIDRequest = CreateDefaultObject.Create<GetResourceMBInfosByICCIDRequest>();
            var getResourceMBInfo = CreateDefaultObject.Create<ResourceMBInfo>();
            getResourceMBInfo.CustomerInfo = CreateDefaultObject.Create<CustomerInfo>();
            getResourceMBInfo.CustomerInfo.DealerID = 190000;
            getResourceMBInfo.StatusID = (int) ResourceStatus.Active;
            var getResourceMBInfosByICCIDResponse = new GetResourceMBInfosByICCIDResponse()
            {

                ResourceMbInfos = new List<ResourceMBInfo>() { getResourceMBInfo }
            };
            getResourceMBInfosByICCIDMsMock.Process(getResourceMBInfosByICCIDRequest,
                Arg.Any<RequestInvokationEnvironment>()).ReturnsForAnyArgs(getResourceMBInfosByICCIDResponse);

            var requestDto = new SwapSimCardRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                Msisdn = "1000000000"
            };

            MockAbsctractBusinessOperation(requestDto);

            var res = CallBizOp(requestDto);
            Assert.AreEqual(res.resultType, ResultTypes.AuthorizationError);
            Assert.AreEqual(res.errorCode, BizOpsErrors.AuthorizeErrorUser);
        }
    }
}
