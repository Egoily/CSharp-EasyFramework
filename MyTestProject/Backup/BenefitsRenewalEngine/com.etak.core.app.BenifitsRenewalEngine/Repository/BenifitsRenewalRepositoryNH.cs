using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.app.BenifitsRenewalEngine.DTO;
using com.etak.core.repository.NHibernate;
using NHibernate.Transform;

namespace com.etak.core.app.BenifitsRenewalEngine.Repository
{
    public class BenifitsRenewalRepositoryNH<T> : NHibernateRepository<T, Int64>, IBenifitsRenewalRepository<T> where T : RenewalCandidates
    {
        public IList<T> FetchRenewCandidates(int perCount, DateTime fetchDateTime, long promotionId)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine("select DISTINCT top {0} CUSTOMERID from CRM_CUSTOMERS_PROMOTION (nolock)");
            sbSql.AppendLine("where NEXT_RENEWAL_DATE is not null and NEXT_RENEWAL_DATE<= '{2}' and CUSTOMERID>{1} AND ACTIVE=1 AND ACTIONSEXECUTED = 0 order by CUSTOMERID");
            string formatSQL = string.Format(sbSql.ToString(), perCount, promotionId, fetchDateTime);

            IList<T> items = _session.CreateSQLQuery(formatSQL).SetResultTransformer(new AliasToBeanResultTransformer(typeof(T))).List<T>();
            return items.ToList();
        }
        public IList<T> FetchPreRenewCandidates(int perCount, DateTime fetchDateTime, long promotionId)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine("select DISTINCT top {0}  CUSTOMERID from CRM_CUSTOMERS_PROMOTION (nolock)");
            sbSql.AppendLine("where PRERENEWALACTIONS_DATE is not null and PRERENEWALACTIONS_DATE<= '{2}' AND ACTIVE=1 and CUSTOMERID>{1} AND PREACTIONSEXECUTED=0 ");
            sbSql.AppendLine("order by CUSTOMERID");
            string formatSQL = string.Format(sbSql.ToString(), perCount, promotionId, fetchDateTime);
            IList<T> items = _session.CreateSQLQuery(formatSQL).SetResultTransformer(new AliasToBeanResultTransformer(typeof(T))).List<T>();
            return items.ToList();
        }
    }
}
