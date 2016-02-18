
using com.etak.core.model.operation;

namespace com.etak.core.bizops.fullfilment.FreezeCustomer
{
    /// <summary>
    /// Order produced in FreezeCustomer operation
    /// </summary>
    public class FreezeCustomerOrder : Order
    {
        /// <summary>
        /// Discriminator
        /// </summary>
        public override string Discriminator
        {
            get { return OrderDiscriminators.FreezeCustomer; }
        }
    }
}
