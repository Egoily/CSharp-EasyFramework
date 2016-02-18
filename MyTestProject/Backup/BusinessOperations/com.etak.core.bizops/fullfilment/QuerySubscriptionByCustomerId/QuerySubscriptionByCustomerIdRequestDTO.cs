using com.etak.core.model.operation.contract.customer;
using com.etak.core.model.operation.messages;

namespace com.etak.core.bizops.fullfilment.QuerySubscriptionByCustomerId
{
    /// <summary>
    /// Request in DTO form of the QuerySubsriptionByCustomerId bussiness operation
    /// </summary>
    public class QuerySubscriptionByCustomerIdRequestDTO : RequestBaseDTO, ICustomerIdBasedDTORequest
    {
        /// <summary>
        /// Id of the customer, the owner of the subscription to retrieve
        /// </summary>
        public int CustomerId { get; set; }
    }
}
