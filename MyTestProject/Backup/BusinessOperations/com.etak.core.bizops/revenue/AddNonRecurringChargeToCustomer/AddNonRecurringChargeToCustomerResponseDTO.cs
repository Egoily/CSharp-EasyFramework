using com.etak.core.model;
using com.etak.core.model.operation.contract.subscription;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;
using com.etak.core.model.subscription;

namespace com.etak.core.bizops.revenue.AddNonRecurringChargeToCustomer
{
    /// <summary>
    /// ResponseDTO of AddNonRecurringChargeToCustomer
    /// </summary>
    public class AddNonRecurringChargeToCustomerResponseDTO : OrderResponseDTO,ISubscriptionBasedDTOResponse
    {
        /// <summary>
        /// CustomerChargeDTO for return CustomerCharge
        /// </summary>
        public CustomerChargeDTO CustomerCharge { get; set; }
        /// <summary>
        /// Subscription of the customer
        /// </summary>
        public SubscriptionDTO Subscription { get; set; }
    }
}
