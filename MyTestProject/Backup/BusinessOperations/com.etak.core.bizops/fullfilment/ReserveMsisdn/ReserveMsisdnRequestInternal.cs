using com.etak.core.model;
using com.etak.core.model.operation.contract.numberInfo;
using com.etak.core.model.operation.messages;

namespace com.etak.core.bizops.fullfilment.ReserveMsisdn
{
    /// <summary>
    /// ReserveMsisdnRequest internal, with core objects, based on INumberInfoBasedRequest
    /// </summary>
    public class ReserveMsisdnRequestInternal : CreateNewOrderRequest, INumberInfoBasedRequest
    {
        /// <summary>
        /// The Number to be Reserved
        /// </summary>
        public virtual NumberInfo NumberInPool { get; set; }
        /// <summary>
        /// Dealer information corresponding to the owner of the number
        /// </summary>
        public virtual DealerInfo DealerInfo { get; set; }

    }
}
