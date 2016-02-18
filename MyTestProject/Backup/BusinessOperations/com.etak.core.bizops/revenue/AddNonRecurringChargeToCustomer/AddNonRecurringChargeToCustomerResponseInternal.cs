using com.etak.core.model;
using com.etak.core.model.operation.contract.subscription;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;

namespace com.etak.core.bizops.revenue.AddNonRecurringChargeToCustomer
{
    /// <summary>
    /// ResponseInternal of AddNonRecurringChargeToCustomer
    /// </summary>
    public class AddNonRecurringChargeToCustomerResponseInternal : CreateNewOrderResponse,ISubscriptionBasedResponse
    {
        /// <summary>
        /// to send response CustomerCharge
        /// </summary>
        public CustomerCharge CustomerCharge;
        /// <summary>
        /// Subscription of the customer
        /// </summary>
        public ResourceMBInfo Subscription { get; set; }
    }
}
