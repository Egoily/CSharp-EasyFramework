using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using com.etak.core.customer.message.CancelCustomerChargeSchedule;
using com.etak.core.customer.message.GetActiveCustomerAccountAssociationByDate;
using com.etak.core.customer.message.GetChargeSchedulesByCustomer;
using com.etak.core.customer.message.GetCustomerChargesByCustomerId;
using com.etak.core.GSMSubscription.messages.CancelCustomerProductAssignment;
using com.etak.core.GSMSubscription.messages.GetCustomerProductAssignmentById;
using com.etak.core.model;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;
using com.etak.core.operation.contract;
using com.etak.core.operation.contract.exceptions;
using com.etak.core.operation.implementation;
using com.etak.core.operation.manager;
using com.etak.core.product.message.CalculateNextBillRunDateForBillCycle;
using com.etak.core.product.message.CancelCustomerPackages;
using com.etak.core.promotion.messages.CancelCustomersPromotion;
using com.etak.core.promotion.messages.CreateCustomerPromotionLogInfo;
using com.etak.core.promotion.messages.CreateLogPromotion;
using com.etak.core.promotion.messages.GetCustomerPromotionOperationLogByCustomerIDAndPromotion;
using com.etak.core.service.messages.CancelServicesInfo;
using log4net;
using NHibernate;


namespace com.etak.core.bizops.fullfilment.CancelCustomerProduct
{
    /// <summary>
    /// Cancel product assigned to specifc customer and all promotions, packages, bundles, recuring charges if exists and cancel product related
    /// </summary>
    public class CancelCustomerProductBizOp : AbstractSinglePhaseOrderProcessor<CancelCustomerProductRequestDTO, CancelCustomerProductResponseDTO, CancelCustomerProductRequestInternal, CancelCustomerProductResponseInternal, CancelCustomerProductOrder>
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        //PromotionOperationLog constants
        private const int PromotionOperationLogDeleteOperationCode = 4;
        private const string PromotionOperationLogDeleteDescription = "Deleted Subscriber";


        /// <summary>
        /// CancelCustomerAndSubscriptionsBizOp Operation Discriminator
        /// </summary>
        public override string OperationDiscriminator
        {
            get { return OperationDiscriminators.CancelCustomerProduct; }
        }

        /// <summary>
        /// CancelCustomerProductBizOp Operation code 
        /// </summary>
        public override string OperationCode
        {
            get { return OperationCodes.CancelCustomerProduct; }
        }

        /// <summary>
        /// Cancel customer product
        ///     -Cancel promotions
        ///     -Cancel service by bundle
        ///     -Cancel packages
        ///     -Cancel recurring charges
        ///     -Cancel customer product assignment
        /// </summary>
        /// <param name="order"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public override CancelCustomerProductResponseInternal ProcessRequest(CancelCustomerProductOrder order,
            CancelCustomerProductRequestInternal request)
        {

            if (request.CustomerProductAssignment.PurchasedProduct.Status == ProductStatuses.Disabled)
                throw new BusinessLogicErrorException(
                           string.Format("Can not cancel Product with id ({0}) this product is disabled.", request.CustomerProductAssignment.PurchasedProduct.Id),
                           BizOpsErrors.CustomersPromotionNotFound);

            //Check if a product has recurring charges.
            var hasRecurringCharges = HasRecurringCharges(request.CustomerProductAssignment.ProductChargePurchased);
            var endDateWithHasRecurringChargesChecked = hasRecurringCharges && request.UseNextBillCycleEndDateInRecurring
                ? request.NextBillRunDate
                : request.CancelDate;

            #region Cancel Promotion -> CancelCustomersPromotionMS
            if (request.CustomerProductAssignment.PurchasedProduct.AssociatedPrmotionPlan != null)
            {
                //Get All active promotions related with the product
                var promotionsRelatedWithProduct =
                     request.CustomerProductAssignment.PurchasingCustomer.Promotions.Where(
                         x => x.Active && Equals(x.PromotionDetail.RmPromotionPlanInfo, request.CustomerProductAssignment.PurchasedProduct.AssociatedPrmotionPlan) 
                             && (x.EndDate == null || request.CancelDate <= x.EndDate)
                             && x.StartDate != x.EndDate).ToList();

                var cancelCustomersPromotionMS = MicroServiceManager.GetMicroService<CancelCustomersPromotionRequest, CancelCustomersPromotionResponse>();
                var getCustomerPromotionOperationLogByCustomerIDAndPromotionMS = MicroServiceManager.GetMicroService<GetCustomerPromotionOperationLogByCustomerIDAndPromotionRequest, GetCustomerPromotionOperationLogByCustomerIDAndPromotionResponse>();
                var createCustomerPromotionLogInfoMS = MicroServiceManager.GetMicroService<CreateCustomerPromotionLogInfoRequest, CreateCustomerPromotionLogInfoResponse>();


                if (promotionsRelatedWithProduct.Any())
                {
                    // Check if we need to cancel all promotionsRelated to sameproduct
                    if (request.CancelCustomerPromotionsWithSamePromotionPlanDetail)
                    {
                        foreach (var crmCustomersPromotionInfo in promotionsRelatedWithProduct)
                        {
                            CancelCustomerPromotionMsAndLog(request, crmCustomersPromotionInfo, endDateWithHasRecurringChargesChecked, cancelCustomersPromotionMS, getCustomerPromotionOperationLogByCustomerIDAndPromotionMS, createCustomerPromotionLogInfoMS);
                        }
                    }
                    else
                    {
                        //If we get more than one promotion, we will get the closest promotion from date product. 
                        var customerPromotion = promotionsRelatedWithProduct.OrderByDescending(
                            x =>Math.Abs(
                            request.CustomerProductAssignment.PurchasedProduct.AssociatedPrmotionPlan
                                .StartDate.ToDateTime().Ticks - x.StartDate.ToDateTime().Ticks))
                            .FirstOrDefault();

                        CancelCustomerPromotionMsAndLog(request, customerPromotion, endDateWithHasRecurringChargesChecked, cancelCustomersPromotionMS, getCustomerPromotionOperationLogByCustomerIDAndPromotionMS, createCustomerPromotionLogInfoMS);
                    }
                }
            }
            #endregion

            #region Cancel ServiceInfo by Bundle id -> CancelServiceMs

            if (request.CustomerProductAssignment.PurchasedProduct.AssociatedBundle != null)
            {
                //Cancel ServiceInfoMs
                var cancelServiceInfoMS = MicroServiceManager.GetMicroService<CancelServicesInfoRequest, CancelServicesInfoResponse>();

                //Get all active ServiceInfo with associated bundeId
                foreach (ServicesInfo service in request.CustomerProductAssignment.PurchasingCustomer.ServicesInfo.Where(x => x.BundleDefinition.BundleID == request.CustomerProductAssignment.PurchasedProduct.AssociatedBundle.BundleID
                    && ((x.EndDate == null && request.CancelDate >= x.StartDate)
                        || (request.CancelDate >= x.StartDate && request.CancelDate <= x.EndDate))))
                {
                    var cancelServicesInfoRequest = new CancelServicesInfoRequest()
                    {
                        EndDate = request.CancelDate,
                        ServicesInfo = service,

                    };

                    var cancelServicesInfoResponse = cancelServiceInfoMS.Process(cancelServicesInfoRequest, null);
                    if (cancelServicesInfoResponse.ResultType != ResultTypes.Ok)
                    {
                        throw new BusinessLogicErrorException(
                             string.Format("Cannot cancel Service ({0}) for customer Id ({1}) and product id ({2})", service.ServiceID, request.CustomerProductAssignment.PurchasingCustomer.CustomerID.Value, request.CustomerProductAssignment.PurchasedProduct.Id),
                             BizOpsErrors.CanNotCancelService);
                    }

                }

            }

            #endregion

            #region Cancel packages -> CancelCustomerPackagesMS

            if (request.CustomerProductAssignment.PurchasedProduct.AssociatedPackage != null)
            {
                //Cancel CustomerPackagesMS
                var cancelCustomerPackagesMS = MicroServiceManager.GetMicroService<CancelCustomerPackagesRequest, CancelCustomerPackagesResponse>();
                var cancelCustomerPackagesRequest = new CancelCustomerPackagesRequest()
                {
                    Customer = request.CustomerProductAssignment.PurchasingCustomer,
                    EndDate = request.CancelDate,
                    PackageDefinition = request.CustomerProductAssignment.PurchasedProduct.AssociatedPackage,
                };

                var cancelCustomerPackagesResponse = cancelCustomerPackagesMS.Process(cancelCustomerPackagesRequest,
                    null);

                if (cancelCustomerPackagesResponse.ResultType != ResultTypes.Ok)
                {
                    throw new BusinessLogicErrorException(
                         string.Format("Cannot cancel Customer packages ({0}) for customer Id ({1}) and product id ({2})", request.CustomerProductAssignment.PurchasedProduct.AssociatedPackage.PackageID, request.CustomerProductAssignment.PurchasingCustomer.CustomerID.Value, request.CustomerProductAssignment.PurchasedProduct.Id),
                         BizOpsErrors.CanNotCancelPackage);
                }

            }

            #endregion

            #region Cancel customerChargeSchedules (Recurring Charges)  -> CancelCustomerProductAssignmentMS

            if (hasRecurringCharges)
            {
                //GetCustomerChargesByCustomerIdMS
                var getCustomerChargesByCustomerIdMS = MicroServiceManager.GetMicroService<GetCustomerChargesByCustomerIdRequest, GetCustomerChargesByCustomerIdResponse>();
                var getCustomerChargesByCustomerIdRequest = new GetCustomerChargesByCustomerIdRequest()
                {
                    CustomerId = request.CustomerProductAssignment.PurchasingCustomer.CustomerID.Value,
                };
                var getCustomerChargesByCustomerIdResponse = getCustomerChargesByCustomerIdMS.Process(getCustomerChargesByCustomerIdRequest, null);

                //Get all CustomerChargeSchedules from CustomerCharges
                var customerChargeSchedules = new List<CustomerChargeSchedule>();
                foreach (var customerCharge in getCustomerChargesByCustomerIdResponse.CustomerCharges)
                {
                    if (customerCharge.Schedule != null)
                        customerChargeSchedules.Add(customerCharge.Schedule);
                }

                //Get anothers CustomerChargeSchedules, beside CustomerChargeSchedules which is from CustomerCharges
                var getCustomerChargeScheduleByCustomerMS =
                    MicroServiceManager
                        .GetMicroService<GetCustomerChargeScheduleByCustomerRequest, GetCustomerChargeScheduleByCustomerResponse>();
                var getCustomerChargeScheduleByCustomerRequest = new GetCustomerChargeScheduleByCustomerRequest()
                {
                    Customer = request.CustomerProductAssignment.PurchasingCustomer,
                };
                var getCustomerChargeScheduleByCustomerResponse =
                    getCustomerChargeScheduleByCustomerMS.Process(getCustomerChargeScheduleByCustomerRequest, null);

                foreach (var custChargeSchedule in getCustomerChargeScheduleByCustomerResponse.CustomerChargeSchedule)
                {
                    if (custChargeSchedule != null)
                        customerChargeSchedules.Add(custChargeSchedule);
                }

                if (getCustomerChargesByCustomerIdResponse.ResultType == ResultTypes.Ok && getCustomerChargeScheduleByCustomerResponse.ResultType == ResultTypes.Ok && customerChargeSchedules.Any())
                {

                    foreach (
                        var chargeRecurring in
                            request.CustomerProductAssignment.ProductChargePurchased.Charges.Where(
                                x => x is ChargeRecurring|| NHibernateUtil.GetClass(x) == typeof(ChargeRecurring)))
                    {

                        var validCustomerChargeSchedules = customerChargeSchedules.Where(x => x.Status != ScheduleChargeStatus.Disabled && x.ChargeDefinition.Id == chargeRecurring.Id);

                        if (validCustomerChargeSchedules.Any())
                        {
                            //CancelCustomerChargeScheduleMs
                            var cancelCustomerChargeScheduleMs =
                                MicroServiceManager
                                    .GetMicroService
                                    <CancelCustomerChargeScheduleRequest, CancelCustomerChargeScheduleResponse>();
                            foreach (var customerChargeSchedule in validCustomerChargeSchedules)
                            {
                                var cancelCustomerChargeScheduleRequest = new CancelCustomerChargeScheduleRequest()
                                {
                                    CustomerChargeSchedule = customerChargeSchedule,
                                    PurchaseDate = request.CancelDate,
                                };

                                var cancelCustomerChargeScheduleResponse = cancelCustomerChargeScheduleMs.Process(cancelCustomerChargeScheduleRequest, null);

                                if (cancelCustomerChargeScheduleResponse.ResultType != ResultTypes.Ok)
                                {
                                    throw new BusinessLogicErrorException(
                                         string.Format("Cannot cancel Customer Charge Schedule with id  ({0}) for customer Id ({1}) and product id ({2})", cancelCustomerChargeScheduleRequest.CustomerChargeSchedule.Id, request.CustomerProductAssignment.PurchasingCustomer.CustomerID.Value, request.CustomerProductAssignment.PurchasedProduct.Id),
                                         BizOpsErrors.CanNotCancelCustomerChargeSchedule);
                                }
                            }

                        }

                    }

                }

            }

            #endregion

            #region Cancel Customer Product Assignment -> CancelCustomerProductAssignmentMS

            if (!request.CustomerProductAssignment.EndDate.HasValue ||
                request.CustomerProductAssignment.EndDate > endDateWithHasRecurringChargesChecked)
            {
                var cancelCustomerProductAssignmentMs = MicroServiceManager.GetMicroService<CancelCustomerProductAssignmentRequest, CancelCustomerProductAssignmentResponse>();
                var cancelCustomerProductAssignmentRequest = new CancelCustomerProductAssignmentRequest()
                {
                    CustomerProductAssignment = request.CustomerProductAssignment,
                    EndDate = endDateWithHasRecurringChargesChecked,
                };
                var cancelCustomerProductAssignmentResponse = cancelCustomerProductAssignmentMs.Process(cancelCustomerProductAssignmentRequest, null);
                if (cancelCustomerProductAssignmentResponse.ResultType != ResultTypes.Ok)
                {
                    throw new BusinessLogicErrorException(
                         string.Format("Cannot cancel Customer Product Assignement id ({0}) ", request.CustomerProductAssignment.Id),
                         BizOpsErrors.CanNotCancelProduct);
                }
            }

            #endregion

            return new CancelCustomerProductResponseInternal
            {
                ResultType = ResultTypes.Ok,
                Customer = request.CustomerProductAssignment.PurchasingCustomer,
                Subscription = request.CustomerProductAssignment.PurchasingCustomer.ResourceMBInfo.FirstOrDefault(x => x.StatusID == (int)ResourceStatus.Active),
                ProductOffering = request.CustomerProductAssignment.ProductOffering
            };
        }

        private static void CancelCustomerPromotionMsAndLog(CancelCustomerProductRequestInternal request,
            CrmCustomersPromotionInfo customerPromotion, DateTime endDateWithHasRecurringChargesChecked,
            IMicroService<CancelCustomersPromotionRequest, CancelCustomersPromotionResponse> cancelCustomersPromotionMS, IMicroService<GetCustomerPromotionOperationLogByCustomerIDAndPromotionRequest, GetCustomerPromotionOperationLogByCustomerIDAndPromotionResponse> getCustomerPromotionOperationLogByCustomerIDAndPromotionMS,
            IMicroService<CreateCustomerPromotionLogInfoRequest, CreateCustomerPromotionLogInfoResponse> createCustomerPromotionLogInfoMS)
        {
            #region Create CustomerPromotion
            if (Log.IsDebugEnabled)
                Log.DebugFormat(
                    "Set CancelCustomersPromotionRequest with CrmCustomersPromotionInfo.CustomerId ({0}) specified and CrmCustomersPromotionInfo.PromotionId ({1}).",
                    customerPromotion.CustomerId, customerPromotion.PromotionId);
            var cancelCustomersPromotionRequest = new CancelCustomersPromotionRequest()
            {
                CrmCustomersPromotionInfo = customerPromotion,
            };

            //set Cancel customers promotions date
            cancelCustomersPromotionRequest.EndDate = customerPromotion.EndDate.HasValue &&
                                                      customerPromotion.EndDate <
                                                      endDateWithHasRecurringChargesChecked
                ? (DateTime) customerPromotion.EndDate
                : endDateWithHasRecurringChargesChecked;

            var cancelCustomersPromotionResponse =
                cancelCustomersPromotionMS.Process(cancelCustomersPromotionRequest, null);

            #endregion

            #region Create PromotionOperationLog

            if (cancelCustomersPromotionResponse.ResultType == ResultTypes.Ok &&
                cancelCustomersPromotionResponse.CrmCustomersPromotionInfo != null)
            {
                var cancelledCrmCustomersPromotionInfo =
                    cancelCustomersPromotionResponse.CrmCustomersPromotionInfo;
                //get customerPromotionLog by Customerid and Promotion if exists
                if (Log.IsDebugEnabled)
                    Log.DebugFormat(
                        "Set GetCustomerPromotionOperationLogByCustomerIDAndPromotionRequest with CustomerID ({0}) specified and PromotionDetail.PromotionPlanDetailId ({1}).",
                        cancelledCrmCustomersPromotionInfo.CustomerId,
                        cancelledCrmCustomersPromotionInfo.PromotionDetail.PromotionPlanDetailId);

                var getCustomerPromotionOperationLogByCustomerIdAndPromotionRequest = new GetCustomerPromotionOperationLogByCustomerIDAndPromotionRequest
                    ()
                {
                    CustomerID = cancelledCrmCustomersPromotionInfo.CustomerId,
                    PromotionIDList = new List<int>()
                    {
                        cancelledCrmCustomersPromotionInfo.PromotionDetail.PromotionPlanDetailId,
                    },
                };
                var getCustomerPromotionOperationLogByCustomerIdAndPromotionResponse =
                    getCustomerPromotionOperationLogByCustomerIDAndPromotionMS.Process(
                        getCustomerPromotionOperationLogByCustomerIdAndPromotionRequest, null);

                var promotionlog = new CrmCustomersPromotionOperationLogInfo()
                {
                    OPERATIONDATE = DateTime.Now,
                    OPCODE = PromotionOperationLogDeleteOperationCode,
                    DEALERID =
                        cancelledCrmCustomersPromotionInfo.PromotionDetail.RmPromotionPlanInfo.DealerId,
                    //TODO MSISDN = msisdn,
                    CUSTOMERID = cancelledCrmCustomersPromotionInfo.Customer.CustomerID.Value,
                    PROMOTIONPLANID =
                        cancelledCrmCustomersPromotionInfo.PromotionDetail.RmPromotionPlanInfo
                            .PromotionPlanId,
                    PROMOTIONPLANDETAILID =
                        cancelledCrmCustomersPromotionInfo.PromotionDetail.PromotionPlanDetailId,
                    STATUS = OperationStatus.CO.ToString(),
                    DESCRIPTION = PromotionOperationLogDeleteDescription,
                };

                if (getCustomerPromotionOperationLogByCustomerIdAndPromotionResponse.LogInfo != null)
                {
                    promotionlog.PRELOGID =
                        getCustomerPromotionOperationLogByCustomerIdAndPromotionResponse.LogInfo.LOGID;
                    promotionlog.AAMOUNT = cancelledCrmCustomersPromotionInfo.CurrentLimit;
                    promotionlog.APRIORITY =
                        getCustomerPromotionOperationLogByCustomerIdAndPromotionResponse.LogInfo.BPRIORITY;
                    promotionlog.ASTARTDATE =
                        getCustomerPromotionOperationLogByCustomerIdAndPromotionResponse.LogInfo.BSTARTDATE;
                    promotionlog.AENDDATE =
                        getCustomerPromotionOperationLogByCustomerIdAndPromotionResponse.LogInfo.BENDDATE;
                    promotionlog.ANEXTRENEWDATE =
                        getCustomerPromotionOperationLogByCustomerIdAndPromotionResponse.LogInfo
                            .BNEXTRENEWDATE;
                }

                //Create customerPromotionLog
                var createCustomerPromotionLogInfoRequest = new CreateCustomerPromotionLogInfoRequest()
                {
                    CustomerPromotionOperationLog = promotionlog,
                };

                createCustomerPromotionLogInfoMS.Process(createCustomerPromotionLogInfoRequest, null);
            }
            else
            {
                throw new BusinessLogicErrorException(
                    "Cannot create Customer Promotion Log. The CustomerPromotion was not informed.",
                    BizOpsErrors.CustomersPromotionNotFound);
            }

            #endregion
        }

        /// <summary>
        /// Check if product charge options has recurring charges.
        /// </summary>
        /// <param name="productChargeOption"></param>
        /// <returns></returns>
        protected static bool HasRecurringCharges(ProductChargeOption productChargeOption)
        {

            return (productChargeOption != null 
                && productChargeOption.Charges != null 
                && productChargeOption.Charges.Any(x => x is ChargeRecurring || NHibernateUtil.GetClass(x) == typeof(ChargeRecurring)));
        }




        /// <summary>
        /// Maps all the inboud properties of the request that are not mapped by the framework
        /// </summary>
        /// <param name="request"></param>
        /// <param name="coreInput"></param>
        protected override void MapNotAutomappedOrderInboundProperties(CancelCustomerProductRequestDTO request,
            ref CancelCustomerProductRequestInternal coreInput)
        {

            //GetCustomerProductAssignmentByIdMS
            var getCustomerProductAssignmentByIdMS = MicroServiceManager.GetMicroService<GetCustomerProductAssignmentByIdRequest, GetCustomerProductAssignmentByIdResponse>();

            if (Log.IsDebugEnabled)
                Log.DebugFormat(
                    "Set GetActiveCustomerAccountAssociationByDateRequest with customerProductAssignmentId ({0}).",
                    request.CustomerProductAssignmentId);
            var getCustomerProductAssignmentByIdResponse = getCustomerProductAssignmentByIdMS.Process(new GetCustomerProductAssignmentByIdRequest()
            {
                Id = request.CustomerProductAssignmentId,
            }, null);
            if (getCustomerProductAssignmentByIdResponse.ResultType != ResultTypes.Ok || getCustomerProductAssignmentByIdResponse.CustomerProductAssignment == null || getCustomerProductAssignmentByIdResponse.CustomerProductAssignment.PurchasedProduct == null)
                throw new BusinessLogicErrorException("GetCustomerProductAssignmentByIdMS: Failed", BizOpsErrors.CustomerAccountNotFound);
            coreInput.CustomerProductAssignment = getCustomerProductAssignmentByIdResponse.CustomerProductAssignment;

            //GetActiveCusotmerAccountMs
            var getActiveCusotmerAccountMs = MicroServiceManager.GetMicroService<GetActiveCustomerAccountAssociationByDateRequest, GetActiveCustomerAccountAssociationByDateResponse>();

            if (Log.IsDebugEnabled)
                Log.DebugFormat(
                    "Set GetActiveCustomerAccountAssociationByDateRequest with customerid ({0}) and  ActiveCustomerAccountAssociationDate ({1}).",
                    coreInput.CustomerProductAssignment.PurchasingCustomer.CustomerID.Value,
                    request.CancelDate);
            var getActiveCustomerAccountAssociationByDateResponse = getActiveCusotmerAccountMs.Process(new GetActiveCustomerAccountAssociationByDateRequest()
            {
                CustomerInfo = coreInput.CustomerProductAssignment.PurchasingCustomer,
                ActiveCustomerAccountAssociationDate = request.CancelDate,
            }, null);
            if (getActiveCustomerAccountAssociationByDateResponse.ResultType != ResultTypes.Ok)
                throw new BusinessLogicErrorException("GetActiveCustomerAccountAssociationByDate: Failed", BizOpsErrors.CustomerAccountNotFound);

            if (getActiveCustomerAccountAssociationByDateResponse.CustomerAccountAssociation == null)
            {
                throw new BusinessLogicErrorException(String.Format("Unable to find an CustomerAccountAssociation for the CustomerID:{0}",
                    coreInput.CustomerProductAssignment.PurchasingCustomer.CustomerID), BizOpsErrors.CustomerAccountAssociationNotFound);
            }
            var currentAccount = getActiveCustomerAccountAssociationByDateResponse.CustomerAccountAssociation.Account;

            //GetCustomerChargesByCustomerIdMS
            var calculateNextBillRunDateMS = MicroServiceManager.GetMicroService<CalculateNextBillRunDateForBillCycleRequest, CalculateNextBillRunDateForBillCycleResponse>();
            if (Log.IsDebugEnabled)
                Log.DebugFormat(
                    "Set CalculateNextBillRunDateForBillCycleRequest with BillCycle id ({0}) , purchaseTime ({1}) and firstDayOfWeek({2}).",
                    currentAccount.BillingCycle.Id,
                    request.CancelDate,
                    CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek);
            var calculateNextBillRunDateForBillCycleResponse = calculateNextBillRunDateMS.Process(new CalculateNextBillRunDateForBillCycleRequest()
            {
                BillCycleDefinition = currentAccount.BillingCycle,
                PurchaseTime = request.CancelDate,
                FirstDayOfWeek = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek,
            }, null);
            if (calculateNextBillRunDateForBillCycleResponse.ResultType != ResultTypes.Ok)
                throw new BusinessLogicErrorException("CalculateNextBillRunDateForBillCycle: Failed", BizOpsErrors.CalculateNextBillrunDateError);

            coreInput.NextBillRunDate = calculateNextBillRunDateForBillCycleResponse.NextBillRun;
            coreInput.CancelDate = request.CancelDate;
            coreInput.UseNextBillCycleEndDateInRecurring = request.UseNextBillCycleEndDateInRecurring;


            if (request.CancelDate < (request.CurrentTime ?? DateTime.Now))
                throw new BusinessLogicErrorException("The requested cancel date can't be earler than now!", BizOpsErrors.InvalidCancelDate);

            var hasRecurringCharges = HasRecurringCharges(coreInput.CustomerProductAssignment.ProductChargePurchased);
            var endDateWithHasRecurringChargesChecked = hasRecurringCharges && request.UseNextBillCycleEndDateInRecurring
                ? coreInput.NextBillRunDate
                : request.CancelDate;
            if (coreInput.CustomerProductAssignment.EndDate.HasValue && coreInput.CustomerProductAssignment.EndDate <= endDateWithHasRecurringChargesChecked)
            {
                throw new BusinessLogicErrorException("This purchased product has been expired.", BizOpsErrors.ProductHasBeenExpired);
            }

        }

        /// <summary>
        /// Maps all the outboud properties of the response that are not mapped by the framework
        /// </summary>
        /// <param name="source"></param>
        /// <param name="DTOOutput"></param>
        protected override void MapNotAutomappedOrderOutboundProperties(CancelCustomerProductResponseInternal source,
            ref CancelCustomerProductResponseDTO DTOOutput)
        {
        }
    }
}
