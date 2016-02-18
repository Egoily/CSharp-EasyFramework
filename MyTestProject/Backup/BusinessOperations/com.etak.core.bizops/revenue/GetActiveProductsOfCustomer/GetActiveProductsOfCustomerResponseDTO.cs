using System.Collections.Generic;
using com.etak.core.model.operation.contract.subscription;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;
using com.etak.core.model.subscription;

namespace com.etak.core.bizops.revenue.GetActiveProductsOfCustomer
{
    /// <summary>
    /// The response DTO for GetActiveProducsOfCustomerBizOp
    /// </summary>
    public class GetActiveProductsOfCustomerResponseDTO : ResponseBaseDTO,ISubscriptionBasedDTOResponse
    {
        /// <summary>
        /// All the active products for the customer, within a time
        /// </summary>
        public IEnumerable<CustomerProductAssignmentDTO> CustomerProductAssignments { get; set; }
        /// <summary>
        /// Subscription of the customer
        /// </summary>
        public SubscriptionDTO Subscription { get; set; }
    }
}
