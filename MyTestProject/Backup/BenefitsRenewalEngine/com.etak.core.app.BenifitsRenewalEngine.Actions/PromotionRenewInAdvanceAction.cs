using System;
using System.Collections.Generic;
using System.Linq;
using com.etak.core.model;
using com.etak.core.repository;
using com.etak.core.repository.crm;
using com.etak.core.repository.crm.promotion;
using log4net;
using log4net.Repository.Hierarchy;

namespace com.etak.core.app.BenifitsRenewalEngine.Actions
{
    public class PromotionRenewInAdvanceAction : AbstractPromotionRenewAction
    {
        public static ILog Loger = LogManager.GetLogger(typeof(PromotionRenewInAdvanceAction));
      

        public override void Renew(CustomerInfo info, model.revenueManagement.BillRun oblBillRun, model.revenueManagement.BillRun newBillRun)
        {
            CrmCustomersPromotionInfo crmCustPromInfo = null;

            var rmPpRepo = RepositoryManager.GetRepository<IRmPromotionPlanInfoRepository<RmPromotionPlanInfo>>();
            var customerPromotionInfoRepo = RepositoryManager.GetRepository<ICrmCustomersPromotionInfoRepository<CrmCustomersPromotionInfo>>();

            //TODO: no need to do this
            crmCustPromInfo = info.Promotions.Where(e => e.PromotionDetail.PromotionPlanDetailId == this.PreRefferredPromotion.PromotionPlanDetailId && e.StartDate >= oblBillRun.StarteDate && e.StartDate < e.EndDate
                    && e.EndDate >= oblBillRun.EndDate && e.EndDate <= oblBillRun.EndDate.AddSeconds(1) && !e.PreActionsExecuted)
                .FirstOrDefault();

            if (crmCustPromInfo == null)
            {
                //loger.InfoFormat("PromotionRenewInAdvanceAction, promotion is not legal,customer: {0}, promtion plan detail: {1}", info.CustomerID.Value, this.PreRefferredPromotion.PromotionPlanDetailId);
                throw new Exception(string.Format("PromotionRenewInAdvanceAction, promotion is not legal,customer: {0}, promtion plan detail: {1}", info.CustomerID.Value, this.PreRefferredPromotion.PromotionPlanDetailId));
            }

            if(!crmCustPromInfo.NextPeriodNumber.HasValue)
            {
                Loger.InfoFormat("PromotionRenewInAdvanceAction, can not find the next period number,customer promotion: {0} ", crmCustPromInfo.PromotionId);
                return;
            }

            int currentPeriodNumber = crmCustPromInfo.NextPeriodNumber.Value;
            int currentCycleNumber = crmCustPromInfo.CurrentCycleNumber ?? 1;
            currentCycleNumber = crmCustPromInfo.NextPeriodNumber.Value == this.PreRefferredPromotion.StartPeriodNumber ? (currentCycleNumber + 1) : currentCycleNumber;
            int iCycleNumber = 0;
            int? iNextPeriodNumber = 0;

            int incCycle = 0;
            if (this.PreRefferredPromotion.StartPeriodNumber == this.PreRefferredPromotion.EndPeriodNumber && this.PreRefferredPromotion.Periodicity == 1)
            {
                incCycle = 1;
                iNextPeriodNumber = this.PreRefferredPromotion.EndPeriodNumber;
            }
            else
            {
                incCycle = (currentPeriodNumber + 1) / this.PreRefferredPromotion.Periodicity;
                iNextPeriodNumber = (currentPeriodNumber + 1) % this.PreRefferredPromotion.Periodicity;
            }
            iCycleNumber = incCycle + currentCycleNumber;

            if (this.PreRefferredPromotion.CycleRepeatCount != -1 && iCycleNumber > this.PreRefferredPromotion.CycleRepeatCount)
            {
                iNextPeriodNumber = null;
            }

            List<RmPromotionPlanDetailInfo> lstRmPpds = new List<RmPromotionPlanDetailInfo>();
            if (this.PreRefferredPromotion.RmPromotionPlanInfo.RmPromotionPlanDetailInfoList == null || this.PreRefferredPromotion.RmPromotionPlanInfo.RmPromotionPlanDetailInfoList.Count < 1)
            {
                lstRmPpds = rmPpRepo.GetById(this.PreRefferredPromotion.RmPromotionPlanInfo.PromotionPlanId).RmPromotionPlanDetailInfoList.ToList();
            }
            else
            {
                lstRmPpds = this.PreRefferredPromotion.RmPromotionPlanInfo.RmPromotionPlanDetailInfoList.ToList();
            }

            RmPromotionPlanDetailInfo nextPpdInfo = lstRmPpds
                .Where(e => e.ServiceTypeId == this.PreRefferredPromotion.ServiceTypeId && e.StartPeriodNumber <= currentPeriodNumber && e.EndPeriodNumber >= currentPeriodNumber).FirstOrDefault();

            if (nextPpdInfo == null)
            {
                throw new Exception(string.Format("PromotionRenewInAdvanceAction, can not find the next plan detail info,customer promotion: {0} ", crmCustPromInfo.PromotionId));
            }

            CrmCustomersPromotionInfo newCrmCustomerPromInfo = new CrmCustomersPromotionInfo()
            {
                Active = crmCustPromInfo.Active,
                ActionsExecuted = false,
                ActiveWithoutCredit = crmCustPromInfo.ActiveWithoutCredit,
                BatchId = crmCustPromInfo.BatchId,
                BatchNo = crmCustPromInfo.BatchNo,
                CurrentCycleNumber = currentCycleNumber,
                CurrentLimit = nextPpdInfo.Limit,
                Customer = crmCustPromInfo.Customer,
                CustomerId = crmCustPromInfo.CustomerId,
                DeActiveReason = crmCustPromInfo.DeActiveReason,
                EndDate = newBillRun.EndDate,
                FirstUsed = crmCustPromInfo.FirstUsed,
                InitialLimit = crmCustPromInfo.InitialLimit,
                IsBasePromotion = crmCustPromInfo.IsBasePromotion,
                MSISDN = crmCustPromInfo.MSISDN,
                NextPeriodNumber = iNextPeriodNumber,
                NextRenewDate = newBillRun.EndDate.AddSeconds(1),
                PreRenewalActionsDate = newBillRun.EndDate.AddSeconds(1).AddMinutes(0 - this.PreRefferredPromotion.PreRenewalActionsMinutesOffset),
                PreActionsExecuted = false,
                Priority = crmCustPromInfo.Priority,
                PromotionDetail = nextPpdInfo,
                RenewalCount = crmCustPromInfo.RenewalCount,
                RenewAutomatically = crmCustPromInfo.RenewAutomatically,
                RenewDate = DateTime.Now,
                StartDate = newBillRun.StarteDate,
                WhiteList = crmCustPromInfo.WhiteList
            };
            customerPromotionInfoRepo.Create(newCrmCustomerPromInfo);
            info.Promotions.Add(newCrmCustomerPromInfo);

            if (Loger.IsDebugEnabled)
                Loger.DebugFormat("create a new promotion for customer :{0}", info.CustomerID.Value);
        }
    }
}