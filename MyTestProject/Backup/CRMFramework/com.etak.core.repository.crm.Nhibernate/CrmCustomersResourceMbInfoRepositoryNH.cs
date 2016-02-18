using System;
using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.repository.crm.subscription;
using com.etak.core.repository.NHibernate;
using NHibernate.Criterion;

namespace com.etak.core.repository.crm.Nhibernate
{
    /// <summary>
    /// Nhibernate repository for inheritance tree of entity CrmCustomersResourceMbInfo 
    /// </summary>
    /// <typeparam name="TCrmCustomersResourceMbInfo">Entity managed by the repository, is or extends CrmCustomersResourceMbInfo</typeparam>
    public class CrmCustomersResourceMbInfoRepositoryNH<TCrmCustomersResourceMbInfo> : 
        NHibernateRepository<TCrmCustomersResourceMbInfo, Int32>,
        ICrmCustomersResourceMbInfoRepository<TCrmCustomersResourceMbInfo> where TCrmCustomersResourceMbInfo : CrmCustomersResourceMbInfo
    {
        /// <summary>
        /// Loads the TCrmCustomersResourceMbInfo of a given resourceId
        /// </summary>
        /// <param name="resourceId">the unique id of the resourceMB</param>
        /// <returns>List with 0 or 1 TCrmCustomersResourceMbInfo</returns>
        public IEnumerable<TCrmCustomersResourceMbInfo> LoadResourceMBAsociations(Int32 resourceId)
        {
            IEnumerable<TCrmCustomersResourceMbInfo> resourceMbs =

            GetQuery().
               Fetch(x => x.CrmCustomersResourceMbPropertyInfo).Eager.
               Where(x => x.RESOURCEID == resourceId).
               TransformUsing(global::NHibernate.Transform.Transformers.DistinctRootEntity).
               Future();
            return resourceMbs;

        }

        /// <summary>
        /// Get TCrmCustomersResourceMbInfo of a given msisdn
        /// </summary>
        /// <param name="msisdn">MSISDN</param>
        /// <returns>List of TCrmCustomersResourceMbInfo</returns>
        public IEnumerable<TCrmCustomersResourceMbInfo> GetActiveSubscriptionByMsisdn(string msisdn)
        {
            IEnumerable<TCrmCustomersResourceMbInfo> resourceMbs =
            GetQuery().Where(x => x.RESOURCE == msisdn && (x.STARTDATE < DateTime.Now) && (x.ENDDATE > DateTime.Now)).
               Future();


            return resourceMbs;
        }
    }
}
