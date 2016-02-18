using System.Collections.Generic;
using com.etak.core.model.dto;
using com.etak.core.model.operation.contract.customer;
using com.etak.core.model.operation.messages;

namespace com.etak.core.bizops.fullfilment.CancelCustomerAndSubscriptions
{
    /// <summary>
    /// Class for CancelCustomerAndSubscriptions response  in DTO model 
    /// </summary>
    public class CancelCustomerAndSubscriptionsResponseDTO : OrderResponseDTO,ICustomerBasedDTOResponse
    {
        /// <summary>
        /// Customer to be cancelled
        /// </summary>
        public CustomerDTO Customer { get; set; }
    }
}
