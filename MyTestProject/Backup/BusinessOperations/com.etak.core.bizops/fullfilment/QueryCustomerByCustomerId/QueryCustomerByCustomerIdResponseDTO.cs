using com.etak.core.model.dto;
using com.etak.core.model.operation.contract.customer;
using com.etak.core.model.operation.contract.subscription;
using com.etak.core.model.operation.messages;
using com.etak.core.model.subscription;

namespace com.etak.core.bizops.fullfilment.QueryCustomerByCustomerId
{
    /// <summary>
    /// Response in DTO form for QueryCustomerByCustomerId
    /// </summary>
    public class QueryCustomerByCustomerIdResponseDTO : ResponseBaseDTO, ICustomerBasedDTOResponse,ISubscriptionBasedDTOResponse
    {
        /// <summary>
        /// Customer with certain CustomerId
        /// </summary>
        public CustomerDTO Customer { get; set; }
        /// <summary>
        /// Subscription corresponding with certain CustomerId
        /// </summary>
        public SubscriptionDTO Subscription { get; set; }
    }
}
