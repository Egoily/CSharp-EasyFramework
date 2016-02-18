using System;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;

namespace com.etak.core.bizops.revenue.ChargeService
{
    /// <summary>
    /// ChargeServiceRequestInternal
    /// </summary>
    public class ChargeServiceRequestInternal: RequestBase
    {
        /// <summary>
        /// CustomerChargeSchedule
        /// </summary>
        public virtual CustomerChargeSchedule CustomerChargeSchedule { get; set; }

        /// <summary>
        /// datetimePurchase
        /// </summary>
        public virtual DateTime datetimePurchase { get; set; }

        /// <summary>
        /// datetimePriceEffective
        /// </summary>
        public virtual DateTime? datetimePriceEffective { get; set; }

        /// <summary>
        /// Invoice
        /// </summary>
        public virtual Invoice Invoice { get; set; }

        /// <summary>
        /// URL for the API ApllyFirstCharge
        /// </summary>
        public virtual string Url { get; set; }

    }
}
