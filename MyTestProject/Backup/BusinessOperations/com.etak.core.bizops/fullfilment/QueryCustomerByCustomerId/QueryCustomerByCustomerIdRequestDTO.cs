using com.etak.core.model.operation.contract.customer;
using com.etak.core.model.operation.messages;

namespace com.etak.core.bizops.fullfilment.QueryCustomerByCustomerId
{
    /// <summary>
    /// Input paramer for QueryCustomerByCustomerId in DTO form.
    /// </summary>
    public class QueryCustomerByCustomerIdRequestDTO : RequestBaseDTO, ICustomerIdBasedDTORequest
    {
        /// <summary>
        /// Tte Id of the customer to query
        /// </summary>
        public int CustomerId { get; set; }
    }
}
