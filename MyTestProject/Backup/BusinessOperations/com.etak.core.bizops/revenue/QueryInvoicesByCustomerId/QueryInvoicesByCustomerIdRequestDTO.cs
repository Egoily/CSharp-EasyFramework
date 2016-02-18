using com.etak.core.model.operation.contract.customer;
using com.etak.core.model.operation.messages;

namespace com.etak.core.bizops.revenue.QueryInvoicesByCustomerId
{
    /// <summary>
    /// Request for QueryInvoicesByCustomerId receives a customer id
    /// </summary>
    public class QueryInvoicesByCustomerIdRequestDTO : RequestBaseDTO , ICustomerIdBasedDTORequest
    {
        /// <summary>
        /// Id of the customer from the invoice needs to be retrieved
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// The number of invoices to retrieve
        /// </summary>
        public int? NumberOfInvoices { get; set; }

    }
}
