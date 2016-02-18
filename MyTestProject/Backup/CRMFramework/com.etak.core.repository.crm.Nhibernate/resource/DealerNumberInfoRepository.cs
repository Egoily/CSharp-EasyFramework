using System;
using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.repository.crm.resource;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate
{
    /// <summary>
    /// Nhibernate repository for inheritance tree of entity DealerNumberInfo 
    /// </summary>
    /// <typeparam name="TDealerNumberInfo">Entity managed by the repository, is or extends DealerNumberInfo</typeparam>
    public class DealerNumberInfoRepository<TDealerNumberInfo> 
        : NHibernateRepository<TDealerNumberInfo, Int32>,
          IDealerNumberInfoRepository<TDealerNumberInfo> where TDealerNumberInfo : DealerNumberInfo
    {
        /// <summary>
        /// Gets the dealer number Infor repository
        /// </summary>
        /// <param name="resource">the msisdn to look for</param>
        /// <returns>the list of entities that fullfil the condition</returns>
        public IEnumerable<TDealerNumberInfo> GetByResource(string resource)
        {
            return GetQuery()
                    .Where(e => e.Resource.Resource == resource)
                    .Future();
        }
    }
}
