using System;
using com.etak.core.model.operation.contract.customer;
using com.etak.core.model.operation.messages;
using com.etak.core.model.operation.contract.subscription;

namespace com.etak.core.bizops.revenue.QueryUsageDetails
{
    /// <summary>
    /// Request DTO of QueryUsageDetails
    /// </summary>
    public class QueryUsageDetailsRequestDTO : RequestBaseDTO, ICustomerIdBasedDTORequest, IMsisdnBasedDTORequest
    {
        /// <summary>
        /// Customer Id of the customer
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Msisdn that the customer uses
        /// </summary>
        public String MSISDN { get; set; }
        /// <summary>
        /// Date of start period
        /// </summary>
        public DateTime PeriodStartRange { get; set; }
        /// <summary>
        /// Date of end period
        /// </summary>
        public DateTime PeriodEndRange { get; set; }
        /// <summary>
        /// SubServiceTypeID 
        /// </summary>
        public Int32? SubServiceTypeID { get; set; }
        /// <summary>
        /// Filter Rule
        /// </summary>
        public Int32 FilterRule { get; set; }

    }
}
