using com.etak.core.model.operation.contract.customer;
using com.etak.core.model.operation.messages;

namespace com.etak.core.operation.IntTests.automapping.customer
{
    public class ExternalCustomerIdBasedRequestDTO : RequestBaseDTO, IExternalCustomerIdBasedDTORequest
    {
        public virtual string ExternalCustomerId { get; set; }
    }
}
