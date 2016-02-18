using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.model.operation.contract.customer;
using com.etak.core.model.operation.contract.subscription;
using com.etak.core.model.operation.messages;
		
namespace com.etak.core.bizops.fullfilment.QuerySubscriptionAndServicesAndPromotionsByMsisdn
{
    /// <summary>
    /// Internal response message for QuerySubscriptionAndServicesAndPromotionsByMsisdn
    /// </summary>
    public class QuerySubscriptionAndServicesAndPromotionsByMsisdnResponseInternal:ResponseBase,ISubscriptionBasedResponse,ICustomerBasedResponse
    {
		/// <summary>
		/// List of customer services
		/// </summary>
        public IList<ServicesInfo> Services;
		/// <summary>
		/// List of customer promotions
		/// </summary>
        public IList<CrmCustomersPromotionInfo> Promotions; 
		/// <summary>
		/// Customer Subscription
		/// </summary>
        public ResourceMBInfo Subscription { get; set; }
        /// <summary>
        /// Customer related to the subscription
        /// </summary>
        public CustomerInfo Customer { get; set; }
    }
}
