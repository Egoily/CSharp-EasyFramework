using System;
using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.repository.crm.resource;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate.drl
{
    /// <summary>
    /// Nhibernate repository for inheritance tree of entity ImeiAssn 
    /// </summary>
    /// <typeparam name="TImeiAssn">Entity managed by the repository, is or extends ImeiAssn</typeparam>
    public class ImeiAssnRepositoryNH<TImeiAssn> : NHibernateRepository<TImeiAssn, Int32>,
        IImeiAssnRepository<TImeiAssn> where TImeiAssn : ImeiAssn
    {
        /// <summary>
        /// Gets all the TMVNOAPNIPPoolInfo for the given msisdn
        /// </summary>
        /// <param name="imei">the msisdn to look for</param>
        /// <returns>the list of entities that fullfil the requiremets</returns>
        public IEnumerable<TImeiAssn> GetByImei(string imei)
        {
            return GetQuery()
                    .Where(e => e.Imei == imei)
                    .Future();
        }    
    }
}
