using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model;
using com.etak.core.model.operation.messages;

namespace com.etak.core.bizops.Temporal_AnnaM
{
    /// <summary>
    /// GetPriorityBundleInfoFromBundleInfosResponse
    /// </summary>
    public class GetPriorityBundleInfoFromBundleInfosResponse: ResponseBase
    {
        /// <summary>
        /// PriorityBundleInfo with info for credit limit (Priority one)
        /// </summary>
        public BundleInfo PriorityBundleInfo { get; set; }
    }
}
