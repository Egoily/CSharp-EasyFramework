﻿using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.model.operation.messages;

namespace com.etak.core.test.utilities.UnitTests.BizOpTest
{
    public class BizOpTestResponseDTO : ResponseBaseDTO
    {
        public IList<RoamingBlackListInfo> list { get; set; }
    } 
}
