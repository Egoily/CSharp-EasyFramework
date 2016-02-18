using com.etak.core.model.dto;
using com.etak.core.model.operation.contract.customer;
using com.etak.core.model.operation.messages;

namespace com.etak.core.bizops.fullfilment.UpdateCustomerData
{
    /// <summary>
    /// DTO Request with customer information to be updated
    /// </summary>
    public class UpdateCustomerDataDTORequest: OrderRequestDTO, ICustomerIdBasedDTORequest
    {
        /// <summary>
        /// Customer information to be used in update process
        /// </summary>
        public CustomerDTO Customer { get; set; }
        /// <summary>
        /// Customer Id information of customer
        /// </summary>
        public int CustomerId { get; set; }
        
    }
}
