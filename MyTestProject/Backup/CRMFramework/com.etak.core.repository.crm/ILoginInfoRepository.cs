using System;
using System.Collections.Generic;
using com.etak.core.model;

namespace com.etak.core.repository.crm
{
    /// <summary>
    /// Repository Interface for <typeparamref name="TLoginInfo"/>entity
    /// </summary>
    /// <typeparam name="TLoginInfo">The type of the entity managed is or extends LoginInfo</typeparam>
    public interface ILoginInfoRepository<TLoginInfo> : IRepository<TLoginInfo, Int32> where TLoginInfo : LoginInfo
    {
        /// <summary>
        /// Gets the entity of the given user 
        /// </summary>
        /// <param name="userId">the unique id for the user</param>
        /// <returns>the user  wrapped in a enumerable</returns>
        IEnumerable<TLoginInfo> GetByUserId(int userId);
    }
}

