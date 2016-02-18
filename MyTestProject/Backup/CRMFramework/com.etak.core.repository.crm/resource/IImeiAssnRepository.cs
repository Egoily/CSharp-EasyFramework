using System;
using System.Collections.Generic;
using com.etak.core.model;

namespace com.etak.core.repository.crm.resource
{
    /// <summary>
    /// Repository interface for ImeiAssn
    /// </summary>
    /// <typeparam name="TImeiAssn">The type of the entity managed by the repository, is or extends ImeiAssn</typeparam>
    public interface IImeiAssnRepository<TImeiAssn> : IRepository<TImeiAssn, Int32>
        where TImeiAssn : ImeiAssn
    {
        /// <summary>
        /// Get by Imei field
        /// </summary>
        /// <param name="imei"></param>
        /// <returns></returns>
        IEnumerable<TImeiAssn> GetByImei(string imei);
    }
}
