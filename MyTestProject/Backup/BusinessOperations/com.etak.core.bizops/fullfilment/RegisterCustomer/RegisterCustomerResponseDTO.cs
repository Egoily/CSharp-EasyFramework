using com.etak.core.model.dto;
using com.etak.core.model.operation.contract.customer;
using com.etak.core.model.operation.contract.subscription;
using com.etak.core.model.operation.messages;
using com.etak.core.model.subscription;

namespace com.etak.core.bizops.fullfilment.RegisterCustomer
{
    /// <summary>
    /// Dto Response for Register Operation
    /// </summary>
    public class RegisterCustomerResponseDTO : OrderResponseDTO, ICustomerBasedDTOResponse, ISubscriptionBasedDTOResponse
    {
        /// <summary>
        /// Customer in dto model information in the response
        /// </summary>
        public CustomerDTO Customer { get; set; }

        /// <summary>
        /// The subscription information of the response
        /// </summary>
        public SubscriptionDTO Subscription { get; set; }
    }
}
