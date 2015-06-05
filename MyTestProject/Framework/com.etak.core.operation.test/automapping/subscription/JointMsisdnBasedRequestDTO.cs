using com.etak.core.model.operation.contract.subscription;
using com.etak.core.model.operation.messages;

namespace com.etak.core.operation.test.automapping.subscription
{
    public class JointMsisdnBasedRequestDTO : RequestBaseDTO, IJointMsisdnDTOBasedRequest
    {
        public string DestinationMSISDN { get; set; }

        public string SourceMSISDN { get; set; }
    }
}
