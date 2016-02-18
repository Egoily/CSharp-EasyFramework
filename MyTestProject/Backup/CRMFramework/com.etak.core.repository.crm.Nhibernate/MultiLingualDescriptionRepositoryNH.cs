using System;
using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.repository.NHibernate;
using com.etak.core.repository.crm.Nhibernate.Factory;

namespace com.etak.core.repository.crm.Nhibernate
{
    /// <summary>
    /// Nhibernate implementation of IMultiLingualInfoRepository
    /// </summary>
    /// <typeparam name="TMultiLingualDescription">Entity managed by  the repository is or extends MultiLingualDescription</typeparam>
    public class MultiLingualDescriptionRepositoryNH<TMultiLingualDescription> : NHibernateRepository<TMultiLingualDescription, Int32>,
       IMultiLingualDescriptionRepository<TMultiLingualDescription> where TMultiLingualDescription : MultiLingualDescription
    {
        private const String CacheRegion = CacheRegions.SystemSettingsCacheRegion;
        /// <summary>
        /// Get MultiLinualDescription by type
        /// </summary>
        /// <param name="type">type of description</param>
        /// <returns></returns>
        public IEnumerable<TMultiLingualDescription> GetByType(MultiLingualType type)
        {
            return GetQuery().Where(x => x.Type == type).Cacheable().CacheRegion(CacheRegion).Future();
        }
        
    }
}
