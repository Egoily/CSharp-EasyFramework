using System;
using System.Collections.Generic;
using System.Linq;
using com.etak.core.model;
using com.etak.core.repository.crm.Nhibernate.Factory;
using com.etak.core.repository.NHibernate;
using NHibernate;
using NHibernate.Collection;

namespace com.etak.core.repository.crm.Nhibernate
{
    /// <summary>
    /// Nhibernate repository for inheritance tree of entity RmPromotionPlanInfo 
    /// </summary>
    /// <typeparam name="TRmPromotionPlanInfo">Entity managed by the repository, is or extends RmPromotionPlanInfo</typeparam>
    public class RmPromotionPlanInfoRepositoryNH<TRmPromotionPlanInfo>
                    : NHibernateRepository<TRmPromotionPlanInfo, Int32>,   //Extends and gets basic CRUD operations
                      IRmPromotionPlanInfoRepository<TRmPromotionPlanInfo> //Implementes 
                        where TRmPromotionPlanInfo : RmPromotionPlanInfo
    {

        private const String CacheRegion = CacheRegions.UserDealerCacheRegion;

        /// <summary>
        /// Gets all promotion plans
        /// </summary>
        /// <returns>all the promotion plans</returns>
        public IEnumerable<TRmPromotionPlanInfo> GetAllRmPromotionPlanInfo()
        {

            IEnumerable<TRmPromotionPlanInfo> promotionPlans =
             GetQuery().
                    Cacheable().CacheRegion(CacheRegion).
                    Future();

            GetQuery().
                  Fetch(x => x.RmPromotionPlanDetailInfoList).Eager.
                  Cacheable().CacheRegion(CacheRegion).
                  Future();

            return (promotionPlans.ToList());
        }

        /// <summary>
        /// Gets all promotion plans fetching extra associations
        /// </summary>
        /// <param name="promotionPlanId"></param>
        /// <returns></returns>
        public IEnumerable<TRmPromotionPlanInfo> GetRmPromotionPlanInfo(int promotionPlanId)
        {
            IEnumerable<TRmPromotionPlanInfo> promotionPlans = GetQuery()
                .Where(x => x.PromotionPlanId == promotionPlanId)
                .Cacheable().CacheRegion(CacheRegion)
                .Future();

            GetQuery()
                     .Where(x => x.PromotionPlanId == promotionPlanId)
                     .Fetch(x => x.RmPromotionPlanDetailInfoList).Eager
                     .Cacheable().CacheRegion(CacheRegion)
                     .Future();

            return (promotionPlans);
        }

        /// <summary>
        /// gets all promotion plans for a dealer id
        /// </summary>
        /// <param name="dealerId">the vmno/dealer that owns the promotion plan</param>
        /// <returns>the list of promotion plans</returns>
        public IEnumerable<TRmPromotionPlanInfo> GetAllRmPromotionPlanForDealerId(int dealerId)
        {
            IEnumerable<TRmPromotionPlanInfo> promotionPlans =
               GetQuery().
                    Where(x => x.DealerId == dealerId).
                    Cacheable().CacheRegion(CacheRegion).
                    Future();

            GetQuery().
                Where(x => x.DealerId == dealerId).
                Fetch(x => x.RmPromotionPlanDetailInfoList).Eager.
                Cacheable().CacheRegion(CacheRegion).
                Future();

            return (promotionPlans);

        }

        /// <summary>
        /// gets all promotion plans for a dealer id that are visible
        /// </summary>
        /// <param name="dealerId">the vmno/dealer that owns the promotion plan</param>
        /// <returns>the list of visible promotion plans</returns>
        public IEnumerable<TRmPromotionPlanInfo> GetAllVisibleRmPromotionPlanForDealerId(int dealerId)
        {
            IEnumerable<TRmPromotionPlanInfo> promotionPlans =
               GetQuery().
                    Where(x => x.DealerId == dealerId).And(x => x.APIVisible != 0).
                    Cacheable().CacheRegion(CacheRegion).
                    Future();

            GetQuery().
                Where(x => x.DealerId == dealerId).And(x => x.APIVisible != 0).
                Fetch(x => x.RmPromotionPlanDetailInfoList).Eager.
                Cacheable().CacheRegion(CacheRegion).
                Future();

            return (promotionPlans);
        }

        /// <summary>
        /// gets all promotion plans for a category
        /// </summary>
        /// <param name="promotionCategorys">the category that the promotion plan is</param>
        /// <returns>the list of promotion plans</returns>
        public IEnumerable<TRmPromotionPlanInfo> GetByCategoryId(PromotionCategorys promotionCategorys)
        {

            IEnumerable<TRmPromotionPlanInfo> promotionPlans =
               GetQuery().
                    Where(x => x.PromotionCategoryId == (Int32)promotionCategorys).
                    Cacheable().CacheRegion(CacheRegion).
                    Future();

            GetQuery().
                Where(x => x.PromotionCategoryId == (Int32)promotionCategorys).
                Fetch(x => x.RmPromotionPlanDetailInfoList).Eager.
                Cacheable().CacheRegion(CacheRegion).
                Future();

            GetQuery().
               Where(x => x.PromotionCategoryId == (Int32)promotionCategorys).
               Fetch(x => x.RmSpecificNumberGroupInfoList).Eager.
               Cacheable().CacheRegion(CacheRegion).
               Future();

            return (promotionPlans);

        }

        /// <summary>
        /// Gets all promotion plans of a set of vmnos/dealers that are in a category
        /// </summary>
        /// <param name="mvnoIds">the list of vmnoid to filter</param>
        /// <param name="promotionCategoryIds">the list of categories to filer</param>
        /// <returns>the list of promotion plans</returns>
        public IEnumerable<TRmPromotionPlanInfo> GetRmPromotionPlanByVMNOAndPromotionCategory(List<int> mvnoIds, int[] promotionCategoryIds)
        {
            if (mvnoIds == null || mvnoIds.Count == 0)
                return new List<TRmPromotionPlanInfo>();

            IQueryOver<TRmPromotionPlanInfo, TRmPromotionPlanInfo> queryOver = GetQuery();
            queryOver.AndRestrictionOn(x => x.DealerId).IsIn(mvnoIds.ToArray());

            if (promotionCategoryIds.Length > 0 && !promotionCategoryIds.Contains(-1))
            {
                queryOver.AndRestrictionOn(x => x.PromotionCategoryId).IsIn(promotionCategoryIds.ToArray());
            }

            IEnumerable<TRmPromotionPlanInfo> promotionPlans = queryOver.Cacheable().CacheRegion(CacheRegion).Future();

            return (promotionPlans);
        }

        /// <summary>
        /// Gets all promotion plans of a set of vmnos/dealers that are in a category, fetchin plans and rules
        /// </summary>
        /// <param name="mvnoIds">the list of vmnoid to filter</param>
        /// <param name="promotionCategoryIds">the list of categories to filer</param>
        /// <returns>the list of promotion plans</returns>
        public IEnumerable<TRmPromotionPlanInfo> GetRmPromotionPlanByVMNOAndPromotionCategoryWithPlanAndRule(List<int> mvnoIds, int[] promotionCategoryIds)
        {
            if (mvnoIds == null || mvnoIds.Count == 0)
                return new List<TRmPromotionPlanInfo>();

            IQueryOver<TRmPromotionPlanInfo, TRmPromotionPlanInfo> rootQuery = GetQuery();
            rootQuery.WhereRestrictionOn(x => x.DealerId).IsIn(mvnoIds.ToArray());

            if (promotionCategoryIds.Length > 0 && !promotionCategoryIds.Contains(-1))
            {
                rootQuery.AndRestrictionOn(x => x.PromotionCategoryId).IsIn(promotionCategoryIds.ToArray());
            }

            IQueryOver<TRmPromotionPlanInfo, TRmPromotionPlanInfo> queryOverRmPromotionPlanDetailInfoList = rootQuery.Clone();
            IQueryOver<TRmPromotionPlanInfo, TRmPromotionPlanInfo> queryOverRmPromotionPlanRuleInfoList = rootQuery.Clone();

            IEnumerable<TRmPromotionPlanInfo> promotionPlans = rootQuery              
              .Cacheable().CacheRegion(CacheRegion).Future();

            queryOverRmPromotionPlanDetailInfoList
                .Fetch(x => x.RmPromotionPlanDetailInfoList).Eager
                .Cacheable().CacheRegion(CacheRegion).Future();

            queryOverRmPromotionPlanRuleInfoList
              .Fetch(x => x.RmPromotionPlanRuleInfoList).Eager
              .Cacheable().CacheRegion(CacheRegion).Future();


            foreach (TRmPromotionPlanInfo p in promotionPlans)
            {
                _session.GetSessionImplementation().InitializeCollection(p.RmPromotionPlanDetailInfoList as IPersistentCollection, false);
                _session.GetSessionImplementation().InitializeCollection(p.RmPromotionPlanRuleInfoList as IPersistentCollection, false);
            }
            return (promotionPlans);
        }

        /// <summary>
        /// Gets a set of promotion plans by their ID
        /// </summary>
        /// <param name="promotionPlanIds">the list of promotion plan ids</param>
        /// <returns>the list of promotion plans</returns>
        public IEnumerable<TRmPromotionPlanInfo> GetRmPromotionPlanInfoByIds(List<int> promotionPlanIds)
        {
            IEnumerable<TRmPromotionPlanInfo> promotionPlans = GetQuery()
               .WhereRestrictionOn(x => x.PromotionPlanId).IsIn(promotionPlanIds.ToArray())
               .Cacheable().CacheRegion(CacheRegion)
               .Future();

            GetQuery()
                      .WhereRestrictionOn(x => x.PromotionPlanId).IsIn(promotionPlanIds.ToArray())
                     .Fetch(x => x.RmPromotionPlanDetailInfoList).Eager
                     .Cacheable().CacheRegion(CacheRegion)
                     .Future();

            return (promotionPlans);
        }
    }
}
