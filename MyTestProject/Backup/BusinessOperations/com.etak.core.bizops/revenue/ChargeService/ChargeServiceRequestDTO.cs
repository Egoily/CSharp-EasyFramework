using System;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;

namespace com.etak.core.bizops.revenue.ChargeService
{
    /// <summary>
    /// ChargeServiceRequestDTO
    /// </summary>
    public class ChargeServiceRequestDTO: RequestBaseDTO
    {
        /// <summary>
        /// CustomerChargeSchedule
        /// </summary>
        public long CustomerChargeScheduleId { get; set; }

        /// <summary>
        /// datetimePurchase
        /// </summary>
        public DateTime datetimePurchase { get; set; }

        /// <summary>
        /// datetimePriceEffective
        /// </summary>
        public DateTime? datetimePriceEffective { get; set; }

        /// <summary>
        /// Invoice
        /// </summary>
        public InvoiceDTO Invoice { get; set; }

        // Not used, by now
    }
}
