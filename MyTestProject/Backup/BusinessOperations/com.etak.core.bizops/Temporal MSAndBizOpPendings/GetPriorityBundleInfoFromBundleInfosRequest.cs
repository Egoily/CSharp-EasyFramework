using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model;
using com.etak.core.model.operation.messages;

namespace com.etak.core.bizops.Temporal_AnnaM
{
    /// <summary>
    /// GetPriorityBundleInfoFromBundleInfosRequest 
    /// </summary>
    public class GetPriorityBundleInfoFromBundleInfosRequest: RequestBase
    {
        /// <summary>
        /// List of bundleinfo
        /// </summary>
        public List<BundleInfo> BundleInfos { get; set; }
    }
}
