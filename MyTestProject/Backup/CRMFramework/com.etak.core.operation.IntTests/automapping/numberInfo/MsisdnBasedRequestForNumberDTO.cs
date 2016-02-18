using com.etak.core.model.operation.contract.subscription;
using com.etak.core.model.operation.messages;

namespace com.etak.core.operation.IntTests.automapping.numberInfo
{
    public class MsisdnBasedRequestForNumberDTO : RequestBaseDTO, IMsisdnBasedDTORequest
    {
        public string MSISDN { get; set; }
    }
}
