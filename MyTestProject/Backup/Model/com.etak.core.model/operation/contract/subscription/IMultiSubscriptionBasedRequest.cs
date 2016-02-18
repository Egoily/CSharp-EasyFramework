using System;
using System.Collections.Generic;


namespace com.etak.core.model.operation.contract.subscription
{
    /// <summary>
    /// Core request that is based on a subscription
    /// </summary>
    public interface IMultiSubscriptionBasedRequest
    {
        /// <summary>
        /// The list of subscriptions that the request is based on
        /// </summary>
        IEnumerable<ResourceMBInfo> Subscriptions { get; set; }

        /// <summary>
        /// The msisdn of the subscription that the request is based on
        /// </summary>
        String MSISDN { get; set; }
    }
}
