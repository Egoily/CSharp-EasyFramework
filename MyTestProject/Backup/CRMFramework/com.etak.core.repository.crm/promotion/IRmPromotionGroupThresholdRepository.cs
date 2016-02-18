using System;
using com.etak.core.model;

namespace com.etak.core.repository.crm.promotion
{
    /// <summary>
    /// Repository Interface for <typeparamref name="TRmPromotionGroupThreshold"/> entity
    /// </summary>
    /// <typeparam name="TRmPromotionGroupThreshold">The type of the entity managed is or extends RmPromotionGroupThreshold</typeparam>
    public interface IRmPromotionGroupThresholdRepository<TRmPromotionGroupThreshold> : 
        IRepository<TRmPromotionGroupThreshold, Int32> where TRmPromotionGroupThreshold : RmPromotionGroupThreshold
    {

    }
}
