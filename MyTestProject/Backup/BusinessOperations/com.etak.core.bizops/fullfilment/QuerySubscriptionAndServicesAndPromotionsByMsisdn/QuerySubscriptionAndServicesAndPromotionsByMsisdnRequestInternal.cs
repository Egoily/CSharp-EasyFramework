using com.etak.core.model;
using com.etak.core.model.operation.contract.subscription;
using com.etak.core.model.operation.messages;

	
namespace com.etak.core.bizops.fullfilment.QuerySubscriptionAndServicesAndPromotionsByMsisdn
{
    /// <summary>
    /// Internal Request message for QuerySubscriptionAndServicesAndPromotionsByMsisdn
    /// </summary>
    public class QuerySubscriptionAndServicesAndPromotionsByMsisdnRequestInternal : RequestBase,ISubscriptionLastActiveBasedRequest
    {
        /// <summary>
		/// Customer Subscription
		/// </summary>
        public ResourceMBInfo Subscription { get; set; }
		/// <summary>
		/// Msisdn
		/// </summary>
        public string MSISDN { get; set; }
    }
}
