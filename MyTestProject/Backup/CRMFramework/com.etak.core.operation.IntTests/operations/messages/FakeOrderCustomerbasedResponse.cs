using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model.operation.contract.customer;
using com.etak.core.model.operation.messages;

namespace com.etak.core.operation.IntTests.operations.messages
{
    public class FakeOrderCustomerbasedResponse: CreateNewOrderResponse , ICustomerBasedResponse
    {
        public model.CustomerInfo Customer { get; set; }
        
    }
}
