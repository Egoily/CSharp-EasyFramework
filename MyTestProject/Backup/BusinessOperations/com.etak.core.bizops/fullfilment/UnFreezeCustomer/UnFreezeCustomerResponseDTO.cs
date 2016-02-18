
using com.etak.core.model.operation.contract.customer;
using com.etak.core.model.operation.messages;

namespace com.etak.core.bizops.fullfilment.UnFreezeCustomer
{
    /// <summary>
    /// Class for UnFreezeCustomer response  in DTO model
    /// </summary>
    public class UnFreezeCustomerResponseDTO : OrderResponseDTO, ICustomerBasedDTOResponse
    {
        /// <summary>
        /// CustomerBased 
        /// </summary>
        public model.dto.CustomerDTO Customer { get; set; }
    }
}
