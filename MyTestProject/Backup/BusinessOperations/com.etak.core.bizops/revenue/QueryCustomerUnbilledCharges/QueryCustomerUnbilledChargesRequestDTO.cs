using com.etak.core.model.operation.contract.customer;
using com.etak.core.model.operation.messages;

namespace com.etak.core.bizops.revenue.QueryCustomerUnbilledCharges
{
    /// <summary>
    /// Request DTO of QueryCustomerUnbilledCharges
    /// </summary>
    public class QueryCustomerUnbilledChargesRequestDTO : RequestBaseDTO, ICustomerIdBasedDTORequest
    {
        /// <summary>
        /// CustomerId requested
        /// </summary>
        public int CustomerId { get; set; }
    }
}
