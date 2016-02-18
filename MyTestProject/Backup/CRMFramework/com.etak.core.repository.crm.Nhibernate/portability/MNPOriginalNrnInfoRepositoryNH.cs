using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model;
using com.etak.core.repository.crm.portability;
using com.etak.core.repository.NHibernate;
using NHibernate.Criterion;

namespace com.etak.core.repository.crm.Nhibernate
{
    /// <summary>
    /// Nhibernate repository for inheritance tree of entity TMNPOriginalNrnInfo 
    /// </summary>
    /// <typeparam name="TMNPOriginalNrnInfo">Entity managed by the repository, is or extends TMNPOriginalNrnInfo</typeparam>
    public class MNPOriginalNrnInfoRepositoryNH<TMNPOriginalNrnInfo> :
        NHibernateRepository<TMNPOriginalNrnInfo, Int64>,
         IMNPOriginalNrnInfoRepository<TMNPOriginalNrnInfo> where TMNPOriginalNrnInfo : MNPOriginalNrnInfo
    {
        /// <summary>
        /// Gets all the MNPOriginalNrnInfo of a given MSISDN
        /// </summary>
        /// <param name="msisdn">the msisdn to filter the portabilities</param>
        /// <returns>all the MNPOriginalNrnInfo of a given MSISDN</returns>
        public IEnumerable<TMNPOriginalNrnInfo> GetByMsisdn(string msisdn)
        {
            ICriterion crit = Restrictions.And(Restrictions.Le("Range1", msisdn),
                                   Restrictions.Ge("Range2", msisdn));
            return GetQuery().Where(crit).Future();
        }
    }
}
