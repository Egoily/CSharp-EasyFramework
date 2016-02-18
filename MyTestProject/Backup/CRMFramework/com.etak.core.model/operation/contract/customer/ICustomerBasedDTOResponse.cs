using System.Security.Cryptography.X509Certificates;
using com.etak.core.model.dto;

namespace com.etak.core.model.operation.contract.customer
{
    /// <summary>
    /// DTO Response for operations that return a CustomerDTO
    /// </summary>
    public interface ICustomerBasedDTOResponse 
    {
        /// <summary>
        /// Customer in dto model information in the response
        /// </summary>
        CustomerDTO Customer { get; set; }
    }
}
