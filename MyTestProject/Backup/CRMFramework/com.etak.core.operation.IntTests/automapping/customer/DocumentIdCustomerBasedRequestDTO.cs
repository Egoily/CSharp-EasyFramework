using com.etak.core.model.operation.contract.customer;
using com.etak.core.model.operation.messages;

namespace com.etak.core.operation.IntTests.automapping.customer
{
    public class DocumentIdCustomerBasedRequestDTO : RequestBaseDTO, IDocumentIdBasedDTORequest
    {
        public virtual string DocumentNumber { get; set; }

        public virtual int DocumentType { get; set; }
    }
}
