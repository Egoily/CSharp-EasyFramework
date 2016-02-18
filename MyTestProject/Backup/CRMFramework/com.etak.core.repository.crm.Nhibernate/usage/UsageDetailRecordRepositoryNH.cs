using System;
using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate.usage
{
    class UsageDetailRecordRepositoryNH<TUsageDetailRecord> : NHibernateRepository<TUsageDetailRecord, Int64>,
        IUsageDetailRecordRepository<TUsageDetailRecord> where TUsageDetailRecord : UsageDetailRecord

    {
        /*
         * EXEC CRM25_CUSTOMER_QA.dbo.USP_GET_CDR_USAGE_DETAIL_API
 @CUSTOMERID INT = NULL, -- if @CUSTOMERID is not completed, the @MSISDN will be used
 @MSISDN VARCHAR(25) = NULL,
 @STARTDATE DATETIME = NULL, -- DATE 1 filter
 @ENDDATE DATETIME = NULL, -- DATE 2 filter
 @DATE_FILTER_RULE BIT = 0 -- 0 - filtered by STARTDATE, 1 - filtered by ENDDATE
         */

        public IEnumerable<TUsageDetailRecord> GetRecordsByMsisdnBetweenDates(Int32 vmoId, Int32? SubServiceTypeId, string msisdn, DateTime? startDate, DateTime? endDate, FilterCdrDates dateToFilter)
        {
            if (String.IsNullOrWhiteSpace(msisdn))
                throw new ArgumentNullException("msisdn", "Can't GetRecordsByMsisdnBetweenDates msisdn was null");

            return (ExecUSP_GET_CDR_USAGE_DETAIL_API(vmoId, SubServiceTypeId, msisdn, null, startDate, endDate, dateToFilter));
        }

        public IEnumerable<TUsageDetailRecord> GetRecordsByCustomerIdBetweenDates(Int32 vmoId, Int32 ? SubServiceTypeId, int customerId, DateTime? startDate, DateTime? endDate, FilterCdrDates dateToFilter)
        {
            return (ExecUSP_GET_CDR_USAGE_DETAIL_API(vmoId,SubServiceTypeId, null, customerId, startDate, endDate, dateToFilter));
        }

        private IEnumerable<TUsageDetailRecord> ExecUSP_GET_CDR_USAGE_DETAIL_API(Int32 vmoId, Int32 ? SubServiceTypeId, string msisdn, int? customerId, DateTime? startDate, DateTime? endDate,  FilterCdrDates dateToFilter)
        {
            bool filterRule = dateToFilter == FilterCdrDates.StartDate;

            return _session.CreateSQLQuery("exec USP_GET_CDR_USAGE_DETAIL_API @MVNOID=:MVNOID, @SUBSERVICETYPEID=:SUBSERVICETYPEID, @CUSTOMERID=:CUSTOMERID, @MSISDN=:MSISDN, @STARTDATE=:STARTDATE, @ENDDATE=:ENDDATE, @DATE_FILTER_RULE=:DATE_FILTER_RULE ")
            .AddEntity(typeof(TUsageDetailRecord))
                .SetParameter("MVNOID", vmoId)
                .SetParameter("SUBSERVICETYPEID", SubServiceTypeId)
                .SetParameter("CUSTOMERID", customerId)
                .SetParameter("MSISDN", msisdn)
                .SetParameter("STARTDATE", startDate)
                .SetParameter("ENDDATE", endDate.Value)
                .SetParameter("DATE_FILTER_RULE", filterRule)
                .List<TUsageDetailRecord>();
        }
    }
}
