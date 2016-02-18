using com.etak.core.model.subscription.catalog;
using com.etak.core.repository.crm.Nhibernate.Factory;
using com.etak.core.repository.NHibernate.IDGeneration;
using FluentNHibernate.Mapping;

namespace com.etak.core.repository.crm.Nhibernate.subscription.catalog.Mapping
{
    /// <summary>
    /// Fluent Nhibernate mapping class for ProductOfferingGroup
    /// </summary>
    public class ProductOfferingGroupMap : ClassMap<ProductOfferingGroup>
    {
        /// <summary>
        /// Default constructor so fluent mappigns builds the hbm.xml file on runtime
        /// </summary>
        public ProductOfferingGroupMap()
        {
            Schema("dbo");
            Table("RM_PRODUCT_OFFERING_GROUPS");
            DynamicUpdate();
            DynamicInsert();

            Cache.Region(CacheRegions.CatalogCacheRegion).NonStrictReadWrite().IncludeAll();

            Id(x => x.Id, "PRODUCT_OFFERING_GROUPID").GeneratedBy.Custom<PrefixIdGenerator>();
            References(x => x.Names, "NAMEID").Cascade.All();
            References(x => x.Description, "DESCRIPTIONID").Cascade.All();
            
        }
    }
}
