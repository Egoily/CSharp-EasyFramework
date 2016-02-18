using com.etak.core.model.dto;
using com.etak.core.model.operation.contract.customer;
using com.etak.core.model.operation.contract.subscription;
using com.etak.core.model.operation.messages;
using com.etak.core.model.subscription;

namespace com.etak.core.bizops.fullfilment.QuerySubscriptionByMsisdn
{
    /// <summary>
    /// Response DTO of QuerySubscriptionByMsisdn
    /// </summary>
    public class QuerySubscriptionByMsisdnDTOResponse : ResponseBaseDTO, ISubscriptionBasedDTOResponse,ICustomerBasedDTOResponse
    {
        /// <summary>
        /// DTO object of subscription 
        /// </summary>
        public SubscriptionDTO Subscription { get; set; }
        /// <summary>
        /// Customer related to the subscription
        /// </summary>
        public CustomerDTO Customer { get; set; }
    }
}
