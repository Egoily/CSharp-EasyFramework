using com.etak.core.model.operation.contract.simcard;
using com.etak.core.model.operation.messages;

namespace com.etak.core.operation.test.automapping.sim
{
    public class ICCIDBasedRequestDTO : RequestBaseDTO, IICCIDBasedDTORequest
    {
        public string ICCID { get; set; }
    }
}
