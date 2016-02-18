using com.etak.core.model;
using com.etak.core.repository.crm.Nhibernate.Factory;
using com.etak.core.repository.NHibernate.IDGeneration;
using FluentNHibernate.Mapping;

namespace com.etak.core.repository.crm.Nhibernate.rules.mapping
{
    class PackageBussinessRulesMap : ClassMap<PackageBussinessRules>
    {
        public PackageBussinessRulesMap()
        {
            Table("CRM_BUSINESSRULES_PACKAGES");
            Cache.NonStrictReadWrite().Region(CacheRegions.UserDealerCacheRegion);
            DynamicUpdate();
            DynamicInsert();

            Id(x => x.ID, "Id").GeneratedBy.Custom<PrefixIdGenerator>();

            References(x => x.PackageInfo, "PACKAGEID");
            References(x => x.RuleInfo, "RULEID");
            
        }
    }
}
