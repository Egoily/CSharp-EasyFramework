using System;
using com.etak.core.model;

namespace com.etak.core.repository.crm
{
    /// <summary>
    /// Repository Interface for <typeparamref name="TRmPromotionPlanDetailInfo"/> entity
    /// </summary>
    /// <typeparam name="TRmPromotionPlanDetailInfo">The type of the entity managed is or extends RmPromotionPlanDetailInfo</typeparam>
    public interface IRmPromotionPlanDetailInfoRepository<TRmPromotionPlanDetailInfo> : IRepository<TRmPromotionPlanDetailInfo, Int32> where TRmPromotionPlanDetailInfo : RmPromotionPlanDetailInfo
    {
    }
}
