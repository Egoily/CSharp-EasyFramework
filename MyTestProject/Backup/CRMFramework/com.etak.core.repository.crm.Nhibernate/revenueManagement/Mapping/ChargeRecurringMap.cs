using com.etak.core.model;
using com.etak.core.model.revenueManagement;
using FluentNHibernate.Mapping;

namespace com.etak.core.repository.crm.Nhibernate.revenueManagement.Mapping
{
    /// <summary>
    /// NHibernate mapping for ChargeRecurring entity
    /// </summary>
    public class ChargeRecurringMap : SubclassMap<ChargeRecurring>
    {
        /// <summary>
        /// Class map for ChargeRecurring so fluent nhibernate builds the hbm.xml on runtime
        /// </summary>
        public ChargeRecurringMap()
        {
            DiscriminatorValue(2);
            Map(x => x.PeriodUnit).CustomType<TimeUnits>().Column("PERIOD_UNITID");
            Map(x => x.PeriodCount).Column("PERIOD_UNIT_COUNT");
            Map(x => x.StartPeriodNumber).Column("START_PERIODNUMBER");
            Map(x => x.EndPeriodNumber).Column("END_PERIODNUMBER");
            Map(x => x.Periodicity).Column("PERIODICITY");
            Map(x => x.CycleRepeatCount).Column("CYCLE_REPEAT_COUNT");
            Map(x => x.PeriodCommitment).Column("PERIOD_COMMITMENT");
        }
    }
}
