using com.etak.core.model.dto;
using com.etak.core.model.operation.contract.customer;
using com.etak.core.model.operation.contract.subscription;
using com.etak.core.model.operation.messages;
using com.etak.core.model.subscription;

namespace com.etak.core.bizops.fullfilment.CreateCustomer
{
    /// <summary>
    /// The DTO Response for Create Customer
    /// </summary>
    public class CreateCustomerResponseDTO : OrderResponseDTO, ICustomerBasedDTOResponse,ISubscriptionBasedDTOResponse
    {
        /// <summary>
        /// The DTO Object that contains the information for the Customer
        /// </summary>
        public CustomerDTO Customer { get; set; }
        /// <summary>
        /// The DTO Object that contains the information for the customer Subscription
        /// </summary>
        public SubscriptionDTO Subscription { get; set; }
    }
}
