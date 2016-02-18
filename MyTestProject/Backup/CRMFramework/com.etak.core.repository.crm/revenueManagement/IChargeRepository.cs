using System;
using System.Collections.Generic;
using com.etak.core.model.revenueManagement;

namespace com.etak.core.repository.crm.revenueManagement
{
    /// <summary>
    /// Repository interface for Charge
    /// </summary>
    /// <typeparam name="TCharge">The type of the entity managed by the repository, is or extends Charge</typeparam>
    public interface IChargeRepository<TCharge> : IRepository<TCharge, Int32> where TCharge : Charge
    {
        /// <summary>
        /// Gets all the charges of a category
        /// </summary>
        /// <param name="category">the id of the category that the charges must have</param>
        /// <returns>the list of charges</returns>
        IEnumerable<TCharge> GetByCategoryId(Int32 category);
    }
}
