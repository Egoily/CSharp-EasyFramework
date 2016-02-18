using System;
using System.Collections.Generic;
using com.etak.core.model;

namespace com.etak.core.repository.crm
{
    /// <summary>
    /// The respository interface for <typeparamref name="TDealerInfo"/> entity
    /// </summary>
    /// <typeparam name="TDealerInfo">The entity managed by the interface, is or extends DealerInfo</typeparam>
    public interface IDealerInfoRepository<TDealerInfo> : IRepository<TDealerInfo, Int32> where TDealerInfo : DealerInfo
    {
        /// <summary>
        /// Gets all the 
        /// </summary>
        /// <param name="dealerId">gets the dealer by dealer id and caches the resutls</param>
        /// <returns>A enumerable with 0 or 1 dealers with the DealerInfo of that id</returns>
        IEnumerable<TDealerInfo> GetByDealerIdAndCache(Int32 dealerId);

        /// <summary>
        /// Gets the dealer id loading all it's associations
        /// </summary>
        /// <param name="dealerId">the id of the dealer to load</param>
        /// <returns>the delaer with the given id</returns>
        TDealerInfo GetByDealerIdAndLoadAllAsociations(int dealerId);

        /// <summary>
        /// Gets all the dealers not in a given state
        /// </summary>
        /// <param name="state">the state in which the dealers can't be</param>
        /// <returns>the list of dealers not in state (state param)</returns>
        IEnumerable<TDealerInfo> GetDealersStateNE(int state);

        /// <summary>
        /// Gets the maximun date of the update date of dealers
        /// </summary>
        /// <returns>the maximun date</returns>
        DateTime GetMaxUpdateDate();

        /// <summary>
        /// Gets the dealerInfo that is the MVNO/FiscalUnit of a given dealer
        /// </summary>
        /// <param name="dealerId">the unique id of the dealer which FiscalUnit/MVNO will be retreived</param>
        /// <returns>the MVNO (FiscalUnit) of the dealer </returns>
        TDealerInfo GetMVNOByDealerId(int dealerId);

        /// <summary>
        /// Gets all the dealers of the given MVNO/FiscalUnit
        /// </summary>
        /// <param name="fiscalUnitId">the FiscalUnit/MVMNO parent of the dealers to retrieve</param>
        /// <returns>all the dealers of the  MVNO/FiscalUnit</returns>
        IEnumerable<TDealerInfo> GetDealersByFiscalUnitId(int fiscalUnitId);
    }
}
