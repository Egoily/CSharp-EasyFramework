using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace com.etak.core.model.revenueManagement
{

    [DataContract]
    [Serializable]
    public class Product
    {
        public Product()
        {            
            ChargingOptions = new List<ProductChargeOption>();
            ChildProducts = new List<Product>();
            ParentProducts = new List<Product>();
        }

        /// <summary>
        /// Unique ID of the product
        /// </summary>
        public virtual Int32 Id {get; set; }

        /// <summary>
        /// Status of the product
        /// </summary>
        public virtual ProductStatuses Status { get; set; }

        /// <summary>
        /// The operator owner of the product
        /// </summary>
        public virtual DealerInfo VMO { get; set; }

        /// <summary>
        /// The name of the product. To be used to display to internal personnel, like to display on the application screens.
        /// </summary>
        public virtual MultiLingualDescription Names { get; set; }
        
        /// <summary>
        /// The description of the product (for presenting to the customer).
        /// </summary>
        public virtual MultiLingualDescription Description { get; set; }

        /// <summary>
        /// List of charging options available to purchase the product
        /// </summary>
        public virtual IList<ProductChargeOption> ChargingOptions { get; set; }

        /// <summary>
        /// List of possible relations with other products (compatibility/requirement)
        /// </summary>
        public virtual IList<ProductDependencyRelation> ProductRelationDependencies { get; set; }

        /// <summary>
        /// BundleInfo associated to this product
        /// </summary>
        public virtual BundleInfo AssociatedBundle { get; set; }

        /// <summary>
        /// BundleInfo associated to this product
        /// </summary>
        public virtual RmPromotionPlanInfo AssociatedPrmotionPlan { get; set; }

        /// <summary>
        /// BundleInfo associated to this product
        /// </summary>
        public virtual RmPromotionGroupInfo AssociatedPrmotionGroup { get; set; }

        /// <summary>
        /// BundleInfo associated to this product
        /// </summary>
        public virtual PackageInfo AssociatedPackage { get; set; }

        /// <summary>
        /// Type of the product
        /// </summary>
        public virtual ProductType Type { get; set; }

        #region Product Grouping
        /// <summary>
        /// The list of child products of this product in case a product group
        /// </summary>
        public virtual IList<Product> ChildProducts { get; set; }
 
        /// <summary>
        /// The parent products in case of this product belongs to a product group
        /// </summary>
        public virtual IList<Product> ParentProducts { get; set; }
        #endregion
    }
}