using System;
using System.Linq;
using com.etak.core.model;
using com.etak.core.repository.crm.topup;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate
{
    /// <summary>
    /// Repository based on NHibernate for CrmCustomersTopupBonusApplyCountInfo entity inheritance tree
    /// </summary>
    /// <typeparam name="TCrmCustomersTopupBonusApplyCountInfo">the type of entity managed, is or extends CrmCustomersTopupBonusApplyCountInfo</typeparam>
    public class CrmCustomersTopupBonusApplyCountInfoRepositoryNH<TCrmCustomersTopupBonusApplyCountInfo> :
        NHibernateRepository<TCrmCustomersTopupBonusApplyCountInfo, Int32>, 
        ICrmCustomersTopupBonusApplyCountInfoRepository<TCrmCustomersTopupBonusApplyCountInfo> where TCrmCustomersTopupBonusApplyCountInfo : CrmCustomersTopupBonusApplyCountInfo
    {
        /// <summary>
        /// Gets the TCrmCustomersTopupBonusApplyCountInfo
        /// </summary>
        /// <param name="resource">The resource to filter the TCrmCustomersTopupBonusApplyCountInfo</param>
        /// <param name="bonusId">The Id of the bonus to filter the TCrmCustomersTopupBonusApplyCountInfo</param>
        /// <param name="year">The year to filter the TCrmCustomersTopupBonusApplyCountInfo</param>
        /// <param name="month">The month to filter the TCrmCustomersTopupBonusApplyCountInfo</param>
        /// <returns>the matching TCrmCustomersTopupBonusApplyCountInfo</returns>
        public TCrmCustomersTopupBonusApplyCountInfo GetApplyCount(string resource, int bonusId, string year, string month)
        {
            return GetQuery().Where(ee => ee.Resource == resource)
                .And(ee => ee.BonusId == bonusId)
                .And(ee => ee.Year == year)
                .And(ee => ee.Month == month).Future().FirstOrDefault();
        }

    }
}
