using com.etak.core.model.subscription.catalog;
using com.etak.core.repository.crm.Nhibernate.Factory;
using com.etak.core.repository.NHibernate.IDGeneration;
using FluentNHibernate.Mapping;

namespace com.etak.core.repository.crm.Nhibernate.subscription.catalog.Mapping
{
    /// <summary>
    /// Fluent Nhibernate mapping class for ProductCatalog
    /// </summary>
    public class ProductOfferingCatalogMap : ClassMap<ProductOfferingCatalog>
    {
        
        /// <summary>
        /// public constructor for fluent Nhibernate
        /// </summary>
        public ProductOfferingCatalogMap()
        {
            Schema("dbo");
            Table("RM_PRODUCT_OFFERING_CATALOGS");
            DynamicUpdate();
            DynamicInsert();
            Cache.Region(CacheRegions.CatalogCacheRegion).NonStrictReadWrite().IncludeAll();

            Id(x => x.Id, "PRODUCT_OFFERING_CATALOGID").GeneratedBy.Custom<PrefixIdGenerator>();
            Map(x => x.Channel, "CHANNEL");
            References(x => x.MNO).Column("DEALERID").ForeignKey("FK_POC_DEALERS"); ;
            
            HasMany(x => x.ProductOfferingsInCatalog).
                KeyColumn("PRODUCT_OFFERING_CATALOGID")
                .ForeignKeyConstraintName("FK_PO_PRODUCT_OFFERING_CATALOGID")
                .Cache.Region(CacheRegions.CatalogCacheRegion).NonStrictReadWrite().IncludeAll();

            
        }
    }
}
