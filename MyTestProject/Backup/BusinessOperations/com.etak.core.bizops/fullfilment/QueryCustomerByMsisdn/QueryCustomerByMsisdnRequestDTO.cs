using com.etak.core.model.operation.contract.subscription;
using com.etak.core.model.operation.messages;

namespace com.etak.core.bizops.fullfilment.QueryCustomerByMsisdn
{
    /// <summary>
    /// Request in DTO form of Query Customer By Exact Msisdn
    /// </summary>
    public class QueryCustomerByMsisdnRequestDTO : RequestBaseDTO, IMsisdnBasedDTORequest
    {
        /// <summary>
        /// Exact Msisdn to be queried
        /// </summary>
        public string MSISDN { get; set; }
    }
}
