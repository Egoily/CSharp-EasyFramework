using com.etak.core.model.dto;
using com.etak.core.model.operation.contract.customer;
using com.etak.core.model.operation.messages;

namespace com.etak.core.bizops.fullfilment.QueryCustomerByMsisdn
{
    /// <summary>
    /// Response in DTO form of Query Customer By Exact Msisdn
    /// </summary>
    public class QueryCustomerByMsisdnResponseDTO : ResponseBaseDTO, ICustomerBasedDTOResponse
    {
        /// <summary>
        /// Customer with given msisdn
        /// </summary>
        public CustomerDTO Customer { get; set; }
    }
}
