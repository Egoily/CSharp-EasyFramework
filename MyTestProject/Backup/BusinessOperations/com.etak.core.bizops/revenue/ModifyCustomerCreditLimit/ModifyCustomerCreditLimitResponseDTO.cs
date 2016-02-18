using com.etak.core.model.operation.contract.subscription;
using com.etak.core.model.operation.messages;
using com.etak.core.model.subscription;

namespace com.etak.core.bizops.revenue.ModifyCustomerCreditLimit
{
    /// <summary>
    /// Order Response DTO of ModifyCustomerCreditLimitBizOp
    /// </summary>
    public class ModifyCustomerCreditLimitResponseDTO : OrderResponseDTO,ISubscriptionBasedDTOResponse
    {
        /// <summary>
        /// Customer's Subscription
        /// </summary>
        public SubscriptionDTO Subscription { get; set; }
    }
}
