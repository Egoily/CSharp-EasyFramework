using System;
using System.Collections.Generic;
using com.etak.core.model;

namespace com.etak.core.repository.crm
{
    /// <summary>
    /// The respository interface for <typeparamref name="TCrmDefaultProvisionInfo"/> entity
    /// </summary>
    /// <typeparam name="TCrmDefaultProvisionInfo">The entity managed by the interface, is or extends CrmDefaultProvisionInfo</typeparam>
    public interface ICrmDefaultProvisionInfoRepository<TCrmDefaultProvisionInfo> : IRepository<TCrmDefaultProvisionInfo, Int32> where TCrmDefaultProvisionInfo : CrmDefaultProvisionInfo
    {
        /// <summary>
        /// Gets the TCrmDefaultProvisionInfo with a given anem
        /// </summary>
        /// <param name="name">the name of the TCrmDefaultProvisionInfo to retrieve</param>
        /// <returns>the list of TCrmDefaultProvisionInfo that have that name</returns>
        IEnumerable<TCrmDefaultProvisionInfo> GetProvisionByName(string name);
    }
}
