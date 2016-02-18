using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model.subscription.catalog;
using FluentNHibernate.Mapping;

namespace com.etak.core.repository.crm.Nhibernate.subscription.catalog.Mapping
{
    /// <summary>
    /// NHibernate mapping for ProductGroupOption entity
    /// </summary>
    public class ProductOfferingGroupOptionMap : SubclassMap<ProductOfferingGroupOption>
    {
        /// <summary>
        /// Class map for ProductGroupOption so fluent nhibernate builds the hbm.xml on runtime
        /// </summary>
        public ProductOfferingGroupOptionMap()
        {
            DiscriminatorValue((int)ProductOfferingOptionTypes.ProductOfferingGroupOption);
            References(x => x.Group).Column("PRODUCT_OFFERING_GROUPID").Cascade.All().ForeignKey("FK_POO_PRODUCT_OFFERING_GROUPID");
        }
    }
}
