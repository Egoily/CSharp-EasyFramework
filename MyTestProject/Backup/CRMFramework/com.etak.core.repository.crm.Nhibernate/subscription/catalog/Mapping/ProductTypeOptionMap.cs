using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model.subscription.catalog;
using FluentNHibernate.Mapping;

namespace com.etak.core.repository.crm.Nhibernate.subscription.catalog.Mapping
{
     
    /// <summary>
    /// NHibernate mapping for ProductTypeOption entity
    /// </summary>
    public class ProductTypeOptionMap : SubclassMap<ProductTypeOption>
    {
        /// <summary>
        /// Class map for ProductTypeOption so fluent nhibernate builds the hbm.xml on runtime
        /// </summary>
        public ProductTypeOptionMap()
        {
            DiscriminatorValue((int)ProductOfferingOptionTypes.ProductTypeOption);
            References(x => x.Group).Column("PRODUCTTYPEID").Cascade.All().ForeignKey("FK_POO_PRODUCTTYPEID");
        }
    }
}
