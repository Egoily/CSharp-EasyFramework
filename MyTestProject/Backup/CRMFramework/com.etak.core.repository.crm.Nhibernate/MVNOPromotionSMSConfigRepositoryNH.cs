using System;
using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.repository.crm.Nhibernate.Factory;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate
{
    /// <summary>
    /// Nhibernate repository for inheritance tree of entity MVNOPromotionSMSConfig 
    /// </summary>
    /// <typeparam name="TMVNOPromotionSMSConfig">Entity managed by the repository, is or extends MVNOPromotionSMSConfig</typeparam>
    public class MVNOPromotionSMSConfigRepositoryNH<TMVNOPromotionSMSConfig> :
        NHibernateRepository<TMVNOPromotionSMSConfig, Int32>,
        IMVNOPromotionSMSConfigRepository<TMVNOPromotionSMSConfig> where TMVNOPromotionSMSConfig : MVNOPromotionSMSConfig
    {
        private const String CacheRegion = CacheRegions.UserDealerCacheRegion;

        IEnumerable<TMVNOPromotionSMSConfig> IMVNOPromotionSMSConfigRepository<TMVNOPromotionSMSConfig>.GetByPromotionPlanIdAndType(int promotionPlanId, int type)
        {
            return GetQuery().Where(x => x.PROMOTIONPLANID == promotionPlanId && x.SMSType == type).Cacheable().CacheRegion(CacheRegion).Future();
        }
    }
}
