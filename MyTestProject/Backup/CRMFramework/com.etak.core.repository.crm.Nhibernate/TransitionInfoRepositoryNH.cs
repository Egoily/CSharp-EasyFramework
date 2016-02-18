using System;
using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.repository.crm.Nhibernate.Factory;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate
{
    /// <summary>
    /// Nhibernate repository for inheritance tree of entity TransitionInfo 
    /// </summary>
    /// <typeparam name="TTransitionInfo">Entity managed by the repository, is or extends TransitionInfo</typeparam>
    public class TransitionInfoRepositoryNH<TTransitionInfo>
        : NHibernateRepository<TTransitionInfo, Int32>,
       ITransitionInfoRepository<TTransitionInfo> where TTransitionInfo : TransitionInfo
    {


        private const String CacheRegion = CacheRegions.UserDealerCacheRegion;

        /// <summary>
        /// Gets all the TTransitionInfo
        /// </summary>
        /// <returns>List with all the TTransitionInfo</returns>
        public IEnumerable<TransitionInfo> GetAll()
        {
            return GetQuery().Cacheable().CacheRegion(CacheRegion).Future();
        }

     
    }
}
