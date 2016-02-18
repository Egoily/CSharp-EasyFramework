
using com.etak.core.model.operation.contract.customer;
using com.etak.core.model.operation.messages;

namespace com.etak.core.bizops.fullfilment.FreezeCustomer
{
    /// <summary>
    /// Class for FreezeCustomer response  in DTO model
    /// </summary>
    public class FreezeCustomerResponseDTO : OrderResponseDTO, ICustomerBasedDTOResponse
    {
        /// <summary>
        /// IBased Customer
        /// </summary>
        public model.dto.CustomerDTO Customer { get; set; }
    }
}
