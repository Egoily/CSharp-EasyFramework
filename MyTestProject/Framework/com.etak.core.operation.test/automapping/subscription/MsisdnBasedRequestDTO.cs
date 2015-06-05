using com.etak.core.model.operation.contract.subscription;
using com.etak.core.model.operation.messages;

namespace com.etak.core.operation.test.automapping.subscription
{
    public class MsisdnBasedRequestDTO : RequestBaseDTO, IMsisdnBasedDTORequest
    {
        public string MSISDN { get; set; }
    }
}
