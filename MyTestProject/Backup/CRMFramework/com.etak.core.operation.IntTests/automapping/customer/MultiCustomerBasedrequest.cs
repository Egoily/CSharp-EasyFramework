using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.model.operation.contract.customer;
using com.etak.core.model.operation.messages;

namespace com.etak.core.operation.IntTests.automapping.customer
{
    public class MultiCustomerBasedrequest : RequestBase, IMultiCustomerRequestBased
    {
        public virtual IEnumerable<CustomerInfo> Customers { get; set; }
    }
}
 