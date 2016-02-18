using com.etak.core.model.operation.contract.customer;
using com.etak.core.model.operation.messages;

namespace com.etak.core.bizops.fullfilment.QueryCustomerByExternalId
{
    /// <summary>
    /// Request DTO of QueryCustomerByExternalIdBizOp
    /// </summary>
    public class QueryCustomerByExternalIdRequestDTO : RequestBaseDTO, IExternalCustomerIdBasedDTORequest
    {
        /// <summary>
        /// External Customer Id to be requested
        /// </summary>
        public string ExternalCustomerId { get; set; }
    }
}
