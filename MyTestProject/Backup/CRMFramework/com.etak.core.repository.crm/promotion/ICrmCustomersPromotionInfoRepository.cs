using System;
using System.Collections.Generic;
using com.etak.core.model;

namespace com.etak.core.repository.crm.promotion
{
    /// <summary>
    /// Repository interface for CrmCustomersPromotionInfo
    /// </summary>
    /// <typeparam name="TCrmCustomersPromotionInfo">The entity managed by the repository, is or extends CrmCustomersPromotionInfo</typeparam>
    public interface ICrmCustomersPromotionInfoRepository<TCrmCustomersPromotionInfo> :
        IRepository<TCrmCustomersPromotionInfo, Int64> where TCrmCustomersPromotionInfo : CrmCustomersPromotionInfo
    {


        /// <summary>
        /// Loads a Promotion by Id loading associations of promotion
        /// </summary>
        /// <param name="promotionKey">the id of the promotion to load</param>
        /// <returns>the loaded promotion</returns>
        IEnumerable<TCrmCustomersPromotionInfo> LoadAsociations(long promotionKey);

        /// <summary>
        /// Gets all promotions in a set of Ids that belogns to a customer iD
        /// </summary>
        /// <param name="customerId">the id of the customer, owner of the promotions</param>
        /// <param name="promotionPlanIds">the ids of the promotion plans</param>
        /// <returns>the promotins that fullfill the conditions</returns>
        IEnumerable<TCrmCustomersPromotionInfo> GetCustomerPromotionPlanById(int customerId, long[] promotionPlanIds);
    }
}
