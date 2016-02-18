using System;
using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.repository.crm.drl;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate.drl
{
    /// <summary>
    /// Nhibernate repository for inheritance tree of entity RoamingBlackListInfo 
    /// </summary>
    /// <typeparam name="TRoamingBlackListInfo">Entity managed by the repository, is or extends RoamingBlackListInfo</typeparam>
    public class RoamingBlackListInfoRepositoryNH<TRoamingBlackListInfo> : NHibernateRepository<TRoamingBlackListInfo, Int32>,
        IRoamingBlackListInfoRepository<TRoamingBlackListInfo> where TRoamingBlackListInfo : RoamingBlackListInfo
    {
        /// <summary>
        /// Gets The roabming TRoamingBlackListInfo of the customer
        /// </summary>
        /// <param name="customerId">the Id of the customer that has TRoamingBlackListInfo</param>
        /// <returns>The list of Blacklists for a fiven customer</returns>
        public IEnumerable<TRoamingBlackListInfo> GetByCustomerID(int customerId)
        {
           return(GetQuery().Where(x => x.CustomerID == customerId).Future());
        }
    }
}
