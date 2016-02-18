using System;
using System.Collections.Generic;
using com.etak.core.model;

namespace com.etak.core.repository.crm
{
    /// <summary>
    /// Repository interface for MVNOPromotionSMSConfig
    /// </summary>
    /// <typeparam name="TMVNOPromotionSMSConfig">The entity managed by the repository, is or extends MVNOPromotionSMSConfig</typeparam>
    public interface IMVNOPromotionSMSConfigRepository<TMVNOPromotionSMSConfig> : IRepository<TMVNOPromotionSMSConfig, Int32> 
        where TMVNOPromotionSMSConfig : MVNOPromotionSMSConfig
    {
        /// <summary>
        /// Get al promotion sms configuration of a promotion plan id and a type
        /// </summary>
        /// <param name="promotionPlanId">the promotion plan that has the sms</param>
        /// <param name="type">the id of the type for the sms</param>
        /// <returns>the list of the sms configurations</returns>
        IEnumerable<TMVNOPromotionSMSConfig> GetByPromotionPlanIdAndType(int promotionPlanId, int type);
    }
}
