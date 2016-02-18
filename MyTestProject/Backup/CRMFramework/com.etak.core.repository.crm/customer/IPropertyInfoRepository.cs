using System;
using System.Collections.Generic;
using com.etak.core.model;

namespace com.etak.core.repository.crm.customer
{
    /// <summary>
    /// Repository interface for PropertyInfo
    /// </summary>
    /// <typeparam name="TPropertyInfo">The entity managed by the repository, is or extends PropertyInfo</typeparam>
    public interface IPropertyInfoRepository<TPropertyInfo> : IRepository<TPropertyInfo, Int32> where TPropertyInfo : PropertyInfo
    {
        /// <summary>
        /// Gets the customer properties that have a given identufication number
        /// </summary>
        /// <param name="idType">the type of the document</param>
        /// <param name="idNumber">the document number</param>
        /// <returns>the list of customer properties that have this id</returns>
        IEnumerable<TPropertyInfo> GetByDocumentId(Int32 idType, string idNumber);
    }
}
