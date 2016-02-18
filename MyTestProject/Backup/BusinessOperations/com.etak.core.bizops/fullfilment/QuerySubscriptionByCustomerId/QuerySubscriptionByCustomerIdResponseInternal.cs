using com.etak.core.model;
using com.etak.core.model.operation.contract.subscription;
using com.etak.core.model.operation.messages;

namespace com.etak.core.bizops.fullfilment.QuerySubscriptionByCustomerId
{
    /// <summary>
    /// Response in core model for QuerySubsriptionByCustomerId, contains the customer
    /// </summary>
    public class QuerySubscriptionByCustomerIdResponseInternal : ResponseBase, ISubscriptionBasedResponse
    {
        /// <summary>
        /// Subscription of customer
        /// </summary>
        public virtual ResourceMBInfo Subscription { get; set; }
    }
}
