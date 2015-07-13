using System;

using com.etak.core.model;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate
{
    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ProfileInfoRepositoryNH<T> : NHibernateRepository<T, Int64>, IProfileInfoRepository<T> where T : ProfileInfo
    {
    }
}