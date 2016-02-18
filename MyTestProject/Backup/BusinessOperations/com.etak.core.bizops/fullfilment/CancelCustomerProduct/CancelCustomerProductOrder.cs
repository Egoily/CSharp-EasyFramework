using com.etak.core.model.operation;

namespace com.etak.core.bizops.fullfilment.CancelCustomerProduct
{
    /// <summary>
    /// Order produced in the cancel customer product operation
    /// </summary>
    public class CancelCustomerProductOrder : Order
    {
        /// <summary>
        /// Discriminator
        /// </summary>
        public override string Discriminator
        {
            get { return OrderDiscriminators.CancelCustomerProduct; }
        }
    }
}
