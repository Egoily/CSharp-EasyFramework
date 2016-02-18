using System;
using com.etak.core.model;

namespace com.etak.core.repository.crm.user
{
    /// <summary>
    /// Interface for repository of entity UserInfo
    /// </summary>
    /// <typeparam name="TUserInfo">The type of the managed entity is or extends UserInfo</typeparam>
    public interface IUserInfoRepository<TUserInfo> : IRepository<TUserInfo, Int32> where TUserInfo : UserInfo
    {
    }
}
