using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using com.etak.core.model.provisioning;

namespace com.etak.core.model.revenueManagement
{

    [DataContract]
    [Serializable]
    public class Product
    {
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

        /// <summary>
        /// The external reference that could be linked to a carrier
        /// </summary>
        public virtual String ExternalReference { get; set; }
        /// <summary>
        /// If set, relates the product with a certain carrier
        /// </summary>
        public virtual Carrier Carrier { get; set; }
    }
}