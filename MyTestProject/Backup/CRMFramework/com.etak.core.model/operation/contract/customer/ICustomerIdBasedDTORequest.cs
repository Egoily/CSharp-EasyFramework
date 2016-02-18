using System;

namespace com.etak.core.model.operation.contract.customer
{
    /// <summary>
    /// DTO Request that is based on CustomerId
    /// </summary>
    public interface ICustomerIdBasedDTORequest
    {
        /// <summary>
        /// The Id of the customer that the request is based on
        /// </summary>
        Int32 CustomerId { get; set; }
    }
}
