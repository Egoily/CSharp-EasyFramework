using com.etak.core.model;
using com.etak.core.model.operation.contract.subscription;
using com.etak.core.model.operation.messages;

namespace com.etak.core.test.utilitiesTests.BizOpTest
{
    public class BizOpTestRequestInternal : RequestBase, ISubscriptionBasedRequest
    {
        public ResourceMBInfo Subscription { get; set; }
        public string MSISDN { get; set; }
    }
}
