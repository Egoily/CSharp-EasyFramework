using System.Collections.Generic;


namespace com.etak.core.model.operation.contract.customer
{
    public interface IMultiCustomerRequestBased
    {
        IEnumerable<CustomerInfo> Customers { get; set; }
    }
}
