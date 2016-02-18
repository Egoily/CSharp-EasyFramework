using System;
using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.repository.crm.Nhibernate.Factory;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate
{
    /// <summary>
    /// Nhibernate repository for inheritance tree of entity LoginInfo 
    /// </summary>
    /// <typeparam name="TLoginInfo">Entity managed by the repository, is or extends LoginInfo</typeparam>
    public class LoginInfoRepositoryNH<TLoginInfo>
        : NHibernateRepository<TLoginInfo, Int32>,
       ILoginInfoRepository<TLoginInfo> where TLoginInfo : LoginInfo
    {

        private const String CacheRegion = CacheRegions.UserDealerCacheRegion;
        /// <summary>
        /// Gets the user by Id, this could be done over normal Get repository, but it's done to achieve Future API and 
        /// Cacheable
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IEnumerable<TLoginInfo> GetByUserId(int userId)
        {
           IEnumerable<TLoginInfo> queryMainTable = 
                        GetQuery().
                        Where(x => x.UserID == userId).                      
                        //TransformUsing(new NHibernate.Transform.DistinctRootEntityResultTransformer())). // ==> This should be there, but breaks cache query see: https://nhibernate.jira.com/browse/NH-3596
                        Cacheable().CacheRegion(CacheRegion).
                        Future();

             IEnumerable<TLoginInfo> queryMainTableAndAssociation = 
                        GetQuery().
                        Where(x => x.UserID == userId).
                        Fetch(x => x.UserDealerInfo).Eager.
                        Cacheable().CacheRegion(CacheRegion).
                        Future();

             return (queryMainTable);
        }

        
    }
}
