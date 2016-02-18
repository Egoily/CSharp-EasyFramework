using System;
using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.repository.crm.Nhibernate.Factory;
using com.etak.core.repository.crm.user;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate
{
    /// <summary>
    /// Nhibernate repository for inheritance tree of entity UserDealerInfo 
    /// </summary>
    /// <typeparam name="TUserDealerInfo">Entity managed by the repository, is or extends UserDealerInfo</typeparam>
    public class UserDealerInfoRepositoryNH<TUserDealerInfo>
        : NHibernateRepository<TUserDealerInfo, Int32>,
       IUserDealerInfoRepository<TUserDealerInfo> where TUserDealerInfo : UserDealerInfo
    {
        private const String CacheRegion = CacheRegions.UserDealerCacheRegion;

        /// <summary>
        /// Gets a user by it's Id
        /// </summary>
        /// <param name="userId">the id of the user</param>
        /// <returns>an enumerable with the user</returns>
        public IEnumerable<TUserDealerInfo> GetByUserId(int userId)
        {
            return (GetQuery().Where(x => x.UserID == userId)
                    .Cacheable().CacheRegion(CacheRegion).
                    Future());
        }


        /// <summary>
        /// Gets all users of a given dealer. 
        /// </summary>
        /// <param name="delaerId">the dealer owning the users</param>
        /// <returns>an enumerable with the users of the dealer</returns>
        public IEnumerable<TUserDealerInfo> GetByDealerId(int delaerId)
        {
            return (GetQuery().Where(x => x.DealerID == delaerId)
                    .Cacheable().CacheRegion(CacheRegion).
                    Future());
        }
    }
}
