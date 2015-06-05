using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model.operation.contract.customer;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;

namespace com.etak.core.operation.test.automapping.customer
{
    public class ChargeBasedResponse : ResponseBase, IChargeBasedResponse
    {


        public Charge Charge { get; set; }
    }
}
