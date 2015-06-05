using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.model.operation.messages;

namespace com.etak.core.test.utilitiesTests.MicroServiceTest
{
    public class MicroserviceTestResponse : ResponseBase
    {
        public IEnumerable<RoamingBlackListInfo> list;
        public object DealerInfo { get; set; }
    }
}
