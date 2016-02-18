using com.etak.core.model;
using com.etak.core.model.operation.contract.numberInfo;
using com.etak.core.model.operation.messages;

namespace com.etak.core.bizops.fullfilment.UnreserveMsisdn
{
    /// <summary>
    /// Request Internal of UnreserveMsisdnBizOp
    /// </summary>
    public class UnreserveMsisdnRequestInternal : CreateNewOrderRequest, INumberInfoBasedRequest
    {
        /// <summary>
        /// NumberInfo of the MSISDN
        /// </summary>
        public virtual NumberInfo NumberInPool { get; set; }
        /// <summary>
        /// DealerInfo of the MSISDN used for validation
        /// </summary>
        public virtual DealerInfo DealerInfo { get; set; }
        
    }
}
