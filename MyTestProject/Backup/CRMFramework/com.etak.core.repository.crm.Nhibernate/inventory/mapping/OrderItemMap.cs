using com.etak.core.model.order;
using com.etak.core.repository.NHibernate.IDGeneration;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.etak.core.repository.crm.Nhibernate.inventory.mapping
{
    /// <summary>
    /// Class to map to Nhibernate class OrderItem
    /// </summary>
    public class OrderItemMap:ClassMap<OrderItem>
    {
        /// <summary>
        /// The constructor called by fluent to map the class. 
        /// </summary>
        public OrderItemMap()
        {
            Id(x => x.Id, "ORDERITEMID").GeneratedBy.Custom<PrefixIdGenerator>();
            Table("CRM_ORDER_ITEMS");
            Map(x => x.Price).Column("PRICE");
            Map(x => x.Quantity).Column("QUANTITY");
            References(x => x.Product).Column("PRODUCTGK");
            References(x => x.Order).Column("ORDERID").Cascade.None();
            References(x => x.ProductOffering).Column("PRODUCT_OFFERINGID");            
        }
    }
}
