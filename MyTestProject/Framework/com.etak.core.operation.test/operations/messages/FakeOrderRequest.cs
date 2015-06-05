using com.etak.core.model.operation.contract.customer;
using com.etak.core.model.operation.messages;

namespace com.etak.core.operation.test.operations.messages
{
    public class FakeOrderRequest : CreateNewOrderRequest, ICustomerBasedRequest
    {
        public virtual model.CustomerInfo Customer { get; set; }
    }
  
}
