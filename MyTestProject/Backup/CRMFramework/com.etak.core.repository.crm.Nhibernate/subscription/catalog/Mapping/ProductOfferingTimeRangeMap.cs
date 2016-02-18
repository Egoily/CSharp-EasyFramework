using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model.subscription.catalog;
using com.etak.core.repository.crm.Nhibernate.Factory;
using com.etak.core.repository.NHibernate.IDGeneration;
using FluentNHibernate.Mapping;

namespace com.etak.core.repository.crm.Nhibernate.subscription.catalog.Mapping
{
    /// <summary>
    /// Fluent Nhibernate mapping class for ProductTimeRange
    /// </summary>
    public class ProductOfferingTimeRangeMap : ClassMap<ProductOfferingTimeRange>
    {
        
        /// <summary>
        /// public constructor for fluent Nhibernate
        /// </summary>
        public ProductOfferingTimeRangeMap()
        {
            Schema("dbo");
            Table("RM_PRODUCT_OFFERING_TIME_RANGES");
            DynamicUpdate();
            Cache.Region(CacheRegions.CatalogCacheRegion).NonStrictReadWrite().IncludeAll();

            Id(x => x.Id, "PRODUCT_OFFERING_TIME_RANGEID").GeneratedBy.Custom<PrefixIdGenerator>();

            References(x => x.ProductOffering).Column("PRODUCT_OFFERINGID").ForeignKey("FK_POTR_PRODUCT_OFFERINGID").Not.Nullable();

            Map(x => x.StartDate, "STARTDATE");
            Map(x => x.EndDate, "ENDDATE");
            Map(x => x.Status, "STATUSID").CustomType<ProductOfferingTimeRangeStatus>();

            
            
        }
    }
}
