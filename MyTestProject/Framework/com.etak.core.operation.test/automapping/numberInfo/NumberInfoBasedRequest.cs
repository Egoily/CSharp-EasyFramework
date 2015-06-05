using com.etak.core.model;
using com.etak.core.model.operation.contract.numberInfo;
using com.etak.core.model.operation.messages;

namespace com.etak.core.operation.test.automapping.numberInfo
{
    public class NumberInfoBasedRequest : RequestBase, INumberInfoBasedRequest
    {
        public virtual NumberInfo NumberInPool { get; set; }
    }
}
