using com.etak.core.model;
using com.etak.core.model.operation.messages;

namespace com.etak.vfes.implementation.bizOps.ChangeOfHolderBizOp
{
    /// <summary>
    /// ChangeOfHolderResponseInternal
    /// </summary>
    public class ChangeOfHolderResponseInternal:CreateNewOrderResponse
    {
        /// <summary>
        /// the created new customer 
        /// </summary>
        public CustomerInfo Customer { get; set; }
        /// <summary>
        ///   the total amount that transferd from old holder to new holder
        /// </summary>
        public decimal BenefitTransferAmount { get; set; }
    }
}
