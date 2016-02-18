using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.model.operation.contract.customer;
using com.etak.core.model.operation.messages;

namespace com.etak.core.bizops.fullfilment.QueryCustomerByDocumentId
{
    /// <summary>
    /// Request Internal of QueryCustomerByDocumentIdBizOp
    /// </summary>
    public class QueryCustomerByDocumentIdRequestInternal : RequestBase, IMultiCustomerRequestBased
    {
        /// <summary>
        /// A list of Customers with the Document Id Specified
        /// </summary>
        public IEnumerable<CustomerInfo> Customers { get; set; }
    }
}
