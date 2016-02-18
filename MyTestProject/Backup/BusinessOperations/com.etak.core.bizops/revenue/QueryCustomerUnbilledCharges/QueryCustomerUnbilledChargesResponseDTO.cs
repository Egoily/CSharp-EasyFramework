using System.Collections.Generic;
using com.etak.core.model.operation.contract.subscription;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;
using com.etak.core.model.subscription;

namespace com.etak.core.bizops.revenue.QueryCustomerUnbilledCharges
{
    /// <summary>
    /// Response DTO of QueryCustomerUnbilledCharges
    /// </summary>
    public class QueryCustomerUnbilledChargesResponseDTO : ResponseBaseDTO,ISubscriptionBasedDTOResponse
    {
        /// <summary>
        /// IList of CustomerChargeDTO
        /// </summary>
        public IList<CustomerChargeDTO> CustomerCharges { get; set; }
        /// <summary>
        /// Subscription of the customer
        /// </summary>
        public SubscriptionDTO Subscription { get; set; }
    }
}
