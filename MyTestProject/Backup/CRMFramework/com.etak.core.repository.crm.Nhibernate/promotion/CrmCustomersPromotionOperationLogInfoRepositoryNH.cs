using System;
using System.Collections.Generic;
using System.Linq;
using com.etak.core.model;
using com.etak.core.repository.crm.promotion;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate
{
    /// <summary>
    /// Nhibernate repository for inheritance tree of entity CrmCustomersPromotionOperationLogInfo 
    /// </summary>
    /// <typeparam name="TCrmCustomersPromotionOperationLogInfo">Entity managed by the repository, is or extends CrmCustomersPromotionOperationLogInfo</typeparam>
    public class CrmCustomersPromotionOperationLogInfoRepositoryNH<TCrmCustomersPromotionOperationLogInfo> : 
        NHibernateRepository<TCrmCustomersPromotionOperationLogInfo, Int64>, 
        ICrmCustomersPromotionOperationLogInfoRepository<TCrmCustomersPromotionOperationLogInfo> where TCrmCustomersPromotionOperationLogInfo : CrmCustomersPromotionOperationLogInfo
    {
        /// <summary>
        /// Gets the CrmCustomersPromotionOperationLogInfo for a given customer of a set of promotion plan details
        /// </summary>
        /// <param name="customerId">the id of the customer of the changes of the promotion(s)</param>
        /// <param name="promotionPlanDetailIds">the plan detail of the changes of the promotion(s)</param>
        /// <returns>he CrmCustomersPromotionOperationLogInfo</returns>
        public IEnumerable<TCrmCustomersPromotionOperationLogInfo> GetPrePromotionOperationLogs(int customerId, IList<int> promotionPlanDetailIds )
        {
            return GetQuery().Where(ee => ee.CUSTOMERID == customerId)
                .AndRestrictionOn(ee => ee.PROMOTIONPLANDETAILID)
                .IsIn(promotionPlanDetailIds.ToArray())
                .OrderBy(ee => ee.OPERATIONDATE).Desc.Future();
        }
    }
}
