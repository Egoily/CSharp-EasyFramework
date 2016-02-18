using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.model.operation.messages;

namespace com.etak.core.bizops.fullfilment.QueryAvailableMsisdn
{
    /// <summary>
    /// Request Internal of QueryAvailableMsisdnBizOp
    /// </summary>
    public class QueryAvailableMsisdnRequestInternal : RequestBase
    {
        /// <summary>
        /// Available Number
        /// </summary>
        public virtual IEnumerable<NumberInfo> NumberInfos { get; set; }
    }
}
