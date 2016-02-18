using com.etak.core.model.operation.contract.customer;
using com.etak.core.model.operation.messages;

namespace com.etak.core.bizops.fullfilment.QueryCustomerByDocumentId
{
    /// <summary>
    /// Request DTO of QueryCustomerByDocumentId BizOp
    /// </summary>
    public class QueryCustomerByDocumentIdRequestDTO : RequestBaseDTO, IDocumentIdBasedDTORequest
    {
        /// <summary>
        /// Document Number
        /// </summary>
        public string DocumentNumber { get; set; }

        /// <summary>
        /// Type of document that is used to filter the document
        /// </summary>
        public int DocumentType { get; set; }
    }
}
