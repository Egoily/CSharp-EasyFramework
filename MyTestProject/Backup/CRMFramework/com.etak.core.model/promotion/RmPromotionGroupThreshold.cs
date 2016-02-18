using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    [DataContract]
    [Serializable]
    public class RmPromotionGroupThreshold
    {
        virtual public int ThresholdId { get; set; }
        virtual public RmPromotionGroupInfo PromotionGroup { get; set; }
        virtual public decimal ThresholdValue { get; set; }
        virtual public ThresholdType ThresholdType { get; set; }
        virtual public ThresholdDirection Direction { get; set; }
        virtual public int GenerateEventId { get; set; }
    }

    [Serializable]
    public enum ThresholdType
    {
        /// <summary>
        /// Absolute
        /// </summary>
        A,
        /// <summary>
        /// Percent
        /// </summary>
        P
    }

    [Serializable]
    public enum ThresholdDirection
    {
        /// <summary>
        /// UP
        /// </summary>
        U,
        /// <summary>
        /// Down
        /// </summary>
        D
    }
}
