using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model.revenueManagement;
using com.etak.core.model.subscription.catalog;
using com.etak.core.repository.crm.Nhibernate.Factory;
using com.etak.core.repository.NHibernate.IDGeneration;
using FluentNHibernate.Mapping;
using NHibernate.Type;

namespace com.etak.core.repository.crm.Nhibernate.subscription.catalog.Mapping
{
    /// <summary>
    /// Fluent Nhibernate mapping class for ProductOption
    /// </summary>
    public class ProductOfferingOptionMap : ClassMap<ProductOfferingOption>
    {
        /// <summary>
        /// public constructor for fluent Nhibernate
        /// </summary>
        public ProductOfferingOptionMap()
        {
            Schema("dbo");
            Table("RM_PRODUCT_OFFERING_OPTIONS");
            DynamicUpdate();
            DynamicInsert();

            Cache.Region(CacheRegions.CatalogCacheRegion).NonStrictReadWrite().IncludeAll();
            DiscriminateSubClassesOnColumn<Int32>("OPTION_TYPEID", (int)ProductOfferingOptionTypes.ProductOfferingOption).Not.Nullable();

            Id(x => x.Id, "PRODUCT_OFFERING_OPTIONID").GeneratedBy.Custom<PrefixIdGenerator>();
            Map(x => x.MaxOccurs, "MAX_OCCURS");
            Map(x => x.MinOccurs, "MIN_OCCURS");
            Map(x => x.ConflictResolutionStrategy, "CONFLICT_STRATEGY").CustomType<EnumStringType<ProductConflictResolutionsStrategies>>();
            References(x => x.RelatedProductOffering, "PRODUCT_OFFERINGID").ForeignKey("FK_POO_PRODUCT_OFFERINGID").Not.Nullable();

        }
    }
}
