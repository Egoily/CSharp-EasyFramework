using System.Collections.Generic;
using com.etak.core.model.operation.messages;
using com.etak.core.model.resource;

namespace com.etak.core.bizops.fullfilment.QueryAvailableMsisdn
{
    /// <summary>
    /// Response DTO of QueryAvailableMsisdnBizOp
    /// </summary>
    public class QueryAvailableMsisdnResponseDTO : ResponseBaseDTO
    {
        /// <summary>
        /// List of MSISDNResourceDTO that is available
        /// </summary>
        public IList<MSISDNResourceDTO> AvailableMsisdns { get; set; }
    }
}
