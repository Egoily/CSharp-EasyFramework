using com.etak.core.model;
using System;
using System.Collections.Generic;
using System.Text;

namespace com.etak.core.repository.crm
{
    public interface IPackageBussinessRulesRepository<T> : IRepository<T, Int32> where T : PackageBussinessRules
    {
        IEnumerable<T> GetAllPackageRuleRelationships();
        IEnumerable<T> GetPackageRuleRelationshipsByPackageID(int packageID);
        IEnumerable<T> GetPackageRuleRelationshipsByRuleID(int ruleID);
    }
}
