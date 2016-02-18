using System;
using com.etak.core.model.operation.messages;
using com.etak.core.model.operation.contract.customer;

namespace com.etak.core.bizops.revenue.GetActiveProductsOfCustomer
{
    /// <summary>
    /// The request DTO for GetActiveProducsOfCustomer operation
    /// </summary>
    public class GetActiveProductsOfCustomerRequestDTO : RequestBaseDTO, ICustomerIdBasedDTORequest
    {
        /// <summary>
        /// Customer id
        /// </summary>
        public Int32 CustomerId { get; set; }
    }
}
