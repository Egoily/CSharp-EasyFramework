using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.etak.core.model.operation.contract.customer
{
    /// <summary>
    /// DTO Request based on an Account
    /// </summary>
    public interface IAccountIdBasedDTORequest
    {
        /// <summary>
        /// The Account Id to be used in the operation
        /// </summary>
        Int64 AccountId { get; set; }
    }
}
