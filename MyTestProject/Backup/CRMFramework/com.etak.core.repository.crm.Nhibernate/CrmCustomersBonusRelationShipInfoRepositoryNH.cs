using System;
using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.repository.crm.Nhibernate.Factory;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate
{
    /// <summary>
    /// Nhibernate repository for inheritance tree of entity CrmCustomersBonusRelationShipInfo 
    /// </summary>
    /// <typeparam name="TCrmCustomersBonusRelationShipInfo">Entity managed by the repository, is or extends CrmCustomersBonusRelationShipInfo</typeparam>
    public class CrmCustomersBonusRelationShipInfoRepositoryNH<TCrmCustomersBonusRelationShipInfo>
                    : NHibernateRepository<TCrmCustomersBonusRelationShipInfo, Int32>,                         //Extends and gets basic CRUD operations
                      ICrmCustomersBonusRelationShipInfoRepository<TCrmCustomersBonusRelationShipInfo> 
                    where TCrmCustomersBonusRelationShipInfo : CrmCustomersBonusRelationShipInfo //Implementes 
    {
        private const String CacheRegion = CacheRegions.UserDealerCacheRegion;

        /// <summary>
        /// Gets all the TCrmCustomersBonusRelationShipInfo for a package and an vmno
        /// </summary>
        /// <param name="VMNOId">vmno to filter the query</param>
        /// <param name="packageID">the owner of the relations</param>
        /// <returns>all the TCrmCustomersBonusRelationShipInfo for a package and an vmno</returns>
        public IEnumerable<TCrmCustomersBonusRelationShipInfo> GetByVmnoAndPackage(Int32 VMNOId, Int32 packageID)
        {
            return (GetQuery().
                        Where(x => x.StartDate < DateTime.Now).
                        And(x => x.EndDate == null || x.EndDate > DateTime.Now).
                        And(x => x.Status == 1 && x.BeRelationShipedID == packageID).
                        Cacheable().CacheRegion(CacheRegion).Future());
        }

    }
}
