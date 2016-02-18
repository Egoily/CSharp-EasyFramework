using System.Collections.Generic;
using com.etak.core.model.operation.contract.subscription;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;
using com.etak.core.model.subscription;

namespace com.etak.core.bizops.fullfilment.QueryPurchasedProductsByCustomerId
{
    /// <summary>
    /// Output DTO of QueryPurchasedProductsByCustomerId
    /// </summary>
    public class QueryPurchasedProductsByCustomerIdResponseDTO : ResponseBaseDTO,ISubscriptionBasedDTOResponse
    {
        /// <summary>
        /// DTO list of customer's purchased product
        /// </summary>
        public IList<CustomerProductAssignmentDTO> ProductsPurchaseDto { get; set; }
        /// <summary>
        /// Customer subscription
        /// </summary>
        public SubscriptionDTO Subscription { get; set; }
    }
}
