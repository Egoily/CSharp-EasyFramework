using System;
using com.etak.core.model.operation.contract.customer;
using com.etak.core.model.operation.messages;

namespace com.etak.core.operation.test.operations.messages
{
    public class FakeOrderRequestDTO : OrderRequestDTO, ICustomerIdBasedDTORequest
    {
        public String MyProperty { get; set; }
        public String MyField;
        public String MyProperty2 { get; set; }
        public String MyField2;
        public int CustomerId { get; set; }
    }
}
