using com.etak.core.model;
using com.etak.core.model.operation.contract.subscription;
using com.etak.core.model.operation.messages;

namespace com.etak.core.bizops.revenue.ModifyCustomerCreditLimit
{
    /// <summary>
    /// Response Internal of ModifyCustomerCreditLimitBizOp
    /// </summary>
    public class ModifyCustomerCreditLimitResponseInternal : CreateNewOrderResponse,ISubscriptionBasedResponse
    {
        /// <summary>
        /// Customer's Subscription
        /// </summary>
        public ResourceMBInfo Subscription { get; set; }
    }
}
