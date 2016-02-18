using System;
using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.model.revenueManagement;

namespace com.etak.core.repository.crm.revenueManagement
{
    /// <summary>
    /// The respository interface for <typeparamref name="TBillCycle"/> entity
    /// </summary>
    /// <typeparam name="TBillCycle">The entity managed by the interface, is or extends BillCycle</typeparam>
    public interface IBillCycleRepository<TBillCycle> : IRepository<TBillCycle, Int32> where TBillCycle : BillCycle
    {
        /// <summary>
        /// Gets all the billing cycles for a mvno
        /// </summary>
        /// <param name="dealer">the vmno owning the billing cycles</param>
        /// <returns>the list of billing cycles</returns>
        IEnumerable<TBillCycle> GetBillingCyclesForVMNO(DealerInfo dealer);
    }
}
