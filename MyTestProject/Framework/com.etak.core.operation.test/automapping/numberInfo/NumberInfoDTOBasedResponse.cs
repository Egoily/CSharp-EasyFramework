using com.etak.core.model.operation.contract.numberInfo;
using com.etak.core.model.operation.messages;
using com.etak.core.model.resource;

namespace com.etak.core.operation.test.automapping.numberInfo
{
    public class NumberInfoDTOBasedResponse : ResponseBaseDTO, INumberInfoBasedDTOResponse
    {
        public MSISDNResourceDTO NumberInPool { get; set; }
    }
}
