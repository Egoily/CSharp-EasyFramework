using com.etak.core.model;
using com.etak.core.model.operation.contract.numberInfo;
using com.etak.core.model.operation.messages;

namespace com.etak.core.operation.test.automapping.numberInfo
{
    public class NumberInfoBasedResponse : ResponseBase, INumberInfoBasedResponse
    {
        public NumberInfo NumberInPool { get; set; }
    }
}
