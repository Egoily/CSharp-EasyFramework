using System;
using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.repository.crm.Nhibernate.Factory;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate
{
    /// <summary>
    /// Nhibernate repository for inheritance tree of entity ServicesInfo 
    /// </summary>
    /// <typeparam name="TRmPromotionGroupInfo">Entity managed by the repository, is or extends ServicesInfo</typeparam>
    public class RmPromotionGroupInfoRepositoryNH<TRmPromotionGroupInfo> : 
        NHibernateRepository<TRmPromotionGroupInfo, Int32>, 
        IRmPromotionGroupInfoRepository<TRmPromotionGroupInfo> where TRmPromotionGroupInfo : RmPromotionGroupInfo
    {
        private const String CacheRegion = CacheRegions.UserDealerCacheRegion;

        /// <summary>
        /// Gets all promotion groups of an vmno
        /// </summary>
        /// <param name="mvnoIds">the list of mvnos owning the RmPromotionGroupInfo</param>
        /// <returns>all promotion groups of an vmno</returns>
        public IEnumerable<TRmPromotionGroupInfo> GetByMvnos(List<int> mvnoIds)
        {
            if (mvnoIds != null && mvnoIds.Count > 0)
                return GetQuery().WhereRestrictionOn(x => x.MvnoID).IsIn(mvnoIds.ToArray()).Cacheable().CacheRegion(CacheRegion).Future();
            return GetAll();
        }


        /// <summary>
        /// Gets all promotion groups with a group name
        /// </summary>
        /// <param name="groupName">the name of the promotion group</param>
        /// <returns>all promotion groups with a group name</returns>
        public IEnumerable<TRmPromotionGroupInfo> GetByGroupName(string groupName)
        {
            return GetQuery().Where(x => x.GroupName == groupName && (x.StartPeriod == null || x.StartPeriod == 1)).
                  Cacheable().CacheRegion(CacheRegion).
                  Future();
        }


        /// <summary>
        /// Gets all promotion groups with a group name that belong to a dealer
        /// </summary>
        /// <param name="groupName">the name of the promotion group</param>
        /// <param name="vmnoInfo">the dealer of the promotion group</param>
        /// <returns>all promotion groups with a group name of a dealer</returns>
        public IEnumerable<TRmPromotionGroupInfo> GetByGroupNameAndDealer(string groupName, DealerInfo vmnoInfo)
        {
            return GetQuery().Where(x => x.GroupName == groupName && (x.StartPeriod == null || x.StartPeriod == 1) && x.MvnoID == vmnoInfo.DealerID).
              Cacheable().CacheRegion(CacheRegion).
              Future();
        }

        /// <summary>
        /// Gets all promotion groups with a group name that belong to a dealer
        /// </summary>
        /// <param name="groupName">the name of the promotion group</param>
        /// <param name="vmnoInfo">the dealer of the promotion group</param>
        /// <returns>all promotion groups with a group name of a dealer</returns>
        public  IEnumerable<TRmPromotionGroupInfo> GetAllGroupsByNameAndDealer(string groupName, DealerInfo vmnoInfo)
        {
            return GetQuery().Where(x => x.GroupName == groupName && x.MvnoID == vmnoInfo.DealerID).
                        Cacheable().CacheRegion(CacheRegion).
                        Future();
        }


        /// <summary>
        /// Gets all promotion groups
        /// </summary>
        /// <returns>all promotion groups</returns>
        public IEnumerable<TRmPromotionGroupInfo> GetAll()
        {
            return GetQuery().Cacheable().CacheRegion(CacheRegion).Future();
        }
    }
}
