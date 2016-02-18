using System;
using com.etak.core.model;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate
{
    /// <summary>
    /// Repository based on NHibernate for RmPromotionPlanDetailInfo entity inheritance tree
    /// </summary>
    /// <typeparam name="TRmPromotionPlanDetailInfo">the type of entity managed, is or extends RmPromotionPlanDetailInfo</typeparam>
    public class RmPromotionPlanDetailInfoRepositoryNH<TRmPromotionPlanDetailInfo> : NHibernateRepository<TRmPromotionPlanDetailInfo, Int32>,
        IRmPromotionPlanDetailInfoRepository<TRmPromotionPlanDetailInfo> where TRmPromotionPlanDetailInfo : RmPromotionPlanDetailInfo
    {
    }
}
