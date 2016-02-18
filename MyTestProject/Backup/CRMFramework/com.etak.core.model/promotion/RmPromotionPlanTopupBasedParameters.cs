using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class RmPromotionPlanTopupBasedParameters
    {
        private int promotionPlanID;

        public int PromotionPlanID
        {
            get { return promotionPlanID; }
            set { promotionPlanID = value; }
        }
        private int subServiceTypeID;
        public int SubServiceTypeID
        {
            get { return subServiceTypeID; }
            set { subServiceTypeID = value; }
        }

        private decimal topupThresholdAmount;
        public decimal TopupThresholdAmount
        {
            get { return topupThresholdAmount; }
            set { topupThresholdAmount = value; }
        }
        private int daysToAccumulateTopups;
        public int DaysToAccumulateTopups
        {
            get { return daysToAccumulateTopups; }
            set { daysToAccumulateTopups = value; }
        }
        private int? validityUnit;
        public int? ValidityUnit
        {
            get { return validityUnit; }
            set { validityUnit = value; }
        }
        private int? validityQuantity;
        public int? ValidityQuantity
        {
            get { return validityQuantity; }
            set { validityQuantity = value; }
        }
        private int? limitUnit;
        public int? LimitUnit
        {
            get { return limitUnit; }
            set { limitUnit = value; }
        }
        private decimal limitQuantity;
        public decimal LimitQuantity
        {
            get { return limitQuantity; }
            set { limitQuantity = value; }
        }

        public override bool Equals(object obj)
        {
            RmPromotionPlanTopupBasedParameters value = obj as RmPromotionPlanTopupBasedParameters;
            if (value != null && value.PromotionPlanID == this.PromotionPlanID &&
                value.TopupThresholdAmount == this.TopupThresholdAmount && value.SubServiceTypeID == this.SubServiceTypeID)
            {
                return true;
            }
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
