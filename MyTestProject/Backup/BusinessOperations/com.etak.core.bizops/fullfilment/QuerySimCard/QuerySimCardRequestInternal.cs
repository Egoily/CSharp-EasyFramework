using com.etak.core.model;
using com.etak.core.model.operation.contract.simcard;
using com.etak.core.model.operation.messages;

namespace com.etak.core.bizops.fullfilment.QuerySimCard
{
    /// <summary>
    /// Request Internal of QuerySimCard
    /// </summary>
    public class QuerySimCardRequestInternal : RequestBase, ISimCardBasedRequest
    {
        /// <summary>
        /// SIMCardInfo
        /// </summary>
        public virtual SIMCardInfo SimCard { get; set; }

    }
}
