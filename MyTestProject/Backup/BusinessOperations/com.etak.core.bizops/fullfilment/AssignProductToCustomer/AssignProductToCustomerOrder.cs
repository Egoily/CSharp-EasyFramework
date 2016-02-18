using com.etak.core.model.operation;

namespace com.etak.core.bizops.fullfilment.AssignProductToCustomer
{
    /// <summary>
    /// AssignProductToCustomerOrder for individual product assignmebt to customer
    /// </summary>
    public class AssignProductOfferingToCustomerOrder : Order
    {
        /// <summary>
        /// Discriminator for PurchaseProductForCustomerOrder
        /// </summary>
        public override string Discriminator
        {
            get { return (OrderDiscriminators.AssignProductToCustomerOrder); }
        }
    }
}
