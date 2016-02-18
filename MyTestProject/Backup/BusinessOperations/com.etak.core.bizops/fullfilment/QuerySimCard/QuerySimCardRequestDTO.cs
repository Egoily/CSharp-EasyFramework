using com.etak.core.model.operation.contract.simcard;
using com.etak.core.model.operation.messages;

namespace com.etak.core.bizops.fullfilment.QuerySimCard
{
    /// <summary>
    /// Request DTO of QuerySimCard
    /// </summary>
    public class QuerySimCardRequestDTO : RequestBaseDTO, IICCIDBasedDTORequest
    {
        /// <summary>
        /// ICCID requested
        /// </summary>
        public string ICCID { get; set; }
    }
}
