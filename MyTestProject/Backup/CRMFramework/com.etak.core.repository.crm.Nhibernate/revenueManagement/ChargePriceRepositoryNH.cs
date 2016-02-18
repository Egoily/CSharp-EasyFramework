using System;
using com.etak.core.model.revenueManagement;
using com.etak.core.repository.crm.revenueManagement;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate.revenueManagement
{
    /// <summary>
    /// Nhibernate repository for inheritance tree of entity ChargePrice 
    /// </summary>
    /// <typeparam name="TChargePrice">Entity managed by the repository, is or extends ChargePrice</typeparam>
    public class ChargePriceRepositoryNH<TChargePrice> : 
        NHibernateRepository<TChargePrice, Int64>, 
        IChargePriceRepository<TChargePrice> where TChargePrice : ChargePrice
    {
    }
}
