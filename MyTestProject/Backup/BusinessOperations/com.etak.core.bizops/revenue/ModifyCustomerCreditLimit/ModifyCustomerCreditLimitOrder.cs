using com.etak.core.model.operation;

namespace com.etak.core.bizops.revenue.ModifyCustomerCreditLimit
{
    /// <summary>
    /// Order of ModifyCustomerCreditLimitBizOp
    /// </summary>
    public class ModifyCustomerCreditLimitOrder : Order
    {
        /// <summary>
        /// Discriminator of ModifyCustomerCreditLimitOrder
        /// </summary>
        public override string Discriminator
        {
            get { return OrderDiscriminators.ModifyCustomerCreditLimitOrder; }
        }
    }
}
