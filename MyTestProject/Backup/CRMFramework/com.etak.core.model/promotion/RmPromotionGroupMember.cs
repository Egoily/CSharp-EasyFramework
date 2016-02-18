using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class RmPromotionGroupMember
    {
        public virtual decimal? Limit { get; set; }
        public virtual RmPromotionGroupInfo PromotionGroup { get; set; }
        public virtual RmPromotionPlanInfo PromotionPlan { get; set; }
        public virtual int? MaxMSISDNNumberSize { get; set; }


        public override bool Equals(object obj)
        {
            RmPromotionGroupMember toCompare = obj as RmPromotionGroupMember;
            if (toCompare == null)
            {
                return false;
            }
            if (this.PromotionGroup.PromotionGroupID != toCompare.PromotionGroup.PromotionGroupID ||
                this.PromotionPlan.PromotionPlanId != toCompare.PromotionPlan.PromotionPlanId)
            {
                return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            return PromotionGroup.GetHashCode() + PromotionPlan.GetHashCode();
        }
    }
}
