using com.etak.core.model;
using com.etak.core.model.operation.contract.customer;
using com.etak.core.model.operation.contract.subscription;
using com.etak.core.model.operation.messages;

namespace com.etak.core.bizops.fullfilment.SwapSimCard
{
    /// <summary>
    /// SwapSimCardResponseInternal of SwapSimCardBizOp
    /// </summary>
    public class SwapSimCardResponseInternal : CreateNewOrderResponse,ICustomerBasedResponse,ISubscriptionBasedResponse
    {
        /// <summary>
        /// SimCardInfos to give data response of SimCardInfo
        /// </summary>
        public SIMCardInfo SimCardInfo { get; set; }
        /// <summary>
        /// Customer related to the simcard
        /// </summary>
        public CustomerInfo Customer { get; set; }
        /// <summary>
        /// Subscription related to the simcard
        /// </summary>
        public ResourceMBInfo Subscription { get; set; }
    }
}
