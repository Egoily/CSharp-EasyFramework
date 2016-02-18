using System;
using com.etak.core.model.operation.contract.customer;
using com.etak.core.model.operation.contract.subscription;
using com.etak.core.model.operation.messages;
using com.etak.core.model.operation;

namespace com.etak.core.bizops.revenue.QueryTransferences
{
    /// <summary>
    /// The request DTO for QueryTransferences
    /// </summary>
    public class QueryTransferencesRequestDTO : RequestBaseDTO, ICustomerIdBasedDTORequest, IMsisdnBasedDTORequest
    {
        /// <summary>
        /// Customer id
        /// </summary>
        public Int32 CustomerId { get; set; }

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
    }
}
