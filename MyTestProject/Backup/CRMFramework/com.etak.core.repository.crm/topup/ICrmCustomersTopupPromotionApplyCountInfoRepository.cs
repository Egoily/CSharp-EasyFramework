using System;
using System.Collections.Generic;
using com.etak.core.model;

namespace com.etak.core.repository.crm.topup
{
    /// <summary>
    /// The respository interface for <typeparamref name="TCrmCustomersTopupPromotionApplyCountInfo"/> entity
    /// </summary>
    /// <typeparam name="TCrmCustomersTopupPromotionApplyCountInfo">The entity managed by the interface, is or extends CustomerAccountAssociation</typeparam>
    public interface ICrmCustomersTopupPromotionApplyCountInfoRepository<TCrmCustomersTopupPromotionApplyCountInfo> 
        : IRepository<TCrmCustomersTopupPromotionApplyCountInfo, Int32> where TCrmCustomersTopupPromotionApplyCountInfo : CrmCustomersTopupPromotionApplyCountInfo
    {
        /// <summary>
        /// Gets the list of CrmCustomersTopupPromotionApplyCountInfo that mathes the conditions
        /// </summary>
        /// <param name="resource">The resource to filter CrmCustomersTopupPromotionApplyCountInfo</param>
        /// <param name="bonusId">The bonusId to filter CrmCustomersTopupPromotionApplyCountInfo</param>
        /// <param name="year">The year to filter CrmCustomersTopupPromotionApplyCountInfo</param>
        /// <param name="month">The month to filter CrmCustomersTopupPromotionApplyCountInfo</param>
        /// <returns>The list of matching TCrmCustomersTopupPromotionApplyCountInfo</returns>
        IEnumerable<TCrmCustomersTopupPromotionApplyCountInfo> GetApplyCount(string resource, int bonusId, string year, string month);        
    }
}
