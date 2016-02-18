using com.etak.core.model;
using com.etak.core.repository.crm.Nhibernate.Factory;
using com.etak.core.repository.NHibernate.IDGeneration;
using FluentNHibernate.Mapping;

namespace com.etak.core.repository.crm.Nhibernate.rules.mapping
{
    /// <summary>
    /// Fluent Nhibernate mapping class for BussinessRule
    /// </summary>
    public class BusinessRuleMap : ClassMap<BussinessRule>
    {
        /// <summary>
        /// public constructor for fluent Nhibernate
        /// </summary>
        public BusinessRuleMap()
        {
            Table("CRM_BUSINESSRULES");
            Cache.NonStrictReadWrite().Region(CacheRegions.CatalogCacheRegion);
            DynamicUpdate();
            DynamicInsert();
            DiscriminateSubClassesOnColumn("ClassType");
            Id(x => x.Id, "Id").GeneratedBy.Custom<PrefixIdGenerator>();
            Map(x=> x.ClassType, "ClassType").Not.Update().Not.Insert();
            Map(x=> x.Name, "Name").Length(255);
            Map(x=> x.JSonConfig, "JSONConfig").Length(255);



        }
    }
}
