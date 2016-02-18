using com.etak.core.model;
using com.etak.core.model.operation.contract.customer;
using com.etak.core.model.operation.messages;

namespace com.etak.core.operation.IntTests.automapping.customer
{
    public class CustomerBasedResponse : ResponseBase, ICustomerBasedResponse
    {
        public virtual CustomerInfo Customer { get; set; }
    }
}
