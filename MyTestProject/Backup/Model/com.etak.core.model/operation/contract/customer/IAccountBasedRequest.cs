using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model.revenueManagement;

namespace com.etak.core.model.operation.contract.customer
{
    /// <summary>
    /// Request based on an Account
    /// </summary>
    public interface IAccountBasedRequest
    {
        /// <summary>
        /// Account needed in the operation
        /// </summary>
        Account Account { get; set; }
    }
}
