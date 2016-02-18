using System;

namespace com.etak.core.model.revenueManagement
{
    /// <summary>
    /// Bill run of a given BillingCycle
    /// </summary>
    [Serializable]
    public class BillRun
    {
        /// <summary>
        /// Unique Id of the bill run
        /// </summary>
        public virtual Int32 Id { get; set; }

        /// <summary>
        /// the BillCycle that generated this bill run
        /// </summary>
        public virtual BillCycle BillingCycle { get; set; }

        /// <summary>
        /// the date the bill run was executed 
        /// </summary>
        public virtual DateTime ? RunDate { get; set; }

        /// <summary>
        /// The Start Dates of the charges being processed
        /// </summary>
        public virtual DateTime ? StarteDate { get; set; }

        /// <summary>
        /// The End Date  of the charges being processed
        /// </summary>
        public virtual DateTime ? EndDate { get; set; }

        /// <summary>
        /// The Due Date  of the charges being processed
        /// </summary>
        public virtual DateTime ? DueDate { get; set; }

        /// <summary>
        /// The Cut off date
        /// </summary>
        public virtual DateTime ? CutOffDate { get; set; }

        /// <summary>
        /// The first Id of USAGE_DETAIL that is being taken into account for this bill run
        /// </summary>
        public virtual Int64 ? FirstUsageDetailId { get; set; }

        /// <summary>
        /// The last Id of USAGE_DETAIL that is being taken into account for this bill run
        /// </summary>
        public virtual Int64 ? LastUsageDetailId { get; set; }

    }
}