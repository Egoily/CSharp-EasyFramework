using com.etak.core.model.revenueManagement;
using com.etak.core.repository.crm.Nhibernate.Factory;
using com.etak.core.repository.NHibernate.IDGeneration;
using FluentNHibernate.Mapping;

namespace com.etak.core.repository.crm.Nhibernate.revenueManagement
{
    /// <summary>
    /// Fluent Nhibernate mapping class for BillRun
    /// </summary>
    public class BillRunMap : ClassMap<BillRun>
    {
        /// <summary>
        /// public constructor for fluent Nhibernate
        /// </summary>
        public BillRunMap()
        {
            Schema("dbo");
            Table("RM_BILL_RUN");
            DynamicUpdate();
            Cache.Region(CacheRegions.CatalogCacheRegion).NonStrictReadWrite().IncludeAll();
            Id(x => x.Id, "BILLRUNID").GeneratedBy.Custom<PrefixIdGenerator>();

            References(x => x.BillingCycle, "BILLCYCLEID").ForeignKey("FK_BILLRUN_CYCLE");
            Map(x => x.RunDate, "RUNDATE");
            Map(x => x.StarteDate, "STARTDATE").CustomType("Date");
            Map(x => x.EndDate, "ENDDATE").CustomType("Date");
            Map(x => x.DueDate, "DUEDATE").CustomType("Date");
            Map(x => x.FirstUsageDetailId, "START_USAGE_DETAILID");
            Map(x => x.LastUsageDetailId, "END_USAGE_DETAILID");
            Map(x => x.CutOffDate, "CUTOFF_DATE");
        }
    }
}