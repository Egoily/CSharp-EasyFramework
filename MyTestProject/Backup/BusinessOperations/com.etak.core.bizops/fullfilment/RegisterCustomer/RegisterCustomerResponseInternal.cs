using com.etak.core.model;
using com.etak.core.model.operation.contract.customer;
using com.etak.core.model.operation.contract.subscription;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;

namespace com.etak.core.bizops.fullfilment.RegisterCustomer
{
    /// <summary>
    /// Response for the Register Operation that contains the Core Objects generated
    /// </summary>
    public class RegisterCustomerResponseInternal : CreateNewOrderResponse, ICustomerBasedResponse, ISubscriptionBasedResponse
    {
        /// <summary>
        /// Customer created in the registration process
        /// </summary>
        public virtual CustomerInfo Customer { get; set; }

        /// <summary>
        /// Subscription created in the registration process
        /// </summary>
        public virtual ResourceMBInfo Subscription { get; set; }

        /// <summary>
        /// The invoice created
        /// </summary>
        public virtual Invoice Invoice { get; set; }
    }
}
