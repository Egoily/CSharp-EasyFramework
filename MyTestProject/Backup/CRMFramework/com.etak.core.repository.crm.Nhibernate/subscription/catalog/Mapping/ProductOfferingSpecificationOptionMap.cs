using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model.subscription.catalog;
using FluentNHibernate.Mapping;

namespace com.etak.core.repository.crm.Nhibernate.subscription.catalog.Mapping
{
    /// <summary>
    /// NHibernate mapping for ProductSpecificationOption entity
    /// </summary>
    public class ProductOfferingSpecificationOptionMap : SubclassMap<ProductOfferingSpecificationOption>
    {
        /// <summary>
        /// Class map for ProductSpecificationOption so fluent nhibernate builds the hbm.xml on runtime
        /// </summary>
        public ProductOfferingSpecificationOptionMap()
        {
            DiscriminatorValue((int)ProductOfferingOptionTypes.ProductOfferingSpecification);
            References(x => x.SpecifiedProductOffering).Column("SPECIFIED_PRODUCT_OFFERINGID").Cascade.All().ForeignKey("FK_POO_SPECIFIED_PRODUCT_OFFERINGID");
        }
    }
}
