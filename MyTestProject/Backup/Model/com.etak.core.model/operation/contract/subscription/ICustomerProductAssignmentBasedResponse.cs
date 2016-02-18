using com.etak.core.model.revenueManagement;

namespace com.etak.core.model.operation.contract.subscription
{
    /// <summary>
    /// Interface to implement a CustomerProductAssignment Based Response
    /// </summary>
    public interface ICustomerProductAssignmentBasedResponse
    {
        /// <summary>
        /// The CustomerProductAssignment to be returned
        /// </summary>
        CustomerProductAssignment CustomerProductAssignment { get; set; }
    }
}
