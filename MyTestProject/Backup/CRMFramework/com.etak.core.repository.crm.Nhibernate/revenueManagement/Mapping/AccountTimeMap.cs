using com.etak.core.model;
using com.etak.core.model.revenueManagement;
using FluentNHibernate.Mapping;

namespace com.etak.core.repository.crm.Nhibernate.revenueManagement
{
    /// <summary>
    /// Class to map to Nhibernate class AccountTime
    /// </summary>
    public class AccountTimeMap : SubclassMap<AccountTime>
    {
        /// <summary>
        /// The constructor called by fluent to map the class
        /// </summary>
        public AccountTimeMap()
        {
            DiscriminatorValue(2);
            Map(x => x.BalanceUnit).
                Column("BALANCE_UNITTYPEID").
                CustomType<TimeUnits>();            
        }
    }

}