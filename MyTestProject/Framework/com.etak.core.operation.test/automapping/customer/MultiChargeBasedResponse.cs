using System.Collections.Generic;
using com.etak.core.model.operation.contract.customer;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;

namespace com.etak.core.operation.test.automapping.customer
{
    public class MultiChargeBasedResponse : ResponseBase, IMultiChargeBasedResponse
    {
        public IEnumerable<Charge> Charges { get; set; }
    }
}
