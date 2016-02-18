using System.Collections.Generic;
using com.etak.core.model.dto;
using com.etak.core.model.operation.contract.customer;
using com.etak.core.model.operation.contract.subscription;
using com.etak.core.model.operation.messages;
using com.etak.core.model.subscription;

namespace com.etak.core.bizops.fullfilment.QueryCustomerByExternalId
{
    /// <summary>
    /// Response DTO of QueryCustomerByExternalIdBizOp
    /// </summary>
    public class QueryCustomerByExternalIdResponseDTO : ResponseBaseDTO,ICustomerBasedDTOResponse,ISubscriptionBasedDTOResponse
    {
        /// <summary>
        /// List of CustomerInfo with certain external Id
        /// </summary>
        public IList<CustomerDTO> CustomerDTOs { get; set; }
        /// <summary>
        /// First customer on the list
        /// </summary>
        public CustomerDTO Customer { get; set; }
        /// <summary>
        /// Subscription of the first customer on the list
        /// </summary>
        public SubscriptionDTO Subscription { get; set; }
    }
}
