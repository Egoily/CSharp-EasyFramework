
namespace com.etak.core.model.operation.contract.customer
{
    /// <summary>
    /// Public Interface with Core Objects to perform a transfer or 
    /// a joint operation between two customers
    /// </summary>
    public interface IJointCustomerBasedRequest
    {
        /// <summary>
        /// Source Customer to perform the operation
        /// </summary>
        CustomerInfo SourceCustomerInfo { get; set; }
        /// <summary>
        /// Destination Customer to perform the operation
        /// </summary>
        CustomerInfo DestinationCustomerInfo { get; set; }
    }
}
