using System.Collections.Generic;
using com.etak.core.model.operation.contract.subscription;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;
using com.etak.core.model.subscription;

namespace com.etak.core.bizops.fullfilment.QueryCustomerProduct
{
    /// <summary>
    /// QueryCustomerProductResponseDTO used in QueryCustomerProductBizOp
    /// </summary>
    public class QueryCustomerProductResponseDTO : ResponseBaseDTO,ISubscriptionBasedDTOResponse
    {
        /// <summary>
        /// CustomerProductAssignmentDTO DTO object
        /// </summary>
        public IList<CustomerProductAssignmentDTO> ProductPurchaseDto { get; set; }
        /// <summary>
        /// Subscription of the customer
        /// </summary>
        public SubscriptionDTO Subscription { get; set; }
    }
}
