using com.etak.core.model.operation.contract.customer;
using com.etak.core.model.operation.messages;

namespace com.etak.core.bizops.fullfilment.QueryCustomerProduct
{
    /// <summary>
    /// Input paramer for QueryCustomerProduct in DTO form.
    /// </summary>
    public class QueryCustomerProductRequestDTO : RequestBaseDTO, ICustomerIdBasedDTORequest
    {
        /// <summary>
        /// Id of the customer to query
        /// </summary>
        public int CustomerId { get; set; }
    }
}
