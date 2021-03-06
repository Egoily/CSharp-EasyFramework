﻿using com.etak.core.model;
using com.etak.core.model.operation.contract.simcard;
using com.etak.core.model.operation.messages;

namespace com.etak.core.operation.test.automapping.sim
{
    public class SimCardBasedRequest : RequestBase, ISimCardBasedRequest
    {

        public virtual SIMCardInfo SimCard { get; set; }
    }
}
