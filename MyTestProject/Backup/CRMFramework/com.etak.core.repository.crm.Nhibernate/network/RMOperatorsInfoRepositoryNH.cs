using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.repository.crm.network;
using com.etak.core.repository.crm.Nhibernate.Factory;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate.network
{
    /// <summary>
    /// NHibernate implementation of  IRMOperatorsInfoRepository
    /// </summary>
    /// <typeparam name="TRMOperatorsInfo">The type of entity managed by the repository, is or extends RMOperatorsInfo</typeparam>
    public class RMOperatorsInfoRepositoryNH<TRMOperatorsInfo>
        : NHibernateRepository< TRMOperatorsInfo, string>,
        IRMOperatorsInfoRepository<TRMOperatorsInfo> where TRMOperatorsInfo : RMOperatorsInfo
    {
        /// <summary>
        /// Gets all the operators with a given CN operator code, should return only one
        /// </summary>
        /// <param name="cnOperatorCode">the operator code in the central node</param>
        /// <returns>all the operators with a given CN operator code</returns>
        public IEnumerable<TRMOperatorsInfo> GetByCNOperatorCode(string cnOperatorCode)
        {
            return GetQuery()
                .Where(x => x.CNOperatorCode == cnOperatorCode)
                .Cacheable()
                .CacheRegion(CacheRegions.SystemSettingsCacheRegion)
                .Future();
        }
    }
}
