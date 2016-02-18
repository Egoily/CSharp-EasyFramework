using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.etak.core.model.operation.contract.subscription
{
    public interface ISubscriptionLastActiveBasedRequest
    {
        /// <summary>
        /// The subscription that the request is based on
        /// </summary>
        ResourceMBInfo Subscription { get; set; }

        /// <summary>
        /// The msisdn of the subscription that the request is based on
        /// </summary>
        String MSISDN { get; set; }
    }
}
