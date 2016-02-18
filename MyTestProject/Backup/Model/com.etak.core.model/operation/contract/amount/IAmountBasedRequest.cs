using System;

namespace com.etak.core.model.operation.contract.amount
{
    /// <summary>
    /// Interface to perform an operation with a certain amount
    /// </summary>
    public interface IAmountBasedRequest
    {
        /// <summary>
        /// The Amount needed in the Operation
        /// </summary>
        Decimal Amount { get; set; }
    }
}
