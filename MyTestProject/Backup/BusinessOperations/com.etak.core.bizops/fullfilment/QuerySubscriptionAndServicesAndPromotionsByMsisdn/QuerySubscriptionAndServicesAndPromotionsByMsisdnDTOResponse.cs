using System.Collections.Generic;
using com.etak.core.model.dto;
using com.etak.core.model.operation.contract.customer;
using com.etak.core.model.operation.contract.subscription;
using com.etak.core.model.operation.messages;
using com.etak.core.model.subscription;

	
namespace com.etak.core.bizops.fullfilment.QuerySubscriptionAndServicesAndPromotionsByMsisdn
{
    /// <summary>
    /// DTO Response message for QuerySubscriptionAndServicesAndPromotionsByMsisdn
    /// </summary>
    public class QuerySubscriptionAndServicesAndPromotionsByMsisdnDTOResponse:ResponseBaseDTO,ISubscriptionBasedDTOResponse, ICustomerBasedDTOResponse
    {
		/// <summary>
		/// List of Customer Services
		/// </summary>
        public IList<ServicesInfoDTO> CustomerServices;
		/// <summary>
		/// List of Customer promotions
		/// </summary>
        public IList<CustomerPromotionDTO> CustomerPromotions;
		/// <summary>
		/// Customer Subscription
		/// </summary>
        public SubscriptionDTO Subscription { get; set; }
        /// <summary>
        /// Customer owner of the subscription
        /// </summary>
        public CustomerDTO Customer { get; set; }
    }
}
