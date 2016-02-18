using System;
using com.etak.core.model;
using com.etak.core.repository.crm.Nhibernate.Factory;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate
{
    /// <summary>
    /// Nhibernate repository for inheritance tree of entity BRSTaxCodePostCode 
    /// </summary>
    /// <typeparam name="TBRSTaxCodePostCode">Entity managed by the repository, is or extends BRSTaxCodePostCode</typeparam>
    public class BRSTaxCodePostCodeRepositoryNH<TBRSTaxCodePostCode> : 
        NHibernateRepository<TBRSTaxCodePostCode, Int32>, 
        IBRSTaxCodePostCodeRepository<TBRSTaxCodePostCode> where TBRSTaxCodePostCode : BRSTaxCodePostCode
    {

        private const String CacheRegion = CacheRegions.SystemSettingsCacheRegion;

        /// <summary>
        /// Gets the TBRSTaxCodePostCode by the postcode
        /// </summary>
        /// <param name="postcode">the post code of the TBRSTaxCodePostCode</param>
        /// <returns>the TBRSTaxCodePostCode of the PostCode</returns>
        public TBRSTaxCodePostCode GetByPostCode(string postcode)
        {
            return GetQuery().
                Where(x => x.PostCode == postcode).
                Cacheable().CacheRegion(CacheRegion).
                SingleOrDefault();
        }
    }
}
