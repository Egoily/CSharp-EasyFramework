using com.etak.core.model;
using com.etak.core.model.operation.contract.subscription;
using com.etak.core.model.operation.messages;

namespace com.etak.core.bizops.fullfilment.QuerySubscriptionByMsisdn
{
    /// <summary>
    /// Interal request of QuerySubscriptionByMsisdn, this object is use by business logic
    /// </summary>
    public class QuerySubscriptionByMsisdnRequestInternal : RequestBase, ISubscriptionLastActiveBasedRequest
    {
        /// <summary>
        /// information of MSISDN
        /// </summary>
        public virtual string MSISDN { get; set; }
        /// <summary>
        /// core object that store information of subcription 
        /// </summary>
        public virtual ResourceMBInfo Subscription { get; set; }
       
    }
}
