using com.etak.core.model.revenueManagement;
using com.etak.core.repository.crm.Nhibernate.Factory;
using com.etak.core.repository.NHibernate.IDGeneration;
using FluentNHibernate.Mapping;

namespace com.etak.core.repository.crm.Nhibernate.revenueManagement.Mapping
{
    /// <summary>
    /// Fluent Nhibernate mapping class for TaxZipRanges
    /// </summary>
    public class TaxZipRangesMap : ClassMap<TaxZipRanges>
    {

//        CREATE TABLE TAX_ZIPCODE_RANGES
//( 
//       TAX_ZIPCODE_RANGEID  INT  NOT NULL ,
//       TAXID                int  NULL ,
//       ZIPCODE_LOW          VARCHAR(20)  NULL ,
//       ZIPCODE_HIGH         VARCHAR(20)  NULL 
//)

        /// <summary>
        /// Default constructor so fluent map builds the xml on runtime.
        /// </summary>
        public TaxZipRangesMap()
        {
            Schema("dbo");
            Table("TAX_ZIPCODE_RANGES");
            DynamicUpdate();
            Cache.Region(CacheRegions.CatalogCacheRegion).NonStrictReadWrite().IncludeAll();

            Id(x => x.Id, "TAX_ZIPCODE_RANGEID").GeneratedBy.Custom<PrefixIdGenerator>();
            References(x => x.RangeOwner).Column("TAXID");
            Map(x => x.ZipRangesHigh).Column("ZIPCODE_HIGH");
            Map(x => x.ZipRangesLow).Column("ZIPCODE_LOW");
        }
    }
}