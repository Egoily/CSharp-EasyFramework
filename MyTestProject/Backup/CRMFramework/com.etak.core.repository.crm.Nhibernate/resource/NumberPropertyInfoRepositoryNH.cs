using System;
using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.repository.crm.resource;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate
{
    /// <summary>
    /// Nhibernate repository for inheritance tree of entity NumberPropertyInfo 
    /// </summary>
    /// <typeparam name="TNumberPropertyInfo">Entity managed by the repository, is or extends NumberPropertyInfo</typeparam>
    public class NumberPropertyInfoRepositoryNH<TNumberPropertyInfo>    : NHibernateRepository<TNumberPropertyInfo, String>,
       INumberPropertyInfoRepository<TNumberPropertyInfo> where TNumberPropertyInfo : NumberPropertyInfo
    {
        /// <summary>
        /// Gets the Number in the pool by the MSISDN 
        /// </summary>
        /// <param name="msisdn">the msisdn too look up</param>
        /// <returns></returns>
        public IEnumerable<TNumberPropertyInfo> getByMSISDN(string msisdn)
        {
            return (GetQuery().Where(x => x.Resource == msisdn).Future());
        }   
    }
}
