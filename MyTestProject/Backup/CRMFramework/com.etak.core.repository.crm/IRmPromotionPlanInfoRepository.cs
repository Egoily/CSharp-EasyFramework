using System;
using System.Collections.Generic;
using com.etak.core.model;

namespace com.etak.core.repository.crm
{
    /// <summary>
    /// Repository interface for RmPromotionPlanInfo
    /// </summary>
    /// <typeparam name="TRmPromotionPlanInfo">The type of the entity managed by the repository, is or extends RmPromotionPlanInfo</typeparam>
    public interface IRmPromotionPlanInfoRepository<TRmPromotionPlanInfo> : IRepository<TRmPromotionPlanInfo, Int32> where TRmPromotionPlanInfo : RmPromotionPlanInfo
    {
        /// <summary>
        /// Gets all promotion plans
        /// </summary>
        /// <returns>all the promotion plans</returns>
        IEnumerable<TRmPromotionPlanInfo> GetAllRmPromotionPlanInfo();

        /// <summary>
        /// Gets all promotion plans fetching extra associations
        /// </summary>
        /// <param name="promotionPlanId"></param>
        /// <returns></returns>
        IEnumerable<TRmPromotionPlanInfo> GetRmPromotionPlanInfo(int promotionPlanId);

        /// <summary>
        /// gets all promotion plans for a dealer id
        /// </summary>
        /// <param name="dealerId">the vmno/dealer that owns the promotion plan</param>
        /// <returns>the list of promotion plans</returns>
        IEnumerable<TRmPromotionPlanInfo> GetAllRmPromotionPlanForDealerId(int dealerId);

        /// <summary>
        /// gets all promotion plans for a dealer id that are visible
        /// </summary>
        /// <param name="dealerId">the vmno/dealer that owns the promotion plan</param>
        /// <returns>the list of visible promotion plans</returns>
        IEnumerable<TRmPromotionPlanInfo> GetAllVisibleRmPromotionPlanForDealerId(int dealerId);

        /// <summary>
        /// gets all promotion plans for a category
        /// </summary>
        /// <param name="promotionCategorys">the category that the promotion plan is</param>
        /// <returns>the list of promotion plans</returns>
        IEnumerable<TRmPromotionPlanInfo> GetByCategoryId(PromotionCategorys promotionCategorys);

        /// <summary>
        /// Gets all promotion plans of a set of vmnos/dealers that are in a category
        /// </summary>
        /// <param name="mvnoIds">the list of vmnoid to filter</param>
        /// <param name="promotionCategoryIds">the list of categories to filer</param>
        /// <returns>the list of promotion plans</returns>
        IEnumerable<TRmPromotionPlanInfo> GetRmPromotionPlanByVMNOAndPromotionCategory(List<int> mvnoIds, int[] promotionCategoryIds);

        /// <summary>
        /// Gets all promotion plans of a set of vmnos/dealers that are in a category, fetchin plans and rules
        /// </summary>
        /// <param name="mvnoIds">the list of vmnoid to filter</param>
        /// <param name="promotionCategoryIds">the list of categories to filer</param>
        /// <returns>the list of promotion plans</returns>
        IEnumerable<TRmPromotionPlanInfo> GetRmPromotionPlanByVMNOAndPromotionCategoryWithPlanAndRule(List<int> mvnoIds, int[] promotionCategoryIds);

        /// <summary>
        /// Gets a set of promotion plans by their ID
        /// </summary>
        /// <param name="promotionPlanIds">the list of promotion plan ids</param>
        /// <returns>the list of promotion plans</returns>
        IEnumerable<TRmPromotionPlanInfo> GetRmPromotionPlanInfoByIds(List<int> promotionPlanIds);
    }
}
