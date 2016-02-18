using System;
using System.Collections.Generic;
using com.etak.core.model;

namespace com.etak.core.repository.crm
{
    /// <summary>
    /// The respository interface for <typeparamref name="TRmPromotionGroupMember"/> entity
    /// </summary>
    /// <typeparam name="TRmPromotionGroupMember">The entity managed by the interface, is or extends RmPromotionGroupMember</typeparam>
    public interface IRmPromotionGroupMemberRepository<TRmPromotionGroupMember> : IRepository<TRmPromotionGroupMember, Int32> where TRmPromotionGroupMember : RmPromotionGroupMember
    {
        /// <summary>
        /// Gets all TRmPromotionGroupMember
        /// </summary>
        /// <returns>all TRmPromotionGroupMember</returns>
        IEnumerable<TRmPromotionGroupMember> GetAll();

        /// <summary>
        /// Gets all TRmPromotionGroupMember of a groupId (id  of PromotionGroup) relation
        /// </summary>
        /// <returns>all TRmPromotionGroupMember</returns>
        IEnumerable<TRmPromotionGroupMember> GetByPromotionGroup(int groupId);
    }
}
