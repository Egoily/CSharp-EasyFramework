using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.model.operation.contract.customer;
using com.etak.core.model.operation.contract.subscription;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;

namespace com.etak.core.bizops.revenue.ChargeService
{
    /// <summary>
    /// ChargeServiceResponseInternal
    /// </summary>
    public class ChargeServiceResponseInternal: ResponseBase, ICustomerBasedResponse, ISubscriptionBasedResponse
    {
        /// <summary>
        /// CustomerCharges List
        /// </summary>
        public virtual List<CustomerCharge> CustomerCharges { get; set; }
        /// <summary>
        /// Customer that is charged
        /// </summary>
        public CustomerInfo Customer { get; set; }
        /// <summary>
        /// Subscription that is charged
        /// </summary>
        public ResourceMBInfo Subscription { get; set; }
    }
}
