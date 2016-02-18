using System;
using System.Collections.Generic;
using com.etak.core.model.revenueManagement;
using com.etak.core.model.subscription.catalog;

namespace com.etak.core.bizops.helper
{
    /// <summary>
    /// Interface for PurchaseHelper
    /// </summary>
    public interface IPurchaseHelper
    {
        /// <summary>
        /// GetCustomer4GProducts
        /// </summary>
        /// <param name="productsToPurchase"></param>
        /// <param name="mvnoId"></param>
        /// <returns></returns>
        IEnumerable<Product> GetCustomer4GProducts(List<Product> productsToPurchase, int mvnoId);

        /// <summary>
        /// Main operation that will take all the Products form the list, and get the corresponding Charge Option.
        /// If the chargeoption is set, that will be the one putted in the dictionary. If not,
        /// the ChargeOption marked as Default will be the one putted in the dictionary.
        /// </summary>
        /// <param name="productsDto">A list of ProductCatalogDTO, with the list ChargingOptions containing the Charge Option to be used</param>
        /// <returns></returns>
        List<Tuple<ProductOffering, ProductChargeOption>> GetProductsAndChargesOptions(List<ProductCatalogDTO> productsDto);

        /// <summary>
        /// Check if is Recurring Charge
        /// </summary>
        /// <param name="charges"></param>
        /// <returns></returns>
        bool IsRecurringCharge(IEnumerable<Charge> charges);

        /// <summary>
        /// Check if is necessary to disable 4G service
        /// </summary>
        /// <param name="customerProductAssignments"></param>
        /// <param name="customerProductAssignmentToCancel"></param>
        /// <param name="mvnoId"></param>
        /// <returns></returns>
        bool IsNeedtoDisable4GService(IEnumerable<CustomerProductAssignment> customerProductAssignments, CustomerProductAssignment customerProductAssignmentToCancel, int mvnoId);
    }
}