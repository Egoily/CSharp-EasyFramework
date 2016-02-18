using com.etak.core.model.operation.contract.subscription;
using com.etak.core.model.operation.messages;

namespace com.etak.core.bizops.fullfilment.QuerySubscriptionByMsisdn
{
    /// <summary>
    /// DTO request of QuerySubscriptionByMsisdn
    /// </summary>
    public class QuerySubscriptionByMsisdnDTORequest : RequestBaseDTO, IMsisdnBasedDTORequest
    {
        /// <summary>
        /// given MSISDN to get related subscription
        /// </summary>
        public string MSISDN { get; set; }
    }
}
