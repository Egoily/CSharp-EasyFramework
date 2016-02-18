using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.model.operation.contract.customer;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;

namespace com.etak.core.bizops.fullfilment.QueryPurchasedProductsByCustomerId
{
    /// <summary>
    /// Internal Request for QueryPurchasedProductsByCustomerId
    /// </summary>
    public class QueryPurchasedProductsByCustomerIdRequestInternal : RequestBase, ICustomerBasedRequest
    {
        /// <summary>
        /// Information on customer
        /// </summary>
        public virtual CustomerInfo Customer { get; set; }

        /// <summary>
        /// List of Customer product assignments
        /// </summary>
        public virtual IList<CustomerProductAssignment> CustomerProductAssignments { get; set; }
    }
}
