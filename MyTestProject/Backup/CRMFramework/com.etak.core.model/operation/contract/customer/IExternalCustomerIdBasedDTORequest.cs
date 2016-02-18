using System;

namespace com.etak.core.model.operation.contract.customer
{
    /// <summary>
    /// DTO request that is based on an external customer id
    /// </summary>
    public interface IExternalCustomerIdBasedDTORequest
    {
        /// <summary>
        /// The external customer id that the request is based on.
        /// </summary>
        String ExternalCustomerId { get; set; }
    }
}
