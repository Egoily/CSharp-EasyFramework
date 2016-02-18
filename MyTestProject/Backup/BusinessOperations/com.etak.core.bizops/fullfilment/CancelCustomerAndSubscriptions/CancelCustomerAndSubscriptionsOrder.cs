using com.etak.core.model.operation;

namespace com.etak.core.bizops.fullfilment.CancelCustomerAndSubscriptions
{
    /// <summary>
    /// Order produced in the cancel customer and subscriptions operation
    /// </summary>
    public class CancelCustomerAndSubscriptionsOrder : Order
    {
        /// <summary>
        /// Discriminator
        /// </summary>
        public override string Discriminator
        {
            get { return OrderDiscriminators.CancelCustomerAndSubscription; }
        }
    }
}
