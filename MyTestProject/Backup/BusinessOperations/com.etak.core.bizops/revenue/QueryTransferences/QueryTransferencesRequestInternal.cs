using System;
using com.etak.core.model;
using com.etak.core.model.operation.contract.customer;
using com.etak.core.model.operation.contract.subscription;
using com.etak.core.model.operation.messages;
using com.etak.core.model.operation;

namespace com.etak.core.bizops.revenue.QueryTransferences
{
    /// <summary>
    /// The internal request for QueryTransferences
    /// </summary>
    public class QueryTransferencesRequestInternal : RequestBase, ICustomerBasedRequest, ISubscriptionLastActiveBasedRequest
    {
        /// <summary>
        /// Customer information
        /// </summary>
        public CustomerInfo Customer { get; set; }

        /// <summary>
        /// MSISDN requested
        /// </summary>
        public string MSISDN { get; set; }

        /// <summary>
        /// The start date in which this association begins
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// The end date in which this association ends
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// The operation definition object to QueryTransferences
        /// </summary>
        public BusinessOperation OperationDefinition { get; set; }

        /// <summary>
        /// The subscription of the given msisdn
        /// </summary>
        public ResourceMBInfo Subscription { get; set; }
    }
}
