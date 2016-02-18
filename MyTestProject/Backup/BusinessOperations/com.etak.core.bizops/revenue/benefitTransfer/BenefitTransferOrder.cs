using com.etak.core.model.operation;

namespace com.etak.core.bizops.revenue.BenefitTransfer
{
    /// <summary>
    /// Order produced in the benefit transfer operation
    /// </summary>
    public class BenefitTransferOrder : Order
    {
        /// <summary>
        /// Discriminator
        /// </summary>
        public override string Discriminator
        {
            get { return OrderDiscriminators.BenefitTransferOrder; }
        }
    }
}
