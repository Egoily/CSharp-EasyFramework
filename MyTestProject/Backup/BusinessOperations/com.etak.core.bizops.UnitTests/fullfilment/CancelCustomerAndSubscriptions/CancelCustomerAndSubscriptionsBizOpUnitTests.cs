using System;
using System.Collections.Generic;
using System.Linq;
using com.etak.core.bizops.fullfilment.CancelCustomerAndSubscriptions;
using com.etak.core.bizops.fullfilment.CancelCustomerProduct;
using com.etak.core.customer.message.CancelCustomerAccountAssociation;
using com.etak.core.customer.message.DeleteCustomerInfo;
using com.etak.core.customer.message.GetActiveCustomerAccountAssociationByDate;
using com.etak.core.GSMSubscription.message.GetCustomerDataRoamingLimitNotificationByCustomerId;
using com.etak.core.GSMSubscription.messages.CancelResourceMBInfo;
using com.etak.core.GSMSubscription.messages.CreateHLRRequestErrors;
using com.etak.core.GSMSubscription.messages.DeleteCustomerDataRoamingLimit;
using com.etak.core.GSMSubscription.messages.DeleteCustomerDataRoamingLimitNotification;
using com.etak.core.GSMSubscription.messages.DeleteResourceMBInfo;
using com.etak.core.GSMSubscription.messages.DeleteRoamingBlackList;
using com.etak.core.GSMSubscription.messages.GetCustomerDataRoamingLimitsByCustomerID;
using com.etak.core.GSMSubscription.messages.GetResourceMBInfosByMSISDNAndStatusNotInAndActiveDates;
using com.etak.core.GSMSubscription.messages.GetRoamingBlackListByCustomerID;
using com.etak.core.microservices.messages.GetSettingInfosByDealerId;
using com.etak.core.microservices.messages.GetSystemConfigDataInfosByItem;
using com.etak.core.model;
using com.etak.core.model.dto;
using com.etak.core.model.operation;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;
using com.etak.core.operation;
using com.etak.core.operation.contract;
using com.etak.core.operation.manager;
using com.etak.core.product.message.CalculateNextBillRunDateForBillCycle;
using com.etak.core.promotion.messages.CancelCustomersPromotion;
using com.etak.core.promotion.messages.CreateCustomerPromotionLogInfo;
using com.etak.core.promotion.messages.CreateLogPromotion;
using com.etak.core.promotion.messages.GetCustomerPromotionOperationLogByCustomerIDAndPromotion;
using com.etak.core.repository.crm.configuration;
using com.etak.core.repository.crm.customer;
using com.etak.core.repository.crm.resource;
using com.etak.core.repository.crm.subscription;
using com.etak.core.resource.msisdn.message.CoolDownNumberMS;
using com.etak.core.resource.msisdn.message.DeleteNumberMS;
using com.etak.core.resource.msisdn.message.GetMVNOAPNIPPoolByMsisdn;
using com.etak.core.resource.msisdn.message.GetNumberByResource;
using com.etak.core.resource.msisdn.message.RecycleIpByMsisdn;
using com.etak.core.resource.simCard.message.ExpirateSimCard;
using com.etak.core.resource.simCard.message.GetImeiByResourceId;
using com.etak.core.resource.simCard.message.GetSimCardByICCId;
using com.etak.core.resource.simCard.message.InitSimCard;
using com.etak.core.test.utilities;
using com.etak.core.test.utilities.abstracts;
using com.etak.core.test.utilities.Helpers;
using Newtonsoft.Json;
using NHibernate.Util;
using NSubstitute;
using NUnit.Framework;
using com.etak.core.dealer.messages.CheckAuthorization;

namespace com.etak.core.bizops.UnitTests.fullfilment.CancelCustomerAndSubscriptions
{
    [TestFixture]
    public class CancelCustomerAndSubscriptionsBizOpUnitTests :
        AbstractSinglePhaseOrderProcessorTest
            <CancelCustomerAndSubscriptionsBizOp, CancelCustomerAndSubscriptionsRequestDTO,
                CancelCustomerAndSubscriptionsResponseDTO, CancelCustomerAndSubscriptionsRequestInternal,
                CancelCustomerAndSubscriptionsResponseInternal, CancelCustomerAndSubscriptionsOrder>
    {
        private IMicroService<CheckAuthorizationRequest, CheckAuthorizationResponse> mockMicroServiceCheckAuthorization;

        [TestFixtureSetUp]
        public void Initialize()
        {
            FakeSessionFactorySingleton.Init();
            mockMicroServiceCheckAuthorization =
                MockMicroService<CheckAuthorizationRequest, CheckAuthorizationResponse>();
        }

        [Test()]
        public void
            CancelCustomerAndSubscriptionsBizOp_CorrectCustomerAndSubscriptionsGiven_ShouldCancelCustomerAndSubscriptionsOK
            ()
        {
            #region Mock CheckAuthorization

            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse {IsAuthorized = true};

            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
                actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>())
                .ReturnsForAnyArgs(actualCheckAuthorizationResponse);

            #endregion

            var request = new CancelCustomerAndSubscriptionsRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                MSISDN = "1000000000",
                NeedRecycle = false,
            };

            //Mocked GetOperationConfigForDealer<CancelCustomerAndSubscriptionConfiguration>(request.MVNO)
            var mockedRepoConfig =
                MockRepositoryManager.GetMockedRepository<IOperationConfigurationRepository<OperationConfiguration>>();
            var actualOperationConfigurations = new List<OperationConfiguration>();
            var actualOperationConfiguration = CreateDefaultObject.Create<OperationConfiguration>();
            var actualCancelCustomerAndSubscriptionConfiguration = new CancelCustomerAndSubscriptionConfiguration();

            actualCancelCustomerAndSubscriptionConfiguration.CanDeleteDataRoamingLimit = true;
            actualCancelCustomerAndSubscriptionConfiguration.CanDeletePromotions = true;
            actualCancelCustomerAndSubscriptionConfiguration.CanRemoveRoamingBlackList = true;
            actualCancelCustomerAndSubscriptionConfiguration.SettingId = 1;
            actualCancelCustomerAndSubscriptionConfiguration.DetailId = 1;
            actualCancelCustomerAndSubscriptionConfiguration.HasToUseHLR = true;
            actualOperationConfiguration.JSonConfig =
                JsonConvert.SerializeObject(actualCancelCustomerAndSubscriptionConfiguration);
            actualOperationConfiguration.EndDate = new DateTime(2016, 5, 5);
            actualOperationConfiguration.StarTime = DateTime.Now;
            actualOperationConfigurations.Add(actualOperationConfiguration);
            mockedRepoConfig.GetByDiscriminatorAndDealerId(Arg.Any<DealerInfo>(), Arg.Any<BusinessOperation>())
                .Returns(actualOperationConfigurations);

            //Not porting

            //Mocked GetActiveCustomerAccountAssociationByDateMS
            var mockedGetActiveCustomerAccountAssociationByDateMS =
                MockMicroService
                    <GetActiveCustomerAccountAssociationByDateRequest, GetActiveCustomerAccountAssociationByDateResponse
                        >();
            var actualGetActiveCustomerAccountAssociationByDateResponse = new GetActiveCustomerAccountAssociationByDateResponse
                ()
            {
                CustomerAccountAssociation = CreateDefaultObject.Create<CustomerAccountAssociation>(),
                ResultType = ResultTypes.Ok,
            };
            mockedGetActiveCustomerAccountAssociationByDateMS.Process(
                Arg.Any<GetActiveCustomerAccountAssociationByDateRequest>(), null)
                .Returns(actualGetActiveCustomerAccountAssociationByDateResponse);

            //Generic
            //Mock GetSimCardByICCIDMS
            var mockedGetSimCardByICCIDMS = MockMicroService<GetSimCardByICCIdRequest, GetSimCardByICCIdResponse>();
            var actualGetSimCardByICCIdRequest = Arg.Is<GetSimCardByICCIdRequest>(x => x.IccId == "ICC1");
            var actualSimCard = CreateDefaultObject.Create<SIMCardInfo>();
            actualSimCard.Dealer = CreateDefaultObject.Create<DealerInfo>();
            actualSimCard.Dealer.DealerID = 1;

            var actualGetSimCardByICCIdResponse = new GetSimCardByICCIdResponse()
            {
                SimCardInfo = actualSimCard,
                ResultType = ResultTypes.Ok,
            };

            mockedGetSimCardByICCIDMS.Process(actualGetSimCardByICCIdRequest, null)
                .Returns(actualGetSimCardByICCIdResponse);

            //Mock GetImeiByRescourceIDMS
            var mockedGetImeiByRescourceIDMS =
                MockMicroService<GetImeiByResourceIdRequest, GetImeiByResourceIdResponse>();
            var actualGetImeiByResourceIdRequest = Arg.Is<GetImeiByResourceIdRequest>(x => x.ResourceId == 1);
            var actualGetImeiByResourceIdResponse = new GetImeiByResourceIdResponse()
            {
                ImeiAssnInfo = CreateDefaultObject.Create<ImeiAssn>(),
                ResultType = ResultTypes.Ok,
            };
            mockedGetImeiByRescourceIDMS.Process(actualGetImeiByResourceIdRequest, null)
                .Returns(actualGetImeiByResourceIdResponse);

            //Mocked GetCustomerDataRoamingLimitsByCustomerIDMS
            var mockedGetCustomerDataRoamingLimitsByCustomerIDMS =
                MockMicroService
                    <GetCustomerDataRoamingLimitsByCustomerIDRequest, GetCustomerDataRoamingLimitsByCustomerIDResponse>();
            var actualGetCustomerDataRoamingLimitsByCustomerIDRequest =
                Arg.Is<GetCustomerDataRoamingLimitsByCustomerIDRequest>(x => x.CustomerID == 1);
            var actualGetCustomerDataRoamingLimitsByCustomerIDResponse = new GetCustomerDataRoamingLimitsByCustomerIDResponse
                ()
            {
                CustomerDataRoamingLimits =
                    new List<CustomerDataRoamingLimit>()
                    {
                        CreateDefaultObject.Create<CustomerDataRoamingLimit>(),
                        CreateDefaultObject.Create<CustomerDataRoamingLimit>()
                    },
                ResultType = ResultTypes.Ok,
            };
            mockedGetCustomerDataRoamingLimitsByCustomerIDMS.Process(
                actualGetCustomerDataRoamingLimitsByCustomerIDRequest, null)
                .Returns(actualGetCustomerDataRoamingLimitsByCustomerIDResponse);

            //Mocked GetCustomerDataRoamingLimitNotificationByCustomerIdMS
            var mockedGetCustomerDataRoamingLimitNotificationByCustomerIdMS =
                MockMicroService
                    <GetCustomerDataRoamingLimitNotificationByCustomerIdRequest,
                        GetCustomerDataRoamingLimitNotificationByCustomerIdResponse>();
            var actualGetCustomerDataRoamingLimitNotificationByCustomerIdRequest =
                Arg.Is<GetCustomerDataRoamingLimitNotificationByCustomerIdRequest>(x => x.CustomerId == 1);
            var actualGetCustomerDataRoamingLimitNotificationByCustomerIdResponse = new GetCustomerDataRoamingLimitNotificationByCustomerIdResponse
                ()
            {
                CustomerDataRoamingLimitNotifications =
                    new List<CustomerDataRoamingLimitNotification>()
                    {
                        CreateDefaultObject.Create<CustomerDataRoamingLimitNotification>(),
                        CreateDefaultObject.Create<CustomerDataRoamingLimitNotification>()
                    },
                ResultType = ResultTypes.Ok,
            };
            mockedGetCustomerDataRoamingLimitNotificationByCustomerIdMS.Process(
                actualGetCustomerDataRoamingLimitNotificationByCustomerIdRequest, null)
                .Returns(actualGetCustomerDataRoamingLimitNotificationByCustomerIdResponse);

            //Mocked GetRoamingBlackListByCustomerIDMS
            var mockedGetRoamingBlackListByCustomerIDMS =
                MockMicroService<GetRoamingBlackListByCustomerIDRequest, GetRoamingBlackListByCustomerIDResponse>();
            var actualGetRoamingBlackListByCustomerIDRequest =
                Arg.Is<GetRoamingBlackListByCustomerIDRequest>(x => x.CustomerId == 1);
            var actualGetRoamingBlackListByCustomerIDResponse = new GetRoamingBlackListByCustomerIDResponse()
            {
                RoamingBlackListInfos =
                    new List<RoamingBlackListInfo>()
                    {
                        CreateDefaultObject.Create<RoamingBlackListInfo>(),
                        CreateDefaultObject.Create<RoamingBlackListInfo>()
                    },
                ResultType = ResultTypes.Ok,
            };
            mockedGetRoamingBlackListByCustomerIDMS.Process(actualGetRoamingBlackListByCustomerIDRequest, null)
                .Returns(actualGetRoamingBlackListByCustomerIDResponse);

            //Mocked GetMVNOAPNIPPoolByMsisdnMS
            var mockedGetMVNOAPNIPPoolByMsisdnMS =
                MockMicroService<GetMVNOAPNIPPoolByMsisdnRequest, GetMVNOAPNIPPoolByMsisdnResponse>();
            var actualGetMVNOAPNIPPoolByMsisdnRequest =
                Arg.Is<GetMVNOAPNIPPoolByMsisdnRequest>(x => x.Msisdn == request.MSISDN);
            var actualGetMVNOAPNIPPoolByMsisdnResponse = new GetMVNOAPNIPPoolByMsisdnResponse()
            {
                MvnoapnipPoolInfo =
                    new List<MVNOAPNIPPoolInfo>()
                    {
                        CreateDefaultObject.Create<MVNOAPNIPPoolInfo>(),
                        CreateDefaultObject.Create<MVNOAPNIPPoolInfo>()
                    },
                ResultType = ResultTypes.Ok,
            };
            mockedGetMVNOAPNIPPoolByMsisdnMS.Process(actualGetMVNOAPNIPPoolByMsisdnRequest, null)
                .Returns(actualGetMVNOAPNIPPoolByMsisdnResponse);

            //if not eroski
            //Mocked cancelCustomersPromotionMS
            var mockedRepoResourceMBInfo =
                MockRepositoryManager.GetMockedRepository<IResourceMBRepository<ResourceMBInfo>>();
            var actualResourceMBInfos = new List<ResourceMBInfo>();
            var actualResourveMB = CreateDefaultObject.Create<ResourceMBInfo>();
            actualResourveMB.Resource = request.MSISDN;
            actualResourveMB.CustomerInfo = CreateDefaultObject.Create<CustomerInfo>();
            actualResourveMB.CustomerInfo.Promotions = new List<CrmCustomersPromotionInfo>()
            {
                CreateDefaultObject.Create<CrmCustomersPromotionInfo>(),
                CreateDefaultObject.Create<CrmCustomersPromotionInfo>()
            };

            actualResourceMBInfos.Add(actualResourveMB);
            mockedRepoResourceMBInfo.GetByMSISDNAndStatusNotInAndActiveDates(request.MSISDN, Arg.Any<Int32[]>())
                .Returns(actualResourceMBInfos);

            var mockedCancelCustomersPromotionMS =
                MockMicroService<CancelCustomersPromotionRequest, CancelCustomersPromotionResponse>();
            var actualCancelCustomersPromotionRequest =
                Arg.Is<CancelCustomersPromotionRequest>(
                    x => x.CrmCustomersPromotionInfo.PromotionId == 1 || x.CrmCustomersPromotionInfo.PromotionId == 2);
            var actualCrmCustomersPromotionInfo = CreateDefaultObject.Create<CrmCustomersPromotionInfo>();
            actualCrmCustomersPromotionInfo.PromotionDetail = new RmPromotionPlanDetailInfo();
            actualCrmCustomersPromotionInfo.PromotionDetail.PromotionPlanDetailId = 1;
            actualCrmCustomersPromotionInfo.PromotionDetail.RmPromotionPlanInfo = new RmPromotionPlanInfo();
            actualCrmCustomersPromotionInfo.PromotionDetail.RmPromotionPlanInfo.DealerId = 1;
            actualCrmCustomersPromotionInfo.PromotionDetail.RmPromotionPlanInfo.PromotionPlanId = 1;
            actualCrmCustomersPromotionInfo.Customer = CreateDefaultObject.Create<CustomerInfo>();

            var actualCancelCustomersPromotionResponse = new CancelCustomersPromotionResponse()
            {
                CrmCustomersPromotionInfo = actualCrmCustomersPromotionInfo,
                ResultType = ResultTypes.Ok,
            };
            mockedCancelCustomersPromotionMS.Process(actualCancelCustomersPromotionRequest, null)
                .Returns(actualCancelCustomersPromotionResponse);

            //Mocked GetCustomerPromotionOperationLogByCustomerIDAndPromotionMS
            var mockedGetCustomerPromotionOperationLogByCustomerIDAndPromotionMS =
                MockMicroService
                    <GetCustomerPromotionOperationLogByCustomerIDAndPromotionRequest,
                        GetCustomerPromotionOperationLogByCustomerIDAndPromotionResponse>();
            var actualGetCustomerPromotionOperationLogByCustomerIDAndPromotionRequest =
                Arg.Is<GetCustomerPromotionOperationLogByCustomerIDAndPromotionRequest>(
                    x =>
                        x.CustomerID == 1 &&
                        x.PromotionIDList.Contains(actualCrmCustomersPromotionInfo.PromotionDetail.PromotionPlanDetailId));
            var actualGetCustomerPromotionOperationLogByCustomerIDAndPromotionResponse = new GetCustomerPromotionOperationLogByCustomerIDAndPromotionResponse
                ()
            {
                LogInfo = CreateDefaultObject.Create<CrmCustomersPromotionOperationLogInfo>(),
                ResultType = ResultTypes.Ok,
            };
            mockedGetCustomerPromotionOperationLogByCustomerIDAndPromotionMS.Process(
                actualGetCustomerPromotionOperationLogByCustomerIDAndPromotionRequest, null)
                .Returns(actualGetCustomerPromotionOperationLogByCustomerIDAndPromotionResponse);

            //Mocked CreateCustomerPromotionLogInfoMS
            var createCustomerPromotionLogInfoMS =
                MockMicroService<CreateCustomerPromotionLogInfoRequest, CreateCustomerPromotionLogInfoResponse>();
            var actualCreateCustomerPromotionLogInfoResponse = new CreateCustomerPromotionLogInfoResponse()
            {
                ResultType = ResultTypes.Ok,
            };
            createCustomerPromotionLogInfoMS.Process(Arg.Any<CreateCustomerPromotionLogInfoRequest>(), null)
                .Returns(actualCreateCustomerPromotionLogInfoResponse);

            //Mocked DeleteRoamingBlackListMS
            var mockedDeleteRoamingBlackListMS =
                MockMicroService<DeleteRoamingBlackListRequest, DeleteRoamingBlackListResponse>();
            var actualDeleteRoamingBlackListResponse = new DeleteRoamingBlackListResponse()
            {
                ResultType = ResultTypes.Ok,
            };
            mockedDeleteRoamingBlackListMS.Process(Arg.Any<DeleteRoamingBlackListRequest>(), null)
                .Returns(actualDeleteRoamingBlackListResponse);

            //Mocked DeleteCustomerDataRoamingLimitMS
            var mockeDeleteCustomerDataRoamingLimitMS =
                MockMicroService<DeleteCustomerDataRoamingLimitRequest, DeleteCustomerDataRoamingLimitResponse>();
            var actualDeleteCustomerDataRoamingLimitResponse = new DeleteCustomerDataRoamingLimitResponse()
            {
                ResultType = ResultTypes.Ok,
            };
            mockeDeleteCustomerDataRoamingLimitMS.Process(Arg.Any<DeleteCustomerDataRoamingLimitRequest>(), null)
                .Returns(actualDeleteCustomerDataRoamingLimitResponse);

            //Mocked DeleteCustomerDataRoamingLimitNotificationMS
            var mockedDeleteCustomerDataRoamingLimitNotificationMS =
                MockMicroService
                    <DeleteCustomerDataRoamingLimitNotificationRequest,
                        DeleteCustomerDataRoamingLimitNotificationResponse>();
            var actualDeleteCustomerDataRoamingLimitNotificationResponse = new DeleteCustomerDataRoamingLimitNotificationResponse
                ()
            {
                ResultType = ResultTypes.Ok,
            };
            mockedDeleteCustomerDataRoamingLimitNotificationMS.Process(
                Arg.Any<DeleteCustomerDataRoamingLimitNotificationRequest>(), null)
                .Returns(actualDeleteCustomerDataRoamingLimitNotificationResponse);
            //todo unbindimeitoOta

            //Not portin
            //Mocked CancelResourceMBInfoMS
            var mockedCancelResourceMBInfoMS =
                MockMicroService<CancelResourceMBInfoRequest, CancelResourceMBInfoResponse>();
            var actualCancelResourceMBInfoResponse = new CancelResourceMBInfoResponse()
            {
                ResultType = ResultTypes.Ok,
            };
            mockedCancelResourceMBInfoMS.Process(Arg.Any<CancelResourceMBInfoRequest>(), null)
                .Returns(actualCancelResourceMBInfoResponse);

            //Mocked CoolDownNumberMS
            var mockedCoolDownNumberMS = MockMicroService<CoolDownNumberRequest, CoolDownNumberResponse>();
            var actualCoolDownNumberResponse = new CoolDownNumberResponse()
            {
                ResultType = ResultTypes.Ok,
            };
            mockedCoolDownNumberMS.Process(Arg.Any<CoolDownNumberRequest>(), null).Returns(actualCoolDownNumberResponse);

            //Mocked ExpirateSimCardMS
            var mockedExpirateSimCardMS = MockMicroService<ExpirateSimCardRequest, ExpirateSimCardResponse>();
            var actualExpirateSimCardResponse = new ExpirateSimCardResponse()
            {
                ResultType = ResultTypes.Ok,
            };
            mockedExpirateSimCardMS.Process(Arg.Any<ExpirateSimCardRequest>(), null)
                .Returns(actualExpirateSimCardResponse);

            //generic
            //Mocked CancelCustomerAccountAssociationMS
            var mockedCancelCustomerAccountAssociationMS =
                MockMicroService<CancelCustomerAccountAssociationRequest, CancelCustomerAccountAssociationResponse>();
            var actualCancelCustomerAccountAssociationResponse = new CancelCustomerAccountAssociationResponse()
            {
                ResultType = ResultTypes.Ok,
            };
            mockedCancelCustomerAccountAssociationMS.Process(Arg.Any<CancelCustomerAccountAssociationRequest>(), null)
                .Returns(actualCancelCustomerAccountAssociationResponse);

            //Mocked DeleteCustomerInfoMS
            var mockedDeleteCustomerInfoMS = MockMicroService<DeleteCustomerInfoRequest, DeleteCustomerInfoResponse>();
            var actualDeleteCustomerInfoResponse = new DeleteCustomerInfoResponse()
            {
                ResultType = ResultTypes.Ok,
            };
            mockedDeleteCustomerInfoMS.Process(Arg.Any<DeleteCustomerInfoRequest>(), null)
                .Returns(actualDeleteCustomerInfoResponse);

            //Mocked RecycleIpByMsisdnMS
            var mockedRecycleIpByMsisdnMS = MockMicroService<RecycleIpByMsisdnRequest, RecycleIpByMsisdnResponse>();
            var actualRecycleIpByMsisdnResponse = new RecycleIpByMsisdnResponse()
            {
                ResultType = ResultTypes.Ok,
            };
            mockedRecycleIpByMsisdnMS.Process(Arg.Any<RecycleIpByMsisdnRequest>(), null)
                .Returns(actualRecycleIpByMsisdnResponse);

            //Mocked GetSystemConfigDataInfosByItemMS
            var mockedGetSystemConfigDataInfosByItemMS =
                MockMicroService<GetSystemConfigDataInfosByItemRequest, GetSystemConfigDataInfosByItemResponse>();
            var actualGetSystemConfigDataInfosByItemResponse = new GetSystemConfigDataInfosByItemResponse()
            {
                ResultType = ResultTypes.Ok,
            };
            mockedGetSystemConfigDataInfosByItemMS.Process(Arg.Any<GetSystemConfigDataInfosByItemRequest>(), null)
                .Returns(actualGetSystemConfigDataInfosByItemResponse);

            //Mocked CreateHLRRequestErrorsMS
            var mockedCreateHLRRequestErrorsMS =
                MockMicroService<CreateHLRRequestErrorsRequest, CreateHLRRequestErrorsResponse>();
            var actualCreateHLRRequestErrorsResponse = new CreateHLRRequestErrorsResponse()
            {
                ResultType = ResultTypes.Ok,
            };
            mockedCreateHLRRequestErrorsMS.Process(Arg.Any<CreateHLRRequestErrorsRequest>(), null)
                .Returns(actualCreateHLRRequestErrorsResponse);

            //Mock CalculateNextBillRunDateForBillCycleMS
            var mockedCalculateNextBillRunDateForBillCycleMS =
                MockMicroService
                    <CalculateNextBillRunDateForBillCycleRequest, CalculateNextBillRunDateForBillCycleResponse>();
            mockedCalculateNextBillRunDateForBillCycleMS.Process(
                Arg.Any<CalculateNextBillRunDateForBillCycleRequest>(), Arg.Any<RequestInvokationEnvironment>())
                .Returns(new CalculateNextBillRunDateForBillCycleResponse()
                {
                    NextBillRun = DateTime.Now,
                    ResultType = ResultTypes.Ok
                });

            //Mock GetSettingInfosByDealerId
            var mockedGetSettingInfosByDealerIdMS =
                MockMicroService<GetSettingInfosByDealerIdRequest, GetSettingInfosByDealerIdResponse>();

            var settingInfos = new List<SettingInfo>()
            {
                CreateDefaultObject.Create<SettingInfo>()
            };

            settingInfos.FirstOrDefault().SettingId = actualCancelCustomerAndSubscriptionConfiguration.SettingId;
            settingInfos.FirstOrDefault().SettingDetailInfos = new List<SettingDetailInfo>()
            {
                CreateDefaultObject.Create<SettingDetailInfo>()
            };
            settingInfos.FirstOrDefault().SettingDetailInfos.FirstOrDefault().DetailId =
                actualCancelCustomerAndSubscriptionConfiguration.DetailId;
            settingInfos.FirstOrDefault().SettingDetailInfos.FirstOrDefault().Unit = "Months";
            settingInfos.FirstOrDefault().SettingDetailInfos.FirstOrDefault().Interval = 3;

            mockedGetSettingInfosByDealerIdMS.Process(Arg.Any<GetSettingInfosByDealerIdRequest>(),
                Arg.Any<RequestInvokationEnvironment>()).Returns(new GetSettingInfosByDealerIdResponse()
                {
                    SettingInfos = settingInfos,
                    ResultType = ResultTypes.Ok
                });

            //Mocked BusinessOperation
            var mockedCancelCustomerProductBizOp =
                Substitute
                    .For
                    <ICoreBusinessOperation<CancelCustomerProductRequestInternal, CancelCustomerProductResponseInternal>
                        >();
            mockedCancelCustomerProductBizOp.Process(Arg.Any<CancelCustomerProductRequestInternal>(), null)
                .Returns(new CancelCustomerProductResponseInternal()
                {
                    ResultType = ResultTypes.Ok,
                });

            BusinessOperationManager.RebindCoreInterfaceToConstant(1, mockedCancelCustomerProductBizOp);

            MockAbstractSinglePhaseOrderProcessor(request);

            //Remock Subscription
            var actualResourceMB = new ResourceMBInfo();
            actualResourceMB.Resource = request.MSISDN;
            actualResourceMB.CustomerInfo = CreateDefaultObject.Create<CustomerInfo>();
            actualResourceMB.CustomerInfo.CustomerID = 999;
            actualResourceMB.OperatorInfo = CreateDefaultObject.Create<DealerInfo>();
            actualResourceMB.ResourceDIDInfo = CreateDefaultObject.Create<ResourceDIDInfo>();
            actualResourceMBInfos.Add(actualResourceMB);

            mockedRepoResourceMBInfo.GetByMSISDNAndStatusNotInAndActiveDates(request.MSISDN, Arg.Any<Int32[]>())
                .Returns(actualResourceMBInfos);

            var expectedCancelCustomerAndSubscriptionsResponseDTO = new CancelCustomerAndSubscriptionsResponseDTO()
            {
                Customer = new CustomerDTO() {CustomerId = actualResourveMB.CustomerInfo.CustomerID.Value}
            };


            var response = CallBizOp(request);

            Assert.AreEqual(ResultTypes.Ok, response.resultType);
            Assert.IsTrue(expectedCancelCustomerAndSubscriptionsResponseDTO.Customer.CustomerId ==
                          response.Customer.CustomerId);
        }

        [Test()]
        public void
            CancelCustomerAndSubscriptionsBizOp_NeedRecycleGivenInternalPortin_ShouldCancelCustomerAndSubscriptionsOK()
        {
            #region Mock CheckAuthorization

            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse {IsAuthorized = true};

            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
                actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>())
                .ReturnsForAnyArgs(actualCheckAuthorizationResponse);

            #endregion

            var request = new CancelCustomerAndSubscriptionsRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                MSISDN = "1234567890",
                NeedRecycle = true,
            };

            //Mocked GetOperationConfigForDealer<CancelCustomerAndSubscriptionConfiguration>(request.MVNO)
            var mockedRepoConfig =
                MockRepositoryManager.GetMockedRepository<IOperationConfigurationRepository<OperationConfiguration>>();
            var actualOperationConfigurations = new List<OperationConfiguration>();
            var actualOperationConfiguration = CreateDefaultObject.Create<OperationConfiguration>();
            var actualCancelCustomerAndSubscriptionConfiguration = new CancelCustomerAndSubscriptionConfiguration();
            actualCancelCustomerAndSubscriptionConfiguration.CanDeleteDataRoamingLimit = false;
            actualCancelCustomerAndSubscriptionConfiguration.CanDeletePromotions = false;
            actualCancelCustomerAndSubscriptionConfiguration.CanRemoveRoamingBlackList = false;
            actualCancelCustomerAndSubscriptionConfiguration.HasToUseHLR = true;
            actualOperationConfiguration.JSonConfig =
                JsonConvert.SerializeObject(actualCancelCustomerAndSubscriptionConfiguration);
            actualOperationConfiguration.EndDate = new DateTime(2016, 5, 5);
            actualOperationConfiguration.StarTime = DateTime.Now;
            actualOperationConfigurations.Add(actualOperationConfiguration);
            mockedRepoConfig.GetByDiscriminatorAndDealerId(Arg.Any<DealerInfo>(), Arg.Any<BusinessOperation>())
                .Returns(actualOperationConfigurations);

            //Mocked GetActiveCustomerAccountAssociationByDateMS
            var mockedGetActiveCustomerAccountAssociationByDateMS =
                MockMicroService
                    <GetActiveCustomerAccountAssociationByDateRequest, GetActiveCustomerAccountAssociationByDateResponse
                        >();
            var actualGetActiveCustomerAccountAssociationByDateResponse = new GetActiveCustomerAccountAssociationByDateResponse
                ()
            {
                CustomerAccountAssociation = CreateDefaultObject.Create<CustomerAccountAssociation>(),
                ResultType = ResultTypes.Ok,
            };
            mockedGetActiveCustomerAccountAssociationByDateMS.Process(
                Arg.Any<GetActiveCustomerAccountAssociationByDateRequest>(), null)
                .Returns(actualGetActiveCustomerAccountAssociationByDateResponse);

            //Portin
            //
            //Mocked GetResourceMBInfosByCustomerIDMS with "0" + request.MSISDN
            var mockedGetResourceMBInfosByCustomerIDMS =
                MockMicroService
                    <GetResourceMBInfosByMSISDNAndStatusNotInAndActiveDatesRequest,
                        GetResourceMBInfosByMSISDNAndStatusNotInAndActiveDatesResponse>();
            var actualGetResourceMBInfosByMSISDNAndStatusNotInAndActiveDatesRequest =
                Arg.Is<GetResourceMBInfosByMSISDNAndStatusNotInAndActiveDatesRequest>(
                    x => x.Msisdn == ("0" + request.MSISDN));


            var actualResourceMbInfos = new List<ResourceMBInfo>() {CreateDefaultObject.Create<ResourceMBInfo>()};
            var actualCustomerInfo = CreateDefaultObject.Create<CustomerInfo>();
            actualCustomerInfo.CustomerID = 99;
            actualResourceMbInfos.FirstOrDefault().CustomerInfo = actualCustomerInfo;
            var actualGetResourceMBInfosByMSISDNAndStatusNotInAndActiveDatesResponse = new GetResourceMBInfosByMSISDNAndStatusNotInAndActiveDatesResponse
                ()
            {
                ResourceMbInfos = actualResourceMbInfos,
                ResultType = ResultTypes.Ok,
            };
            mockedGetResourceMBInfosByCustomerIDMS.Process(
                actualGetResourceMBInfosByMSISDNAndStatusNotInAndActiveDatesRequest, null)
                .Returns(actualGetResourceMBInfosByMSISDNAndStatusNotInAndActiveDatesResponse);

            //Mocked GetNumberByResourceMS
            var mockedGetNumberByResourceMS =
                MockMicroService<GetNumberByResourceRequest, GetNumberByResourceResponse>();
            var actualNumberInfo = CreateDefaultObject.Create<NumberInfo>();
            actualNumberInfo.NumberProperty = CreateDefaultObject.Create<NumberPropertyInfo>();
            actualNumberInfo.NumberProperty.StatusID = (int) ResourceStatus.Installed;
            var mockedGetNumberByResourceResponse = new GetNumberByResourceResponse()
            {
                NumberInfo = actualNumberInfo,
                ResultType = ResultTypes.Ok,
            };
            mockedGetNumberByResourceMS.Process(Arg.Any<GetNumberByResourceRequest>(), null)
                .Returns(mockedGetNumberByResourceResponse);

            //Generic
            //Mock GetSimCardByICCIDMS
            var mockedGetSimCardByICCIDMS = MockMicroService<GetSimCardByICCIdRequest, GetSimCardByICCIdResponse>();
            var actualGetSimCardByICCIdRequest = Arg.Is<GetSimCardByICCIdRequest>(x => x.IccId == "ICC1");
            var actualSimCard = CreateDefaultObject.Create<SIMCardInfo>();
            actualSimCard.Dealer = CreateDefaultObject.Create<DealerInfo>();
            actualSimCard.Dealer.DealerID = 1;
            var actualGetSimCardByICCIdResponse = new GetSimCardByICCIdResponse()
            {
                SimCardInfo = actualSimCard,
                ResultType = ResultTypes.Ok,
            };
            mockedGetSimCardByICCIDMS.Process(actualGetSimCardByICCIdRequest, null)
                .Returns(actualGetSimCardByICCIdResponse);

            //Mock GetImeiByRescourceIDMS
            var mockedGetImeiByRescourceIDMS =
                MockMicroService<GetImeiByResourceIdRequest, GetImeiByResourceIdResponse>();
            var actualGetImeiByResourceIdRequest = Arg.Is<GetImeiByResourceIdRequest>(x => x.ResourceId == 1);
            var actualGetImeiByResourceIdResponse = new GetImeiByResourceIdResponse()
            {
                ImeiAssnInfo = CreateDefaultObject.Create<ImeiAssn>(),
                ResultType = ResultTypes.Ok,
            };
            mockedGetImeiByRescourceIDMS.Process(actualGetImeiByResourceIdRequest, null)
                .Returns(actualGetImeiByResourceIdResponse);

            //Mocked GetCustomerDataRoamingLimitsByCustomerIDMS
            var mockedGetCustomerDataRoamingLimitsByCustomerIDMS =
                MockMicroService
                    <GetCustomerDataRoamingLimitsByCustomerIDRequest, GetCustomerDataRoamingLimitsByCustomerIDResponse>();
            var actualGetCustomerDataRoamingLimitsByCustomerIDRequest =
                Arg.Is<GetCustomerDataRoamingLimitsByCustomerIDRequest>(x => x.CustomerID == actualCustomerInfo.CustomerID);
            var actualGetCustomerDataRoamingLimitsByCustomerIDResponse = new GetCustomerDataRoamingLimitsByCustomerIDResponse
                ()
            {
                CustomerDataRoamingLimits =
                    new List<CustomerDataRoamingLimit>()
                    {
                        CreateDefaultObject.Create<CustomerDataRoamingLimit>(),
                        CreateDefaultObject.Create<CustomerDataRoamingLimit>()
                    },
                ResultType = ResultTypes.Ok,
            };
            mockedGetCustomerDataRoamingLimitsByCustomerIDMS.Process(
                actualGetCustomerDataRoamingLimitsByCustomerIDRequest, null)
                .Returns(actualGetCustomerDataRoamingLimitsByCustomerIDResponse);

            //Mocked GetCustomerDataRoamingLimitNotificationByCustomerIdMS
            var mockedGetCustomerDataRoamingLimitNotificationByCustomerIdMS =
                MockMicroService
                    <GetCustomerDataRoamingLimitNotificationByCustomerIdRequest,
                        GetCustomerDataRoamingLimitNotificationByCustomerIdResponse>();
            var actualGetCustomerDataRoamingLimitNotificationByCustomerIdRequest =
                Arg.Is<GetCustomerDataRoamingLimitNotificationByCustomerIdRequest>(x => x.CustomerId == actualCustomerInfo.CustomerID);
            var actualGetCustomerDataRoamingLimitNotificationByCustomerIdResponse = new GetCustomerDataRoamingLimitNotificationByCustomerIdResponse
                ()
            {
                CustomerDataRoamingLimitNotifications =
                    new List<CustomerDataRoamingLimitNotification>()
                    {
                        CreateDefaultObject.Create<CustomerDataRoamingLimitNotification>(),
                        CreateDefaultObject.Create<CustomerDataRoamingLimitNotification>()
                    },
                ResultType = ResultTypes.Ok,
            };
            mockedGetCustomerDataRoamingLimitNotificationByCustomerIdMS.Process(
                actualGetCustomerDataRoamingLimitNotificationByCustomerIdRequest, null)
                .Returns(actualGetCustomerDataRoamingLimitNotificationByCustomerIdResponse);

            //Mocked GetRoamingBlackListByCustomerIDMS
            var mockedGetRoamingBlackListByCustomerIDMS =
                MockMicroService<GetRoamingBlackListByCustomerIDRequest, GetRoamingBlackListByCustomerIDResponse>();
            var actualGetRoamingBlackListByCustomerIDRequest =
                Arg.Is<GetRoamingBlackListByCustomerIDRequest>(x => x.CustomerId == actualCustomerInfo.CustomerID);
            var actualGetRoamingBlackListByCustomerIDResponse = new GetRoamingBlackListByCustomerIDResponse()
            {
                RoamingBlackListInfos =
                    new List<RoamingBlackListInfo>()
                    {
                        CreateDefaultObject.Create<RoamingBlackListInfo>(),
                        CreateDefaultObject.Create<RoamingBlackListInfo>()
                    },
                ResultType = ResultTypes.Ok,
            };
            mockedGetRoamingBlackListByCustomerIDMS.Process(actualGetRoamingBlackListByCustomerIDRequest, null)
                .Returns(actualGetRoamingBlackListByCustomerIDResponse);

            //Mocked GetMVNOAPNIPPoolByMsisdnMS
            var mockedGetMVNOAPNIPPoolByMsisdnMS =
                MockMicroService<GetMVNOAPNIPPoolByMsisdnRequest, GetMVNOAPNIPPoolByMsisdnResponse>();
            var actualGetMVNOAPNIPPoolByMsisdnRequest =
                Arg.Is<GetMVNOAPNIPPoolByMsisdnRequest>(x => x.Msisdn == request.MSISDN);
            var actualGetMVNOAPNIPPoolByMsisdnResponse = new GetMVNOAPNIPPoolByMsisdnResponse()
            {
                MvnoapnipPoolInfo =
                    new List<MVNOAPNIPPoolInfo>()
                    {
                        CreateDefaultObject.Create<MVNOAPNIPPoolInfo>(),
                        CreateDefaultObject.Create<MVNOAPNIPPoolInfo>()
                    },
                ResultType = ResultTypes.Ok,
            };
            mockedGetMVNOAPNIPPoolByMsisdnMS.Process(actualGetMVNOAPNIPPoolByMsisdnRequest, null)
                .Returns(actualGetMVNOAPNIPPoolByMsisdnResponse);
            //if portin
            //Mocked DeleteResourceMBInfoMS
            var mockedDeleteResourceMBInfoMS =
                MockMicroService<DeleteResourceMBInfoRequest, DeleteResourceMBInfoResponse>();
            var acualDeleteResourceMBInfoResponse = new DeleteResourceMBInfoResponse()
            {
                ResultType = ResultTypes.Ok,
            };
            mockedDeleteResourceMBInfoMS.Process(Arg.Any<DeleteResourceMBInfoRequest>(), null)
                .Returns(acualDeleteResourceMBInfoResponse);
            //Mocked InitSimCardMS
            var mockedInitSimCardMS = MockMicroService<InitSimCardRequest, InitSimCardResponse>();
            var acualInitSimCardResponse = new InitSimCardResponse()
            {
                ResultType = ResultTypes.Ok,
            };
            mockedInitSimCardMS.Process(Arg.Any<InitSimCardRequest>(), null)
                .Returns(acualInitSimCardResponse);

            //Mocked DeleteNumberMS
            var mockedDeleteNumberMS = MockMicroService<DeleteNumberRequest, DeleteNumberResponse>();
            var acualDeleteNumberResponse = new DeleteNumberResponse()
            {
                ResultType = ResultTypes.Ok,
            };
            mockedDeleteNumberMS.Process(Arg.Any<DeleteNumberRequest>(), null)
                .Returns(acualDeleteNumberResponse);

            //generic
            //Mocked CancelCustomerAccountAssociationMS
            var mockedCancelCustomerAccountAssociationMS =
                MockMicroService<CancelCustomerAccountAssociationRequest, CancelCustomerAccountAssociationResponse>();
            var actualCancelCustomerAccountAssociationResponse = new CancelCustomerAccountAssociationResponse()
            {
                ResultType = ResultTypes.Ok,
            };
            mockedCancelCustomerAccountAssociationMS.Process(Arg.Any<CancelCustomerAccountAssociationRequest>(), null)
                .Returns(actualCancelCustomerAccountAssociationResponse);

            //Mocked DeleteCustomerInfoMS
            var mockedDeleteCustomerInfoMS = MockMicroService<DeleteCustomerInfoRequest, DeleteCustomerInfoResponse>();
            var actualDeleteCustomerInfoResponse = new DeleteCustomerInfoResponse()
            {
                ResultType = ResultTypes.Ok,
            };
            mockedDeleteCustomerInfoMS.Process(Arg.Any<DeleteCustomerInfoRequest>(), null)
                .Returns(actualDeleteCustomerInfoResponse);

            //Mocked RecycleIpByMsisdnMS
            var mockedRecycleIpByMsisdnMS = MockMicroService<RecycleIpByMsisdnRequest, RecycleIpByMsisdnResponse>();
            var actualRecycleIpByMsisdnResponse = new RecycleIpByMsisdnResponse()
            {
                ResultType = ResultTypes.Ok,
            };
            mockedRecycleIpByMsisdnMS.Process(Arg.Any<RecycleIpByMsisdnRequest>(), null)
                .Returns(actualRecycleIpByMsisdnResponse);

            //Mocked GetSystemConfigDataInfosByItemMS
            var mockedGetSystemConfigDataInfosByItemMS =
                MockMicroService<GetSystemConfigDataInfosByItemRequest, GetSystemConfigDataInfosByItemResponse>();
            var actualGetSystemConfigDataInfosByItemResponse = new GetSystemConfigDataInfosByItemResponse()
            {
                ResultType = ResultTypes.Ok,
            };
            mockedGetSystemConfigDataInfosByItemMS.Process(Arg.Any<GetSystemConfigDataInfosByItemRequest>(), null)
                .Returns(actualGetSystemConfigDataInfosByItemResponse);

            //Mocked CreateHLRRequestErrorsMS
            var mockedCreateHLRRequestErrorsMS =
                MockMicroService<CreateHLRRequestErrorsRequest, CreateHLRRequestErrorsResponse>();
            var actualCreateHLRRequestErrorsResponse = new CreateHLRRequestErrorsResponse()
            {
                ResultType = ResultTypes.Ok,
            };
            mockedCreateHLRRequestErrorsMS.Process(Arg.Any<CreateHLRRequestErrorsRequest>(), null)
                .Returns(actualCreateHLRRequestErrorsResponse);

            //Mock CalculateNextBillRunDateForBillCycleMS
            var mockedCalculateNextBillRunDateForBillCycleMS =
                MockMicroService
                    <CalculateNextBillRunDateForBillCycleRequest, CalculateNextBillRunDateForBillCycleResponse>();
            mockedCalculateNextBillRunDateForBillCycleMS.Process(
                Arg.Any<CalculateNextBillRunDateForBillCycleRequest>(), Arg.Any<RequestInvokationEnvironment>())
                .Returns(new CalculateNextBillRunDateForBillCycleResponse()
                {
                    NextBillRun = DateTime.Now,
                    ResultType = ResultTypes.Ok
                });

            //Mocked BusinessOperation
            var mockedCancelCustomerProductBizOp =
                Substitute
                    .For
                    <ICoreBusinessOperation<CancelCustomerProductRequestInternal, CancelCustomerProductResponseInternal>
                        >();
            mockedCancelCustomerProductBizOp.Process(Arg.Any<CancelCustomerProductRequestInternal>(), null)
                .Returns(new CancelCustomerProductResponseInternal()
                {
                    ResultType = ResultTypes.Ok,
                });

            BusinessOperationManager.RebindCoreInterfaceToConstant(1, mockedCancelCustomerProductBizOp);

            MockAbstractSinglePhaseOrderProcessor(request);

            //Remock Subscription
            var mockedRepoResourceMBInfo = MockRepositoryManager.GetMockedRepository<IResourceMBRepository<ResourceMBInfo>>();
            var mockResourceMbList = new List<ResourceMBInfo>(){new ResourceMBInfo()}
            ;
            mockedRepoResourceMBInfo.GetByMSISDNAndStatusNotInAndActiveDates(request.MSISDN, Arg.Any<Int32[]>()).Returns(mockResourceMbList);

            var expectedCancelCustomerAndSubscriptionsResponseDTO = new CancelCustomerAndSubscriptionsResponseDTO()
            {
                Customer = new CustomerDTO() {CustomerId = actualCustomerInfo.CustomerID.Value}
            };

            var mockedRepoNumberInfo = MockRepositoryManager.GetMockedRepository<INumberInfoRepository<NumberInfo>>();
            mockedRepoNumberInfo.GetById(request.MSISDN).Returns((NumberInfo) null);


            var response = CallBizOp(request);

            Assert.IsTrue(response.resultType == ResultTypes.Ok);
            Assert.IsTrue(expectedCancelCustomerAndSubscriptionsResponseDTO.Customer.CustomerId ==
                          response.Customer.CustomerId);

        }

        [Test()]
        public void CancelCustomerAndSubscriptionsBizOp_NullMsisdnGiven_ShouldThrowException()
        {
            #region Mock CheckAuthorization

            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse {IsAuthorized = true};

            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
                actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>())
                .ReturnsForAnyArgs(actualCheckAuthorizationResponse);

            #endregion

            var request = new CancelCustomerAndSubscriptionsRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                MSISDN = null,
                NeedRecycle = true,
            };

            MockAbstractSinglePhaseOrderProcessor(request);

            var response = CallBizOp(request);

            Assert.IsTrue(response.resultType == ResultTypes.BussinessLogicError);
        }

        [Test()]
        public void
            CancelCustomerAndSubscriptionsBizOp_CorrectCustomerAndSubscriptionsGiven_NoHLRCall_ShouldCancelCustomerAndSubscriptionsOK
            ()
        {
            #region Mock CheckAuthorization

            var actualCheckAuthorizationResponse = new CheckAuthorizationResponse { IsAuthorized = true };

            var actualCheckAuthorizationRequest = CreateDefaultObject.Create<CheckAuthorizationRequest>();
            mockMicroServiceCheckAuthorization.Process(
                actualCheckAuthorizationRequest, Arg.Any<RequestInvokationEnvironment>())
                .ReturnsForAnyArgs(actualCheckAuthorizationResponse);

            #endregion

            var request = new CancelCustomerAndSubscriptionsRequestDTO()
            {
                user = "1644000204",
                password = "123456",
                vmno = "970100",
                MSISDN = "1000000000",
                NeedRecycle = false,
            };

            //Mocked GetOperationConfigForDealer<CancelCustomerAndSubscriptionConfiguration>(request.MVNO)
            var mockedRepoConfig =
                MockRepositoryManager.GetMockedRepository<IOperationConfigurationRepository<OperationConfiguration>>();
            var actualOperationConfigurations = new List<OperationConfiguration>();
            var actualOperationConfiguration = CreateDefaultObject.Create<OperationConfiguration>();
            var actualCancelCustomerAndSubscriptionConfiguration = new CancelCustomerAndSubscriptionConfiguration();

            actualCancelCustomerAndSubscriptionConfiguration.CanDeleteDataRoamingLimit = true;
            actualCancelCustomerAndSubscriptionConfiguration.CanDeletePromotions = true;
            actualCancelCustomerAndSubscriptionConfiguration.CanRemoveRoamingBlackList = true;
            actualCancelCustomerAndSubscriptionConfiguration.SettingId = 1;
            actualCancelCustomerAndSubscriptionConfiguration.DetailId = 1;
            actualCancelCustomerAndSubscriptionConfiguration.HasToUseHLR = false;
            actualOperationConfiguration.JSonConfig =
                JsonConvert.SerializeObject(actualCancelCustomerAndSubscriptionConfiguration);
            actualOperationConfiguration.EndDate = new DateTime(2016, 5, 5);
            actualOperationConfiguration.StarTime = DateTime.Now;
            actualOperationConfigurations.Add(actualOperationConfiguration);
            mockedRepoConfig.GetByDiscriminatorAndDealerId(Arg.Any<DealerInfo>(), Arg.Any<BusinessOperation>())
                .Returns(actualOperationConfigurations);

            //Not porting

            //Mocked GetActiveCustomerAccountAssociationByDateMS
            var mockedGetActiveCustomerAccountAssociationByDateMS =
                MockMicroService
                    <GetActiveCustomerAccountAssociationByDateRequest, GetActiveCustomerAccountAssociationByDateResponse
                        >();
            var actualGetActiveCustomerAccountAssociationByDateResponse = new GetActiveCustomerAccountAssociationByDateResponse
                ()
            {
                CustomerAccountAssociation = CreateDefaultObject.Create<CustomerAccountAssociation>(),
                ResultType = ResultTypes.Ok,
            };
            mockedGetActiveCustomerAccountAssociationByDateMS.Process(
                Arg.Any<GetActiveCustomerAccountAssociationByDateRequest>(), null)
                .Returns(actualGetActiveCustomerAccountAssociationByDateResponse);

            //Generic
            //Mock GetSimCardByICCIDMS
            var mockedGetSimCardByICCIDMS = MockMicroService<GetSimCardByICCIdRequest, GetSimCardByICCIdResponse>();
            var actualGetSimCardByICCIdRequest = Arg.Is<GetSimCardByICCIdRequest>(x => x.IccId == "ICC1");
            var actualSimCard = CreateDefaultObject.Create<SIMCardInfo>();
            actualSimCard.Dealer = CreateDefaultObject.Create<DealerInfo>();
            actualSimCard.Dealer.DealerID = 1;

            var actualGetSimCardByICCIdResponse = new GetSimCardByICCIdResponse()
            {
                SimCardInfo = actualSimCard,
                ResultType = ResultTypes.Ok,
            };

            mockedGetSimCardByICCIDMS.Process(actualGetSimCardByICCIdRequest, null)
                .Returns(actualGetSimCardByICCIdResponse);

            //Mock GetImeiByRescourceIDMS
            var mockedGetImeiByRescourceIDMS =
                MockMicroService<GetImeiByResourceIdRequest, GetImeiByResourceIdResponse>();
            var actualGetImeiByResourceIdRequest = Arg.Is<GetImeiByResourceIdRequest>(x => x.ResourceId == 1);
            var actualGetImeiByResourceIdResponse = new GetImeiByResourceIdResponse()
            {
                ImeiAssnInfo = CreateDefaultObject.Create<ImeiAssn>(),
                ResultType = ResultTypes.Ok,
            };
            mockedGetImeiByRescourceIDMS.Process(actualGetImeiByResourceIdRequest, null)
                .Returns(actualGetImeiByResourceIdResponse);

            //Mocked GetCustomerDataRoamingLimitsByCustomerIDMS
            var mockedGetCustomerDataRoamingLimitsByCustomerIDMS =
                MockMicroService
                    <GetCustomerDataRoamingLimitsByCustomerIDRequest, GetCustomerDataRoamingLimitsByCustomerIDResponse>();
            var actualGetCustomerDataRoamingLimitsByCustomerIDRequest =
                Arg.Is<GetCustomerDataRoamingLimitsByCustomerIDRequest>(x => x.CustomerID == 1);
            var actualGetCustomerDataRoamingLimitsByCustomerIDResponse = new GetCustomerDataRoamingLimitsByCustomerIDResponse
                ()
            {
                CustomerDataRoamingLimits =
                    new List<CustomerDataRoamingLimit>()
                    {
                        CreateDefaultObject.Create<CustomerDataRoamingLimit>(),
                        CreateDefaultObject.Create<CustomerDataRoamingLimit>()
                    },
                ResultType = ResultTypes.Ok,
            };
            mockedGetCustomerDataRoamingLimitsByCustomerIDMS.Process(
                actualGetCustomerDataRoamingLimitsByCustomerIDRequest, null)
                .Returns(actualGetCustomerDataRoamingLimitsByCustomerIDResponse);

            //Mocked GetCustomerDataRoamingLimitNotificationByCustomerIdMS
            var mockedGetCustomerDataRoamingLimitNotificationByCustomerIdMS =
                MockMicroService
                    <GetCustomerDataRoamingLimitNotificationByCustomerIdRequest,
                        GetCustomerDataRoamingLimitNotificationByCustomerIdResponse>();
            var actualGetCustomerDataRoamingLimitNotificationByCustomerIdRequest =
                Arg.Is<GetCustomerDataRoamingLimitNotificationByCustomerIdRequest>(x => x.CustomerId == 1);
            var actualGetCustomerDataRoamingLimitNotificationByCustomerIdResponse = new GetCustomerDataRoamingLimitNotificationByCustomerIdResponse
                ()
            {
                CustomerDataRoamingLimitNotifications =
                    new List<CustomerDataRoamingLimitNotification>()
                    {
                        CreateDefaultObject.Create<CustomerDataRoamingLimitNotification>(),
                        CreateDefaultObject.Create<CustomerDataRoamingLimitNotification>()
                    },
                ResultType = ResultTypes.Ok,
            };
            mockedGetCustomerDataRoamingLimitNotificationByCustomerIdMS.Process(
                actualGetCustomerDataRoamingLimitNotificationByCustomerIdRequest, null)
                .Returns(actualGetCustomerDataRoamingLimitNotificationByCustomerIdResponse);

            //Mocked GetRoamingBlackListByCustomerIDMS
            var mockedGetRoamingBlackListByCustomerIDMS =
                MockMicroService<GetRoamingBlackListByCustomerIDRequest, GetRoamingBlackListByCustomerIDResponse>();
            var actualGetRoamingBlackListByCustomerIDRequest =
                Arg.Is<GetRoamingBlackListByCustomerIDRequest>(x => x.CustomerId == 1);
            var actualGetRoamingBlackListByCustomerIDResponse = new GetRoamingBlackListByCustomerIDResponse()
            {
                RoamingBlackListInfos =
                    new List<RoamingBlackListInfo>()
                    {
                        CreateDefaultObject.Create<RoamingBlackListInfo>(),
                        CreateDefaultObject.Create<RoamingBlackListInfo>()
                    },
                ResultType = ResultTypes.Ok,
            };
            mockedGetRoamingBlackListByCustomerIDMS.Process(actualGetRoamingBlackListByCustomerIDRequest, null)
                .Returns(actualGetRoamingBlackListByCustomerIDResponse);

            //Mocked GetMVNOAPNIPPoolByMsisdnMS
            var mockedGetMVNOAPNIPPoolByMsisdnMS =
                MockMicroService<GetMVNOAPNIPPoolByMsisdnRequest, GetMVNOAPNIPPoolByMsisdnResponse>();
            var actualGetMVNOAPNIPPoolByMsisdnRequest =
                Arg.Is<GetMVNOAPNIPPoolByMsisdnRequest>(x => x.Msisdn == request.MSISDN);
            var actualGetMVNOAPNIPPoolByMsisdnResponse = new GetMVNOAPNIPPoolByMsisdnResponse()
            {
                MvnoapnipPoolInfo =
                    new List<MVNOAPNIPPoolInfo>()
                    {
                        CreateDefaultObject.Create<MVNOAPNIPPoolInfo>(),
                        CreateDefaultObject.Create<MVNOAPNIPPoolInfo>()
                    },
                ResultType = ResultTypes.Ok,
            };
            mockedGetMVNOAPNIPPoolByMsisdnMS.Process(actualGetMVNOAPNIPPoolByMsisdnRequest, null)
                .Returns(actualGetMVNOAPNIPPoolByMsisdnResponse);

            //if not eroski
            //Mocked cancelCustomersPromotionMS
            var mockedRepoResourceMBInfo =
                MockRepositoryManager.GetMockedRepository<IResourceMBRepository<ResourceMBInfo>>();
            var actualResourceMBInfos = new List<ResourceMBInfo>();
            var actualResourveMB = CreateDefaultObject.Create<ResourceMBInfo>();
            actualResourveMB.Resource = request.MSISDN;
            actualResourveMB.CustomerInfo = CreateDefaultObject.Create<CustomerInfo>();
            actualResourveMB.CustomerInfo.Promotions = new List<CrmCustomersPromotionInfo>()
            {
                CreateDefaultObject.Create<CrmCustomersPromotionInfo>(),
                CreateDefaultObject.Create<CrmCustomersPromotionInfo>()
            };

            actualResourceMBInfos.Add(actualResourveMB);
            mockedRepoResourceMBInfo.GetByMSISDNAndStatusNotInAndActiveDates(request.MSISDN, Arg.Any<Int32[]>())
                .Returns(actualResourceMBInfos);

            var mockedCancelCustomersPromotionMS =
                MockMicroService<CancelCustomersPromotionRequest, CancelCustomersPromotionResponse>();
            var actualCancelCustomersPromotionRequest =
                Arg.Is<CancelCustomersPromotionRequest>(
                    x => x.CrmCustomersPromotionInfo.PromotionId == 1 || x.CrmCustomersPromotionInfo.PromotionId == 2);
            var actualCrmCustomersPromotionInfo = CreateDefaultObject.Create<CrmCustomersPromotionInfo>();
            actualCrmCustomersPromotionInfo.PromotionDetail = new RmPromotionPlanDetailInfo();
            actualCrmCustomersPromotionInfo.PromotionDetail.PromotionPlanDetailId = 1;
            actualCrmCustomersPromotionInfo.PromotionDetail.RmPromotionPlanInfo = new RmPromotionPlanInfo();
            actualCrmCustomersPromotionInfo.PromotionDetail.RmPromotionPlanInfo.DealerId = 1;
            actualCrmCustomersPromotionInfo.PromotionDetail.RmPromotionPlanInfo.PromotionPlanId = 1;
            actualCrmCustomersPromotionInfo.Customer = CreateDefaultObject.Create<CustomerInfo>();

            var actualCancelCustomersPromotionResponse = new CancelCustomersPromotionResponse()
            {
                CrmCustomersPromotionInfo = actualCrmCustomersPromotionInfo,
                ResultType = ResultTypes.Ok,
            };
            mockedCancelCustomersPromotionMS.Process(actualCancelCustomersPromotionRequest, null)
                .Returns(actualCancelCustomersPromotionResponse);

            //Mocked GetCustomerPromotionOperationLogByCustomerIDAndPromotionMS
            var mockedGetCustomerPromotionOperationLogByCustomerIDAndPromotionMS =
                MockMicroService
                    <GetCustomerPromotionOperationLogByCustomerIDAndPromotionRequest,
                        GetCustomerPromotionOperationLogByCustomerIDAndPromotionResponse>();
            var actualGetCustomerPromotionOperationLogByCustomerIDAndPromotionRequest =
                Arg.Is<GetCustomerPromotionOperationLogByCustomerIDAndPromotionRequest>(
                    x =>
                        x.CustomerID == 1 &&
                        x.PromotionIDList.Contains(actualCrmCustomersPromotionInfo.PromotionDetail.PromotionPlanDetailId));
            var actualGetCustomerPromotionOperationLogByCustomerIDAndPromotionResponse = new GetCustomerPromotionOperationLogByCustomerIDAndPromotionResponse
                ()
            {
                LogInfo = CreateDefaultObject.Create<CrmCustomersPromotionOperationLogInfo>(),
                ResultType = ResultTypes.Ok,
            };
            mockedGetCustomerPromotionOperationLogByCustomerIDAndPromotionMS.Process(
                actualGetCustomerPromotionOperationLogByCustomerIDAndPromotionRequest, null)
                .Returns(actualGetCustomerPromotionOperationLogByCustomerIDAndPromotionResponse);

            //Mocked CreateCustomerPromotionLogInfoMS
            var createCustomerPromotionLogInfoMS =
                MockMicroService<CreateCustomerPromotionLogInfoRequest, CreateCustomerPromotionLogInfoResponse>();
            var actualCreateCustomerPromotionLogInfoResponse = new CreateCustomerPromotionLogInfoResponse()
            {
                ResultType = ResultTypes.Ok,
            };
            createCustomerPromotionLogInfoMS.Process(Arg.Any<CreateCustomerPromotionLogInfoRequest>(), null)
                .Returns(actualCreateCustomerPromotionLogInfoResponse);

            //Mocked DeleteRoamingBlackListMS
            var mockedDeleteRoamingBlackListMS =
                MockMicroService<DeleteRoamingBlackListRequest, DeleteRoamingBlackListResponse>();
            var actualDeleteRoamingBlackListResponse = new DeleteRoamingBlackListResponse()
            {
                ResultType = ResultTypes.Ok,
            };
            mockedDeleteRoamingBlackListMS.Process(Arg.Any<DeleteRoamingBlackListRequest>(), null)
                .Returns(actualDeleteRoamingBlackListResponse);

            //Mocked DeleteCustomerDataRoamingLimitMS
            var mockeDeleteCustomerDataRoamingLimitMS =
                MockMicroService<DeleteCustomerDataRoamingLimitRequest, DeleteCustomerDataRoamingLimitResponse>();
            var actualDeleteCustomerDataRoamingLimitResponse = new DeleteCustomerDataRoamingLimitResponse()
            {
                ResultType = ResultTypes.Ok,
            };
            mockeDeleteCustomerDataRoamingLimitMS.Process(Arg.Any<DeleteCustomerDataRoamingLimitRequest>(), null)
                .Returns(actualDeleteCustomerDataRoamingLimitResponse);

            //Mocked DeleteCustomerDataRoamingLimitNotificationMS
            var mockedDeleteCustomerDataRoamingLimitNotificationMS =
                MockMicroService
                    <DeleteCustomerDataRoamingLimitNotificationRequest,
                        DeleteCustomerDataRoamingLimitNotificationResponse>();
            var actualDeleteCustomerDataRoamingLimitNotificationResponse = new DeleteCustomerDataRoamingLimitNotificationResponse
                ()
            {
                ResultType = ResultTypes.Ok,
            };
            mockedDeleteCustomerDataRoamingLimitNotificationMS.Process(
                Arg.Any<DeleteCustomerDataRoamingLimitNotificationRequest>(), null)
                .Returns(actualDeleteCustomerDataRoamingLimitNotificationResponse);
            //todo unbindimeitoOta

            //Not portin
            //Mocked CancelResourceMBInfoMS
            var mockedCancelResourceMBInfoMS =
                MockMicroService<CancelResourceMBInfoRequest, CancelResourceMBInfoResponse>();
            var actualCancelResourceMBInfoResponse = new CancelResourceMBInfoResponse()
            {
                ResultType = ResultTypes.Ok,
            };
            mockedCancelResourceMBInfoMS.Process(Arg.Any<CancelResourceMBInfoRequest>(), null)
                .Returns(actualCancelResourceMBInfoResponse);

            //Mocked CoolDownNumberMS
            var mockedCoolDownNumberMS = MockMicroService<CoolDownNumberRequest, CoolDownNumberResponse>();
            var actualCoolDownNumberResponse = new CoolDownNumberResponse()
            {
                ResultType = ResultTypes.Ok,
            };
            mockedCoolDownNumberMS.Process(Arg.Any<CoolDownNumberRequest>(), null).Returns(actualCoolDownNumberResponse);

            //Mocked ExpirateSimCardMS
            var mockedExpirateSimCardMS = MockMicroService<ExpirateSimCardRequest, ExpirateSimCardResponse>();
            var actualExpirateSimCardResponse = new ExpirateSimCardResponse()
            {
                ResultType = ResultTypes.Ok,
            };
            mockedExpirateSimCardMS.Process(Arg.Any<ExpirateSimCardRequest>(), null)
                .Returns(actualExpirateSimCardResponse);

            //generic
            //Mocked CancelCustomerAccountAssociationMS
            var mockedCancelCustomerAccountAssociationMS =
                MockMicroService<CancelCustomerAccountAssociationRequest, CancelCustomerAccountAssociationResponse>();
            var actualCancelCustomerAccountAssociationResponse = new CancelCustomerAccountAssociationResponse()
            {
                ResultType = ResultTypes.Ok,
            };
            mockedCancelCustomerAccountAssociationMS.Process(Arg.Any<CancelCustomerAccountAssociationRequest>(), null)
                .Returns(actualCancelCustomerAccountAssociationResponse);

            //Mocked DeleteCustomerInfoMS
            var mockedDeleteCustomerInfoMS = MockMicroService<DeleteCustomerInfoRequest, DeleteCustomerInfoResponse>();
            var actualDeleteCustomerInfoResponse = new DeleteCustomerInfoResponse()
            {
                ResultType = ResultTypes.Ok,
            };
            mockedDeleteCustomerInfoMS.Process(Arg.Any<DeleteCustomerInfoRequest>(), null)
                .Returns(actualDeleteCustomerInfoResponse);

            //Mocked RecycleIpByMsisdnMS
            var mockedRecycleIpByMsisdnMS = MockMicroService<RecycleIpByMsisdnRequest, RecycleIpByMsisdnResponse>();
            var actualRecycleIpByMsisdnResponse = new RecycleIpByMsisdnResponse()
            {
                ResultType = ResultTypes.Ok,
            };
            mockedRecycleIpByMsisdnMS.Process(Arg.Any<RecycleIpByMsisdnRequest>(), null)
                .Returns(actualRecycleIpByMsisdnResponse);

            //Mocked GetSystemConfigDataInfosByItemMS
            var mockedGetSystemConfigDataInfosByItemMS =
                MockMicroService<GetSystemConfigDataInfosByItemRequest, GetSystemConfigDataInfosByItemResponse>();
            var actualGetSystemConfigDataInfosByItemResponse = new GetSystemConfigDataInfosByItemResponse()
            {
                ResultType = ResultTypes.Ok,
            };
            mockedGetSystemConfigDataInfosByItemMS.Process(Arg.Any<GetSystemConfigDataInfosByItemRequest>(), null)
                .Returns(actualGetSystemConfigDataInfosByItemResponse);

            //Mock CalculateNextBillRunDateForBillCycleMS
            var mockedCalculateNextBillRunDateForBillCycleMS =
                MockMicroService
                    <CalculateNextBillRunDateForBillCycleRequest, CalculateNextBillRunDateForBillCycleResponse>();
            mockedCalculateNextBillRunDateForBillCycleMS.Process(
                Arg.Any<CalculateNextBillRunDateForBillCycleRequest>(), Arg.Any<RequestInvokationEnvironment>())
                .Returns(new CalculateNextBillRunDateForBillCycleResponse()
                {
                    NextBillRun = DateTime.Now,
                    ResultType = ResultTypes.Ok
                });

            //Mock GetSettingInfosByDealerId
            var mockedGetSettingInfosByDealerIdMS =
                MockMicroService<GetSettingInfosByDealerIdRequest, GetSettingInfosByDealerIdResponse>();

            var settingInfos = new List<SettingInfo>()
            {
                CreateDefaultObject.Create<SettingInfo>()
            };

            settingInfos.FirstOrDefault().SettingId = actualCancelCustomerAndSubscriptionConfiguration.SettingId;
            settingInfos.FirstOrDefault().SettingDetailInfos = new List<SettingDetailInfo>()
            {
                CreateDefaultObject.Create<SettingDetailInfo>()
            };
            settingInfos.FirstOrDefault().SettingDetailInfos.FirstOrDefault().DetailId =
                actualCancelCustomerAndSubscriptionConfiguration.DetailId;
            settingInfos.FirstOrDefault().SettingDetailInfos.FirstOrDefault().Unit = "Months";
            settingInfos.FirstOrDefault().SettingDetailInfos.FirstOrDefault().Interval = 3;

            mockedGetSettingInfosByDealerIdMS.Process(Arg.Any<GetSettingInfosByDealerIdRequest>(),
                Arg.Any<RequestInvokationEnvironment>()).Returns(new GetSettingInfosByDealerIdResponse()
                {
                    SettingInfos = settingInfos,
                    ResultType = ResultTypes.Ok
                });

            //Mocked BusinessOperation
            var mockedCancelCustomerProductBizOp =
                Substitute
                    .For
                    <ICoreBusinessOperation<CancelCustomerProductRequestInternal, CancelCustomerProductResponseInternal>
                        >();
            mockedCancelCustomerProductBizOp.Process(Arg.Any<CancelCustomerProductRequestInternal>(), null)
                .Returns(new CancelCustomerProductResponseInternal()
                {
                    ResultType = ResultTypes.Ok,
                });

            BusinessOperationManager.RebindCoreInterfaceToConstant(1, mockedCancelCustomerProductBizOp);

            MockAbstractSinglePhaseOrderProcessor(request);

            //Remock Subscription
            var actualResourceMB = new ResourceMBInfo();
            actualResourceMB.Resource = request.MSISDN;
            actualResourceMB.CustomerInfo = CreateDefaultObject.Create<CustomerInfo>();
            actualResourceMB.CustomerInfo.CustomerID = 999;
            actualResourceMB.OperatorInfo = CreateDefaultObject.Create<DealerInfo>();
            actualResourceMB.ResourceDIDInfo = CreateDefaultObject.Create<ResourceDIDInfo>();
            actualResourceMBInfos.Add(actualResourceMB);

            mockedRepoResourceMBInfo.GetByMSISDNAndStatusNotInAndActiveDates(request.MSISDN, Arg.Any<Int32[]>())
                .Returns(actualResourceMBInfos);

            var expectedCancelCustomerAndSubscriptionsResponseDTO = new CancelCustomerAndSubscriptionsResponseDTO()
            {
                Customer = new CustomerDTO() { CustomerId = actualResourveMB.CustomerInfo.CustomerID.Value }
            };


            var response = CallBizOp(request);

            Assert.AreEqual(ResultTypes.Ok, response.resultType);
            Assert.IsTrue(expectedCancelCustomerAndSubscriptionsResponseDTO.Customer.CustomerId ==
                          response.Customer.CustomerId);
        }
    }
}