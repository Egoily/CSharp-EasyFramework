using com.etak.core.model.operation.contract.subscription;
using com.etak.core.model.operation.messages;

namespace com.etak.core.operation.IntTests.automapping.subscription
{
    /// <summary>
    /// Sample class for QuerySubscriptionByCustomerId
    /// </summary>
    public class SubscriptionBasedDTOResponse : ResponseBaseDTO, ISubscriptionBasedDTOResponse
    {

        //public GSMSubscriptionDTO Subscription { get; set; }
        public model.subscription.SubscriptionDTO Subscription { get; set; }
       
    }
}
