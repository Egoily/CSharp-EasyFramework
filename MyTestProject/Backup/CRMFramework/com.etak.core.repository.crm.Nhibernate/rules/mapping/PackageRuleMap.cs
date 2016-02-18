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
    public class DummyPackageRule : PackageRule
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        /// <param name="ruleCaller"></param>
        public override void ExecuteActions(PackageRuleParam param, RuleCaller ruleCaller) {}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        /// <param name="ruleCaller"></param>
        /// <returns></returns>
        public override EligibleResult IsEligible(PackageRuleParam param, RuleCaller ruleCaller) { return EligibleResult.Unknown; }
    }

    /// <summary>
    /// Fluent Nhibernate mapping class for DummyPackageRule
    /// </summary>
    public class DummyPackageRuleMap : SubclassMap<DummyPackageRule>
    {
        /// <summary>
        /// Default constructor so fluent mappigns builds the hbm.xml file on runtime
        /// </summary>
        public DummyPackageRuleMap()
        {
            DiscriminatorValue("DummyPackageRule");
        }         
    }
    #endregion

    /// <summary>
    /// Fluent Nhibernate mapping class for PackageRule
    /// </summary>
    public class PackageRuleMap : SubclassMap<PackageRule>
    {
        /// <summary>
        /// Default constructor so fluent map builds the xml on runtime.
        /// </summary>
        public PackageRuleMap()
        {
            DiscriminatorValue("PackageRule");
        }       
    }
}
