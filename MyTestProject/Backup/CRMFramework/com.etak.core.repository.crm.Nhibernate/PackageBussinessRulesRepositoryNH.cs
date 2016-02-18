using com.etak.core.model;
using com.etak.core.repository.crm.Nhibernate.Factory;
using com.etak.core.repository.NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.etak.core.repository.crm.Nhibernate
{
   public class PackageBussinessRulesRepositoryNH<T> :
        NHibernateRepository<T, Int32>,
        IPackageBussinessRulesRepository<T>
        where T : PackageBussinessRules
    {

       private const String CACHE_REGION = CacheRegions.SystemSettingsCacheRegion;
       public IEnumerable<T> GetAllPackageRuleRelationships()
       {
           return GetQuery().CacheRegion(CACHE_REGION).Future();
       }
       public IEnumerable<T> GetPackageRuleRelationshipsByPackageID(int packageID)
       {
           return GetQuery().Where(rl => rl.PackageInfo.PackageID == packageID).Future();
       }
       public IEnumerable<T> GetPackageRuleRelationshipsByRuleID(int ruleID)
       {
           return GetQuery().Where(rl => rl.RuleInfo.Id == ruleID).Future();
       }
    }
}
