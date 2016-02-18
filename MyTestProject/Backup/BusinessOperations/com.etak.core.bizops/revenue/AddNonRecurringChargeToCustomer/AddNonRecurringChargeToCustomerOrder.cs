using com.etak.core.model.operation;

namespace com.etak.core.bizops.revenue.AddNonRecurringChargeToCustomer
{
    /// <summary>
    /// Order of AddNonRecurringChargeToCustomer
    /// </summary>
    public class AddNonRecurringChargeToCustomerOrder : Order
    {
        /// <summary>
        /// Discriminator of AddNonRecurringChargeToCustomerOrder
        /// </summary>
        public override string Discriminator
        {
            get { return OrderDiscriminators.AddNonRecurringChargeToCustomerOrder; }
        }
    }
}
