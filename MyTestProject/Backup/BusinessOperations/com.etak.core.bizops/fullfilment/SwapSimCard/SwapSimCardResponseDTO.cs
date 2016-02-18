using com.etak.core.model.dto;
using com.etak.core.model.operation.contract.customer;
using com.etak.core.model.operation.contract.subscription;
using com.etak.core.model.operation.messages;
using com.etak.core.model.resource;
using com.etak.core.model.subscription;

namespace com.etak.core.bizops.fullfilment.SwapSimCard
{
    /// <summary>
    /// SwapSimCardResponseDTO of SwapSimCardBizOp
    /// </summary>
    public class SwapSimCardResponseDTO : OrderResponseDTO,ICustomerBasedDTOResponse,ISubscriptionBasedDTOResponse
    {
        /// <summary>
        /// SimCardDTO to give response SimcardInfo
        /// </summary>
        public SimCardDTO SimCard { get; set; }
        /// <summary>
        /// Customer related to the simcard
        /// </summary>
        public CustomerDTO Customer { get; set; }
        /// <summary>
        /// subscription related to the simcard
        /// </summary>
        public SubscriptionDTO Subscription { get; set; }
    }
}
