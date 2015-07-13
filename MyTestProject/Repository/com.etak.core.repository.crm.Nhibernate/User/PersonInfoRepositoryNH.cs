using System;

using com.etak.core.model;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate
{
    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PersonInfoRepositoryNH<T> : NHibernateRepository<T, Int64>, IPersonInfoRepository<T> where T : PersonInfo
    {
    }
}