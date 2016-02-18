using System.Collections.Generic;
using com.etak.core.model.operation.contract.subscription;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;
using com.etak.core.model.subscription;

namespace com.etak.core.bizops.revenue.QueryCustomerRecurringCharges
{
    /// <summary>
    /// QueryCustomerRecurringChargesResponseDTO of QueryCustomerRecurringChargesBizOp
    /// </summary>
    public class QueryCustomerRecurringChargesResponseDTO : ResponseBaseDTO, ISubscriptionBasedDTOResponse
    {
        /// <summary>
        /// CustomerRecurringChargeDTO responseDto that give CustomerInfo
        /// </summary>
        public IEnumerable<CustomerRecurringChargeDTO> RecurringCharges { get; set; }
        /// <summary>
        /// Subscription of Customer 
        /// </summary>
        public SubscriptionDTO Subscription { get; set; }
    }
}
