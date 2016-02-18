using System;

namespace com.etak.core.model.operation.contract.subscription
{
    /// <summary>
    /// Interface with Core Objects to perform a joint operation
    /// between them
    /// </summary>
    public interface IJointSubscriptionBasedRequest
    {
        /// <summary>
        /// The Source Subscription to perform the operation
        /// </summary>
        ResourceMBInfo SourceSubscription { get; set; }

        /// <summary>
        /// The Msisdn corresponding to the source Subscription
        /// </summary>
        String SourceMSISDN { get; set; }

        /// <summary>
        /// The Destination Subscription to perform the operation
        /// </summary>
        ResourceMBInfo DestinationSubscription { get; set; }

        /// <summary>
        /// The Msisdn corresponding to the destination Subscription
        /// </summary>
        String DestinationMSISDN { get; set; }
    }
}
