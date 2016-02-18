using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.etak.core.model.operation.contract.amount
{
    /// <summary>
    /// Interface for Amount Based Response, having a Property to set the properly amount
    /// </summary>
    public interface IAmountBasedResponse
    {
        /// <summary>
        /// The Amount to be returned
        /// </summary>
        Decimal Amount { get; set; }
    }
}
