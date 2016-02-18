
using com.etak.core.model.operation;

namespace com.etak.core.bizops.fullfilment.UnFreezeCustomer
{
    /// <summary>
    /// Order produced in UnFreezeCustomer operation
    /// </summary>
    public class UnFreezeCustomerOrder : Order
    {
        /// <summary>
        /// Discriminator
        /// </summary>
        public override string Discriminator
        {
            get { return OrderDiscriminators.UnFreezeCustomer; }
        }
    }
}
