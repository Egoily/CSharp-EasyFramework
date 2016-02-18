using com.etak.core.operation.contract;

namespace com.etak.vfes.implementation.bizOps.ChangeOfHolderBizOp
{
    /// <summary>
    ///  config class for ChangeOfHolder
    /// </summary>
    public class ChangeOfHolderConfiguration : BasicOperationConfiguration
    {
        /// <summary>
        /// MaxAmountTransfer
        /// </summary>
        public decimal MaxAmountTransfer { get; set; }
        /// <summary>
        /// PromotionGroupID
        /// </summary>
        public int PromotionGroupID { get; set; }
    }
}
