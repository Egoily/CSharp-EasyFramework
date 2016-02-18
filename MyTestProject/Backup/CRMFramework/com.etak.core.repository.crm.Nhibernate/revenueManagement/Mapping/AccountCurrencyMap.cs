using com.etak.core.model;
using com.etak.core.model.revenueManagement;
using FluentNHibernate.Mapping;

namespace com.etak.postpaid.mapping.revenueManagement
{
 
    /// <summary>
    /// Class to map to Nhibernate class AccountCurrency
    /// </summary>
    public class AccountCurrencyMap : SubclassMap<AccountCurrency>
    {
        /// <summary>
        /// The constructor called by fluent to map the class. 
        /// </summary>
        public AccountCurrencyMap()
        {
            DiscriminatorValue(1);//means Currency
            Map(x => x.Currency).
                Column("BALANCE_UNITTYPEID").
                CustomType<ISO4217CurrencyCodes>();
        }
    }
    
}