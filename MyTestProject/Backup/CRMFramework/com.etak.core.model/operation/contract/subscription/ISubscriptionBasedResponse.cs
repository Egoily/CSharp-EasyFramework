
namespace com.etak.core.model.operation.contract.subscription
{
    /// <summary>
    /// Core response that contains subscription information
    /// </summary>
    public interface ISubscriptionBasedResponse
    {
        /// <summary>
        /// The subscription information of the response
        /// </summary>
        ResourceMBInfo Subscription { get; set; }
    }
}
