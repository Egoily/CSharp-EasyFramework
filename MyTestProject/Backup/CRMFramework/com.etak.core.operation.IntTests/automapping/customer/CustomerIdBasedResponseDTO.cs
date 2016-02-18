using com.etak.core.model.operation.contract.customer;
using com.etak.core.model.operation.messages;

namespace com.etak.core.operation.IntTests.automapping.customer
{
    public class CustomerBasedResponseDTO : ResponseBaseDTO, ICustomerBasedDTOResponse
    {
        public model.dto.CustomerDTO Customer { get; set; }
    }
}
