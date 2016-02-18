using com.etak.core.model;
using com.etak.core.model.operation.contract.customer;
using com.etak.core.model.operation.contract.simcard;
using com.etak.core.model.operation.contract.subscription;
using com.etak.core.model.operation.messages;

namespace com.etak.core.bizops.fullfilment.QuerySimCard
{
    /// <summary>
    /// Response Internal of QuerySimCard
    /// </summary>
    public class QuerySimCardResponseInternal : ResponseBase, ISimCardBasedResponse,ICustomerBasedResponse,ISubscriptionBasedResponse
    {
        /// <summary>
        /// SIMCardInfo that will be converted to SIMCardDTO
        /// </summary>
        public virtual SIMCardInfo SimCard { get; set; }
        /// <summary>
        /// Customer owner of the simcard
        /// </summary>
        public CustomerInfo Customer { get; set; }
        /// <summary>
        /// Subscription associated to the simcard
        /// </summary>
        public ResourceMBInfo Subscription { get; set; }
    }
}
