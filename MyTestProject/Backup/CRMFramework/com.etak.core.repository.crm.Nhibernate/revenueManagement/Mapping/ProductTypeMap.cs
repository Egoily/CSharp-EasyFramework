using com.etak.core.model.revenueManagement;
using com.etak.core.repository.crm.Nhibernate.Factory;
using com.etak.core.repository.NHibernate.IDGeneration;
using FluentNHibernate.Mapping;

namespace com.etak.core.repository.crm.Nhibernate.revenueManagement.Mapping
{
    /// <summary>
    /// Fluent Nhibernate mapping class for ProductType
    /// </summary>
    public class ProductTypeMap : ClassMap<ProductType>
    {
        /// <summary>
        /// Default constructor so fluent mappigns builds the hbm.xml file on runtime
        /// </summary>
        public ProductTypeMap()
        {
            Schema("dbo");
            Table("PRODUCTTYPES");
            DynamicUpdate();
            DynamicInsert();

            Cache.Region(CacheRegions.CatalogCacheRegion).NonStrictReadWrite().IncludeAll();

            Id(x => x.Id, "PRODUCTTYPEID").GeneratedBy.Custom<PrefixIdGenerator>();
            Map(x => x.Description, "DESCRIPTION" ).Length(128);
            
        }
    }
}