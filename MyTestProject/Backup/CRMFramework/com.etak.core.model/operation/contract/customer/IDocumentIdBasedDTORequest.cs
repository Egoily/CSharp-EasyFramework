
using System;

namespace com.etak.core.model.operation.contract.customer
{
    /// <summary>
    /// Request in DTO form that provide a document as input parameter
    /// </summary>
    public interface IDocumentIdBasedDTORequest 
    {
        /// <summary>
        /// The type of the document, Passport, DNI, NIE, 
        /// </summary>
        Int32 DocumentType { get; set; }

        /// <summary>
        /// The id of the document, Passport number, DNI number...
        /// </summary>
        String DocumentNumber { get; set; }
    }
}
