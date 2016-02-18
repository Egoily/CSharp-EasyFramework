using System;
using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.model.inventory;

namespace com.etak.core.repository.crm
{
    /// <summary>
    /// Repository interface for ProductInventory
    /// </summary>
    /// <typeparam name="TProductInventory">The entity managed by the repository, is or extends ProductInventory</typeparam>
    public interface IProductInventoryRepository<TProductInventory> : IRepository<TProductInventory, Int32> where TProductInventory : ProductInventory
    {
        /// <summary>
        /// Get Inventories by mvno
        /// </summary>
        /// <param name="mvnoId">vmo Id</param>
        /// <returns></returns>
        IEnumerable<TProductInventory> GetByMVNO(int mvnoId);
    }
}
