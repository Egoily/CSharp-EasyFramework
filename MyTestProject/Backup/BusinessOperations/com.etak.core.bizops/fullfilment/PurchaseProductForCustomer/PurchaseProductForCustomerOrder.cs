using com.etak.core.model.operation;

namespace com.etak.core.bizops.fullfilment.PurchaseProductForCustomer
{
    /// <summary>
    /// Order for PurchaseProductForCustomer
    /// </summary>
    public class PurchaseProductForCustomerOrder: Order
    {
        /// <summary>
        /// Discriminator for PurchaseProductForCustomerOrder
        /// </summary>
        public override string Discriminator
        {
            get { return (OrderDiscriminators.PurchaseProductOperation); }
        }
    }
}
