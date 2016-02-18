using System;
using System.Collections.Generic;
using com.etak.core.model.revenueManagement;
using com.etak.core.repository.crm.revenueManagement;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate.revenueManagement
{
    /// <summary>
    /// Nhibernate repository for inheritance tree of entity Invoice 
    /// </summary>
    /// <typeparam name="TInvoice">Entity managed by the repository, is or extends Invoice</typeparam>
    public class InvoiceRepositoryNH<TInvoice> :
        NHibernateRepository<TInvoice, Int64>, 
        IInvoiceRepository<TInvoice> where TInvoice : Invoice
    {
        /// <summary>
        /// Gets the last N invoices for a given customer
        /// </summary>
        /// <param name="customerId">the customer of the customers</param>
        /// <param name="n">the number of the invoices to recover</param>
        /// <returns>the list of invoices</returns>
        public IEnumerable<TInvoice> GetLastNInvoices(int customerId, int n)
        {
            return GetQuery().Where(ee => ee.ChargedCustomer.CustomerID == customerId).
                OrderBy(ee => ee.StartDate).Desc.Take(n).
                Future();
        }

        /// <summary>
        /// Gets the invoice for the customer with a given legal number
        /// </summary>
        /// <param name="customerId">The owner of the customer</param>
        /// <param name="legalNumber">The number of the invoice to recover</param>
        /// <returns>the list of invoices</returns>
        public IEnumerable<TInvoice> GetByLegalInvoiceNumber(int customerId, string legalNumber)
        {
            return GetQuery().
                Where(ee => ee.LegalInvoiceNumber == legalNumber && ee.ChargedCustomer.CustomerID == customerId)
                
                .Future();
        }

        /// <summary>
        /// Gets the last N invoices for a given customer that are in a set of status
        /// </summary>
        /// <param name="customerId">the customer of the customers</param>
        /// <param name="n">the number of the invoices to recover</param>
        /// <returns>the list of invoices</returns>
        /// <param name="statuses">the possible list of statu that the invoice needs to be</param>
        /// <returns>the list of n matching invoices</returns>
        public IEnumerable<TInvoice> GetLastNInvoicesAndStatusIn(int customerId, int n, List<InvoiceStatus ?> statuses)
        {
            return GetQuery().Where(ee => ee.ChargedCustomer.CustomerID == customerId)
                .AndRestrictionOn(x => x.Status).IsIn(statuses)
                .OrderBy(ee => ee.StartDate).Desc.Take(n).Future();

        }
    }
}
