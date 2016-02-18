using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model.operation.contract.customer;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;

namespace com.etak.core.bizops.fullfilment.AssignProductToCustomer
{
    /// <summary>
    ///  PurchaseIndividualProductForCustomerRequestDTO for individual product purchase
    /// </summary>
    public class AssignProductOfferingToCustomerRequestDTO : OrderRequestDTO, ICustomerIdBasedDTORequest
    {
        /// <summary>
        /// CreateDate
        /// </summary>
        public virtual DateTime CreateDate { get; set; }

        /// <summary>
        /// StartDate
        /// </summary>
        public virtual DateTime StartDate { get; set; }


        /// <summary>
        /// EndDate
        /// </summary>
        public virtual DateTime? EndDate { get; set; }


        /// <summary>
        /// CustomerID, automapped by ICustomerIdBasedDTORequest
        /// </summary>
        public int CustomerId { get; set; }

        ///// <summary>
        ///// Product to purchase
        ///// </summary>
        //public ProductCatalogDTO product { get; set; }

        /// <summary>
        /// Product to Purchase
        /// </summary>
        public int ProductOfferingId { get; set; }

        /// <summary>
        /// The charge Option to be used
        /// </summary>
        public int ProductChargeOptionId { get; set; }

    }
}
