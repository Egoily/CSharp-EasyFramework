using System;
using System.Collections.Generic;
using com.etak.core.model.revenueManagement;

namespace com.etak.core.repository.crm.revenueManagement
{
    /// <summary>
    /// Repository interface for CustomerCharge
    /// </summary>
    /// <typeparam name="TCustomerCharge">The type of the entity managed by the repository, is or extends CustomerCharge</typeparam>
    public interface ICustomerChargeRepository<TCustomerCharge> : IRepository<TCustomerCharge, Int64> where TCustomerCharge : CustomerCharge
    {

        /// <summary>
        /// Gets the customer charges by customerId
        /// </summary>
        /// <param name="customerId">The customer id to load</param>
        /// <returns>a list containing the charges for the customercustomers charge</returns>
        IEnumerable<TCustomerCharge> GetByCustomerId(Int32 customerId);

        /// <summary>
        /// Gets the customer charges by customerId in with charging dates within a range
        /// </summary>
        /// <param name="customerId">The customer id to load</param>
        /// <param name="startDate">The purchase date range start</param>
        /// <param name="endDate">The purchase date range end</param>
        /// <returns>a list containing the charges for the customercustomers charge</returns>
        IEnumerable<TCustomerCharge> GetByCustomerIdWithRange(Int32 customerId,DateTime startDate,DateTime endDate);

        /// <summary>
        /// Gets the customer charges by customerId of a given invoice
        /// </summary>
        /// <param name="customerId">THe id of the customer owning the charges</param>
        /// <param name="invoice">The invoice to look for</param>
        /// <returns>a list containing the charges for the customer charges</returns>
        IEnumerable<TCustomerCharge> GetByCustomerIdAndInvoice(Int32 customerId, Invoice invoice);
    }
}
