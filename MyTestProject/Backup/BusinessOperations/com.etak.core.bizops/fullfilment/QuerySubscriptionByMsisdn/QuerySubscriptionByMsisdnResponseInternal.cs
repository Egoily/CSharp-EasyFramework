using com.etak.core.model;
using com.etak.core.model.operation.contract.customer;
using com.etak.core.model.operation.contract.subscription;
using com.etak.core.model.operation.messages;

namespace com.etak.core.bizops.fullfilment.QuerySubscriptionByMsisdn
{
    /// <summary>
    /// Internal response of QuerySubscriptionByMsisdn
    /// </summary>
    public class QuerySubscriptionByMsisdnResponseInternal : ResponseBase, ISubscriptionBasedResponse,ICustomerBasedResponse
    {
        /// <summary>
        /// core object that store information of subscription 
        /// </summary>
        public virtual ResourceMBInfo Subscription { get; set; }
        /// <summary>
        /// customer related to the subscription
        /// </summary>
        public CustomerInfo Customer { get; set; }
    }
}
