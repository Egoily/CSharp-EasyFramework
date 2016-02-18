using System;
using com.etak.core.model;
using com.etak.core.repository.crm.promotion;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate
{
    /// <summary>
    /// Repository based on NHibernate for RmPromotionGroupThreshold entity inheritance tree
    /// </summary>
    /// <typeparam name="TRmPromotionGroupThreshold">the type of entity managed, is or extends RmPromotionGroupThreshold</typeparam>
    public class RmPromotionGroupThresholdRepositoryNH<TRmPromotionGroupThreshold> : NHibernateRepository<TRmPromotionGroupThreshold, Int32>, 
        IRmPromotionGroupThresholdRepository<TRmPromotionGroupThreshold> where TRmPromotionGroupThreshold : RmPromotionGroupThreshold
    {

    }
}
