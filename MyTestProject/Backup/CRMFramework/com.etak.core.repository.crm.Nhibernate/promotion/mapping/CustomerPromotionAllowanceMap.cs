using com.etak.core.model;
using com.etak.core.repository.NHibernate.IDGeneration;
using FluentNHibernate.Mapping;

namespace com.etak.core.repository.crm.Nhibernate.promotion.mapping
{
    /// <summary>
    /// NHibernate mapping for CustomerPromotionAllowance entity
    /// </summary>
    public class CustomerPromotionAllowanceMap : ClassMap<CustomerPromotionAllowance>
    {
        /// <summary>
        /// Default constructor so fluent mappigns builds the hbm.xml file on runtime
        /// </summary>
        public CustomerPromotionAllowanceMap()
        {
            Table("CRM_CUSTOMERS_PROMOTION_LIMITS");
            Id(x => x.PromotionAllowanceId, "PROMOTION_LIMITID").GeneratedBy.Custom<PrefixIdGenerator>();
            Map(x => x.PeriodStart, "STARTDATE");
            Map(x => x.PeriodEnd, "ENDDATE");
            Map(x => x.InitialAllowance, "INITIALLIMIT");
            Map(x => x.Allowance, "CURRENTLIMIT");
            References(x => x.CustomerPromotion, "PROMOTIONID");
        }
    }
}
