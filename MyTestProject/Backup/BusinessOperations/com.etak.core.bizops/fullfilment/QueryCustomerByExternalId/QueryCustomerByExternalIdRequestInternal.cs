using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.model.operation.contract.customer;
using com.etak.core.model.operation.messages;

namespace com.etak.core.bizops.fullfilment.QueryCustomerByExternalId
{
    /// <summary>
    /// Request Internal of QueryCustomerByExternalIdBizOp
    /// </summary>
    public class QueryCustomerByExternalIdRequestInternal : RequestBase, IMultiCustomerRequestBased
    {
        /// <summary>
        /// Customers with certain external id
        /// </summary>
        public virtual IEnumerable<CustomerInfo> Customers { get; set; }



    }
}
