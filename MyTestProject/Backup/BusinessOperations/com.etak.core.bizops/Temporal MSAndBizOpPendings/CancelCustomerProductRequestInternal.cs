using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;

namespace com.etak.core.bizops.Temporal_AnnaM
{
    /// <summary>
    /// sgsg
    /// </summary>
    public class CancelCustomerProductRequestInternal: RequestBase
    {
        /// <summary>
        /// ProductToCancel
        /// </summary>
        public Product ProductToCancel { get; set; }
    }
}
