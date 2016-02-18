using System.Collections.Generic;
using com.etak.core.model;

namespace com.etak.core.repository.crm.promotion
{
    /// <summary>
    /// Repository Interface for <typeparamref name="TCrmCustomersPromotionOperationLogInfo"/> entity
    /// </summary>
    /// <typeparam name="TCrmCustomersPromotionOperationLogInfo">The type of the entity managed is or extends CrmCustomersPromotionOperationLogInfo</typeparam>
    public interface ICrmCustomersPromotionOperationLogInfoRepository<TCrmCustomersPromotionOperationLogInfo> : IRepository<TCrmCustomersPromotionOperationLogInfo, long> where TCrmCustomersPromotionOperationLogInfo : CrmCustomersPromotionOperationLogInfo
    {
        /// <summary>
        /// Gets the CrmCustomersPromotionOperationLogInfo for a given customer of a set of promotion plan details
        /// </summary>
        /// <param name="customerId">the id of the customer of the changes of the promotion(s)</param>
        /// <param name="promotionPlanDetailIds">the plan detail of the changes of the promotion(s)</param>
        /// <returns>he CrmCustomersPromotionOperationLogInfo</returns>
        IEnumerable<TCrmCustomersPromotionOperationLogInfo> GetPrePromotionOperationLogs(int customerId, IList<int> promotionPlanDetailIds);
    }
}
