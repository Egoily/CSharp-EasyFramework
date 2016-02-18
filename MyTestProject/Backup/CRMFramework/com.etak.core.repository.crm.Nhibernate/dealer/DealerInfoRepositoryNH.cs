using System;
using System.Collections.Generic;
using System.Linq;
using com.etak.core.model;
using com.etak.core.repository.crm.Nhibernate.Factory;
using com.etak.core.repository.NHibernate;
using NHibernate.Criterion;

namespace com.etak.core.repository.crm.Nhibernate
{
    /// <summary>
    /// Nhibernate repository for inheritance tree of entity DealerInfo 
    /// </summary>
    /// <typeparam name="TDealerInfo">Entity managed by the repository, is or extends DealerInfo</typeparam>
    public class DealerInfoRepositoryNH<TDealerInfo>
                    : NHibernateRepository<TDealerInfo, Int32>,                         //Extends and gets basic CRUD operations
                      IDealerInfoRepository<TDealerInfo> where TDealerInfo : DealerInfo //Implementes 
    {
        private const String CacheRegion = CacheRegions.UserDealerCacheRegion;

        /// <summary>
        /// Gets all the 
        /// </summary>
        /// <param name="dealerId">gets the dealer by dealer id and caches the resutls</param>
        /// <returns>A enumerable with 0 or 1 dealers with the DealerInfo of that id</returns>
        public IEnumerable<TDealerInfo> GetByDealerIdAndCache(int dealerId)
        {
            return (GetQuery().Where(x => x.DealerID == dealerId).
                                Cacheable().CacheRegion(CacheRegion).
                                Future());
        }

        /// <summary>
        /// Gets all the dealers not in a given state
        /// </summary>
        /// <param name="state">the state in which the dealers can't be</param>
        /// <returns>the list of dealers not in state (state param)</returns>
        public IEnumerable<TDealerInfo> GetDealersStateNE(int state)
        {
            return (GetQuery().Where(x => x.State != null && x.State != state).
                                Future());
        }

        /// <summary>
        /// Gets the maximun date of the update date of dealers
        /// </summary>
        /// <returns>the maximun date</returns>
        public DateTime GetMaxUpdateDate()
        {
            return GetQuery().Select(
                                Projections.ProjectionList()
                                    .Add(Projections.Max<TDealerInfo>(x => x.UpdateDate)))
                             .List<DateTime>().First();
        }

        /// <summary>
        /// Gets the dealer id loading all it's associations
        /// </summary>
        /// <param name="dealerId">the id of the dealer to load</param>
        /// <returns>the delaer with the given id</returns>
        public TDealerInfo GetByDealerIdAndLoadAllAsociations(int dealerId)
        {
            IEnumerable<TDealerInfo> dealers = GetQuery().Where(x => x.DealerID == dealerId).
                                 Cacheable().
                                 CacheRegion(CacheRegion).
                                 Future();

            if (!dealers.Any())
                return (null);

            TDealerInfo dealer = dealers.First();
            
            dealer.MVNOConfigActionList.Any();
            dealer.DealerBankList.Any();
            dealer.DealerLoyaltyList.Any();
            //dealer.DealerOBOPRSList.Any();
            dealer.DealerPropertiesList.Any();
            dealer.RoamingSettingList.Any();
            dealer.MvnoDataRoamingLimitList.Any();

            return (dealer);

        }

        /// <summary>
        /// Gets the dealerInfo that is the MVNO/FiscalUnit of a given dealer
        /// </summary>
        /// <param name="dealerId">the unique id of the dealer which FiscalUnit/MVNO will be retreived</param>
        /// <returns>the MVNO (FiscalUnit) of the dealer </returns>
        public TDealerInfo GetMVNOByDealerId(int dealerId)
        {
            var dealer = GetByDealerIdAndCache(dealerId).FirstOrDefault();
            if (dealer != null && dealer.FiscalUnitID.HasValue && dealer.FiscalUnitID != dealer.DealerID)
            {
                dealer = GetMVNOByDealerId(dealer.FiscalUnitID.Value);
            }

            return dealer;
        }

        /// <summary>
        /// Gets all the dealers of the given MVNO/FiscalUnit
        /// </summary>
        /// <param name="fiscalUnitId">the FiscalUnit/MVMNO parent of the dealers to retrieve</param>
        /// <returns>all the dealers of the  MVNO/FiscalUnit</returns>
        public IEnumerable<TDealerInfo> GetDealersByFiscalUnitId(int fiscalUnitId)
        {
            return (GetQuery().Where(x => x.FiscalUnitID == fiscalUnitId).
                               // Cacheable().CacheRegion(CACHE_REGION).
                                Future());
        }
    }
}
