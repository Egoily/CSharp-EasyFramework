using com.etak.core.model.operation.messages;

namespace com.etak.core.bizops.fullfilment.QueryAvailableMsisdn
{
    /// <summary>
    /// Request DTO of QueryAvailableMsisdnBizOp
    /// </summary>
    public class QueryAvailableMsisdnRequestDTO : RequestBaseDTO
    {
        /// <summary>
        /// Quantity of Msisdn requested
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// CategoryId of Msisdn
        /// </summary>
        public int CategoryId { get; set; }
    }
}
