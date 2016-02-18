using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using com.etak.core.bizops.fullfilment.CancelCustomerProduct;
using com.etak.core.customer.message.CancelCustomerAccountAssociation;
using com.etak.core.customer.message.DeleteCustomerInfo;
using com.etak.core.customer.message.GetActiveCustomerAccountAssociationByDate;
using com.etak.core.GSMSubscription.message.GetCustomerDataRoamingLimitNotificationByCustomerId;
using com.etak.core.GSMSubscription.messages.CancelResourceMBInfo;
using com.etak.core.GSMSubscription.messages.DeleteCustomerDataRoamingLimit;
using com.etak.core.GSMSubscription.messages.DeleteCustomerDataRoamingLimitNotification;
using com.etak.core.GSMSubscription.messages.DeleteResourceMBInfo;
using com.etak.core.GSMSubscription.messages.DeleteRoamingBlackList;
using com.etak.core.GSMSubscription.messages.GetCustomerDataRoamingLimitsByCustomerID;
using com.etak.core.GSMSubscription.messages.GetResourceMBInfosByMSISDNAndStatusNotInAndActiveDates;
using com.etak.core.GSMSubscription.messages.GetRoamingBlackListByCustomerID;
using com.etak.core.microservices.messages.GetSettingInfosByDealerId;
using com.etak.core.model;
using com.etak.core.model.operation.messages;
using com.etak.core.operation.contract.exceptions;
using com.etak.core.operation.implementation;
using com.etak.core.operation.manager;
using com.etak.core.product.message.CalculateNextBillRunDateForBillCycle;
using com.etak.core.resource.msisdn.message.CoolDownNumberMS;
using com.etak.core.resource.msisdn.message.DeleteNumberMS;
using com.etak.core.resource.msisdn.message.GetNumberByResource;
using com.etak.core.resource.msisdn.message.RecycleIpByMsisdn;
using com.etak.core.resource.simCard.message.ExpirateSimCard;
using com.etak.core.resource.simCard.message.GetSimCardByICCId;
using com.etak.core.resource.simCard.message.InitSimCard;
using log4net;
using com.etak.core.dealer.messages.CheckAuthorization;

namespace com.etak.core.bizops.fullfilment.CancelCustomerAndSubscriptions
{

    /// <summary>
    /// Cancel Customer and subscriptions
    /// </summary>
    public class CancelCustomerAndSubscriptionsBizOp : AbstractSinglePhaseOrderProcessor<CancelCustomerAndSubscriptionsRequestDTO, CancelCustomerAndSubscriptionsResponseDTO, CancelCustomerAndSubscriptionsRequestInternal, CancelCustomerAndSubscriptionsResponseInternal, CancelCustomerAndSubscriptionsOrder>
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);



        //TODO Use CancelCustomerAndSubscriptionConfiguration to set specific eroski configuration EROSKI_FISCAL_UNIT_ID = 210000; can't do specific actions in this bizop

        /// <summary>
        /// CancelCustomerAndSubscriptionsBizOp Operation code 
        /// </summary>
        public override string OperationCode
        {
            get { return OperationCodes.CancelCustomerAndSubscriptionsOperation; }
        }


        /// <summary>
        /// CancelCustomerAndSubscriptionsBizOp Operation Discriminator
        /// </summary>
        public override string OperationDiscriminator
        {
            get { return OperationDiscriminators.CancelCustomerAndSubscriptionsOperation; }
        }

        /// <summary>
        /// Maps all the inboud properties of the request that are not mapped by the framework
        /// 
        ///     if NeedRecycle:
        ///         -Get ResourceMB by MSISDN with active dates and without delete Status ("0" + request.MSISDN)
        ///         -Get Number by resource ("0" + request.MSISDN)
        ///     -Get customer by Subscription(ResourceMbInfo)
        ///     -Get active Customer account association
        ///     -Get SimCard  by ICCID
        ///     -Get Imei  by RescourceID
        ///     -Get CustomerDataRoamingLimits By CustomerID
        ///     -Get customerDataRoamingLimitNotification by CustomerId	
        ///     -Get roamingblackList by CustomerId
        ///    
        /// </summary>
        /// <param name="request"></param>
        /// <param name="coreInput"></param>
        protected override void MapNotAutomappedOrderInboundProperties(CancelCustomerAndSubscriptionsRequestDTO request,
           ref CancelCustomerAndSubscriptionsRequestInternal coreInput)
        {
            var microServiceCheckAuthorization = MicroServiceManager.GetMicroService<CheckAuthorizationRequest, CheckAuthorizationResponse>();
            if (coreInput.MSISDN == null)
            {
                throw new BusinessLogicErrorException("No MSISDN defined in request", BizOpsErrors.MSISDNNotFound);
            }

            //Set NeedRecycle
            coreInput.NeedRecycle = request.NeedRecycle;

            #region Get Data from repositories and check

            //If portin
            if (request.NeedRecycle)
            {
                #region if is portin
                //Get ResourceMB That means it is a ported number being cancelled. We need to check if it's an internal portin.
                if (coreInput.Subscription == null)
                {

                    if (Log.IsDebugEnabled)
                        Log.DebugFormat(
                            "Calling GetResourceMBInfosByCustomerIDMS to get the ResourceMbInfos of the msisdn ({0}) specified.",
                            "0" + request.MSISDN);
                    //Portin msisdn
                    var portinMsisdn = "0" + request.MSISDN;

                    //Get resourceMBinfo by MSISDN with active dates and without delete Status 
                    var getResourceMBInfosByCustomerIDMS =
                        MicroServiceManager
                            .GetMicroService
                            <GetResourceMBInfosByMSISDNAndStatusNotInAndActiveDatesRequest,
                                GetResourceMBInfosByMSISDNAndStatusNotInAndActiveDatesResponse>();
                    var getResourceMBInfosByMSISDNAndStatusNotInAndActiveDatesRequest = new GetResourceMBInfosByMSISDNAndStatusNotInAndActiveDatesRequest()
                    {
                        Msisdn = portinMsisdn,
                        Status = new List<int> { (int)ResourceStatus.Deleted },
                    };
                    var getResourceMBInfosByMSISDNAndStatusNotInAndActiveDatesResponse =
                        getResourceMBInfosByCustomerIDMS.Process(
                            getResourceMBInfosByMSISDNAndStatusNotInAndActiveDatesRequest, null);
                    coreInput.Subscription =
                        getResourceMBInfosByMSISDNAndStatusNotInAndActiveDatesResponse.ResourceMbInfos.FirstOrDefault();

                    //if Subscription exist replase msisdn
                    if (coreInput.Subscription != null)
                    {
                        request.MSISDN = portinMsisdn;
                    }
                    else
                    {
                        throw new BusinessLogicErrorException(string.Format("Cannot find an active Resource with MSISDN {0}.", request.MSISDN), BizOpsErrors.ResourceMBNotFound);
                    }

                    if (coreInput.NumberInPool == null || coreInput.NumberInPool.NumberProperty == null)
                    {
                        //Get number by resource
                        var getNumberByResourceMS = MicroServiceManager.GetMicroService<GetNumberByResourceRequest, GetNumberByResourceResponse>();
                        var getNumberByResourceRequest = new GetNumberByResourceRequest()
                        {
                            Resource = request.MSISDN,
                        };
                        var getNumberByResourceResponse = getNumberByResourceMS.Process(getNumberByResourceRequest, null);
                        coreInput.NumberInPool = getNumberByResourceResponse.NumberInfo;
                    }

                }

                #endregion
            }


            if (coreInput.Subscription == null)
            {
                throw new BusinessLogicErrorException(string.Format("Cannot find an active Resource with MSISDN {0}.", request.MSISDN), BizOpsErrors.ResourceMBNotFound);
            }
            if (coreInput.Subscription.CustomerInfo == null)
            {
                throw new BusinessLogicErrorException(string.Format("Cannot find Customer Info with MSISDN {0}", request.MSISDN), BizOpsErrors.CustomerInfoNotFound);
            }
            var checkAuthorizationRequest = new CheckAuthorizationRequest
            {
                UserInfo = coreInput.User, 
                DealerId = coreInput.Subscription.CustomerInfo.DealerID.HasValue ? coreInput.Subscription.CustomerInfo.DealerID.Value : 0
            };
            var checkAuthorizationResponse = microServiceCheckAuthorization.Process(checkAuthorizationRequest, null);
            if (!checkAuthorizationResponse.IsAuthorized)
                throw new AuthorizationErrorException("User does not have enough permissions", BizOpsErrors.AuthorizeErrorUser);
            if (coreInput.Subscription.IMSI == null)
            {
                throw new BusinessLogicErrorException(string.Format("Cannot find IMSI in Resource with id {0} and MSISDN {1}.", coreInput.Subscription.ResourceID, request.MSISDN), BizOpsErrors.ResourceMBNotFound);
            }

            if (coreInput.Subscription.StatusID == null)
            {
                throw new BusinessLogicErrorException(string.Format("Cannot find StatusID in Resource with id {0} and MSISDN {1}.", coreInput.Subscription.ResourceID, request.MSISDN), BizOpsErrors.ResourceMBNotFound);
            }

            if (coreInput.NumberInPool == null || coreInput.NumberInPool.NumberProperty == null)
            {
                throw new BusinessLogicErrorException(string.Format("Cannot get Number Information and Property Information for MSISDN {0}", coreInput.MSISDN), BizOpsErrors.NumberPropertyNotFound);
            }

            #region Get Customer and check it isn't deleted
            coreInput.CustomerInfo = coreInput.Subscription.CustomerInfo;
            if (coreInput.CustomerInfo == null || coreInput.CustomerInfo.CustomerID == null)
                throw new BusinessLogicErrorException(string.Format("Cannot get Customer Information for MSISDN {0}", request.MSISDN), BizOpsErrors.CustomerNotFound);

            if (coreInput.CustomerInfo.StatusID == (int)CustomerStatus.Deleted)
            {
                throw new BusinessLogicErrorException(string.Format("The customer {0} is already in Deleted Status", coreInput.CustomerInfo.CustomerID.Value), BizOpsErrors.CustomerInDeletedStatus);
            }
            #endregion

            //Get active Customer account association
            var getActiveCustomerAccountAssociationByDateMS = MicroServiceManager.GetMicroService<GetActiveCustomerAccountAssociationByDateRequest, GetActiveCustomerAccountAssociationByDateResponse>();
            var getActiveCustomerAccountAssociationByDateRequest =
                new GetActiveCustomerAccountAssociationByDateRequest()
                {
                    ActiveCustomerAccountAssociationDate = DateTime.Now,
                    CustomerInfo = coreInput.CustomerInfo,
                };
            var getActiveCustomerAccountAssociationByDateResponse = getActiveCustomerAccountAssociationByDateMS.Process(getActiveCustomerAccountAssociationByDateRequest, null);

            if (getActiveCustomerAccountAssociationByDateResponse.ResultType != ResultTypes.Ok || getActiveCustomerAccountAssociationByDateResponse.CustomerAccountAssociation == null)
            {
                throw new BusinessLogicErrorException(
                    string.Format("Cannot get Customer Last Account for Customer ID {0}", coreInput.CustomerInfo.CustomerID.Value), BizOpsErrors.CustomerAccountAssociationNotFound);
            }
            coreInput.CustomerAccountAssociation = getActiveCustomerAccountAssociationByDateResponse.CustomerAccountAssociation;


            #region Get SimCard Operation
            //Get SimCard  by ICCID
            var getSimCardByICCIDMS = MicroServiceManager.GetMicroService<GetSimCardByICCIdRequest, GetSimCardByICCIdResponse>();

            if (Log.IsDebugEnabled)
                Log.DebugFormat("Set GetSimCardByICCIdRequest with IccId ({0}) specified.", coreInput.Subscription.ICC);

            var getSimCardByICCIdRequest = new GetSimCardByICCIdRequest()
            {
                IccId = coreInput.Subscription.ICC,
            };

            var getSimCardByICCIdResponse = getSimCardByICCIDMS.Process(getSimCardByICCIdRequest, null);

            if (getSimCardByICCIdResponse.ResultType != ResultTypes.Ok || getSimCardByICCIdResponse.SimCardInfo == null)
            {
                throw new BusinessLogicErrorException(string.Format("Cannot get Simcard Information for IccId {0}", coreInput.Subscription.ICC), BizOpsErrors.SimCardNotFound);
            }

            coreInput.SimCardInfo = getSimCardByICCIdResponse.SimCardInfo;
            #endregion

            //Get CustomerDataRoamingLimits By CustomerID
            var getCustomerDataRoamingLimitsByCustomerIDMS =
                MicroServiceManager
                    .GetMicroService
                    <GetCustomerDataRoamingLimitsByCustomerIDRequest, GetCustomerDataRoamingLimitsByCustomerIDResponse>();
            if (Log.IsDebugEnabled)
                Log.DebugFormat("Set GetCustomerDataRoamingLimitsByCustomerIDRequest with CustomerID ({0}) specified.", coreInput.CustomerInfo.CustomerID);
            var getCustomerDataRoamingLimitsByCustomerIDRequest = new GetCustomerDataRoamingLimitsByCustomerIDRequest()
            {
                CustomerID = (int)coreInput.CustomerInfo.CustomerID,
            };
            var getCustomerDataRoamingLimitsByCustomerIDResponse = getCustomerDataRoamingLimitsByCustomerIDMS.Process(getCustomerDataRoamingLimitsByCustomerIDRequest, null);
            coreInput.CustomerDataRoamingLimits =
                getCustomerDataRoamingLimitsByCustomerIDResponse.CustomerDataRoamingLimits;

            //Get customerDataRoamingLimitNotification by CustomerId	
            var getCustomerDataRoamingLimitNotificationByCustomerIdMS =
                MicroServiceManager
                    .GetMicroService
                    <GetCustomerDataRoamingLimitNotificationByCustomerIdRequest,
                        GetCustomerDataRoamingLimitNotificationByCustomerIdResponse>();
            if (Log.IsDebugEnabled)
                Log.InfoFormat("Set GetCustomerDataRoamingLimitNotificationByCustomerIdRequest with CustomerID ({0}) specified.", coreInput.CustomerInfo.CustomerID);
            var getCustomerDataRoamingLimitNotificationByCustomerIdRequest = new GetCustomerDataRoamingLimitNotificationByCustomerIdRequest()
            {
                CustomerId = (int)coreInput.CustomerInfo.CustomerID,
            };
            var getCustomerDataRoamingLimitNotificationByCustomerIdResponse = getCustomerDataRoamingLimitNotificationByCustomerIdMS.Process(getCustomerDataRoamingLimitNotificationByCustomerIdRequest, null);
            coreInput.CustomerDataRoamingLimitNotifications =
                getCustomerDataRoamingLimitNotificationByCustomerIdResponse.CustomerDataRoamingLimitNotifications;

            //Get roamingblackList by CustomerId
            var getRoamingBlackListByCustomerIDMS =
                MicroServiceManager
                    .GetMicroService<GetRoamingBlackListByCustomerIDRequest, GetRoamingBlackListByCustomerIDResponse>();
            if (Log.IsDebugEnabled)
                Log.DebugFormat("Set GetRoamingBlackListByCustomerIDRequest with CustomerID ({0}) specified.", coreInput.CustomerInfo.CustomerID);
            var getRoamingBlackListByCustomerIDRequest = new GetRoamingBlackListByCustomerIDRequest()
            {
                CustomerId = (int)coreInput.CustomerInfo.CustomerID,
            };
            var getRoamingBlackListByCustomerIDResponse = getRoamingBlackListByCustomerIDMS.Process(getRoamingBlackListByCustomerIDRequest, null);
            coreInput.RoamingBlackListInfos = getRoamingBlackListByCustomerIDResponse.RoamingBlackListInfos;

            var calculateNextBillRunDateMS = MicroServiceManager.GetMicroService<CalculateNextBillRunDateForBillCycleRequest, CalculateNextBillRunDateForBillCycleResponse>();
            if (Log.IsDebugEnabled)
                Log.DebugFormat(
                    "Set CalculateNextBillRunDateForBillCycleRequest with BillCycle id ({0}) , purchaseTime ({1}) and firstDayOfWeek({2}).",
                    coreInput.CustomerAccountAssociation.Account.BillingCycle.Id,
                    DateTime.Now,
                    CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek);
            var calculateNextBillRunDateForBillCycleResponse = calculateNextBillRunDateMS.Process(new CalculateNextBillRunDateForBillCycleRequest()
            {
                BillCycleDefinition = coreInput.CustomerAccountAssociation.Account.BillingCycle,
                PurchaseTime = DateTime.Now,
                FirstDayOfWeek = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek,
            }, null);
            if (calculateNextBillRunDateForBillCycleResponse.ResultType != ResultTypes.Ok)
                throw new BusinessLogicErrorException("CalculateNextBillRunDateForBillCycle: Failed", BizOpsErrors.CalculateNextBillrunDateError);
            coreInput.NextBillRunDate = calculateNextBillRunDateForBillCycleResponse.NextBillRun;

            #endregion

        }

        /// <summary>
        /// Maps all the outboud properties of the response that are not mapped by the framework
        /// </summary>
        /// <param name="source"></param>
        /// <param name="coreOutput"></param>
        protected override void MapNotAutomappedOrderOutboundProperties(CancelCustomerAndSubscriptionsResponseInternal source,
            ref CancelCustomerAndSubscriptionsResponseDTO coreOutput)
        {

        }


        /// <summary>
        /// Cancel customer and all Subscriptions
        /// 
        ///     -Delete customer products
        ///     -Delete roaming black list records
        ///     -Delete data roaming limit
        ///     if NeedRecycle:
        ///         -Delete Subscription
        ///         -Init SimCard
        ///         -Delete Number
        ///     else:
        ///         -Cancel subscription
        ///         -Cool down Number
        ///         -Expirate SimCard
        ///     -Cancel Customer Account Association
        ///     -Delete CustomerInfo
        ///     -Recycle Ip by Msisdn
        /// </summary>
        /// <param name="order"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public override CancelCustomerAndSubscriptionsResponseInternal ProcessRequest(CancelCustomerAndSubscriptionsOrder order,
            CancelCustomerAndSubscriptionsRequestInternal request)
        {
            var now = DateTime.Now;
            #region BizOP Manager
            var cancelCustomerProduct =
               BusinessOperationManager
                   .GetCoreBusinessOperation
                   <CancelCustomerProductRequestInternal, CancelCustomerProductResponseInternal>((int)request.MVNO.DealerID);
            #endregion

            //Get configuration of this operation
            var config = GetOperationConfigForDealer<CancelCustomerAndSubscriptionConfiguration>(request.MVNO);

            var cancelCustomerAndSubscriptionsResponseInternal = new CancelCustomerAndSubscriptionsResponseInternal()
            {
                ResultType = ResultTypes.UnknownError,
            };

            if (StatusAbleToDelete(request.Subscription.StatusID.Value))
            {
                //Delete promotions
                if (config == null || config.CanDeletePromotions)
                {
                    if (request.CustomerInfo.RevenueProductsInfo.Any())
                    {
                        foreach (var productAssignment in request.CustomerInfo.RevenueProductsInfo.Where(x=>!x.EndDate.HasValue||x.EndDate>now).ToList())
                        {
                            #region Cancel Customer product
                            var cancelCustomerReq = new CancelCustomerProductRequestInternal()
                            {
                                CustomerProductAssignment = productAssignment,
                                CancelDate = now,
                                NextBillRunDate = request.NextBillRunDate,
                                MVNO = request.MVNO,
                                User = request.User,
                                Channel = request.Channel,
                                UseNextBillCycleEndDateInRecurring = false,
                                CancelCustomerPromotionsWithSamePromotionPlanDetail = true
                            };

                            var purchaseResp = cancelCustomerProduct.Process(cancelCustomerReq, null);
                            if (purchaseResp.ResultType != ResultTypes.Ok)
                                throw new BusinessLogicErrorException(string.Format("Cannot Cancel Product in CancelCustomerAndSubscription process ProductAssignmentId {0}", productAssignment.Id), BizOpsErrors.CanNotCancelProduct);
                            #endregion
                        }

                    }
                }

                //Delete roaming black list records.
                if (config == null || config.CanRemoveRoamingBlackList)
                {
                    RemoveBlackMemberByCustomerId(request.RoamingBlackListInfos);
                }

                //Delete data roaming limit
                if (config == null || config.CanDeleteDataRoamingLimit)
                {
                    DeleteDataRoamingLimit(request.CustomerDataRoamingLimits, request.CustomerDataRoamingLimitNotifications);
                }

                //If we need to Recycle because it's a ported number cancelled, we need to recycle msisdn and simcard.
                #region Recycle Sim and Number
                if (request.NeedRecycle)
                {

                    //Delete Subscription
                    var deleteResourceMBInfoMS = MicroServiceManager.GetMicroService<DeleteResourceMBInfoRequest, DeleteResourceMBInfoResponse>();
                    var deleteResourceMBInfoRequest = new DeleteResourceMBInfoRequest()
                    {
                        ResourceMbInfo = request.Subscription,
                        EndDate = DateTime.Now,
                        MVNO = request.MVNO,
                        User = request.User,
                        Channel = request.Channel,
                        HasToDeleteFromHLR = config != null ? config.HasToUseHLR : true
                    };
                    deleteResourceMBInfoMS.Process(deleteResourceMBInfoRequest, null);

                    //Init SimCard
                    //Validate that SimCard belongs to Dealer 
                    if (request.SimCardInfo.Dealer.DealerID != request.MVNO.FiscalUnitID)
                        throw new DataValidationErrorException(string.Format("User MVNOID ({0}) do not match with Sim Card owner ({1}).", request.User.MVNOID, request.SimCardInfo.Dealer.DealerID), BizOpsErrors.OwnerNotMatch);

                    //The resource has status and the status is not deleted
                    if (!request.SimCardInfo.Status.HasValue || request.SimCardInfo.Status.Value == (int)ResourceStatus.Deleted)
                        throw new DataValidationErrorException(string.Format("Sim Card cannot be in Deleted or undefined ({0}), state.", !request.SimCardInfo.Status.HasValue ? "null" : string.Format("{0}", request.SimCardInfo.Status)), BizOpsErrors.StatusError);

                    var initSimCardMS = MicroServiceManager.GetMicroService<InitSimCardRequest, InitSimCardResponse>();
                    var initSimCardRequest = new InitSimCardRequest()
                    {
                        SimCardInfo = request.SimCardInfo,
                        CustomerInfo = request.CustomerInfo,
                        MVNO = request.MVNO,
                        User = request.User,
                        Channel = request.Channel
                    };
                    initSimCardMS.Process(initSimCardRequest, null);


                    //The resource has status and the status is not installed
                    if (!request.NumberInPool.NumberProperty.StatusID.HasValue || (request.NumberInPool.NumberProperty.StatusID.Value != (int)ResourceStatus.Installed))
                        throw new DataValidationErrorException(string.Format("Resource is not in installed but was on {0} state.", !request.NumberInPool.NumberProperty.StatusID.HasValue ? "null" : string.Format("{0}", request.NumberInPool.NumberProperty.StatusID)), BizOpsErrors.StatusError);

                    //Delete number
                    var deleteNumberMS = MicroServiceManager.GetMicroService<DeleteNumberRequest, DeleteNumberResponse>();
                    var deleteNumberRequest = new DeleteNumberRequest()
                    {
                        NumberPropertyInfo = request.NumberInPool.NumberProperty,
                        CustomerInfo = request.CustomerInfo,
                        MVNO = request.MVNO,
                        User = request.User,
                        Channel = request.Channel
                    };
                    deleteNumberMS.Process(deleteNumberRequest, null);



                }
                #endregion
                #region Normal Cancellation
                else
                {
                    var getSettingInfosByDealerIdRequest = new GetSettingInfosByDealerIdRequest()
                    {
                        DealerId = request.MVNO.DealerID.Value
                    };
                    var getSettingInfosByDealerIdMS =
                        MicroServiceManager
                            .GetMicroService<GetSettingInfosByDealerIdRequest, GetSettingInfosByDealerIdResponse>();
                    var getSettingInfosByDealerIdResponse = getSettingInfosByDealerIdMS.Process(getSettingInfosByDealerIdRequest, null);
                    if (getSettingInfosByDealerIdResponse.SettingInfos.IsEmpty() || getSettingInfosByDealerIdResponse.SettingInfos == null)
                    {
                        throw new DataValidationErrorException(string.Format("Cannot get the SettingInfos for DealerId {0}", request.MVNO.DealerID.Value), BizOpsErrors.SettingInfosIsNull);
                    }
                    var settingInfo = getSettingInfosByDealerIdResponse.SettingInfos.FirstOrDefault(x => x.SettingId == config.SettingId);
                    if (settingInfo == null)
                    {
                        throw new DataValidationErrorException(string.Format("There is no settingInfo with settingId {0}", config.SettingId), BizOpsErrors.SettingInfoWithCertainSettingIdIsNull);
                    }
                    if (settingInfo.SettingDetailInfos.IsEmpty() || settingInfo.SettingDetailInfos == null)
                    {
                        throw new DataValidationErrorException(string.Format("settingInfo with settingId {0} doesn't have settingDetailInfo", config.SettingId), BizOpsErrors.SettingDetailInfosIsNull);
                    }

                    var settingDetailInfo = settingInfo.SettingDetailInfos.FirstOrDefault(x => x.DetailId == config.DetailId);
                    if (settingDetailInfo == null)
                    {
                        throw new DataValidationErrorException(string.Format("There is no SettingDetailInfo with DetailId {0}", config.DetailId), BizOpsErrors.SettingDetailInfoIsNull);
                    }
                    
                    var activeDeadlineDateToBeSet = DateTime.Now;
                    switch (settingDetailInfo.Unit)
                    {
                        case "Months":
                            activeDeadlineDateToBeSet = activeDeadlineDateToBeSet.AddMonths(settingDetailInfo.Interval);
                            break;
                        case "Days":
                            activeDeadlineDateToBeSet = activeDeadlineDateToBeSet.AddDays(settingDetailInfo.Interval);
                            break;
                        case "Years":
                            activeDeadlineDateToBeSet = activeDeadlineDateToBeSet.AddYears(settingDetailInfo.Interval);
                            break;
                        default:
                            throw new DataValidationErrorException(string.Format("SettingDetailInfo Unit is not recognizable"), BizOpsErrors.SettingDetailInfoUnitIsNotRecognizable);
                    }

                    //Cancel subscription
                    var cancelResourceMBInfoMS = MicroServiceManager.GetMicroService<CancelResourceMBInfoRequest, CancelResourceMBInfoResponse>();
                    var cancelResourceMBInfoRequest = new CancelResourceMBInfoRequest()
                    {
                        CustomersResourceMb = request.Subscription,
                        EndDate = DateTime.Now,
                        ChangeStatusDate = DateTime.Now,
                        ActiveDeadlineDateToBeSet = activeDeadlineDateToBeSet,
                        MVNO = request.MVNO,
                        User = request.User,
                        Channel = request.Channel,
                        HasToDeleteFromHLR = config != null ? config.HasToUseHLR : true
                    };
                    cancelResourceMBInfoMS.Process(cancelResourceMBInfoRequest, null);

                    //Cool down Number
                    var coolDownNumberMS = MicroServiceManager.GetMicroService<CoolDownNumberRequest, CoolDownNumberResponse>();
                    var coolDownNumberRequest = new CoolDownNumberRequest()
                    {
                        EndDate = DateTime.Now,
                        NumberPropertyInfo = request.NumberInPool.NumberProperty,
                        CustomerInfo = request.CustomerInfo,
                        MVNO = request.MVNO,
                        User = request.User,
                        Channel = request.Channel
                    };
                    coolDownNumberMS.Process(coolDownNumberRequest, null);

                    //Expirate SimCard
                    //Validate that SimCard belongs to Dealer 
                    if (request.SimCardInfo.Dealer.DealerID != request.MVNO.FiscalUnitID)
                        throw new DataValidationErrorException(string.Format("User MVNOID ({0}) do not match with Sim Card owner ({1}).", request.User.MVNOID, request.SimCardInfo.Dealer.DealerID), BizOpsErrors.OwnerNotMatch);

                    //The resource has status and the status is not Deleted
                    if (!request.SimCardInfo.Status.HasValue || request.SimCardInfo.Status.Value == (int)ResourceStatus.Deleted)
                        throw new DataValidationErrorException(string.Format("Sim Card cannot be in Deleted or undefined ({0}), state.", !request.SimCardInfo.Status.HasValue ? "null" : string.Format("{0}", request.SimCardInfo.Status)), BizOpsErrors.StatusError);

                    var expirateSimCardMS = MicroServiceManager.GetMicroService<ExpirateSimCardRequest, ExpirateSimCardResponse>();
                    var ExpirateSimCardRequest = new ExpirateSimCardRequest()
                    {
                        SimCardInfo = request.SimCardInfo,
                        CustomerInfo = request.CustomerInfo,
                        MVNO = request.MVNO,
                        User = request.User,
                        Channel = request.Channel
                    };
                    expirateSimCardMS.Process(ExpirateSimCardRequest, null);


                }

                #endregion

                //Cancel Customer Account Association
                var cancelCustomerAccountAssociationMS = MicroServiceManager.GetMicroService<CancelCustomerAccountAssociationRequest, CancelCustomerAccountAssociationResponse>();
                var cancelCustomerAccountAssociationRequest = new CancelCustomerAccountAssociationRequest()
                {
                    CustomerAccountAssociation = request.CustomerAccountAssociation,
                    EndTime = DateTime.Now,
                    MVNO = request.MVNO,
                    User = request.User,
                    Channel = request.Channel

                };
                cancelCustomerAccountAssociationMS.Process(cancelCustomerAccountAssociationRequest, null);

                //Delete CustomerInfo
                var deleteCustomerInfoMS = MicroServiceManager.GetMicroService<DeleteCustomerInfoRequest, DeleteCustomerInfoResponse>();
                var deleteCustomerInfoRequest = new DeleteCustomerInfoRequest()
                {
                    CustomerInfo = request.CustomerInfo,
                    MVNO = request.MVNO,
                    User = request.User,
                    Channel = request.Channel
                };
                deleteCustomerInfoMS.Process(deleteCustomerInfoRequest, null);


                //Recycle Ip by Msisdn
                var recycleIpByMsisdnMS = MicroServiceManager.GetMicroService<RecycleIpByMsisdnRequest, RecycleIpByMsisdnResponse>();
                var recycleIpByMsisdnRequest = new RecycleIpByMsisdnRequest()
                {
                    LastModifyTime = DateTime.Now,
                    Msisdn = request.MSISDN,
                    MVNO = request.MVNO,
                    User = request.User,
                    Channel = request.Channel
                };
                recycleIpByMsisdnMS.Process(recycleIpByMsisdnRequest, null);


                cancelCustomerAndSubscriptionsResponseInternal.ResultType = ResultTypes.Ok;
                cancelCustomerAndSubscriptionsResponseInternal.Customer = request.CustomerInfo;
            }
            else
            {
                cancelCustomerAndSubscriptionsResponseInternal.ResultType = ResultTypes.BussinessLogicError;
                cancelCustomerAndSubscriptionsResponseInternal.Message = string.Format("The msisdn {0}, is in {1} status, and cannot be deleted.", request.MSISDN, request.Subscription.StatusID);
                cancelCustomerAndSubscriptionsResponseInternal.Customer = request.Subscription.CustomerInfo;
            }

            return cancelCustomerAndSubscriptionsResponseInternal;
        }



        /// <summary>
        /// Check if subscription status given is able delete 
        /// </summary>
        /// <param name="statusId"></param>
        /// <returns></returns>
        private bool StatusAbleToDelete(int statusId)
        {
            return (statusId == (int)ResourceStatus.Installed ||
                    statusId == (int)ResourceStatus.Active ||
                    statusId == (int)ResourceStatus.Inactive ||
                    statusId == (int)ResourceStatus.Deactive ||
                    statusId == (int)ResourceStatus.Frozen);
        }


        /// <summary>
        /// Remove all RoamingBlackListInfo
        /// </summary>
        /// <param name="blackListInfos"></param>
        private void RemoveBlackMemberByCustomerId(IEnumerable<RoamingBlackListInfo> blackListInfos)
        {
            if (blackListInfos.Any())
            {
                //Delete roaming black list
                var deleteRoamingBlackListMS = MicroServiceManager.GetMicroService<DeleteRoamingBlackListRequest, DeleteRoamingBlackListResponse>();
                var deleteRoamingBlackListRequest = new DeleteRoamingBlackListRequest()
                {
                    RoamingBlackListInfo = blackListInfos,
                };
                deleteRoamingBlackListMS.Process(deleteRoamingBlackListRequest, null);
            }

        }


        /// <summary>
        /// Delete all customerDataRoamingLimits and customerDataRoamingLimitNotifications
        /// </summary>
        /// <param name="customerDataRoamingLimits"></param>
        /// <param name="customerDataRoamingLimitNotifications"></param>
        private void DeleteDataRoamingLimit(IEnumerable<CustomerDataRoamingLimit> customerDataRoamingLimits, IEnumerable<CustomerDataRoamingLimitNotification> customerDataRoamingLimitNotifications)
        {
            if (customerDataRoamingLimits.Any())
            {
                var deleteCustomerDataRoamingLimitMS = MicroServiceManager.GetMicroService<DeleteCustomerDataRoamingLimitRequest, DeleteCustomerDataRoamingLimitResponse>();

                foreach (var customerDataRoamingLimit in customerDataRoamingLimits)
                {
                    //Delete customer data roaming limit
                    var deleteCustomerDataRoamingLimitRequest = new DeleteCustomerDataRoamingLimitRequest()
                    {
                        CustomerDataRoamingLimit = customerDataRoamingLimit,
                    };
                    deleteCustomerDataRoamingLimitMS.Process(deleteCustomerDataRoamingLimitRequest, null);
                }
            }

            if (customerDataRoamingLimitNotifications.Any())
            {
                var deleteCustomerDataRoamingLimitNotificationMS = MicroServiceManager.GetMicroService<DeleteCustomerDataRoamingLimitNotificationRequest, DeleteCustomerDataRoamingLimitNotificationResponse>();
                foreach (var customerDataRoamingLimitNotification in customerDataRoamingLimitNotifications)
                {
                    //Delete customer data roaming limit notification
                    var deleteCustomerDataRoamingLimitNotificationRequest = new DeleteCustomerDataRoamingLimitNotificationRequest()
                    {
                        CustomerDataRoamingLimitNotification = customerDataRoamingLimitNotification,
                    };
                    deleteCustomerDataRoamingLimitNotificationMS.Process(deleteCustomerDataRoamingLimitNotificationRequest, null);
                }
            }
        }

        //public bool msisdnIsPortIn(string msisdn)
        //{

        //    #region Setting Repositories
        //    var repoDealer = RepositoryManager.GetRepository<IDealerInfoRepository<DealerInfo>>();
        //    var repoResource = RepositoryManager.GetRepository<IResourceMBRepository<ResourceMBInfo>>();
        //    var repoDealerNumber = RepositoryManager.GetRepository<IDealerNumberInfoRepository<DealerNumberInfo>>();
        //    #endregion

        //    bool isPortIn = false;

        //    DealerNumberInfo dealerNumber1 = repoDealerNumber.GetByResource(msisdn).FirstOrDefault();
        //    if (dealerNumber1 == null || dealerNumber1.DealerID == null)
        //    {
        //        isPortIn = true;
        //    }
        //    else
        //    {
        //        DealerInfo dealer1 = repoDealer.GetMVNOByDealerId(dealerNumber1.DealerID.Value);
        //        if (dealer1 == null)
        //        {
        //            isPortIn = false;
        //        }
        //        else
        //        {
        //            ResourceMBInfo resource = repoResource.GetByMSISDNAndNotStatusAndActiveDates(msisdn, (int)ResourceStatus.Deleted).FirstOrDefault();
        //            if (resource == null || resource.CustomerInfo == null)
        //            {
        //                isPortIn = false;
        //            }
        //            else
        //            {
        //                DealerInfo dealer2 = repoDealer.GetById(resource.CustomerInfo.CustomerID.Value);

        //                isPortIn = (dealer1.FiscalUnitID == dealer2.FiscalUnitID) ? false : true;

        //            }
        //        }
        //    }

        //    return isPortIn;
        //}

    }
}
