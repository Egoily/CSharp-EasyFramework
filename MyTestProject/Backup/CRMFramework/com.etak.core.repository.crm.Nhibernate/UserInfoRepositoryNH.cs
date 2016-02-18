using System;
using com.etak.core.model;
using com.etak.core.repository.crm.user;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate
{
    /// <summary>
    /// Repository based on NHibernate for UserInfo entity inheritance tree
    /// </summary>
    /// <typeparam name="TUserInfo">the type of entity managed, is or extends UserInfo</typeparam>
    public class UserInfoRepositoryNH<TUserInfo>: NHibernateRepository<TUserInfo, Int32>,
       IUserInfoRepository<TUserInfo> where TUserInfo : UserInfo
    {
    }
}
