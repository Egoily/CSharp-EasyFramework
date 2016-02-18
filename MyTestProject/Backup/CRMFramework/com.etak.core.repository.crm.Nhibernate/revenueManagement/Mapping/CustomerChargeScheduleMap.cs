using com.etak.core.model.revenueManagement;
using com.etak.core.repository.NHibernate.IDGeneration;
using FluentNHibernate.Mapping;

namespace com.etak.core.repository.crm.Nhibernate.revenueManagement
{
    /// <summary>
    /// NHibernate Fluent mapping for Customer Charge schedule
    /// </summary>
    public class CustomerChargeScheduleMap : ClassMap<CustomerChargeSchedule>
    {
        /// <summary>
        /// Default constructor so fluent map builds the xml on runtime.
        /// </summary>
        public CustomerChargeScheduleMap()
        {
            Schema("dbo");
            Table("CRM_CUSTOMER_CHARGE_SCHEDULES");
            DynamicUpdate();


            Id(x => x.Id, "CUSTOMER_CHARGE_SCHEDULEID").GeneratedBy.Custom<PrefixIdGenerator>();

            References(x => x.ChargeDefinition, "CHARGEID");
            References(x => x.Customer, "CUSTOMERID");
            References(x => x.ChargedAccount, "ACCOUNTID");
            References(x => x.Purchase, "CUSTOMERPRODUCTID");
            Map(x => x.NextChargeDate, "NEXT_CHARGE_DATE");
            Map(x => x.NextPeriodNumber, "NEXT_PERIOD_NUMBER");
            Map(x => x.CurrentCyclenumber, "CURRENT_CYCLE_NUMBER");
            Map(x => x.PriceEffectiveDate, "PRICE_EFFECTIVE_DATE");
            Map(x => x.CreateDate, "CREATEDATE");
            Map(x => x.UpdateDate, "UPDATEDATE");
            Map(x => x.Status, "STATUSID").CustomType<ScheduleChargeStatus>();
            HasMany(x => x.Charges).KeyColumn("CUSTOMER_CHARGE_SCHEDULEID");            
        }
    }
}