using com.etak.core.model.operation.contract.customer;
using com.etak.core.model.operation.messages;

namespace com.etak.core.operation.IntTests.automapping.customer
{
    class CustomerIdBasedRequestDTO : RequestBaseDTO, ICustomerIdBasedDTORequest
    {
        public int CustomerId { get; set; }
    }
}
