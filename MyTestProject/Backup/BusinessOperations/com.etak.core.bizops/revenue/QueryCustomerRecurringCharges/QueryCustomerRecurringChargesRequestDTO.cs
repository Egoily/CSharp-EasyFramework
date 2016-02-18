using System;
using com.etak.core.model.operation.contract.customer;
using com.etak.core.model.operation.messages;

namespace com.etak.core.bizops.revenue.QueryCustomerRecurringCharges
{
    /// <summary>
    /// create QueryCustomerRecurringChargesRequestDTO
    /// </summary>
    public class QueryCustomerRecurringChargesRequestDTO : RequestBaseDTO, ICustomerIdBasedDTORequest
    {
        /// <summary>
        /// Id of the customer that did the purchases
        /// </summary>
        public Int32 CustomerId { get; set; }

        /// <summary>
        /// Start date of the range to query 
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// End date of the range to query 
        /// </summary>
        public DateTime EndDate { get; set; }
    }
}
