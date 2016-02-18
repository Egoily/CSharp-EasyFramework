using com.etak.core.model.operation.contract.customer;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;

namespace com.etak.core.operation.UnitTests.automapping.customer
{
    public class AccountBasedRequest : RequestBase, IAccountBasedRequest
    {
        public Account Account { get; set; }
    }
}
