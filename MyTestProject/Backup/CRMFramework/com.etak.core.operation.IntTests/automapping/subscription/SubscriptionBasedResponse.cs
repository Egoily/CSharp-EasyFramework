using com.etak.core.model;
using com.etak.core.model.operation.contract.subscription;
using com.etak.core.model.operation.messages;

namespace com.etak.core.operation.IntTests.automapping.subscription
{
    /// <summary>
    /// Core request for 
    /// </summary>
    public class SubscriptionBasedResponse : ResponseBase, ISubscriptionBasedResponse
    {
        public ResourceMBInfo Subscription { get; set; }
    }
}
