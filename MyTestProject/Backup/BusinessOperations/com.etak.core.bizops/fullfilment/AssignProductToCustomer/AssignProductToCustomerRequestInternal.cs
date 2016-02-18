using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.bizops.helper;
using com.etak.core.model;
using com.etak.core.model.operation.contract.customer;
using com.etak.core.model.operation.messages;
using com.etak.core.model.revenueManagement;
using com.etak.core.model.subscription.catalog;

namespace com.etak.core.bizops.fullfilment.AssignProductToCustomer
{
    /// <summary>
    /// AssignProductToCustomerRequestInternal for Individual product purchase
    /// </summary>
    public class AssignProductOfferingToCustomerRequestInternal : CreateNewOrderRequest, ICustomerBasedRequest
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
        /// CustomerInfo, autoMapped by ICustomerBasedRequest
        /// </summary>
        public virtual CustomerInfo Customer { get; set; }


        /// <summary>
        /// Flattened dictioanry with all the products for purchase with chargeoption (includes child products)
        /// </summary>
        public Tuple<ProductOffering, ProductChargeOption> ProductOffering { get; set; }

    }
}
