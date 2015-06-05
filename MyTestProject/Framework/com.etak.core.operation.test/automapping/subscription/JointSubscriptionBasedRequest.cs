using com.etak.core.model.operation.contract.subscription;
using com.etak.core.model.operation.messages;

namespace com.etak.core.operation.test.automapping.subscription
{
    public class JointSubscriptionBasedRequest : RequestBase, IJointSubscriptionBasedRequest
    {
        public string DestinationMSISDN { get; set; }

        public model.ResourceMBInfo DestinationSubscription { get; set; }

        public string SourceMSISDN { get; set; }

        public model.ResourceMBInfo SourceSubscription { get; set; }
    }
}
