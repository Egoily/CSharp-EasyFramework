using com.etak.core.model.operation.contract.customer;
using com.etak.core.model.operation.messages;

namespace com.etak.core.operation.test.automapping.customer
{
    public class JointCustomerIdBasedRequestDTO : RequestBaseDTO, IJointCustomerIdDTOBasedRequest
    {
        public int DestinationCustomerId { get; set; }

        public int SourceCustomerId { get; set; }
    }
}
