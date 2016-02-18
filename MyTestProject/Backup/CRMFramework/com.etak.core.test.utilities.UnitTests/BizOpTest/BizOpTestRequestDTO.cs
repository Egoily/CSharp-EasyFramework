using com.etak.core.model.operation.contract.subscription;
using com.etak.core.model.operation.messages;

namespace com.etak.core.test.utilities.UnitTests.BizOpTest
{
    public class BizOpTestRequestDTO : RequestBaseDTO, IMsisdnBasedDTORequest
    {
        public string MSISDN { get; set; }
    }
}
