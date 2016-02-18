using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model.operation.contract.subscription;
using com.etak.core.model.operation.messages;

namespace com.etak.core.operation.IntTests.automapping.subscription
{
    public class MultiSubscriptionBasedRequest : RequestBase, IMultiSubscriptionBasedRequest
    {
        public string MSISDN { get; set; }

        public IEnumerable<model.ResourceMBInfo> Subscriptions { get; set; }
    }
}
