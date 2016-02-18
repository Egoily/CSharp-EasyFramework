﻿using System;
using System.Runtime.Serialization;


namespace com.etak.core.model.revenueManagement
{
    /// <summary>
    /// Association between a product and a customer within a time range
    /// </summary>
    [DataContract]
    [Serializable]
    public class CustomerProductAssignmentDTO
    {
        /// <summary>
        /// Unique id for the assingment
        /// </summary>
        public virtual Int64 Id { get; set; }

        /// <summary>
        /// The customer that purchased the product
        /// </summary>
        public virtual Int32 PurchasingCustomerId { get; set; }

        /// <summary>
        /// The product purchased
        /// </summary>
        public virtual Int32 PurchasedProductId { get; set; }

        /// <summary>
        /// The Product Offering Purchased 
        /// </summary>
        public virtual Int32 PurchasedProductOfferingId { get; set; }

        /// <summary>
        /// The charging option used to purchase the product
        /// </summary>
        public virtual Int32 ProductChargePurchasedId { get; set; }

        /// <summary>
        /// The timestamp when the purchase becomes active
        /// </summary>
        public virtual DateTime StartDate { get; set; }

        /// <summary>
        /// The timestamp when the purchase becomes active
        /// </summary>
        public virtual DateTime? EndDate { get; set; }

        /// <summary>
        /// Date in which the association was created
        /// </summary>
        public virtual DateTime CreateDate { get; set; }

        /// <summary>
        /// Order in which association  was created
        /// </summary>
        public virtual Int64 ? CreatingOrderId { get; set; }
    }
}