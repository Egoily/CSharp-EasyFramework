using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.repository.crm.topup;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate
{
    /// <summary>
    /// Repository based on NHibernate for MVNOTopupBundleBasedInfo entity inheritance tree
    /// </summary>
    /// <typeparam name="TMVNOTopupBundleBasedInfo">the type of entity managed, is or extends MVNOTopupBundleBasedInfo</typeparam>
    public class MVNOTopupBundleBasedInfoRepositoryNH<TMVNOTopupBundleBasedInfo>
            : NHibernateRepository<TMVNOTopupBundleBasedInfo, MVNOTopupBundleBasedKey>, 
        IMVNOTopupBundleBasedInfoRepository<TMVNOTopupBundleBasedInfo> where TMVNOTopupBundleBasedInfo : MVNOTopupBundleBasedInfo
    {
        /// <summary>
        /// Gets the All the Topup Bundles of a given package id
        /// </summary>
        /// <param name="packageId">the id of the package associated to TopUpBundles</param>
        /// <returns>the list of TMVNOTopupBundleBasedInfo </returns>
        public IEnumerable<TMVNOTopupBundleBasedInfo> GetByPackageId(int packageId)
        {
            return (GetQuery().Where(x => x.MVNOTopupBundleBasedKey.PackageID == packageId).Future());
        }
    }
}
