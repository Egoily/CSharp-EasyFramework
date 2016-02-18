using com.etak.core.model;
using com.etak.core.model.operation.contract.customer;
using com.etak.core.model.operation.messages;

namespace com.etak.core.bizops.fullfilment.QuerySubscriptionByCustomerId
{
    /// <summary>
    /// QuerySubsriptionByCustomerId request in core model. 
    /// </summary>
    public class QuerySubscriptionByCustomerIdRequestInternal : RequestBase, ICustomerBasedRequest
    {
        /// <summary>
        /// The customer owning the subcription to look for
        /// </summary>
        public virtual CustomerInfo Customer { get; set; }

    }
}
