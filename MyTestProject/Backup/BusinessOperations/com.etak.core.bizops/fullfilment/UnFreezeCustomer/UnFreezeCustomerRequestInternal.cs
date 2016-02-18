
using com.etak.core.model;
using com.etak.core.model.operation.contract.subscription;
using com.etak.core.model.operation.messages;

namespace com.etak.core.bizops.fullfilment.UnFreezeCustomer
{
    /// <summary>
    /// Input request for UnFreezeCustomer input in CORE model
    /// </summary>
    public class UnFreezeCustomerRequestInternal : CreateNewOrderRequest, ISubscriptionLastActiveBasedRequest
    {

        /// <summary>
        /// Subscription related with msisdn
        /// </summary>
        public ResourceMBInfo Subscription { get; set; }

        /// <summary>
        /// Msisdn
        /// </summary>
        public string MSISDN { get; set; }

    }
}
