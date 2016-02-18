using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using com.etak.core.dealer.messages.GetDealerInfoById;
using com.etak.core.dealer.messages.GetDealerInfoMVNOByDealerId;
using com.etak.core.dealer.messages.GetMVNOConfigActionInfosByMVNOIdAndCategoryIdAndItem;
using com.etak.core.model;
using com.etak.core.operation.contract.exceptions;
using com.etak.core.product.message.GetRmPromotionPlanInfosByIds;
using com.etak.core.promotion.messages.CreateCustomersPromotion;
using com.etak.core.promotion.messages.ResetFakePromotionForCustomer;
using com.etak.core.repository;
using com.etak.core.repository.crm.promotion;

namespace com.etak.core.bizops.fullfilment.PurchaseProductForCustomer
{

    /// <summary>
    /// Partial Implementation BizOp PurchaseProductForCustomer
    /// </summary>
    public partial class PurchaseProductForCustomerBizOp
    {

        #region Publics Objects
        
        /// <summary>
        /// Request Parameters, used private methods
        /// </summary>
        public class ApplyCustomerPromotionRequestObject
        {

            /// <summary>
            /// CustomerInfo
            /// </summary>
            public CustomerInfo CustomerInfo { get; set; }
            /// <summary>
            /// EndDate
            /// </summary>
            public DateTime? EndDate { get; set; }

            /// <summary>
            /// //     Factor to change CreditLimit by a proportional value. Default is 1
            /// </summary>
            public decimal? FactorToCreditLimit { get; set; }
            /// <summary>
            /// PromotionPlanIds
            /// </summary>
            public IList<int> PromotionPlanIds { get; set; }
            /// <summary>
            /// ResourceInfo
            /// </summary>
            public ResourceMBInfo ResourceInfo { get; set; }
            /// <summary>
            /// StartDate
            /// </summary>
            public DateTime? StartDate { get; set; }
            /// <summary>
            /// DealerInfo
            /// </summary>
            public DealerInfo DealerInfo { get; set; }
        }

        /// <summary>
        /// Response 
        /// </summary>
        public class ApplyCustomerPromotionResponseObject
        {

            /// <summary>
            /// promotionPlan
            /// </summary>
            public IList<com.etak.core.model.CrmCustomersPromotionInfo> promotionPlan { get; set; }
        }



        #endregion

        #region Private Methods

        /// <summary>
        /// Apply Customer Promotion
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private ApplyCustomerPromotionResponseObject ApplyCustomerPromotion(ApplyCustomerPromotionRequestObject request)
        {
            ApplyCustomerPromotionResponseObject response = new ApplyCustomerPromotionResponseObject();



            #region Init


            DealerInfo dealerInfo = request.DealerInfo;
            ResourceMBInfo resourceMBInfo = request.ResourceInfo;

            
            

            if (request.PromotionPlanIds == null || request.PromotionPlanIds.Count() == 0)
                throw new DataValidationErrorException("No PromotionPlanIds in request were found", BizOpsErrors.NoPromotionPlanIdsFound);

            var plans = _getRmPromotionPlanById.Process(new GetRmPromotionPlanInfosByIdsRequest() { PromotionPlanIds = request.PromotionPlanIds.ToList() }, null).RmPromotionPlanInfos;


            #endregion

            #region verify

            if (request.CustomerInfo == null)
                throw new DataValidationErrorException("ApplyCustomerPromotion must be provided CustomerInfo", BizOpsErrors.ApplyCustomerPromotionMustProvideCustomerInfo);
            if (request.PromotionPlanIds.Count() == 0)
                throw new DataValidationErrorException("ApplyCustomerPromotion must be provided some PromotionPlanId", BizOpsErrors.ApplyCustomerPromotionMustProvidePromotionPlanId);

            foreach (var plan in plans)
            {
                var mvnoInfo = _getDealerInfoByIdMS.Process(new GetDealerInfoByIdRequest() { DealerId = (int)plan.DealerId }, null).DealerInfo;
                    //dealerRep.GetById((int)plan.DealerId);
                if (mvnoInfo.DealerID != mvnoInfo.FiscalUnitID)
                {
                    mvnoInfo = _getDealerInfoByIdMS.Process(new GetDealerInfoByIdRequest() { DealerId = (int)mvnoInfo.FiscalUnitID }, null).DealerInfo;
                    //dealerRep.GetById((int)mvnoInfo.FiscalUnitID);
                }
                var dealerId = dealerInfo.DealerID;
                if (dealerId != dealerInfo.FiscalUnitID)
                    dealerId = _getDealerInfoByIdMS.Process(new GetDealerInfoByIdRequest() { DealerId = dealerInfo.FiscalUnitID.Value }, null).DealerInfo.DealerID;
                        //dealerRep.GetById(dealerInfo.FiscalUnitID.Value).DealerID;
                if (dealerId != mvnoInfo.DealerID)
                    throw new DataValidationErrorException(String.Format((string) "Promotion Plan {0} has not same dealer as Customer", (object) plan.PromotionPlanName), BizOpsErrors.PromotionPlanHasNotSameDealerAsCustomer);
                if (plan.EndDate != null && plan.EndDate.Value < DateTime.Now)
                    throw new DataValidationErrorException("the promotion has expired!", BizOpsErrors.PromotionExpired);
            }

            if (plans == null)
            {
                throw new DataValidationErrorException("No PromotionPlan was found for provided PromotionPlanId", BizOpsErrors.NoPromotionPlanIdsFound);
            }
            if (Enumerable.Count<RmPromotionPlanInfo>(plans) != request.PromotionPlanIds.Count())
            {
                throw new DataValidationErrorException("PromotionPlans were not found for some provided PromotionPlanIds", BizOpsErrors.PromotionPlansNotFoundForSomeProvidedPromotionPlanIds);
            }
            #endregion


            #region create

            var planDetails = Enumerable.SelectMany<RmPromotionPlanInfo, RmPromotionPlanDetailInfo>(plans, ee => ee.RmPromotionPlanDetailInfoList);

            response.promotionPlan = new List<CrmCustomersPromotionInfo>();
            foreach (var planDetail in planDetails)
            {
                CrmCustomersPromotionInfo customerPromotion = new CrmCustomersPromotionInfo();
                customerPromotion.PromotionDetail = planDetail;
                customerPromotion.Customer = request.CustomerInfo;
                customerPromotion.PromotionId = 0;
                customerPromotion.RenewAutomatically = planDetail.RmPromotionPlanInfo.RenewAutomatically;
                customerPromotion.Active = true;
                customerPromotion.FirstUsed = null;
                customerPromotion.RenewalCount = 0;
                customerPromotion.StartDate = System.DateTime.Now;
                customerPromotion.WhiteList = planDetail.WhiteList;
                customerPromotion.Priority = null;
                customerPromotion.ActiveWithoutCredit = null;

                if (request.StartDate.HasValue)
                    customerPromotion.StartDate = request.StartDate;


                customerPromotion.CurrentLimit = (request.FactorToCreditLimit == null ? planDetail.Limit : request.FactorToCreditLimit.Value * planDetail.Limit);
                customerPromotion.Customer = request.CustomerInfo;

                //added by neil at 2015/3/18 if the promotiondail is recurring, we must set these fields: NextRenewalDate, NextPeriodNumber and CurrentCyclenumber
                if (planDetail.Periodicity > 0)
                {
                    customerPromotion.CurrentCycleNumber = 1;
                    customerPromotion.NextRenewDate = CalculateNextRenewDate(customerPromotion, null);
                    customerPromotion.PreRenewalActionsDate = (customerPromotion.NextRenewDate != null)
                        ? customerPromotion.NextRenewDate.Value.AddMinutes(0 - planDetail.PreRenewalActionsMinutesOffset)
                        : (DateTime?)null;
                    if (planDetail.Periodicity > 1)
                        customerPromotion.NextPeriodNumber = 2;
                    else if (planDetail.Periodicity == 1 &&
                        (planDetail.CycleRepeatCount != 0 && planDetail.CycleRepeatCount != 1))
                        customerPromotion.NextPeriodNumber = 1;
                    else
                        customerPromotion.NextPeriodNumber = null;

                    if (customerPromotion.NextRenewDate == null)
                    {
                        DateTime nextMouth = DateTime.Today.AddMonths(1);
                        customerPromotion.EndDate = new DateTime(nextMouth.Year, nextMouth.Month, 1).AddSeconds(-1);
                    }
                    else
                    {
                        customerPromotion.EndDate = customerPromotion.NextRenewDate.Value.AddSeconds(-1);
                    }
                }
                else
                {
                    if (request.EndDate.HasValue)
                        customerPromotion.EndDate = request.EndDate;
                    else
                        customerPromotion.EndDate = CalculateNonRecurringEndDate(customerPromotion);
                }
                request.CustomerInfo.Promotions.Add(customerPromotion);
                response.promotionPlan.Add(customerPromotion);

                _createCustomersPromotionMS.Process(new CreateCustomersPromotionRequest() { CrmCustomersPromotionInfo = customerPromotion }, null);
            }

            #endregion

            return response;
        }


        /// <summary>
        /// Reset Fake Promotion for customer
        /// </summary>
        /// <param name="customerInfo"></param>
        private void ResetFakePromotionForCustomer(CustomerInfo customerInfo)
        {

            var repoCustomerPromotion = RepositoryManager.GetRepository<ICrmCustomersPromotionInfoRepository<CrmCustomersPromotionInfo>>();

            if (customerInfo.DealerID == null)
                return;

            DealerInfo dealerInfo = _getDealerInfoMVNOByDealerIdMS.Process(new GetDealerInfoMVNOByDealerIdRequest() { DealerId = customerInfo.DealerID.Value }, null).DealerInfo;

            if (dealerInfo == null || dealerInfo.FiscalUnitID == null)
                return;
            var customerPromotions = customerInfo.Promotions;
            //GetFake promotion configuration
            List<MVNOConfigActionInfo> configurationList =
                Enumerable.ToList<MVNOConfigActionInfo>(_getMVNOConfigActionInfosByMVNOIdAndCategoryIdAndItemMS.Process(new GetMVNOConfigActionInfosByMVNOIdAndCategoryIdAndItemRequest()
                {
                    MvnoId = dealerInfo.FiscalUnitID.Value,
                    CategoryId = 111,
                    Item = "Mvno_QoS_DataPromotionPlanID",
                    StatusId = 1
                }, null).MvnoConfigActionInfos);
            

            if (configurationList.Count == 0)
            {
                return;
            }
            //Get Fake promotionplan id from configuration
            var fakePromotionPlanId = configurationList.Select(item => item.Value).ToList()[0];

            var customerFakePromotionInfo =
                customerPromotions.FirstOrDefault(
                    x => x.PromotionDetail.RmPromotionPlanInfo.PromotionPlanId.ToString(CultureInfo.InvariantCulture) == fakePromotionPlanId);
            if (customerFakePromotionInfo != null)
            {
                customerFakePromotionInfo.CurrentLimit = customerFakePromotionInfo.PromotionDetail.Limit;
                customerFakePromotionInfo.Active = true;
                //customerFakePromotion.RenewDate = DateTime.Now;

                
                _resetfakePromotion.Process(new ResetFakePromotionForCustomerRequest() { CustomerFakePromotionInfo = customerFakePromotionInfo }, null);
            }
        }

        

        

        /// <summary>
        /// Calculate the EndDate for Non rucurring promotion based on promotionplandetail
        /// Added by neil at 2015/03/30
        /// </summary>
        /// <param name="customerPromotion"></param>
        /// <returns></returns>
        private DateTime? CalculateNonRecurringEndDate(CrmCustomersPromotionInfo customerPromotion)
        {
            var promotionDetail = customerPromotion.PromotionDetail;
            if (promotionDetail == null)
                return null;

            //if charge fee time point be set to when first used, the end date need set to null
            if (promotionDetail.RmPromotionPlanInfo.TimePointForChargeFee == 1)
                return null;

            if (promotionDetail.Periodicity == 0)
            {
                // non recurring 
                return CalculateDate(customerPromotion, null);
            }
            else
            {
                return CalculateNextRenewDate(customerPromotion, null);
            }
        }


        /// <summary>
        /// Calculate Next Renew Date
        /// </summary>
        /// <param name="customerPromotion"></param>
        /// <param name="isNatural"></param>
        /// <returns></returns>
        private DateTime? CalculateNextRenewDate(CrmCustomersPromotionInfo customerPromotion, bool? isNatural)
        {
            var promotionDetail = customerPromotion.PromotionDetail;
            if (promotionDetail == null)
                return null;
            if (promotionDetail.Periodicity == 0 ||
                (promotionDetail.Periodicity == 1 && promotionDetail.CycleRepeatCount == 0) ||
                (promotionDetail.Periodicity == 1 && promotionDetail.CycleRepeatCount == 1)
                )
                return null;

            if (!isNatural.HasValue)
                isNatural = promotionDetail.RmPromotionPlanInfo.DailyResetTime == 0;


            return CalculateDate(customerPromotion, isNatural);
        }

        /// <summary>
        /// Calculate Date
        /// </summary>
        /// <param name="customerPromotion"></param>
        /// <param name="isNatural"></param>
        /// <returns></returns>
        private DateTime? CalculateDate(CrmCustomersPromotionInfo customerPromotion, bool? isNatural)
        {
            var promotionDetail = customerPromotion.PromotionDetail;
            DateTime startDate = customerPromotion.StartDate ?? DateTime.Now;
            DateTime? calculatedDate = null;
            DateTime tempDate = DateTime.Now;
            if (isNatural.HasValue && isNatural.Value)
            {
                switch (promotionDetail.PeriodUnit)
                {
                    case TimeUnits.Year:
                        calculatedDate = new DateTime(startDate.AddYears(promotionDetail.PeriodCount).Year, 1, 1);
                        break;
                    case TimeUnits.Month:
                        calculatedDate = new DateTime(startDate.AddMonths(promotionDetail.PeriodCount).Year, startDate.AddMonths(promotionDetail.PeriodCount).Month, 1);
                        break;
                    case TimeUnits.Week:
                        tempDate = startDate.AddDays(promotionDetail.PeriodCount * 7);
                        calculatedDate = tempDate.AddDays(0 - (int)tempDate.DayOfWeek).Date;
                        break;
                    case TimeUnits.Day:
                        calculatedDate = startDate.Date.AddDays(promotionDetail.PeriodCount);
                        break;
                    case TimeUnits.Hour:
                        tempDate = startDate.AddHours(promotionDetail.PeriodCount);
                        calculatedDate = new DateTime(tempDate.Year, tempDate.Month, tempDate.Day, tempDate.Hour, 0, 0);
                        break;
                    case TimeUnits.Minute:
                        tempDate = startDate.AddMinutes(promotionDetail.PeriodCount);
                        calculatedDate = new DateTime(tempDate.Year, tempDate.Month, tempDate.Day, tempDate.Hour, tempDate.Minute, 0);
                        break;
                    case TimeUnits.Second:
                        tempDate = startDate.AddSeconds(promotionDetail.PeriodCount);
                        calculatedDate = new DateTime(tempDate.Year, tempDate.Month, tempDate.Day, tempDate.Hour, tempDate.Minute, tempDate.Second);
                        break;
                }
            }
            else
            {
                if (promotionDetail.PeriodCount == 0)
                    return null;
                switch (promotionDetail.PeriodUnit)
                {
                    case TimeUnits.Year:
                        calculatedDate = startDate.AddYears(promotionDetail.PeriodCount);
                        break;
                    case TimeUnits.Month:
                        calculatedDate = startDate.AddMonths(promotionDetail.PeriodCount);
                        break;
                    case TimeUnits.Week:
                        calculatedDate = startDate.AddDays(promotionDetail.PeriodCount * 7);
                        break;
                    case TimeUnits.Day:
                        calculatedDate = startDate.AddDays(promotionDetail.PeriodCount);
                        break;
                    case TimeUnits.Hour:
                        calculatedDate = startDate.AddHours(promotionDetail.PeriodCount);
                        break;
                    case TimeUnits.Minute:
                        calculatedDate = startDate.AddMinutes(promotionDetail.PeriodCount);
                        break;
                    case TimeUnits.Second:
                        calculatedDate = startDate.AddSeconds(promotionDetail.PeriodCount);
                        break;
                }
            }

            return calculatedDate;
        }

        #endregion
    }
}
