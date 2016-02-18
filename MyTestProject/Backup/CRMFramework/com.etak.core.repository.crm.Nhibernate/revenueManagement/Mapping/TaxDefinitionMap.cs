using com.etak.core.model.revenueManagement;
using com.etak.core.repository.crm.Nhibernate.Factory;
using com.etak.core.repository.NHibernate.IDGeneration;
using FluentNHibernate.Mapping;

namespace com.etak.core.repository.crm.Nhibernate.revenueManagement.Mapping
{
    /// <summary>
    /// Fluent Nhibernate mapping class for TaxDefinition
    /// </summary>
    public class TaxDefinitionMap : ClassMap<TaxDefinition>
    {
        /// <summary>
        /// Default constructor so fluent mappigns builds the hbm.xml file on runtime
        /// </summary>
        public TaxDefinitionMap()
        {
            Schema("dbo");
            Table("TAXES");
            DynamicUpdate();
            Cache.Region(CacheRegions.CatalogCacheRegion).NonStrictReadWrite().IncludeAll();


            Id(x => x.Id, "TAXID").GeneratedBy.Custom<PrefixIdGenerator>();
            Map(x => x.TaxCategory, "TAX_CATEGORYID");

            References(x => x.Description).Column("DESCRIPTIONID").Cascade.All();
            
            HasMany(x => x.Rates)                
                .Inverse()
                .KeyColumn("TAXID")
                .Cascade.All();

            HasMany(x => x.ZipRanges)
                .Inverse()
                .KeyColumn("TAXID")
                .Cascade.All();
            
        }
    }

}