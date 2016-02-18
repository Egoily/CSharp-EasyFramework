using com.etak.core.model;
using com.etak.core.repository.crm.Nhibernate.Factory;
using com.etak.core.repository.NHibernate.IDGeneration;
using FluentNHibernate.Mapping;

namespace com.etak.core.repository.crm.Nhibernate.promotion.mapping
{
    /// <summary>
    /// Fluent Nhibernate mapping class for RmPromotionGroupThreshold
    /// </summary>
    public class RmPromotionGroupThresholdMap : ClassMap<RmPromotionGroupThreshold>
    {
        /// <summary>
        /// Default constructor so fluent mappigns builds the hbm.xml file on runtime
        /// </summary>
        public RmPromotionGroupThresholdMap()
        {
            Schema("dbo");
            Table("RM_PROMOTIONGROUP_THRESHOLDS");
            DynamicUpdate();
            DynamicInsert();

            Cache.Region(CacheRegions.UserDealerCacheRegion).NonStrictReadWrite().IncludeAll();

            Id(x => x.ThresholdId, "THRESHOLDID").GeneratedBy.Custom<PrefixIdGenerator>();
            References(x => x.PromotionGroup).Column("PROMOTIONGROUPID");

            Map(x => x.ThresholdType).Column("THRESHOLDTYPE");
            Map(x => x.ThresholdValue).Column("THRESHOLDVALUE");
            Map(x => x.Direction).Column("THRESHOLDDIRECTION");
            Map(x => x.GenerateEventId).Column("GENERATE_EVENTID");
        }
    }
}