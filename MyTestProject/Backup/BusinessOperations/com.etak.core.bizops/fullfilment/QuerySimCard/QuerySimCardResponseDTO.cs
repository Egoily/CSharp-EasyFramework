using com.etak.core.model.dto;
using com.etak.core.model.operation.contract.customer;
using com.etak.core.model.operation.contract.simcard;
using com.etak.core.model.operation.contract.subscription;
using com.etak.core.model.operation.messages;
using com.etak.core.model.resource;
using com.etak.core.model.subscription;

namespace com.etak.core.bizops.fullfilment.QuerySimCard
{
    /// <summary>
    /// Response DTO of QuerySimCard
    /// </summary>
    public class QuerySimCardResponseDTO : ResponseBaseDTO, ISimCardBasedDTOResponse,ICustomerBasedDTOResponse,ISubscriptionBasedDTOResponse
    {
        /// <summary>
        /// SimCardDTO
        /// </summary>
        public SimCardDTO SimCard { get; set; }
        /// <summary>
        /// Customer owner of the simcard
        /// </summary>
        public CustomerDTO Customer { get; set; }
        /// <summary>
        /// subscription associated to simcard
        /// </summary>
        public SubscriptionDTO Subscription { get; set; }
    }
}
