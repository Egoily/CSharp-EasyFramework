using System;
using System.Collections.Generic;
using com.etak.core.model.revenueManagement;

namespace com.etak.core.repository.crm.revenueManagement
{
    /// <summary>
    /// The respository interface for <typeparamref name="TInvoice"/> entity
    /// </summary>
    /// <typeparam name="TInvoice">The entity managed by the interface, is or extends Invoice</typeparam>
    public interface IInvoiceRepository<TInvoice> : IRepository<TInvoice, Int64> where TInvoice : Invoice
    {
        /// <summary>
        /// Gets the last N invoices for a given customer
        /// </summary>
        /// <param name="customerId">the customer of the customers</param>
        /// <param name="n">the number of the invoices to recover</param>
        /// <returns>the list of invoices</returns>
        IEnumerable<TInvoice> GetLastNInvoices(int customerId, int n);

        /// <summary>
        /// Gets the invoice for the customer with a given legal number
        /// </summary>
        /// <param name="customerId">The owner of the customer</param>
        /// <param name="legalNumber">The number of the invoice to recover</param>
        /// <returns>the list of invoices</returns>
        IEnumerable<TInvoice> GetByLegalInvoiceNumber(int customerId, string legalNumber);

        /// <summary>
        /// Gets the last N invoices for a given customer that are in a set of status
        /// </summary>
        /// <param name="customerId">the customer of the customers</param>
        /// <param name="n">the number of the invoices to recover</param>
        /// <returns>the list of invoices</returns>
        /// <param name="statuses">the possible list of statu that the invoice needs to be</param>
        /// <returns>the list of n matching invoices</returns>
        IEnumerable<TInvoice> GetLastNInvoicesAndStatusIn(int customerId, int n, List<InvoiceStatus?> statuses);
    }
}
