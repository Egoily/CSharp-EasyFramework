using System;
using System.Collections.Generic;
using com.etak.core.model;

namespace com.etak.core.repository.crm.user
{
    /// <summary>
    /// Repository Interface for <typeparamref name="TUserDealerInfo"/> entity
    /// </summary>
    /// <typeparam name="TUserDealerInfo">The type of the entity managed is or extends UserDealerInfo</typeparam>
    public interface IUserDealerInfoRepository<TUserDealerInfo> : IRepository<TUserDealerInfo, Int32> where TUserDealerInfo : UserDealerInfo
    {
        /// <summary>
        /// Gets a user by it's Id
        /// </summary>
        /// <param name="userId">the id of the user</param>
        /// <returns>an enumerable with the user</returns>
        IEnumerable<TUserDealerInfo> GetByUserId(int userId);

        /// <summary>
        /// Gets all users of a given dealer. 
        /// </summary>
        /// <param name="delaerId">the dealer owning the users</param>
        /// <returns>an enumerable with the users of the dealer</returns>
        IEnumerable<TUserDealerInfo> GetByDealerId(int delaerId);
    }
}
