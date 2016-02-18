using System;
using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate
{
    /// <summary>
    /// Repository based on NHibernate for RmPromotionGroupMember entity inheritance tree
    /// </summary>
    /// <typeparam name="TRmPromotionGroupMember">the type of entity managed, is or extends RmPromotionGroupMember</typeparam>
    public class RmPromotionGroupMemberRepositoryNH<TRmPromotionGroupMember> : 
        NHibernateRepository<TRmPromotionGroupMember, Int32>,
        IRmPromotionGroupMemberRepository<TRmPromotionGroupMember> where TRmPromotionGroupMember : RmPromotionGroupMember
    {
        /// <summary>
        /// Gets all TRmPromotionGroupMember
        /// </summary>
        /// <returns>all TRmPromotionGroupMember</returns>
        public IEnumerable<TRmPromotionGroupMember> GetAll()
        {
            return GetQuery().Future();
        }

        /// <summary>
        /// Gets all TRmPromotionGroupMember of a groupId (id  of PromotionGroup) relation
        /// </summary>
        /// <returns>all TRmPromotionGroupMember</returns>
        public IEnumerable<TRmPromotionGroupMember> GetByPromotionGroup(int groupId)
        {
            return GetQuery().Where(ee => ee.PromotionGroup.PromotionGroupID == groupId).Future();
        }
    }
}
