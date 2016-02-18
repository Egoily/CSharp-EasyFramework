using com.etak.core.model.operation.contract.simcard;
using com.etak.core.model.operation.messages;

namespace com.etak.core.bizops.fullfilment.SwapSimCard
{
    /// <summary>
    /// SwapSimCardRequestDTO of SwapSimCardBizOp
    /// </summary>
    public class SwapSimCardRequestDTO : OrderRequestDTO, IICCIDBasedDTORequest
    {
        /// <summary>
        /// ICCID to get data source
        /// </summary>
        public string ICCID { get; set; }
        /// <summary>
        /// Msisdn to get resourceMbInfo
        /// </summary>
        public string Msisdn { get; set; }
        /// <summary>
        /// NewIccId to swap OldIccId
        /// </summary>
        public string NewIccId { get; set; }
        /// <summary>
        /// OldIccId to be swapped by NewIccId
        /// </summary>
        public string OldIccId { get; set; }
    }
}
