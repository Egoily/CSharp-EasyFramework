using com.etak.core.model.revenueManagement;

namespace com.etak.core.model.operation.contract.subscription
{
    /// <summary>
    /// DTO request that is based on an input CustomerProductAssignment
    /// </summary>
    public interface ICustomerProductAssignmentBasedDTOResponse
    {
        /// <summary>
        /// The customerProductAssigmentDTO to be returned
        /// </summary>
        CustomerProductAssignmentDTO CustomerProductAssignment { get; set; }
    }
}
