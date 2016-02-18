using com.etak.core.model.inventory;
using com.etak.core.repository.NHibernate.IDGeneration;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.etak.core.repository.crm.Nhibernate.inventory.mapping
{
    /// <summary>
    /// Class to map to Nhibernate class ProductInventory
    /// </summary>
    public class ProductInventoryMap:ClassMap<ProductInventory>
    {
        /// <summary>
        /// The constructor called by fluent to map the class. 
        /// </summary>
        public ProductInventoryMap()
        {
            Table("CRM_MVNO_INVENTORY");
            Id(x => x.Id, "MVNO_INVENTORYID").GeneratedBy.Custom<PrefixIdGenerator>();
            Map(x => x.AvailabeQuantity).Column("AVAILABLEQUANTITY");
            Map(x => x.BeginningQuantity).Column("BEGINNINGQUANTITY");
            References(x => x.Product).Column("PRODUCTGK");
        }
    }
}
