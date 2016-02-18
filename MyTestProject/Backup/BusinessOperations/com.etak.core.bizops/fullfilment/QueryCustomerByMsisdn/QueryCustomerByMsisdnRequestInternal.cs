using com.etak.core.model;
using com.etak.core.model.operation.contract.subscription;
using com.etak.core.model.operation.messages;

namespace com.etak.core.bizops.fullfilment.QueryCustomerByMsisdn
{
    /// <summary>
    /// Request internal of QueryCustomerByMsisdn
    /// </summary>
    public class QueryCustomerByMsisdnRequestInternal : RequestBase, ISubscriptionLastActiveBasedRequest

    {
        /// <summary>
        /// MSISDN
        /// </summary>
        public string MSISDN { get; set; }

        /// <summary>
        /// Last active Subscription
        /// </summary>
        public ResourceMBInfo Subscription { get; set; }
    }
}
