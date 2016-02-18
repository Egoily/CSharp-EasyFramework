using System;

using com.etak.core.model.operation.contract.customer;
using com.etak.core.model.operation.messages;

namespace com.etak.core.bizops.fullfilment.QueryPurchasedProductsByCustomerId
{
    /// <summary>
    /// Input parameter for QueryPurchasedProductByCustomerId
    /// </summary>
    public class QueryPurchasedProductsByCustomerIdRequestDTO : RequestBaseDTO, ICustomerIdBasedDTORequest
    {
        /// <summary>
        /// Id of customer to query
        /// </summary>
        public Int32 CustomerId { get; set; }
        /// <summary>
        /// Start date of the range to query 
        /// </summary>
        public DateTime StartDate{ get; set; }

        /// <summary>
        /// End date of the range to query 
        /// </summary>
        public DateTime EndDate { get; set; }
    }
}
