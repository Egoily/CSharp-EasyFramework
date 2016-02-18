using System;

namespace com.etak.core.model.operation.contract.amount
{
    /// <summary>
    /// DTO Request to use an Amount in the Operation
    /// </summary>
    public interface IAmountBasedDTORequest
    {
        /// <summary>
        /// The Amount needed in the Operation to be mapped 
        /// into the Core Request
        /// </summary>
        Decimal amount { get; set; }
    }
}
