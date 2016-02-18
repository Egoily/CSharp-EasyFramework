using com.etak.core.model;
using FluentNHibernate.Mapping;

namespace com.etak.core.repository.crm.Nhibernate.rules.mapping
{
    #region abstract hack
   
    /// <summary>
    /// Due to the fact that implementations will be done
    /// outside the framework, we need to add this hack 
    /// to start the unit tests. So we register a concrete class
    /// </summary>
    public class DummyPackageGroupRule : PromotionGroupRule
    {
      
        /// <summary>
        /// ignore
        /// </summary>
        /// <param name="param">ignore</param>
        /// <param name="ruleCaller">ignore</param>
        public override void ExecuteActions(PromotionGroupRuleParam param, RuleCaller4PromotionGroup ruleCaller) {  }

        /// <summary>
        /// ignore
        /// </summary>
        /// <param name="param">ignore</param>
        /// <param name="ruleCaller">ignore</param>
        /// <returns></returns>
        public override EligibleResult IsEligible(PromotionGroupRuleParam param, RuleCaller4PromotionGroup ruleCaller) { return EligibleResult.Unknown; }
    }

    /// <summary>
    /// Due to the fact that implementations will be done
    /// outside the framework, we need to add this hack 
    /// to start the unit tests. So we register a concrete class
    /// </summary>
    public class DummyPackageGroupRuleMap : SubclassMap<DummyPackageGroupRule>
    {
        /// <summary>
        /// ignore
        /// </summary>
        public DummyPackageGroupRuleMap()
        {
            DiscriminatorValue("DummyPackageGroupRule");
        }
    }
    #endregion

    /// <summary>
    /// Fluent nhibernate mapping for PromotionGroupRule
    /// </summary>
    public class PromotionGroupRuleMap : SubclassMap<PromotionGroupRule>
    {
        /// <summary>
        /// Default constructor so fluent map builds the xml on runtime.
        /// </summary>
        public PromotionGroupRuleMap()
        {
            DiscriminatorValue("PromotionGroupRule");
        }
    }
}
