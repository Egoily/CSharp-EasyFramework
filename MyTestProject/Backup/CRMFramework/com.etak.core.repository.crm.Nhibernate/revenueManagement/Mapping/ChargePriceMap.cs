using com.etak.core.model.revenueManagement;
using com.etak.core.repository.crm.Nhibernate.Factory;
using FluentNHibernate.Mapping;

namespace com.etak.core.repository.crm.Nhibernate.revenueManagement
{
    /// <summary>
    /// NHibernate Fluent mapping for ChargePrice
    /// </summary>
    public class ChargePriceMap : ClassMap<ChargePrice>
    {
        /// <summary>
        /// Default constructor so fluent map builds the xml on runtime.
        /// </summary>
        public ChargePriceMap()
        {
            Schema("dbo");
            Table("RM_CHARGE_PRICES");
            DynamicUpdate();
            
            Cache.Region(CacheRegions.CatalogCacheRegion).NonStrictReadWrite().IncludeAll();

            CompositeId(x => x.Id)
                .KeyProperty(c => c.ChargeId, "CHARGEID")
                .KeyProperty(x => x.StartDate, "STARTDATE");


            References(x => x.Charge).Column("CHARGEID").Not.Insert().Not.Update();
            Map(x => x.StartDate).Column("STARTDATE").Not.Insert().Not.Update();
            
            Map(x => x.EndDate).Column("ENDDATE");
            Map(x => x.Amount).Column("AMOUNT");

            Map(x => x.Currency).Column("ISO4217_CURRENCY_CODE");           
        }
    }
}