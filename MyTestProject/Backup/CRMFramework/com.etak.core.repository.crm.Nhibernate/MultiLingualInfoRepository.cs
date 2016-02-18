using System;
using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate
{
    /// <summary>
    /// Nhibernate implementation of IMultiLingualInfoRepository
    /// </summary>
    /// <typeparam name="TMultiLingualInfo">Entity managed by  the repository is or extends MultiLingualInfo</typeparam>
    public class MultiLingualInfoRepositoryNH<TMultiLingualInfo> : NHibernateRepository<TMultiLingualInfo, Int32>,
       IMultiLingualInfoRepository<TMultiLingualInfo> where TMultiLingualInfo : MultiLingualInfo
    {
        /// <summary>
        /// Gets all the TMultiLingualInfo of a dealer, language, dictionaryTypeId and value
        /// </summary>
        /// <param name="dealerId">the dealer id filter for the query</param>
        /// <param name="languageId">the language id filter for the query</param>
        /// <param name="dictionaryTypeId">the dictionaryTypeId filter for the query</param>
        /// <param name="value">the value filter for the query</param>
        /// <returns>the list of matching TMultiLingualInfo</returns>
        public IEnumerable<TMultiLingualInfo> GetCommonMultiLingual(int dealerId, int languageId, int dictionaryTypeId, string value)
        {
            return GetQuery().Where(where => where.ControlID == "Common" && where.FormFullName == "Common")
                             .And(where => where.DealerID.Value == dealerId && where.LanguageID.Value == languageId)
                             .And(where => where.Value == value && where.DictionaryTypeID == dictionaryTypeId).OrderBy(by => by.UpdateDate).Desc.Future();
        }
    }
}
