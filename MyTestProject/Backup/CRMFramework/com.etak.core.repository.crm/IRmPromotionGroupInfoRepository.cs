using System;
using System.Collections.Generic;
using com.etak.core.model;

namespace com.etak.core.repository.crm
{
    /// <summary>
    /// Repository interface for RmPromotionGroupInfo
    /// </summary>
    /// <typeparam name="TRmPromotionGroupInfo">The entity managed by the repository, is or extends RmPromotionGroupInfo</typeparam>
    public interface IRmPromotionGroupInfoRepository<TRmPromotionGroupInfo> : IRepository<TRmPromotionGroupInfo, Int32> 
        where TRmPromotionGroupInfo : RmPromotionGroupInfo
    {
        /// <summary>
        /// Gets all promotion groups
        /// </summary>
        /// <returns>all promotion groups</returns>
        IEnumerable<TRmPromotionGroupInfo> GetAll();

        /// <summary>
        /// Gets all promotion groups of an vmno
        /// </summary>
        /// <param name="mvnoIds">the list of mvnos owning the RmPromotionGroupInfo</param>
        /// <returns>all promotion groups of an vmno</returns>
        IEnumerable<TRmPromotionGroupInfo> GetByMvnos(List<int> mvnoIds);

        /// <summary>
        /// Gets all promotion groups with a group name
        /// </summary>
        /// <param name="groupName">the name of the promotion group</param>
        /// <returns>all promotion groups with a group name</returns>
        IEnumerable<TRmPromotionGroupInfo> GetByGroupName(string groupName);

        /// <summary>
        /// Gets all promotion groups with a group name that belong to a dealer
        /// </summary>
        /// <param name="groupName">the name of the promotion group</param>
        /// <param name="vmnoInfo">the dealer of the promotion group</param>
        /// <returns>all promotion groups with a group name of a dealer</returns>
        IEnumerable<TRmPromotionGroupInfo> GetByGroupNameAndDealer(string groupName, DealerInfo vmnoInfo);

        /// <summary>
        /// Gets all promotion groups with a group name that belong to a dealer
        /// </summary>
        /// <param name="groupName">the name of the promotion group</param>
        /// <param name="vmnoInfo">the dealer of the promotion group</param>
        /// <returns>all promotion groups with a group name of a dealer</returns>
        IEnumerable<TRmPromotionGroupInfo> GetAllGroupsByNameAndDealer(string groupName, DealerInfo vmnoInfo);
    }
}
