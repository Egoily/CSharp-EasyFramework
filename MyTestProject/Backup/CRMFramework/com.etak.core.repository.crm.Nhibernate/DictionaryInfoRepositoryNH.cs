using System;
using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.repository.crm.configuration;
using com.etak.core.repository.crm.Nhibernate.Factory;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate
{
    /// <summary>
    /// Repository for <typeparamref name="TDictionaryInfo"/> implementation based on NHibernate
    /// </summary>
    /// <typeparam name="TDictionaryInfo">The entity managed by the interface, is or extends DictionaryInfo</typeparam>
    public class DictionaryInfoRepositoryNH<TDictionaryInfo>
        : NHibernateRepository<TDictionaryInfo, Int32>,
       IDictionaryInfoRepository<TDictionaryInfo> where TDictionaryInfo : DictionaryInfo
    {


        private const String CacheRegion = CacheRegions.SystemSettingsCacheRegion;

        /// <summary>
        /// Gets all the dictionary for the given dictionary type
        /// </summary>
        /// <param name="p">the type of the dictionaries to get</param>
        /// <returns>The entities that fullfil the requirements</returns>
        public IEnumerable<TDictionaryInfo> GetByDictType(int p)
        {
            return(GetQuery().
                    Where(x => x.DictionaryType == p).
                    Cacheable().CacheRegion(CacheRegion).
                    Future());
        }
        
    }
}
