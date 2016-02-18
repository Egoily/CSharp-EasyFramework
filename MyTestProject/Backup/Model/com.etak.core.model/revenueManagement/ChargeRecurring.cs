using System;

namespace com.etak.core.model.revenueManagement
{
    [Serializable]
    public class ChargeRecurring : Charge
    {
        #region	Recurring charges information
        /// <summary>
        /// Unit of time that represent the base unit for the periods in the cycle
        /// </summary>
        public virtual TimeUnits PeriodUnit { get; set; }

        /// <summary>
        /// the number of periods between two consecutive charges <example>1 if periodunitid is Monthly and the charge has to be charged every month, 3 in the same case but the charge happens every 3 months</example>
        /// </summary>
        public virtual Int64 PeriodCount { get; set; }

        /// <summary>
        /// The number of the period when the cycle starts <example>for a periodunitid of “week” and a periodunitcount of 1, 3 means that the cycle starts at week 3</example>
        /// </summary>
        public virtual Int32 StartPeriodNumber { get; set; }

        /// <summary>
        /// The number of the period when the cycle ends <example>for a periodunitid of “week” and a periodunitcount of 1, 6 means that the cycle stops at week 6</example>
        /// The last period of the cycle is the period number periodicity.
        /// </summary>
        public virtual Int32 EndPeriodNumber { get; set; }

        /// <summary>
        /// The number of periods within a cycle.
        /// </summary>
        public virtual Int32 Periodicity { get; set; }

        /// <summary>
        /// The number of time the cycle has to be repeated
        /// </summary>
        public virtual Int32 CycleRepeatCount { get; set; }

        public virtual Int32 PeriodCommitment { get; set; }
        #endregion

    }
}
