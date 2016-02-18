using System;
using System.Collections.Generic;
using System.Linq;
using com.etak.core.model;
using com.etak.core.repository.crm.promotion;
using com.etak.core.repository.NHibernate;
using NHibernate;

namespace com.etak.core.repository.crm.Nhibernate
{
    /// <summary>
    /// Nhibernate repository for inheritance tree of entity CrmCustomersPromotionInfo 
    /// </summary>
    /// <typeparam name="TCrmCustomersPromotionInfo">Entity managed by the repository, is or extends CrmCustomersPromotionInfo</typeparam>
    public class CrmCustomersPromotionInfoRepositoryNH<TCrmCustomersPromotionInfo> : 
        NHibernateRepository<TCrmCustomersPromotionInfo, Int64>,
        ICrmCustomersPromotionInfoRepository<TCrmCustomersPromotionInfo> where TCrmCustomersPromotionInfo : CrmCustomersPromotionInfo
    {
        /// <summary>
        /// Loads a Promotion by Id loading associations of promotion
        /// </summary>
        /// <param name="promotionKey">the id of the promotion to load</param>
        /// <returns>the loaded promotion</returns>
        public IEnumerable<TCrmCustomersPromotionInfo> LoadAsociations(long promotionKey)
        {
            return GetQuery()
                .Fetch(x => x.PromotionDetail).Eager
                .Where(x => x.PromotionId == promotionKey)
                .Future();

        }

        /// <summary>
        /// Gets all promotions in a set of Ids that belogns to a customer iD
        /// </summary>
        /// <param name="customerId">the id of the customer, owner of the promotions</param>
        /// <param name="promotionPlanIds">the ids of the promotion plans</param>
        /// <returns>the promotins that fullfill the conditions</returns>
        public IEnumerable<TCrmCustomersPromotionInfo> GetCustomerPromotionPlanById(int customerId, long[] promotionPlanIds)
        {
            IQueryOver<TCrmCustomersPromotionInfo, TCrmCustomersPromotionInfo> rootQuery = GetQuery();
            rootQuery.Where(x => x.Customer.CustomerID.Value == customerId);

            if (promotionPlanIds != null && promotionPlanIds.Length > 0 && !promotionPlanIds.Contains(0) && !promotionPlanIds.Contains(-1))
            {
                rootQuery
                    .Inner.JoinQueryOver<RmPromotionPlanDetailInfo>(o => o.PromotionDetail)
                    .Inner.JoinQueryOver<RmPromotionPlanInfo>(o => o.RmPromotionPlanInfo)
                    .WhereRestrictionOn(x => x.PromotionPlanId).IsIn(promotionPlanIds.ToArray());
            }

            return rootQuery.Future();

        }

    }
}
