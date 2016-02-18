using System;
using System.Collections.Generic;
using com.etak.core.model.revenueManagement;

namespace com.etak.core.repository.crm.revenueManagement
{
    /// <summary>
    /// Repository for BillRun entity
    /// </summary>
    /// <typeparam name="TBillRun">The type of the entity that is or extends BillRun</typeparam>
    public interface IBillRunRepository<TBillRun> : IRepository<TBillRun, Int32> where TBillRun : BillRun
    {
        /// <summary>
        /// gets all the bill runs for a billing cycle
        /// </summary>
        /// <param name="billcycle">the owner of the bill runs to recover</param>
        /// <returns>the list of bill runs for a billing cycle</returns>
        IEnumerable<TBillRun> GetBillRunsForBillCycle(BillCycle billcycle);

        /// <summary>
        /// gets all the bill runs for a billing cycle
        /// </summary>
        /// <param name="billCycle">the owner of the bill runs to recover</param>
        /// <param name="dateRange">The date in which the bill run start/end date must be between</param>
        /// <returns>the list of bill runs for a billing cycle</returns>
        IEnumerable<TBillRun> GetBillRunInDatesForBillCycle(BillCycle billCycle, DateTime dateRange);

    }
}
