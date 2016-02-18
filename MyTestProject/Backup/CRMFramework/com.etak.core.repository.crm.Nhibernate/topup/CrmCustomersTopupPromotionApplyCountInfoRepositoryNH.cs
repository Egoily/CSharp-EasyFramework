using System;
using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.repository.crm.topup;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate
{
    /// <summary>
    /// Nhibernate repository for inheritance tree of entity CrmCustomersTopupPromotionApplyCountInfo 
    /// </summary>
    /// <typeparam name="TCrmCustomersTopupPromotionApplyCountInfo">Entity managed by the repository, is or extends CrmCustomersTopupPromotionApplyCountInfo</typeparam>
    public class CrmCustomersTopupPromotionApplyCountInfoRepositoryNH<TCrmCustomersTopupPromotionApplyCountInfo> :
        NHibernateRepository<TCrmCustomersTopupPromotionApplyCountInfo, Int32>, 
        ICrmCustomersTopupPromotionApplyCountInfoRepository<TCrmCustomersTopupPromotionApplyCountInfo> where TCrmCustomersTopupPromotionApplyCountInfo : CrmCustomersTopupPromotionApplyCountInfo
    {
        /// <summary>
        /// Gets the list of CrmCustomersTopupPromotionApplyCountInfo that mathes the conditions
        /// </summary>
        /// <param name="resource">The resource to filter CrmCustomersTopupPromotionApplyCountInfo</param>
        /// <param name="bonusId">The bonusId to filter CrmCustomersTopupPromotionApplyCountInfo</param>
        /// <param name="year">The year to filter CrmCustomersTopupPromotionApplyCountInfo</param>
        /// <param name="month">The month to filter CrmCustomersTopupPromotionApplyCountInfo</param>
        /// <returns>The list of matching TCrmCustomersTopupPromotionApplyCountInfo</returns>
        public IEnumerable<TCrmCustomersTopupPromotionApplyCountInfo> GetApplyCount(string resource, int bonusId, string year, string month)
        {
            return GetQuery().Where(ee => ee.Resource == resource)
               .And(ee => ee.BonusId == bonusId)
               .And(ee => ee.Year == year)
               .And(ee => ee.Month == month).Future();
        }

    }
}
