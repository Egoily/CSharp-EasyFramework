using com.etak.core.model.operation.contract.subscription;
using com.etak.core.model.operation.messages;

namespace com.etak.core.bizops.fullfilment.UnreserveMsisdn
{
    /// <summary>
    /// Request DTO of UnreserveMsisdnBizOp
    /// </summary>
    public class UnreserveMsisdnRequestDTO : OrderRequestDTO, IMsisdnBasedDTORequest
    {
        /// <summary>
        /// MSISDN to be unreserved
        /// </summary>
        public string MSISDN { get; set; }
    }
}
