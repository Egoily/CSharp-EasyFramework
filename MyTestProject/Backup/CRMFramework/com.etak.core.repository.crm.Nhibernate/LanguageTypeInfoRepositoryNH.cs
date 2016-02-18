using System;
using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.repository.crm.Nhibernate.Factory;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate
{
    /// <summary>
    /// Nhibernate repository for inheritance tree of entity LanguageTypeInfo 
    /// </summary>
    /// <typeparam name="TLanguageTypeInfo">Entity managed by the repository, is or extends LanguageTypeInfo</typeparam>
    public class LanguageTypeInfoRepositoryNH<TLanguageTypeInfo>
        : NHibernateRepository<TLanguageTypeInfo, Int32>,
       ILanguageTypeInfoRepository<TLanguageTypeInfo> where TLanguageTypeInfo : LanguageTypeInfo
    {
        private const String CacheRegion = CacheRegions.SystemSettingsCacheRegion;

        /// <summary>
        /// Gets all languages
        /// </summary>
        /// <returns>the list of all languages</returns>
        public IEnumerable<TLanguageTypeInfo> GetAllLanguages()
        {
            return (GetQuery().
                    Cacheable().CacheRegion(CacheRegion).
                    Future()); 
        }

        /// <summary>
        /// Gets a given language by it's Id
        /// </summary>
        /// <param name="langId">the id of the language</param>
        /// <returns>the languages with the given id</returns>
        public IEnumerable<LanguageTypeInfo> GetAllLanguageById(int langId)
        {
            return (GetQuery().
                    Where( x => x.LanguageID == langId).
                    Cacheable().CacheRegion(CacheRegion).
                    Future()); 
        }

       
    }
}
