using com.etak.core.model;
using com.etak.core.repository.crm.Nhibernate.Factory;
using com.etak.core.repository.NHibernate.IDGeneration;
using FluentNHibernate.Mapping;

namespace com.etak.core.repository.crm.Nhibernate.promotion.mapping
{
    /// <summary>
    /// Fluent Nhibernate mapping class for AbstractPromotionRenewAction
    /// </summary>
    public class AbstractPromotionRenewActionMap: ClassMap<AbstractPromotionRenewAction>
    {
        /// <summary>
        /// Default constructor so fluent mappigns builds the hbm.xml file on runtime
        /// </summary>
        public AbstractPromotionRenewActionMap()
        {
            Table("RM_PROMOTIONPLAN_DETAIL_RENEW_ACTIONS");
            Cache.NonStrictReadWrite().Region(CacheRegions.CatalogCacheRegion);
            DynamicUpdate();
            DynamicInsert();
            DiscriminateSubClassesOnColumn("ACTIONTYPE");
            Id(x => x.Id, "PPD_RENEWACTIONID").GeneratedBy.Custom<PrefixIdGenerator>();
            Map(x => x.Priority, "PRIORITY");
            Map(x => x.ActionType, "ACTIONTYPE").Not.Update().Not.Insert();
            Map(x => x.ConfigValue1, "CONFIG_VALUE1").Length(255);
            Map(x => x.ConfigValue2, "CONFIG_VALUE2").Length(255);
            References(x => x.PreRefferredPromotion, "PRE_PROMOTIONPLAN_DETAILID");
            References(x => x.RefferredPromotion, "POST_PROMOTIONPLAN_DETAILID");
        }
    }


}
