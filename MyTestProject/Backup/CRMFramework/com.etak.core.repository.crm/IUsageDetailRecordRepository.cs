using System;
using System.Collections.Generic;
using com.etak.core.model;

namespace com.etak.core.repository.crm
{
    /// <summary>
    /// Posible date filters
    /// </summary>
    public enum FilterCdrDates
    {
        /// <summary>
        /// Filters the CDR by the start date of the CDR
        /// </summary>
        StartDate,

        /// <summary>
        /// Filters the CDR by the end date of the CDR
        /// </summary>
        EndDate,
    }

    /// <summary>
    /// Repository interface for UsageDetailRecord
    /// </summary>
    public interface IUsageDetailRecordRepository<TUsageDetailRecord> : IRepository<TUsageDetailRecord, Int64>
        where TUsageDetailRecord : UsageDetailRecord
    {
        /// <summary>
        /// Gets all all usage details (CDR) for a given msisdn bewteen dates
        /// </summary>
        /// <param name="vmoId"></param>
        /// <param name="SubServiceTypeId"></param>
        /// <param name="msisdn"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="dateToFilter"></param>
        /// <returns></returns>
        IEnumerable<TUsageDetailRecord> GetRecordsByMsisdnBetweenDates(Int32 vmoId, Int32? SubServiceTypeId, string msisdn,
            DateTime? startDate, DateTime? endDate, FilterCdrDates dateToFilter);



        /// <summary>
        /// Gets all usage details (CDR) for a given customerId between dates
        /// </summary>
        /// <param name="vmoId"></param>
        /// <param name="SubServiceTypeId"></param>
        /// <param name="customerId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="dateToFilter"></param>
        /// <returns></returns>
        IEnumerable<TUsageDetailRecord> GetRecordsByCustomerIdBetweenDates(Int32 vmoId, Int32? SubServiceTypeId, int customerId,
            DateTime? startDate, DateTime? endDate, FilterCdrDates dateToFilter);


    }
}
