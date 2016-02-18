using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [Serializable]
    public class PromotionGroupRuleParam
    {        
        public int UserId { get; set; }
        public int LanguageId { get; set; }
        public CustomerInfo CustomerInfo { get; set; }
        public RmPromotionGroupInfo PromoGroupInfo { get; set; }
    }

    [Serializable]
    public enum RuleCaller4PromotionGroup
    {
        AnyWhere = 0,
        ChangePackageAPI = 1,
        ApplyPromotionGroupAPI=2,
        NewEquipmentRatePlan=3
    }

    [DataContract]
    [Serializable]
    public abstract class PromotionGroupRule : BussinessRule
    {
        public abstract void ExecuteActions(PromotionGroupRuleParam param, RuleCaller4PromotionGroup ruleCaller);
        public abstract EligibleResult IsEligible(PromotionGroupRuleParam param, RuleCaller4PromotionGroup ruleCaller);
    }
}
