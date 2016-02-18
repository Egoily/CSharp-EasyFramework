using System;
using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.repository.crm.Nhibernate.Factory;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate
{
    /// <summary>
    /// Nhibernate repository for inheritance tree of entity SettingExtendDetailInfo 
    /// </summary>
    /// <typeparam name="TSettingExtendDetailInfo">Entity managed by the repository, is or extends SettingExtendDetailInfo</typeparam>
    public class SettingExtendDetailInfoRepositoryNH<TSettingExtendDetailInfo> : 
        NHibernateRepository<TSettingExtendDetailInfo, Int32>,   //Extends
        ISettingExtendDetailInfoRepository<TSettingExtendDetailInfo> where TSettingExtendDetailInfo : SettingExtendDetailInfo //Interface
    {

        private const String CACHE_REGION = CacheRegions.SystemSettingsCacheRegion;


        /// <summary>
        /// Gets the SettingExtendDetailInfo for a given dealer and category
        /// </summary>
        /// <param name="dealerId">the dealer of the entity</param>
        /// <param name="extendSettingCategory">the category of the entity</param>
        /// <returns>the list of entities that fulfill the conditions</returns>
        public IEnumerable<TSettingExtendDetailInfo> GetByDealerIdAndCategoryId(int dealerId, ExtendSettingCategory extendSettingCategory)
        {
            return (GetQuery().
                Where(x => x.DealerId == dealerId && x.CategoryId == (int)extendSettingCategory).
                Cacheable().CacheRegion(CACHE_REGION).
                Future());
        }

        /// <summary>
        /// Gets the SettingExtendDetailInfo for a given dealer
        /// </summary>
        /// <param name="dealerId">The id of the dealer to filter the settings</param>
        /// <returns>The list of matching entities that fulfill the conditions</returns>
        public IEnumerable<SettingExtendDetailInfo> GetByDealerId(Int32 dealerId)
        {
            return (GetQuery().
                Where(x => x.DealerId == dealerId ).
                Cacheable().CacheRegion(CACHE_REGION).
                Future());
        }

 
    }
}
