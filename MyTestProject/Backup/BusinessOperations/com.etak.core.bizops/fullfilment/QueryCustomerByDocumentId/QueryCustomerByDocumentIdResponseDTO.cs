using System.Collections.Generic;
using com.etak.core.model.dto;
using com.etak.core.model.operation.contract.customer;
using com.etak.core.model.operation.contract.subscription;
using com.etak.core.model.operation.messages;
using com.etak.core.model.subscription;

namespace com.etak.core.bizops.fullfilment.QueryCustomerByDocumentId
{
    /// <summary>
    /// Response DTO of QueryCustomerByDocumentIdBizOp
    /// </summary>
    public class QueryCustomerByDocumentIdResponseDTO : ResponseBaseDTO,ICustomerBasedDTOResponse,ISubscriptionBasedDTOResponse
    {
        /// <summary>
        /// CustomerDTO
        /// </summary>
        public IEnumerable<CustomerDTO> Customers { get; set; }
        /// <summary>
        /// CustomerDTO of the first customer in the list
        /// </summary>
        public CustomerDTO Customer { get; set; }
        /// <summary>
        /// SubscriptionDTO of the first customer in the list
        /// </summary>
        public SubscriptionDTO Subscription { get; set; }
    }
}
