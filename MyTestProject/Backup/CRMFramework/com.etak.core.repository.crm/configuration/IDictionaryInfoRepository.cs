using System;
using System.Collections.Generic;
using com.etak.core.model;

namespace com.etak.core.repository.crm.configuration
{
    /// <summary>
    /// Repository interface for <typeparamref name="TDictionaryInfo"/>
    /// </summary>
    /// <typeparam name="TDictionaryInfo">The entity managed by the interface, is or extends DictionaryInfo</typeparam>
    public interface IDictionaryInfoRepository<TDictionaryInfo> : IRepository<TDictionaryInfo, Int32> where TDictionaryInfo : DictionaryInfo
    {
        /// <summary>
        /// Gets all the dictionary for the given dictionary type
        /// </summary>
        /// <param name="p">the type of the dictionaries to get</param>
        /// <returns>The entities that fullfil the requirements</returns>
        IEnumerable<TDictionaryInfo> GetByDictType(int p);
    }
}
