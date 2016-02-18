using com.etak.core.model.revenueManagement;
using FluentNHibernate.Mapping;

namespace com.etak.core.repository.crm.Nhibernate.revenueManagement
{
    /// <summary>
    /// NHibernate mapping for BalanceForAccount class
    /// </summary>
    public class BalanceForAccountMap : ClassMap<BalanceForAccount>
    {
        /// <summary>
        /// Defualt constructor that maps the classes
        /// </summary>
        public BalanceForAccountMap()
        {
            Schema("dbo");
            Table("CRM_ACCOUNT_BALANCES");
            DynamicUpdate();

            Id(x=>x.Id).GeneratedBy.Foreign("Account").Column("ACCOUNTID");
            HasOne(x => x.Account).Constrained();
            Map(x => x.Balance).Column("BALANCE_QTY");            
        }
    }
}