using System;
using System.Collections.Generic;
using com.etak.core.model;

namespace com.etak.core.repository.crm
{
    /// <summary>
    /// Respository for <typeparamref name="TMultiLingualInfo"/> entity
    /// </summary>
    /// <typeparam name="TMultiLingualInfo">The type of the managed entity, is or extends MultiLingualInfo</typeparam>
    public interface IMultiLingualInfoRepository<TMultiLingualInfo> : IRepository<TMultiLingualInfo, Int32> where TMultiLingualInfo : MultiLingualInfo
    {
        /// <summary>
        /// Gets all the TMultiLingualInfo of a dealer, language, dictionaryTypeId and value
        /// </summary>
        /// <param name="dealerId">the dealer id filter for the query</param>
        /// <param name="languageId">the language id filter for the query</param>
        /// <param name="dictionaryTypeId">the dictionaryTypeId filter for the query</param>
        /// <param name="value">the value filter for the query</param>
        /// <returns>the list of matching TMultiLingualInfo</returns>
        IEnumerable<TMultiLingualInfo> GetCommonMultiLingual(int dealerId, int languageId, int dictionaryTypeId, string value);
    }
}
