using com.etak.core.model;
using com.etak.core.model.revenueManagement;
using FluentNHibernate.Mapping;

namespace com.etak.core.repository.crm.Nhibernate.revenueManagement
{
    /// <summary>
    /// Class to map to Nhibernate class AccountData
    /// </summary>
    public class AccountDataMap : SubclassMap<AccountData>
    {
        /// <summary>
        /// The constructor called by fluent to map the class. 
        /// </summary>
        public AccountDataMap()
        {
            DiscriminatorValue(0);//means data
            Map(x => x.BalanceUnit).
                Column("BALANCE_UNITTYPEID").
                CustomType<DataUnits>();  
        }
    }
}