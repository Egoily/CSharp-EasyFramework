using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class PromotionGroupBusinessRules
    {
        public virtual int ID { get; set; }
        public virtual RmPromotionGroupInfo PromotionGroupInfo { get; set; }
        public virtual PromotionGroupRule RuleInfo { get; set; }
        public virtual short Rank { get; set; }

        public override bool Equals(object obj)
        {
            PromotionGroupBusinessRules toCompare = obj as PromotionGroupBusinessRules;
            if (toCompare == null)
            {
                return false;
            }
            if (this.PromotionGroupInfo.PromotionGroupID != toCompare.PromotionGroupInfo.PromotionGroupID ||
                this.RuleInfo.Id != toCompare.RuleInfo.Id ||
                this.ID != toCompare.ID)
            {
                return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            return this.ID.GetHashCode() + PromotionGroupInfo.GetHashCode() + RuleInfo.GetHashCode();
        }
    }
  
}