using System;
using System.Collections.Generic;
using com.etak.core.model;

namespace com.etak.core.repository.crm
{
    /// <summary>
    /// The respository interface for <typeparamref name="TLanguageTypeInfo"/> entity
    /// </summary>
    /// <typeparam name="TLanguageTypeInfo">The entity managed by the interface, is or extends LanguageTypeInfo</typeparam>
    public interface ILanguageTypeInfoRepository<TLanguageTypeInfo> : IRepository<TLanguageTypeInfo, Int32> where TLanguageTypeInfo : LanguageTypeInfo
    {
        /// <summary>
        /// Gets all languages
        /// </summary>
        /// <returns>the list of all languages</returns>
        IEnumerable<TLanguageTypeInfo> GetAllLanguages();

        /// <summary>
        /// Gets a given language by it's Id
        /// </summary>
        /// <param name="langId">the id of the language</param>
        /// <returns>the languages with the given id</returns>
        IEnumerable<LanguageTypeInfo> GetAllLanguageById(int langId);
    }
}
