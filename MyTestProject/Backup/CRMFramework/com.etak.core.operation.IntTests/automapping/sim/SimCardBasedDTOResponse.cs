using com.etak.core.model.operation.contract.simcard;
using com.etak.core.model.operation.messages;
using com.etak.core.model.resource;

namespace com.etak.core.operation.IntTests.automapping.sim
{
    public class SimCardBasedDTOResponse : ResponseBaseDTO, ISimCardBasedDTOResponse
    {
        public virtual SimCardDTO SimCard { get; set; }
    }
}
