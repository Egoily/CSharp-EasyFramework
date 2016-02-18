using com.etak.core.model.operation.contract.subscription;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;

namespace com.etak.core.operation.IntTests.automapping.subscription
{
    public class CustomerProductAssignmentBasedResponse : ResponseBase, ICustomerProductAssignmentBasedResponse
    {
        public CustomerProductAssignment CustomerProductAssignment { get; set; }
    }
}
