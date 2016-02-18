using com.etak.core.model.dto;
using com.etak.core.model.operation.contract.customer;
using com.etak.core.model.operation.contract.subscription;
using com.etak.core.model.operation.messages;
using com.etak.core.model.subscription;

namespace com.etak.core.bizops.revenue.ChargeService
{
    /// <summary>
    /// ChargeServiceResponseDTO
    /// </summary>
    public class ChargeServiceResponseDTO: ResponseBaseDTO,ICustomerBasedDTOResponse,ISubscriptionBasedDTOResponse
    {
        /// <summary>
        /// Customer charged
        /// </summary>
        public CustomerDTO Customer { get; set; }
        /// <summary>
        /// Subscription charged
        /// </summary>
        public SubscriptionDTO Subscription { get; set; }
    }
}
