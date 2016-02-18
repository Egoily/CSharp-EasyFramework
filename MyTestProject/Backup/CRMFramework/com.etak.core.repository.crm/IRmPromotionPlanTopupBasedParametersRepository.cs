using System;
using System.Collections.Generic;
using com.etak.core.model;

namespace com.etak.core.repository.crm
{
    /// <summary>
    /// Repository interface for RmPromotionPlanTopupBasedParameters
    /// </summary>
    /// <typeparam name="TRmPromotionPlanTopupBasedParameters">The entity managed by the repository, is or extends RmPromotionPlanTopupBasedParameters</typeparam>
    public interface IRmPromotionPlanTopupBasedParametersRepository<TRmPromotionPlanTopupBasedParameters> : 
        IRepository<TRmPromotionPlanTopupBasedParameters, Object> where TRmPromotionPlanTopupBasedParameters : 
        RmPromotionPlanTopupBasedParameters
    {
        /// <summary>
        /// Gets the RmPromotionPlanTopupBasedParameters of a given promoionplanid and a given amount
        /// </summary>
        /// <param name="promoionplanid">the promotion plan id to which the promotion plan is asigned</param>
        /// <param name="amount">the threshold amount of the RmPromotionPlanTopupBasedParameters</param>
        /// <returns>The list of matching elements</returns>
        IEnumerable<TRmPromotionPlanTopupBasedParameters> GetByPromotionPlanIdAndAmount(int promoionplanid, decimal amount);

        /// <summary>
        /// Gets the RmPromotionPlanTopupBasedParameters of a given promoionplanid 
        /// </summary>
        /// <param name="promotionPlanID">the promotion plan id to which the promotion plan is asigned</param>
        /// <returns>The list of matching elements</returns>
        IEnumerable<TRmPromotionPlanTopupBasedParameters> GetByPromotionPlanId(int promotionPlanID);
    }
}
