using System;
using com.etak.core.model.revenueManagement;

namespace com.etak.core.repository.crm.revenueManagement
{
    /// <summary>
    /// Repository Interface for <typeparamref name="TChargePrice"/> entity
    /// </summary>
    /// <typeparam name="TChargePrice">The type of the entity managed is or extends ChargePrice</typeparam>
    public interface IChargePriceRepository<TChargePrice> : IRepository<TChargePrice, Int64> where TChargePrice : ChargePrice
    {
    }
}
