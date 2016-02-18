using com.etak.core.model.revenueManagement;
using com.etak.core.repository.crm.Nhibernate.Factory;
using com.etak.core.repository.NHibernate.IDGeneration;
using FluentNHibernate.Mapping;

namespace com.etak.core.repository.crm.Nhibernate.revenueManagement.Mapping
{
    /// <summary>
    /// Fluent Nhibernate mapping class for TaxRates
    /// </summary>
    public class TaxRatesMap : ClassMap<TaxRates>
    {
        /// <summary>
        /// Default constructor so fluent mappigns builds the hbm.xml file on runtime
        /// </summary>
        public TaxRatesMap()
        {
            Schema("dbo");
            Table("TAX_RATES");
            DynamicUpdate();
            Cache.Region(CacheRegions.CatalogCacheRegion).NonStrictReadWrite().IncludeAll();

            Id(x => x.Id, "TAX_RATEID").GeneratedBy.Custom<PrefixIdGenerator>();
            Map(x => x.StartDate, "STARTDATE");
            Map(x => x.EndDate, "ENDDATE");
            Map(x => x.Percentage, "TAX_PERCENTAGE");

            References(x => x.Definition).Column("TAXID");
        }
    }
}