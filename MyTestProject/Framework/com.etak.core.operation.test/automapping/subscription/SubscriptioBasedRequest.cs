using com.etak.core.model;
using com.etak.core.model.operation.contract.subscription;
using com.etak.core.model.operation.messages;

namespace com.etak.core.operation.test.automapping.subscription
{
    public class SubscriptioBasedRequest : RequestBase, ISubscriptionBasedRequest
    {
        public virtual string MSISDN { get; set; }

        public virtual ResourceMBInfo Subscription { get; set; }
    }
}
