using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.model.operation.messages;

namespace com.etak.core.bizops.fullfilment.QueryAvailableMsisdn
{
    /// <summary>
    /// Response Internal of QueryAvailableMsisdnBizOp
    /// </summary>
    public class QueryAvailableMsisdnResponseInternal : ResponseBase
    {
        /// <summary>
        /// Available Msisdn
        /// </summary>
        public virtual IEnumerable<NumberInfo> NumberInfos { get; set; }
       
    }
}
