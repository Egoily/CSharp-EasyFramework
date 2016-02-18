using com.etak.core.model;
using com.etak.core.model.operation.contract.simcard;
using com.etak.core.model.operation.messages;

namespace com.etak.core.bizops.fullfilment.SwapSimCard
{
    /// <summary>
    /// SwapSimCardRequestInternal of SwapSimCardBizOp
    /// </summary>
    public class SwapSimCardRequestInternal : CreateNewOrderRequest, ISimCardBasedRequest
    {
        /// <summary>
        /// Msisdn to get resourceMbInfo
        /// </summary>
        public virtual string Msisdn { get; set; }
        /// <summary>
        /// get data SimcardInfo from framework
        /// </summary>
        public virtual SIMCardInfo SimCard { get; set; }
        /// <summary>
        /// DestinationSim to store new info with new iccid
        /// </summary>
        public virtual SIMCardInfo DestinationSim { get; set; }
        /// <summary>
        /// ResourceMBInfos to store data of resource
        /// </summary>
        public virtual ResourceMBInfo ResourceMBInfos { get; set; }
 
    }
}
