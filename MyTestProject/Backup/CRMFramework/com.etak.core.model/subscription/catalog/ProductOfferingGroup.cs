using System;

namespace com.etak.core.model.subscription.catalog
{
    public class ProductOfferingGroup
    {
        /// <summary>
        /// Unique Id of the product offering group type
        /// </summary>
        public virtual Int32 Id { get; set; }

        public virtual MultiLingualDescription Names { get; set; }

        public virtual MultiLingualDescription Description { get; set; }
    }
}
