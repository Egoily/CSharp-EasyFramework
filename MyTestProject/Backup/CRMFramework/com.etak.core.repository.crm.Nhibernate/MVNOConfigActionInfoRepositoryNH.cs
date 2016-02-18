using System;
using System.Collections.Generic;
using System.Linq;
using com.etak.core.model;
using com.etak.core.repository.crm.Nhibernate.Factory;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate
{
    /// <summary>
    /// Nhibernate repository for inheritance tree of entity MVNOConfigActionInfo 
    /// </summary>
    /// <typeparam name="TMVNOConfigActionInfo">Entity managed by the repository, is or extends MVNOConfigActionInfo</typeparam>
    public class MVNOConfigActionInfoRepositoryNH<TMVNOConfigActionInfo>
        : NHibernateRepository<TMVNOConfigActionInfo, Int32>,
       IMVNOConfigActionInfoRepository<TMVNOConfigActionInfo> where TMVNOConfigActionInfo : MVNOConfigActionInfo
    {
        private const String CacheRegion = CacheRegions.UserDealerCacheRegion;

        /// <summary>
        /// Retrieve the all the MVNO config which are for all MVNOs.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TMVNOConfigActionInfo> GetDefaultMVNOConfigs()
        {
            return GetQuery().Where(config => config.DealerInfo == null)
                             .And(config => config.StatusID != 0)
                             .Cacheable().CacheRegion(CacheRegion)
                             .Future();
        }

        /// <summary>
        /// Retrieve the MVNO config according to the MVNO ID. 
        /// </summary>
        /// <param name="MVNOId">the id to filter the MVNO</param>
        /// <returns>the list of matchin TMVNOConfigActionInfo by MVNOId</returns>
        public IEnumerable<TMVNOConfigActionInfo> GetMVNOConfigsById(Int32 MVNOId)
        {
            return GetQuery().Where(config => config.DealerInfo != null && config.DealerInfo.DealerID == MVNOId)
                             .And(config => config.StatusID != 0)
                             .Cacheable().CacheRegion(CacheRegion)
                             .Future();
        }

        /// <summary>
        /// Retrieve the MVNO config according to the MVNO IDs.
        /// </summary>
        /// <param name="MVNOIds">the ids to filter the MVNO</param>
        /// <returns>the list of matching TMVNOConfigActionInfo by MVNOIds</returns>
        public IEnumerable<TMVNOConfigActionInfo> GetMVNOConfigsByIds(IList<Int32> MVNOIds)
        {
            return GetQuery().Where(config => config.DealerInfo != null)
                             .AndRestrictionOn(config => config.DealerInfo.DealerID).IsIn(MVNOIds.ToArray())
                             .And(config => config.StatusID != 0)
                             .Cacheable().CacheRegion(CacheRegion)
                             .Future();
        }

        /// <summary>
        /// Retrieve the MVNO config according to the MVNO IDs, and category and statusId
        /// </summary>
        /// <param name="MVNOId">the id to filter the MVNO</param>
        /// <param name="categoryName">the category to filter</param>
        /// <param name="statusId">the statud in which the config needs to be</param>
        /// <returns>the list of matching TMVNOConfigActionInfo by MVNOIds</returns>
        public IEnumerable<TMVNOConfigActionInfo> GetMVNOConfigsByIDAndName(int MVNOId, string categoryName, int statusId)
        {
            return GetQuery().Where(x => x.DealerInfo != null && x.DealerInfo.DealerID == MVNOId)
                           .And(x => x.StatusID != 0&&x.StatusID==statusId)
                           .And(x=>x.CategoryName==categoryName)
                           .Cacheable().CacheRegion(CacheRegion)
                           .Future();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="MVNOId">the id to filter the MVNO</param>
        /// <param name="categoryId">the category to filter</param>
        /// <param name="statusId">the statud in which the config needs to be</param>
        /// <returns>the list of matching TMVNOConfigActionInfo by MVNOIds</returns>
        public IEnumerable<TMVNOConfigActionInfo> GetMVNOConfigsByMvnoIdAndCategoryId(int MVNOId, int categoryId, int statusId)
        {
            return GetQuery().Where(x => x.DealerInfo != null && x.DealerInfo.DealerID == MVNOId)
                           .And(x => x.StatusID != 0 && x.StatusID == statusId)
                           .And(x => x.CategoryID == categoryId)
                           .Cacheable().CacheRegion(CacheRegion)
                           .Future();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="MVNOId">the id to filter the MVNO</param>
        /// <param name="categoryId">the category to filter</param>
        /// <param name="item">The item to filter</param>
        /// <param name="statusId">the statud in which the config needs to be</param>
        /// <returns>the list of matching TMVNOConfigActionInfo by MVNOIds</returns>
        public IEnumerable<TMVNOConfigActionInfo> GetMVNOConfigsByMvnoIdAndCategoryIdAndItem(int MVNOId, int categoryId, string item, int statusId)
        {
            return GetQuery().Where(x => x.DealerInfo != null && x.DealerInfo.DealerID == MVNOId)
                         .And(x => x.StatusID != 0 && x.StatusID == statusId)
                         .And(x => x.CategoryID == categoryId)
                          .And(x => x.Item == item)
                         .Cacheable().CacheRegion(CacheRegion)
                         .Future();
        }
    }
}
