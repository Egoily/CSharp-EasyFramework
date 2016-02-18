using System;
using System.Collections.Generic;
using com.etak.core.model;

namespace com.etak.core.repository.crm
{
    /// <summary>
    /// Respository for <typeparamref name="TMultiLingualDescription"/> entity
    /// </summary>
    /// <typeparam name="TMultiLingualDescription">The type of the managed entity, is or extends MultiLingualInfo</typeparam>
    public interface IMultiLingualDescriptionRepository<TMultiLingualDescription> : IRepository<TMultiLingualDescription, Int32> where TMultiLingualDescription : MultiLingualDescription
    {
        /// <summary>
        /// Get MultiLinualDescription by type
        /// </summary>
        /// <param name="type">type of description</param>
        /// <returns></returns>
        IEnumerable<TMultiLingualDescription> GetByType(MultiLingualType type);
    }
}