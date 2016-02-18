using com.etak.core.model.subscription;


namespace com.etak.core.model.operation.contract.subscription
{
    /// <summary>
    /// DTO response that contains subcription information
    /// </summary>
    public interface ISubscriptionBasedDTOResponse
    {
        /// <summary>
        /// The subscription information of the response
        /// </summary>
        SubscriptionDTO Subscription { get; set; }
    }
}
