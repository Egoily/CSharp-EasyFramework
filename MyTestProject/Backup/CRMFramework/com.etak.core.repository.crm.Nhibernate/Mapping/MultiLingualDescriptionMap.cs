using com.etak.core.model;
using com.etak.core.repository.crm.Nhibernate.Factory;
using com.etak.core.repository.NHibernate.IDGeneration;
using FluentNHibernate.Mapping;

namespace com.etak.core.repository.crm.Nhibernate.Mapping
{
    /// <summary>
    /// Fluent Nhibernate mapping class for MultiLingualDescription
    /// </summary>
    public class MultiLingualDescriptionMap : ClassMap<MultiLingualDescription>
    {
        /// <summary>
        /// Default constructor so fluent mappigns builds the hbm.xml file on runtime
        /// </summary>
        public MultiLingualDescriptionMap()
        {
            Schema("dbo");
            Table("TEXT_LOOKUPS");
            DynamicUpdate();
            Cache.Region(CacheRegions.CatalogCacheRegion).NonStrictReadWrite().IncludeAll();

            Id(x => x.Id, "LOOKUPID").GeneratedBy.Custom<PrefixIdGenerator>();
            Map(x => x.DefaultMessage, "LOOKUPSTRING");
            Map(x => x.Type, "TEXT_LOOKUP_TYPE");
            Map(x => x.DisplaySequence, "DISPLAY_SEQ");
            HasMany(x => x.Texts)
                .KeyColumn("LOOKUPID")
                .Cascade.AllDeleteOrphan()
                .Inverse()
                .Cache.Region(CacheRegions.CatalogCacheRegion).NonStrictReadWrite().IncludeAll();
        }
    }
}
