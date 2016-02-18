using com.etak.core.model;
using com.etak.core.model.revenueManagement;
using com.etak.core.repository.crm.Nhibernate.Factory;
using com.etak.core.repository.NHibernate.IDGeneration;
using FluentNHibernate.Mapping;
using NHibernate.Type;

namespace com.etak.core.repository.crm.Nhibernate.revenueManagement
{
    /// <summary>
    /// Class to map to Nhibernate class AccountTime
    /// </summary>
    public class BillCycleMap : ClassMap<BillCycle>
    {
        /// <summary>
        /// The constructor called by fluent to map the class
        /// </summary>
        public BillCycleMap()
        {
            Schema("dbo");
            Table("RM_BILL_CYCLES");
            DynamicUpdate();
            Cache.Region(CacheRegions.CatalogCacheRegion).NonStrictReadWrite().IncludeAll();
            Id(x => x.Id).Column("BILLCYCLEID").GeneratedBy.Custom<PrefixIdGenerator>();

            References(x => x.VMNO, "MVNOID");
            References(x => x.Description).Column("DESCRIPTIONID").Cascade.All();

            Map(x => x.RunDay, "BILLRUNDAY");
            Map(x => x.CutOffDay, "CUTOFFDAY");
            Map(x => x.PeriodUnit, "PERIOD_UNIT").CustomType<EnumStringType<TimeUnits>>();
            Map(x => x.PeriodQuantity, "PERIOD_QUANTITY");
            Map(x => x.DaysUntilLate, "DAYS_UNTIL_LATE");
        }
    }
}