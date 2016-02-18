using com.etak.core.model.order;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.etak.core.repository.crm.Nhibernate.inventory.mapping
{
    /// <summary>
    /// Class to map to Nhibernate class CustomerOrder
    /// </summary>
    public class CustomerOrderMap : SubclassMap<CustomerOrder>
    {
        /// <summary>
        /// The constructor called by fluent to map the class. 
        /// </summary>
        public CustomerOrderMap()
        {
            DiscriminatorValue((new CustomerOrder()).Discriminator);
            HasMany(x => x.OrderItems).KeyColumn("ORDERID").Cascade.All();
            References(x => x.Customer, "CUSTOMERID");
            Map(x => x.SequenceId, "SEQUENCEID");
            References(x => x.DeliveryAddress, "DELIVERY_ADDRESSID");
            References(x => x.DeliveryProduct, "DELIVERY_PRODUCTGK");
            Map(x => x.DeliveryDate, "DELIVERY_DATE");
            Map(x => x.Comments, "COMMENTS");
        }
    }
}
