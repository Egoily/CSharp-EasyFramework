using System;
using System.Collections.Generic;
using com.etak.core.model;

namespace com.etak.core.repository.crm.rules
{
    /// <summary>
    /// Repository interface for BussinessRule
    /// </summary>
    /// <typeparam name="TCrmBusinessRuleInfo">The entity managed by the repository, is or extends BussinessRule</typeparam>
    public interface ICrmBussinessRuleInfoRepository<TCrmBusinessRuleInfo> :
        IRepository<TCrmBusinessRuleInfo, Int64> where TCrmBusinessRuleInfo : BussinessRule
    {
        /// <summary>
        /// This should not be required, the class types are managed by the Repository, not 
        /// </summary>
        /// <param name="classType">the class type of the TCrmBusinessRuleInfo</param>
        /// <returns>the list of mathing TCrmBusinessRuleInfo</returns>
        IEnumerable<TCrmBusinessRuleInfo> GetByClassType(string classType);
    }
}
