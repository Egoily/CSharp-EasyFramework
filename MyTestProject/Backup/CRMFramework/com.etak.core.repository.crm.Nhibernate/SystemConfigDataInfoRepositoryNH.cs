using System;
using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.repository.crm.configuration;
using com.etak.core.repository.crm.Nhibernate.Factory;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate
{
    /// <summary>
    /// Nhibernate repository for inheritance tree of entity SystemConfigDataInfo 
    /// </summary>
    /// <typeparam name="TSystemConfigDataInfo">Entity managed by the repository, is or extends SystemConfigDataInfo</typeparam>
    public class SystemConfigDataInfoRepositoryNH<TSystemConfigDataInfo> :
           NHibernateRepository<TSystemConfigDataInfo, String>,
            ISystemConfigDataInfoRepository<TSystemConfigDataInfo>   where TSystemConfigDataInfo : SystemConfigDataInfo
    {
        private const String CacheRegion = CacheRegions.SystemSettingsCacheRegion;

        /// <summary>
        /// Gets all entities of  TSystemConfigDataInfo
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TSystemConfigDataInfo> GetAllSystemConfigDateInfo()
        {
            return (GetQuery().
                                Cacheable().CacheRegion(CacheRegion).
                                Future());
        }

        /// <summary>
        /// Gets all entities of TSystemConfigDataInfo with a given item
        /// </summary>
        /// <param name="item">the item of the TSystemConfigDataInfo to retrieve</param>
        /// <returns>entities of TSystemConfigDataInfo with a given item</returns>
        public IEnumerable<TSystemConfigDataInfo> GetSystemConfigDateInfoByItem(string item)
        {
            return (GetQuery().Where(cg => cg.Item==item).
                                Cacheable().CacheRegion(CacheRegion).
                                Future());
        }

        /// <summary>
        /// Gets all entities of TSystemConfigDataInfo with a given name
        /// </summary>
        /// <param name="name">the name of the TSystemConfigDataInfo to retrieve</param>
        /// <returns>entities of TSystemConfigDataInfo with a given name</returns>
        public IEnumerable<TSystemConfigDataInfo> GetSystemConfigDateInfoByName(string name)
        {
            return (GetQuery().Where(cg=>cg.Name==name).
                                Cacheable().CacheRegion(CacheRegion).
                                Future());
        }
    }
}
