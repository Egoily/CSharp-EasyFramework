using System;
using com.etak.core.model.operation;

namespace com.etak.core.bizops.revenue.QueryTransferences
{
    /// <summary>
    /// Transfer Object to contain the information related with the Business Operation Execution performed and the msisdn for the donor and the subscriber
    /// </summary>
    public class TransferenceExecutionDTO
    {
        /// <summary>
        /// All the information related with the operation
        /// </summary>
        public BusinessOperationExecutionDTO Operation { get; set; }

        /// <summary>
        /// The msisdn of the Subscription informed in the operation object
        /// </summary>
        public String DonorMsisdn { get; set; }

        /// <summary>
        /// The msisdn of the SubscriptionDestination informed in the operation object
        /// </summary>
        public String ReceiverMsisdn { get; set; }

    }
}
