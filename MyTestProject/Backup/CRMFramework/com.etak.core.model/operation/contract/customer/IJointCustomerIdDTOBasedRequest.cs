using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.etak.core.model.operation.contract.customer
{
    /// <summary>
    /// DTO Request based on two Customers in order to performe some transfer
    /// or a joint operation
    /// </summary>
    public interface IJointCustomerIdDTOBasedRequest
    {
        /// <summary>
        /// CustomerID which will be the source
        /// </summary>
        Int32 SourceCustomerId { get; set; }
        /// <summary>
        /// CustomerId which will be the destination
        /// </summary>
        Int32 DestinationCustomerId { get; set; }
    }
}
