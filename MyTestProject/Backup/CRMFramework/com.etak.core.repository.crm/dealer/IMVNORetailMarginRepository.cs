using com.etak.core.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.etak.core.repository.crm.dealer
{
    /// <summary>
    /// Repository interface for MVNORetailMargin
    /// </summary>
    /// <typeparam name="TMVNORetailMargin">The entity managed by the repository, is or extends MVNORetailMargin</typeparam>
    public interface IMVNORetailMarginRepository<TMVNORetailMargin> : IRepository<TMVNORetailMargin, Int32> where TMVNORetailMargin : MVNORetailMargin
    {
        /// <summary>
        /// Gets all Bundle Info
        /// </summary>
        /// <returns>all the BundleInfo</returns>
        IEnumerable<TMVNORetailMargin> GetAll();
       
    }
}
