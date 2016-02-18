using System;
using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.repository.crm.Nhibernate.Factory;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate
{
    /// <summary>
    /// Repository for TCrmDefaultProvisionInfo entity
    /// </summary>
    /// <typeparam name="TCrmDefaultProvisionInfo">The type of the repository, must extend CrmDefaultProvisionInfo</typeparam>
    public class CrmDefaultProvisionInfoRepositoryNH<TCrmDefaultProvisionInfo> :
        NHibernateRepository<TCrmDefaultProvisionInfo, Int32>,
        ICrmDefaultProvisionInfoRepository<TCrmDefaultProvisionInfo>
        where TCrmDefaultProvisionInfo : CrmDefaultProvisionInfo 
    {
        private const String CacheRegion = CacheRegions.SystemSettingsCacheRegion;

        /// <summary>
        /// Gets all the provisions for a given name
        /// </summary>
        /// <param name="name">the name to look up provisions</param>
        /// <returns>the list of provisions in future mode</returns>
        public IEnumerable<TCrmDefaultProvisionInfo> GetProvisionByName(string name)
        {
            return GetQuery().Where(x => x.PROVISIONAME == name).Cacheable().CacheRegion(CacheRegion).Future();
        }
    }
}
