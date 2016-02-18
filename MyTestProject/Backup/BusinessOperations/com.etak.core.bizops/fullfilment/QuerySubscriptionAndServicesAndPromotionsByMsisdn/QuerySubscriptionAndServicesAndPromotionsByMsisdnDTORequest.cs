using com.etak.core.model.operation.contract.subscription;
using com.etak.core.model.operation.messages;
    
namespace com.etak.core.bizops.fullfilment.QuerySubscriptionAndServicesAndPromotionsByMsisdn
{
    /// <summary>
    /// DTO Request message for QuerySubscriptionAndServicesAndPromotionsByMsisdn
    /// </summary>
    public class QuerySubscriptionAndServicesAndPromotionsByMsisdnDTORequest : RequestBaseDTO, IMsisdnBasedDTORequest
    {
		/// <summary>
		/// Msisdn number
		/// </summary>
        public string MSISDN { get; set; }
    }
}
