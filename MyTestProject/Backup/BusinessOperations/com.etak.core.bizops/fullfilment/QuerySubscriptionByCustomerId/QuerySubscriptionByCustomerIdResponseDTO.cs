using com.etak.core.model.operation.contract.subscription;
using com.etak.core.model.operation.messages;
using com.etak.core.model.subscription;

namespace com.etak.core.bizops.fullfilment.QuerySubscriptionByCustomerId
{
    /// <summary>
    /// Response in DTO form for QuerySubsriptionByCustomerId
    /// </summary>
    public class QuerySubscriptionByCustomerIdResponseDTO : ResponseBaseDTO, ISubscriptionBasedDTOResponse
    {
        /// <summary>
        /// Subscription information in DTO form
        /// </summary>
        public SubscriptionDTO Subscription { get; set; }
    }
}