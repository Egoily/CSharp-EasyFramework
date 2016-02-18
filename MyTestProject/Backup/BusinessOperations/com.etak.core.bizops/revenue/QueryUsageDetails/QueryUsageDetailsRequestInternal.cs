using System;
using com.etak.core.model;
using com.etak.core.model.operation.contract.customer;
using com.etak.core.model.operation.messages;
using com.etak.core.model.operation.contract.subscription;

namespace com.etak.core.bizops.revenue.QueryUsageDetails
{
    /// <summary>
    /// Request Internal of QueryUsageDetailsBizOp
    /// </summary>
    public class QueryUsageDetailsRequestInternal : RequestBase, ICustomerBasedRequest, ISubscriptionLastActiveBasedRequest
    {
        /// <summary>
        /// Customer information
        /// </summary>
        public virtual CustomerInfo Customer { get; set; }
        /// <summary>
        /// Msisdn that the customer uses
        /// </summary>
        public virtual String MSISDN { get; set; }
        /// <summary>
        /// Date of start period
        /// </summary>
        public virtual DateTime PeriodStartRange { get; set; }
        /// <summary>
        /// Date of end period
        /// </summary>
        public virtual DateTime PeriodEndRange { get; set; }
        /// <summary>
        /// SubServiceTypeID 
        /// </summary>
        public virtual Int32? SubServiceTypeID { get; set; }
        /// <summary>
        /// Filter Rule
        /// </summary>
        public virtual Int32 FilterRule { get; set; }

        /// <summary>
        /// subscription information of MSISDN
        /// </summary>
        public ResourceMBInfo Subscription { get; set; }
    }
}
