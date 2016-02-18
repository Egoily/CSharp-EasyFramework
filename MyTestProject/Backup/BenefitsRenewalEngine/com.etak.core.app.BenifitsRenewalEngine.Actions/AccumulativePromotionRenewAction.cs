using System;
using System.Linq;
using com.etak.core.model;
using com.etak.core.repository.crm;
using com.etak.core.repository.crm.promotion;
using log4net;

namespace com.etak.core.app.BenifitsRenewalEngine.Actions
{
    public class AccumulativePromotionRenewAction : AbstractPromotionRenewAction
    {
        public static ILog loger = LogManager.GetLogger(typeof(AccumulativePromotionRenewAction));
        public override void Renew(CustomerInfo info, model.revenueManagement.BillRun odlBillRun, model.revenueManagement.BillRun newBillRun)
        {
            var promotionGroupRepo =
                repository.RepositoryManager.GetRepository<IRmPromotionGroupMemberRepository<RmPromotionGroupMember>>();
            var customerPromotionRepo =
                repository.RepositoryManager
                    .GetRepository<ICrmCustomersPromotionInfoRepository<CrmCustomersPromotionInfo>>();

            var handlingPromotion = (base.RefferredPromotion != null)
                ? base.RefferredPromotion
                : base.PreRefferredPromotion;

            if (handlingPromotion == null)
                return;

            var currentAccumulativePromotion = info.Promotions.FirstOrDefault(p => p.PromotionDetail.PromotionPlanDetailId == handlingPromotion.PromotionPlanDetailId
                && p.PromotionDetail.RmPromotionPlanInfo.Accumulative && p.StartDate >= newBillRun.StarteDate && p.EndDate <= newBillRun.EndDate.AddSeconds(1));

            if (currentAccumulativePromotion == null)
            {
                //loger.InfoFormat("there is no accumulate promotion info for CustomerID#{0} for PromotionDetail#{1}", info.CustomerID, handlingPromotion.PromotionPlanDetailId);
                throw new Exception(string.Format("there is no accumulate promotion info for CustomerID#{0} for PromotionDetail#{1}", info.CustomerID, handlingPromotion.PromotionPlanDetailId));
            }


            if (loger.IsDebugEnabled)
            loger.DebugFormat("begin to accumulate promotion balance for CustomerID#{0} for PromotionDetail#{1}, CustomerPromotion#{2}"
                , info.CustomerID, currentAccumulativePromotion.PromotionDetail.PromotionPlanDetailId, currentAccumulativePromotion.PromotionId);

            //TODO: GROUP SHOULD BE Cached
            var groupMembers = promotionGroupRepo.GetAll();
            var member = groupMembers.FirstOrDefault(gp =>
                        gp.PromotionPlan.PromotionPlanId == currentAccumulativePromotion.PromotionDetail.RmPromotionPlanInfo.PromotionPlanId
                        && gp.PromotionGroup.GroupType == 1);
            if (member != null)
            {
                //we gather together all the base promotions' balance and accumulate it to next period
                var otherMembers = groupMembers.Where(
                    gp =>
                        gp.PromotionGroup.PromotionGroupID == member.PromotionGroup.PromotionGroupID &&
                        gp.PromotionPlan.PromotionPlanId != member.PromotionPlan.PromotionPlanId).ToList();

                if (otherMembers.Any())
                {
                    var rolloverPromotions = from a in info.Promotions
                                             from b in otherMembers.Select(m => m.PromotionPlan.PromotionPlanId)
                                             where a.PromotionDetail.RmPromotionPlanInfo.PromotionPlanId == b
                                             where a.StartDate >= odlBillRun.StarteDate
                                             && a.EndDate.HasValue
                                             && a.StartDate < a.EndDate
                                             && a.EndDate >= odlBillRun.EndDate && a.EndDate <= odlBillRun.EndDate.AddSeconds(1)
                                             select a;

                    var accumulateValue = rolloverPromotions.Sum(p => p.CurrentLimit);
                    
                    if(loger.IsDebugEnabled)
                        loger.DebugFormat("{0} is accumulated from rolloverPromotions for customer#{1}  in handling promotionplandetail{2} ", accumulateValue, info.CustomerID, handlingPromotion.PromotionPlanDetailId);

                    currentAccumulativePromotion.CurrentLimit += accumulateValue;
                    //currentAccumulativePromotion.ActionsExecuted = true;
                    customerPromotionRepo.Update(currentAccumulativePromotion);

                    //set the old customerpromotionPlan to be deactive
                    /*rolloverPromotions.ForEach(cp =>
                    {
                        cp.Active = false;
                        //cp.ActionsExecuted = true;
                        customerPromotionRepo.Update(cp);
                    });*/
                    if (loger.IsDebugEnabled)
                        loger.InfoFormat("renew accumulatived promotiondetail#{0} for customer#{1} successfully", handlingPromotion.PromotionPlanDetailId, info.CustomerID);
                }
                else
                {
                    if (loger.IsDebugEnabled)
                        loger.DebugFormat("there's no base promotionPlan configured  for customer#{0}  in handling promotionplandetail{1}", info.CustomerID, handlingPromotion.PromotionPlanDetailId);
                }
            }
            else
            {
                if (loger.IsDebugEnabled)
                    loger.DebugFormat("there's no accumulative promotionPlan configured  for customer#{0}  in handling promotionplandetail{1}", info.CustomerID, handlingPromotion.PromotionPlanDetailId);
            }

        }


    }
}
