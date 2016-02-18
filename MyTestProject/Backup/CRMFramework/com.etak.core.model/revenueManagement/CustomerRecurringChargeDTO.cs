using System;

namespace com.etak.core.model.revenueManagement
{

    /// <summary>
    /// An invoice issued to a customer
    /// </summary>
    public class CustomerRecurringChargeDTO : CustomerChargeDTO
    {
        /// <summary>
        /// The date in which the next charge applies
        /// </summary>
        public DateTime? NextChargeDate { get; set; }

        /// <summary>
        /// The number of the next period to be charged
        /// </summary>
        public Int32 NextPeriodNumber { get; set; }

        /// <summary>
        /// The number of the current scheduling charge cycle (don't mix with BillCycles).
        /// </summary>
        public Int64 CurrentCycleNumber { get; set; }
    }
}
