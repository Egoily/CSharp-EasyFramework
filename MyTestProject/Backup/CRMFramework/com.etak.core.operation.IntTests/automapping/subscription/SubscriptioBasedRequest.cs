using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.model.operation.contract.subscription;
using com.etak.core.model.operation.messages;

namespace com.etak.core.operation.IntTests.automapping.subscription
{
    public class SubscriptioBasedRequest : RequestBase, ISubscriptionLastActiveBasedRequest
    {
        public virtual string MSISDN { get; set; }

        public ResourceMBInfo Subscription { get; set; }
    }
}
