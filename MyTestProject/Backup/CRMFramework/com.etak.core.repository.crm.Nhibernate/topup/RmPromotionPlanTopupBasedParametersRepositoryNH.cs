using System;
using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate
{
    /// <summary>
    /// Repository based on NHibernate for RmPromotionPlanTopupBasedParameters entity inheritance tree
    /// </summary>
    /// <typeparam name="TRmPromotionPlanTopupBasedParameters">the type of entity managed, is or extends RmPromotionPlanTopupBasedParameters</typeparam>
    public class RmPromotionPlanTopupBasedParametersRepositoryNH<TRmPromotionPlanTopupBasedParameters> : 
        NHibernateRepository<TRmPromotionPlanTopupBasedParameters, Object>, 
        IRmPromotionPlanTopupBasedParametersRepository<TRmPromotionPlanTopupBasedParameters> where TRmPromotionPlanTopupBasedParameters : RmPromotionPlanTopupBasedParameters
    {
        /// <summary>
        /// Gets the RmPromotionPlanTopupBasedParameters of a given promoionplanid and a given amount
        /// </summary>
        /// <param name="promoionplanid">the promotion plan id to which the promotion plan is asigned</param>
        /// <param name="amount">the threshold amount of the RmPromotionPlanTopupBasedParameters</param>
        /// <returns>The list of matching elements</returns>
        public IEnumerable<TRmPromotionPlanTopupBasedParameters> GetByPromotionPlanIdAndAmount(int promoionplanid, decimal amount)
        {
            return (GetQuery().Where(x => x.PromotionPlanID == promoionplanid && x.TopupThresholdAmount == amount).Future());
        }

        /// <summary>
        /// Gets the RmPromotionPlanTopupBasedParameters of a given promoionplanid 
        /// </summary>
        /// <param name="promotionPlanId">the promotion plan id to which the promotion plan is asigned</param>
        /// <returns>The list of matching elements</returns>
        public IEnumerable<TRmPromotionPlanTopupBasedParameters> GetByPromotionPlanId(int promotionPlanId)
        {
            return (GetQuery().Where(x => x.PromotionPlanID == promotionPlanId).Future());
        }
    }
}
