using com.etak.core.model.dto;
using com.etak.core.model.operation.contract.subscription;
using com.etak.core.model.operation.messages;
using com.etak.core.model.subscription;

namespace com.etak.core.bizops.fullfilment.UpdateCustomerData
{
    /// <summary>
    /// DTO response return updated customer information after update is processed
    /// </summary>
    public class UpdateCustomerDataDTOResponse : OrderResponseDTO,ISubscriptionBasedDTOResponse
    {
        /// <summary>
        /// Customer information after update is processed
        /// </summary>
        public CustomerDTO Customer { get; set; }
        /// <summary>
        /// Subscription related to customer
        /// </summary>
        public SubscriptionDTO Subscription { get; set; }
    }
}
