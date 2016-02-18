using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using com.etak.core.bizops;
using com.etak.core.bizops.fullfilment.CancelCustomerAndSubscriptions;
using com.etak.core.bizops.fullfilment.RegisterCustomer;
using com.etak.core.bizops.helper;
using com.etak.core.customer.message.GetActiveCustomerAccountAssociationByDate;
using com.etak.core.customer.message.GetPropertyInfosByDocumentId;
using com.etak.core.GSMSubscription.message.GetCustomerDataRoamingLimitNotificationByCustomerId;
using com.etak.core.GSMSubscription.messages.GetCustomerDataRoamingLimitsByCustomerID;
using com.etak.core.GSMSubscription.messages.GetLastSubscriptionByMsisdn;
using com.etak.core.GSMSubscription.messages.GetProvisioningTemplateById;
using com.etak.core.GSMSubscription.messages.GetRoamingBlackListByCustomerID;
using com.etak.core.model;
using com.etak.core.operation.contract;
using com.etak.core.operation.contract.exceptions;
using com.etak.core.operation.dtoConverters;
using com.etak.core.operation.implementation;
using com.etak.core.operation.manager;
using com.etak.core.product.message.CalculateNextBillRunDateForBillCycle;
using com.etak.core.product.message.GetRmPromotionGroupMemberAll;
using com.etak.core.promotion.messages.CreateCustomerPromotionLogInfo;
using com.etak.core.promotion.messages.CreateLogPromotion;
using com.etak.core.promotion.messages.GetCustomerPromotionOperationLogByCustomerIDAndPromotion;
using com.etak.core.promotion.messages.UpdateCustomersPromotion;
using com.etak.core.resource.msisdn.message.GetNumberByResource;
using com.etak.core.resource.simCard.message.GetSimCardByICCId;
using com.etak.core.resource.simCard.message.InitSimCard;
using log4net;
using ResultTypes = com.etak.core.model.operation.messages.ResultTypes;

namespace com.etak.vfes.implementation.bizOps.ChangeOfHolderBizOp
{
    /// <summary>
    /// ChangeOfHolderBizOp business opertaion, this business will cancel the current active customer whose msidn is the requested,
    /// and Register a new customer with the same msidn, same ICCard, and transfer part or all the privoise customer's non-exhausted benefit
    /// </summary>
    public class ChangeOfHolderBizOp: AbstractSinglePhaseOrderProcessor<ChangeOfHolderRequestDTO,ChangeOfHolderResponseDTO,
        ChangeOfHolderRequestInternal,ChangeOfHolderResponseInternal,ChangeOfHolderOrder>
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// PurchaseHelper
        /// </summary>
        protected IPurchaseHelper _purchaseHelper;

        /// <summary>
        /// Cancel Customer BusinessOperation
        /// </summary>
        protected
            ICoreBusinessOperation
                <CancelCustomerAndSubscriptionsRequestInternal, CancelCustomerAndSubscriptionsResponseInternal>
            cancelCustBiz;
        /// <summary>
        /// Register Custome BusinessOperation
        /// </summary>
        protected ICoreBusinessOperation<RegisterCustomerRequestInternal, RegisterCustomerResponseInternal>
            createCustBiz;

        /// <summary>
        ///  ChangeOfHolderBizOp
        /// </summary>
        public ChangeOfHolderBizOp()
        {
            _purchaseHelper = new PurchaseHelper();
        }
        /// <summary>
        /// OperationCode
        /// </summary>
        public override string OperationCode
        {
            get { return OperationCodes.ChangeOfHolder; }
        }

       /// <summary>
        /// OperationDiscriminator
       /// </summary>
        public override string OperationDiscriminator
        {
            get { return OperationDiscriminators.ChangeOfHolder; }
        }

        /// <summary>
        /// PurchaseHelper
        /// </summary>
        public IPurchaseHelper PurchaseHelper
        {
            get { return _purchaseHelper; }
            set { _purchaseHelper = value; }
        }

        /// <summary>
        /// MapNotAutomappedOrderInboundProperties
        /// </summary>
        /// <param name="request"></param>
        /// <param name="coreInput"></param>
        protected override void MapNotAutomappedOrderInboundProperties(ChangeOfHolderRequestDTO request,
            ref ChangeOfHolderRequestInternal coreInput)
        {
            #region get old infoes
      
           var getSubScriberByMsisdnMS =  MicroServiceManager.GetMicroService<GetLastSubscriptionByMsisdnRequest, GetLastSubscriptionByMsisdnResponse>();
            var getSubScriberResp = getSubScriberByMsisdnMS.Process(new GetLastSubscriptionByMsisdnRequest()
            {
                Status = new []{(int)ResourceStatus.Deleted,(int)ResourceStatus.Expired},
                Msisdn = request.MSISDN,
                Channel = coreInput.Channel,
                MVNO = coreInput.MVNO,
                User = coreInput.User
            }, null);
            if(getSubScriberResp.ResultType!=ResultTypes.Ok)
                throw new BusinessLogicErrorException("there's no such subscriber", BizOpsErrors.SubscriptionNotFound);


            var oldResoureMb = getSubScriberResp.ResourceMBInfo.FirstOrDefault();
            var oldCustomer = oldResoureMb.CustomerInfo;
            #endregion

            #region validate

            if (oldCustomer.PropertyInfo.First().IDNumber != request.DocumentNumber)
            {
                throw new BusinessLogicErrorException(
                    string.Format("the requesting DocumentNumbmer#{0} doesn't match with the MSISDN#{1}",
                        request.DocumentNumber, request.MSISDN), BizOpsErrors.DocumentNumbmerDoesnotMatchWithTheMSISDN); 
            }

            if (oldResoureMb.StatusID != (int)ResourceStatus.Active)
            {
                throw new BusinessLogicErrorException(
                    string.Format("the old holder who owns this msisdn#{0} isn't active!", request.MSISDN),
                    BizOpsErrors.CustomerDoesNotHaveActiveResource);
            }

            if (oldResoureMb.OperatorInfo.DealerID !=coreInput.MVNO.DealerID)
            {
                throw new BusinessLogicErrorException(
                    string.Format("the msisdn doen't belong to the current vmno#{0}!", request.vmno),
                    BizOpsErrors.MVNODontHavePermision);
            }

            var getPropertyInfosByDocumentIdMS = MicroServiceManager
                .GetMicroService<GetPropertyInfosByDocumentIdRequest, GetPropertyInfosByDocumentIdResponse>();
            var getPropertyInfosByDocumentIdResp = getPropertyInfosByDocumentIdMS.Process(new GetPropertyInfosByDocumentIdRequest()
            {
                DocumentType = request.CustomerData.CustomerData.DocumentType,
                DocumentId = request.CustomerData.CustomerData.DocumentNumber,
                Channel = coreInput.Channel,
                MVNO = coreInput.MVNO,
                User = coreInput.User
            }, null);

            if (getPropertyInfosByDocumentIdResp.PropertyInfos.Any())
            {
                foreach (var propertyInfo in getPropertyInfosByDocumentIdResp.PropertyInfos)
                {
                    if (propertyInfo.CustomerInfo.DealerID == coreInput.MVNO.DealerID)
                    {
                        var resource = propertyInfo.CustomerInfo.ResourceMBInfo.FirstOrDefault(
                          rs => rs.StartDate < DateTime.Now && (!rs.EndDate.HasValue || rs.EndDate > DateTime.Now));
                        if (resource != null && resource.StatusID == (int)ResourceStatus.Frozen)
                        {
                            throw new BusinessLogicErrorException(
                                string.Format(
                                    "the new documentID#{0} has been binded with another msisdn which is frozen status!",
                                    request.CustomerData.CustomerData.DocumentNumber), BizOpsErrors.ResourceStatusIsFrozen);
                        }
                    }
                }
            }

            #endregion

            #region get numberInfo

            var getNumberMS =MicroServiceManager.GetMicroService<GetNumberByResourceRequest, GetNumberByResourceResponse>();
            var getNumberResp = getNumberMS.Process(new GetNumberByResourceRequest()
            {
                Resource = oldResoureMb.Resource,
                Channel = coreInput.Channel,
                MVNO = coreInput.MVNO,
                User = coreInput.User
            }, null);

            #endregion

            #region get SIMcardInfo

            var getICCardMS = MicroServiceManager.GetMicroService<GetSimCardByICCIdRequest, GetSimCardByICCIdResponse>();
            var getICCardResp = getICCardMS.Process(new GetSimCardByICCIdRequest()
            {
                IccId = oldResoureMb.ICC,
                Channel = coreInput.Channel,
                MVNO = coreInput.MVNO,
                User = coreInput.User
            }, null);

            #endregion

            #region Get ProvisioningTemplateName

            var getProvitionTemplateMS = MicroServiceManager.GetMicroService<GetProvisioningTemplateByIdRequest, GetProvisioningTemplateByIdResponse>();
            var getProvitionTemplateResp = getProvitionTemplateMS.Process(new GetProvisioningTemplateByIdRequest()
            {
                ProvisionTemplateId = oldResoureMb.ProvisionId ?? 0,
                Channel = coreInput.Channel,
                MVNO = coreInput.MVNO,
                User = coreInput.User
            }, null);
            #endregion

            #region set coreInput

       

            #region set old holder

            #region Get CustomerDataRoamingLimits By CustomerID

            var getCustomerDataRoamingLimitsByCustomerIDMS =
                MicroServiceManager
                    .GetMicroService
                    <GetCustomerDataRoamingLimitsByCustomerIDRequest, GetCustomerDataRoamingLimitsByCustomerIDResponse>();
            if (Log.IsDebugEnabled)
                Log.DebugFormat("Set GetCustomerDataRoamingLimitsByCustomerIDRequest with CustomerID ({0}) specified.",
                    oldCustomer.CustomerID.Value);
            var getCustomerDataRoamingLimitsByCustomerIDRequest = new GetCustomerDataRoamingLimitsByCustomerIDRequest()
            {
                CustomerID = oldCustomer.CustomerID.Value,
            };
            var getCustomerDataRoamingLimitsByCustomerIDResponse =
                getCustomerDataRoamingLimitsByCustomerIDMS.Process(getCustomerDataRoamingLimitsByCustomerIDRequest, null);

            #endregion

            #region Get customerDataRoamingLimitNotification by CustomerId

            var getCustomerDataRoamingLimitNotificationByCustomerIdMS =
                MicroServiceManager
                    .GetMicroService
                    <GetCustomerDataRoamingLimitNotificationByCustomerIdRequest,
                        GetCustomerDataRoamingLimitNotificationByCustomerIdResponse>();
            if (Log.IsDebugEnabled)
                Log.InfoFormat(
                    "Set GetCustomerDataRoamingLimitNotificationByCustomerIdRequest with CustomerID ({0}) specified.",
                    oldCustomer.CustomerID.Value);
            var getCustomerDataRoamingLimitNotificationByCustomerIdRequest = new GetCustomerDataRoamingLimitNotificationByCustomerIdRequest
                ()
            {
                CustomerId = oldCustomer.CustomerID.Value,
            };
            var getCustomerDataRoamingLimitNotificationByCustomerIdResponse =
                getCustomerDataRoamingLimitNotificationByCustomerIdMS.Process(
                    getCustomerDataRoamingLimitNotificationByCustomerIdRequest, null);

            #endregion

            #region Get roamingblackList by CustomerId

            var getRoamingBlackListByCustomerIDMS =
                MicroServiceManager
                    .GetMicroService<GetRoamingBlackListByCustomerIDRequest, GetRoamingBlackListByCustomerIDResponse>();
            if (Log.IsDebugEnabled)
                Log.DebugFormat("Set GetRoamingBlackListByCustomerIDRequest with CustomerID ({0}) specified.",
                    oldCustomer.CustomerID.Value);
            var getRoamingBlackListByCustomerIDRequest = new GetRoamingBlackListByCustomerIDRequest()
            {
                CustomerId = oldCustomer.CustomerID.Value,
            };
            var getRoamingBlackListByCustomerIDResponse =
                getRoamingBlackListByCustomerIDMS.Process(getRoamingBlackListByCustomerIDRequest, null);

            #endregion

            #region Get ActiveCustomerAccount
            var getActiveCustomerAccountAssociationByDateMS = MicroServiceManager.GetMicroService<GetActiveCustomerAccountAssociationByDateRequest, GetActiveCustomerAccountAssociationByDateResponse>();
            var getActiveCustomerAccountAssociationByDateRequest =
                new GetActiveCustomerAccountAssociationByDateRequest()
                {
                    ActiveCustomerAccountAssociationDate = DateTime.Now,
                    CustomerInfo = oldCustomer,
                };
            var getActiveCustomerAccountAssociationByDateResponse = getActiveCustomerAccountAssociationByDateMS.Process(getActiveCustomerAccountAssociationByDateRequest, null);

            if (getActiveCustomerAccountAssociationByDateResponse.ResultType != ResultTypes.Ok || getActiveCustomerAccountAssociationByDateResponse.CustomerAccountAssociation == null)
            {
                throw new BusinessLogicErrorException(
                    string.Format("Cannot get Customer Last Account for Customer ID {0}", oldCustomer.CustomerID.Value), BizOpsErrors.CustomerAccountAssociationNotFound);
            }
            #endregion

            #region Get next BillCycle Date
            var calculateNextBillRunDateMS = MicroServiceManager.GetMicroService<CalculateNextBillRunDateForBillCycleRequest, CalculateNextBillRunDateForBillCycleResponse>();
            if (Log.IsDebugEnabled)
                Log.DebugFormat(
                    "Set CalculateNextBillRunDateForBillCycleRequest with BillCycle id ({0}) , purchaseTime ({1}) and firstDayOfWeek({2}).",
                    getActiveCustomerAccountAssociationByDateResponse.CustomerAccountAssociation.Account.BillingCycle.Id,
                    DateTime.Now,
                    CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek);
            var calculateNextBillRunDateForBillCycleResponse = calculateNextBillRunDateMS.Process(new CalculateNextBillRunDateForBillCycleRequest()
            {
                BillCycleDefinition = getActiveCustomerAccountAssociationByDateResponse.CustomerAccountAssociation.Account.BillingCycle,
                PurchaseTime = DateTime.Now,
                FirstDayOfWeek = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek,
            }, null);
            if (calculateNextBillRunDateForBillCycleResponse.ResultType != ResultTypes.Ok)
                throw new BusinessLogicErrorException("CalculateNextBillRunDateForBillCycle: Failed", BizOpsErrors.CalculateNextBillrunDateError);
            #endregion
            coreInput.oldHolder = new HolderToCancel()
            {
                NeedRecycle = false,
                CustomerInfo = oldCustomer,
                Subscription = oldResoureMb,
                RoamingBlackListInfos = getRoamingBlackListByCustomerIDResponse.RoamingBlackListInfos,
                CustomerDataRoamingLimitNotifications = getCustomerDataRoamingLimitNotificationByCustomerIdResponse.CustomerDataRoamingLimitNotifications,
                CustomerDataRoamingLimits = getCustomerDataRoamingLimitsByCustomerIDResponse.CustomerDataRoamingLimits,
                CustomerAccountAssociation = getActiveCustomerAccountAssociationByDateResponse.CustomerAccountAssociation,
                NextBillRunDate = calculateNextBillRunDateForBillCycleResponse.NextBillRun
            };

            #endregion

            #region set new holder
            coreInput.newHolder = new HolderToCreate();
            Log.Info("Converting the CustomerDTO to CustomerInfo");
            CustomerInfo customerInfo = request.CustomerData.ToCore();
            coreInput.newHolder.ProvisionInfoDefinition = getProvitionTemplateResp.CrmDefaultProvision;
            coreInput.newHolder.PurchasedProducts = _purchaseHelper.GetProductsAndChargesOptions(request.PurchasedProducts.ToList());
          
            coreInput.newHolder.CustomerInfo = customerInfo;
            coreInput.newHolder.IsContain4GProduct = false;

            #endregion

         

            #region  Set CoreInput
            coreInput.NumberInPool = getNumberResp.NumberInfo;
            coreInput.SimCard = getICCardResp.SimCardInfo;
           
            #region set LeftoverDataAmount

            var getRmPromotionGroupMemberMS = MicroServiceManager
                .GetMicroService<GetRmPromotionGroupMemberAllRequest, GetRmPromotionGroupMemberAllResponse>();
            var getRmPromotionGroupMemberResp = getRmPromotionGroupMemberMS.Process(new GetRmPromotionGroupMemberAllRequest()
            {
                Channel = coreInput.Channel,
                MVNO = coreInput.MVNO,
                User = coreInput.User
            }, null);

            var config = GetOperationConfigForDealer<ChangeOfHolderConfiguration>(coreInput.MVNO);

            coreInput.recurringPromotions = getRmPromotionGroupMemberResp.RmPromotionGroupMembers.Where(
                gm => gm.PromotionGroup.PromotionGroupID == config.PromotionGroupID)
                .Select(gm => gm.PromotionPlan)
                .ToList();

            var promotionGroupMembers = coreInput.recurringPromotions.Select(pp => pp.PromotionPlanId);
            var customerPromotions = oldCustomer.Promotions.Where(cp => cp.StartDate < DateTime.Now &&
                (!cp.EndDate.HasValue || cp.EndDate > DateTime.Now) && cp.Active
                && promotionGroupMembers.Contains(cp.PromotionDetail.RmPromotionPlanInfo.PromotionPlanId)).ToList();
            if (customerPromotions.Any())
            {
                coreInput.LeftoverDataAmount +=customerPromotions.Sum(cp => cp.CurrentLimit);
            }

            #endregion
            #endregion

            #endregion

        }

        /// <summary>
        /// MapNotAutomappedOrderOutboundProperties
        /// </summary>
        /// <param name="source"></param>
        /// <param name="DTOOutput"></param>
        protected override void MapNotAutomappedOrderOutboundProperties(ChangeOfHolderResponseInternal source, ref ChangeOfHolderResponseDTO DTOOutput)
        {
            DTOOutput.Customer = source.Customer.ToDto();
            DTOOutput.BenefitTransfered = source.BenefitTransferAmount;
        }
        /// <summary>
        /// ProcessRequest
        /// </summary>
        /// <param name="order"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public override ChangeOfHolderResponseInternal ProcessRequest(ChangeOfHolderOrder order, ChangeOfHolderRequestInternal request)
        {

            #region   Cancel the old customer
            if (cancelCustBiz==null)
             cancelCustBiz = BusinessOperationManager
                .GetCoreBusinessOperation<CancelCustomerAndSubscriptionsRequestInternal, CancelCustomerAndSubscriptionsResponseInternal>((int)request.MVNO.DealerID);

            var cancelCustomerAndSubscriptionsRequestInternal = new CancelCustomerAndSubscriptionsRequestInternal()
            {
                CustomerInfo = request.oldHolder.CustomerInfo,
                CustomerAccountAssociation = request.oldHolder.CustomerAccountAssociation,
                CustomerDataRoamingLimitNotifications = request.oldHolder.CustomerDataRoamingLimitNotifications,
                CustomerDataRoamingLimits = request.oldHolder.CustomerDataRoamingLimits,
                RoamingBlackListInfos = request.oldHolder.RoamingBlackListInfos,
                MSISDN = request.NumberInPool.Resource,
                SimCardInfo = request.SimCard,
                NeedRecycle = request.oldHolder.NeedRecycle,
                NextBillRunDate = request.oldHolder.NextBillRunDate,
                NumberInPool = request.NumberInPool,
                Subscription = request.oldHolder.Subscription,
                Channel = request.Channel,
                User = request.User,
                ExternalReference = request.ExternalReference,
                MVNO = request.MVNO
            };
            var cancelCustomerResp = cancelCustBiz.Process(cancelCustomerAndSubscriptionsRequestInternal, null);
            if (cancelCustomerResp.ResultType != ResultTypes.Ok)
                throw new BusinessLogicErrorException(string.Format("Cancel the old holder for the MSISDN#{0} is failed", request.NumberInPool.Resource), 1);
            #endregion

            #region initialize the SIM card 
            var initSimCardMS = MicroServiceManager.GetMicroService<InitSimCardRequest, InitSimCardResponse>();
            var initSimCardRequest = new InitSimCardRequest()
            {
                SimCardInfo = request.SimCard,
                MVNO = request.MVNO,
                User = request.User,
                Channel = request.Channel
            };
            initSimCardMS.Process(initSimCardRequest, null);
            #endregion

            #region Create new customer
            if(createCustBiz==null)
             createCustBiz = BusinessOperationManager
               .GetCoreBusinessOperation<RegisterCustomerRequestInternal, RegisterCustomerResponseInternal>((int)request.MVNO.DealerID);

            var registerCustomerRequestInternal = new RegisterCustomerRequestInternal()
            {
                CustomerInfoDefinition = request.newHolder.CustomerInfo,
                NumberInPool = request.NumberInPool,
                SimCard = request.SimCard,
                BillCycleForCustomer = request.newHolder.BillCycleForCustomer,
                CreditLimit = null,
                IsContain4GProduct = request.newHolder.IsContain4GProduct,
                ProvisionInfoDefinition = request.newHolder.ProvisionInfoDefinition,
                PurchasedProducts = request.newHolder.PurchasedProducts,
                Channel = request.Channel,
                User = request.User,
                ExternalReference = request.ExternalReference,
                MVNO = request.MVNO
            };
            var createCustResp = createCustBiz.Process(registerCustomerRequestInternal, null);

            if (createCustResp.ResultType != ResultTypes.Ok)
                throw new BusinessLogicErrorException(string.Format("Register the new holder for the MSISDN#{0} is failed", request.NumberInPool.Resource), 1);
        
            #endregion

            #region Transfer all benefits from old customer to new customer

            decimal transferAmount = 0.0M;

            var config = GetOperationConfigForDealer<ChangeOfHolderConfiguration>(request.MVNO);

            var newCustomer =createCustResp.Customer;
            var currentAccumulativePromotion = newCustomer.Promotions.FirstOrDefault(p =>
                p.PromotionDetail.RmPromotionPlanInfo.Accumulative &&
                p.StartDate < DateTime.Now && (!p.EndDate.HasValue || p.EndDate > DateTime.Now)
                && request.recurringPromotions.Contains(p.PromotionDetail.RmPromotionPlanInfo));
            if (currentAccumulativePromotion != null)
            {
                #region calculate the benefit can be transfered
             
                var currentTotalPromotionLimit =
                   newCustomer.Promotions.Where(
                        cp => request.recurringPromotions.Contains(cp.PromotionDetail.RmPromotionPlanInfo))
                        .Sum(cp => cp.CurrentLimit);

                if (config.MaxAmountTransfer <= currentTotalPromotionLimit)
                    transferAmount = 0.0M;
                else
                {
                    transferAmount = (config.MaxAmountTransfer - currentTotalPromotionLimit) >=request.LeftoverDataAmount
                        ? request.LeftoverDataAmount
                        : (config.MaxAmountTransfer -currentTotalPromotionLimit);
                }


                #endregion

                #region reset customer's promotion
                currentAccumulativePromotion.CurrentLimit += transferAmount;
                var updateCustomerPromotionMS = MicroServiceManager
                    .GetMicroService<UpdateCustomersPromotionRequest, UpdateCustomersPromotionResponse>();
                var updateCustomerPromotionReq = new UpdateCustomersPromotionRequest()
                {
                    CrmCustomersPromotionInfo = currentAccumulativePromotion,
                    Channel = request.Channel,
                    MVNO = request.MVNO,
                    User = request.User
                };
                var updateupdateCustomerPromotionResp = updateCustomerPromotionMS.Process(updateCustomerPromotionReq, null);
                #endregion

                #region write promotion change log

                if (updateupdateCustomerPromotionResp.ResultType == ResultTypes.Ok)
                {
                    var getCustomerPromotionOperationLogByCustomerIDAndPromotionMS = MicroServiceManager.GetMicroService<GetCustomerPromotionOperationLogByCustomerIDAndPromotionRequest, GetCustomerPromotionOperationLogByCustomerIDAndPromotionResponse>();
                    var createCustomerPromotionLogInfoMS = MicroServiceManager.GetMicroService<CreateCustomerPromotionLogInfoRequest, CreateCustomerPromotionLogInfoResponse>();

                    var getCustomerPromotionOperationLogByCustomerIdAndPromotionRequest = new GetCustomerPromotionOperationLogByCustomerIDAndPromotionRequest
                        ()
                    {
                        CustomerID =newCustomer.CustomerID.Value,
                        PromotionIDList = new List<int>()
                        {
                            currentAccumulativePromotion.PromotionDetail.PromotionPlanDetailId,
                        },
                    };
                    var getCustomerPromotionOperationLogByCustomerIdAndPromotionResponse =
                        getCustomerPromotionOperationLogByCustomerIDAndPromotionMS.Process(
                            getCustomerPromotionOperationLogByCustomerIdAndPromotionRequest, null);

                    var promotionlog = new CrmCustomersPromotionOperationLogInfo()
                    {
                        BAMOUNT =  currentAccumulativePromotion.CurrentLimit,
                        BNEXTRENEWDATE = currentAccumulativePromotion.NextRenewDate,
                        BSTARTDATE = currentAccumulativePromotion.StartDate,
                        BENDDATE =  currentAccumulativePromotion.EndDate,
                        BPRIORITY =  currentAccumulativePromotion.Priority,
                        OPERATIONDATE = DateTime.Now,
                        OPCODE = 2,
                        DEALERID = newCustomer.DealerID ?? 0,
                        MSISDN = request.NumberInPool.Resource,
                        CUSTOMERID =newCustomer.CustomerID.Value,
                        PROMOTIONPLANID = currentAccumulativePromotion.PromotionDetail.RmPromotionPlanInfo.PromotionPlanId,
                        PROMOTIONPLANDETAILID = currentAccumulativePromotion.PromotionDetail.PromotionPlanDetailId,
                        STATUS = OperationStatus.CO.ToString(),
                        DESCRIPTION = string.Format("transfer benefit from Customer#{0}", newCustomer.CustomerID ?? 0)
                    };

                    if (getCustomerPromotionOperationLogByCustomerIdAndPromotionResponse.LogInfo != null)
                    {
                        promotionlog.PRELOGID =getCustomerPromotionOperationLogByCustomerIdAndPromotionResponse.LogInfo.LOGID;
                        promotionlog.AAMOUNT = currentAccumulativePromotion.CurrentLimit - transferAmount;
                        promotionlog.APRIORITY =getCustomerPromotionOperationLogByCustomerIdAndPromotionResponse.LogInfo.BPRIORITY;
                        promotionlog.ASTARTDATE =getCustomerPromotionOperationLogByCustomerIdAndPromotionResponse.LogInfo.BSTARTDATE;
                        promotionlog.AENDDATE =getCustomerPromotionOperationLogByCustomerIdAndPromotionResponse.LogInfo.BENDDATE;
                        promotionlog.ANEXTRENEWDATE =getCustomerPromotionOperationLogByCustomerIdAndPromotionResponse.LogInfo.BNEXTRENEWDATE;
                    }
                    var createCustomerPromotionLogInfoRequest = new CreateCustomerPromotionLogInfoRequest()
                    {
                        CustomerPromotionOperationLog = promotionlog,
                    };

                    createCustomerPromotionLogInfoMS.Process(createCustomerPromotionLogInfoRequest, null);
                }

                #endregion
            }
            #endregion

            return new ChangeOfHolderResponseInternal()
            {
                BenefitTransferAmount = transferAmount,
                Customer = newCustomer,
                ResultType = ResultTypes.Ok
            };
        }


      

    }
}
