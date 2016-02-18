using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model.operation.messages;

namespace com.etak.core.operation.IntTests.operations.messages
{
    /// <summary>
    /// Special Request not based on Customerbased
    /// </summary>
    public class FakeOrderNoCustomerbasedRequest: CreateNewOrderRequest
    {
        public virtual model.CustomerInfo Customer { get; set; }
    }
}
