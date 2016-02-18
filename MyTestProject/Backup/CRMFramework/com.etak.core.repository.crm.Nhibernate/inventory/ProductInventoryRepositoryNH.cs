using com.etak.core.model.inventory;
using com.etak.core.repository.crm.Nhibernate.Factory;
using com.etak.core.repository.NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace com.etak.core.repository.crm.Nhibernate.inventory
{
    /// <summary>
    /// Nhibernate repository for inheritance tree of entity ProductInventory 
    /// </summary>
    /// <typeparam name="TProductInventory">Entity managed by the repository, is or extends ProductInventory</typeparam>
    public class ProductInventoryRepositoryNH<TProductInventory> :
        NHibernateRepository<TProductInventory, int>, IProductInventoryRepository<TProductInventory> where TProductInventory : ProductInventory
    {
        /// <summary>
        /// Get Inventories by mvno
        /// </summary>
        /// <param name="mvnoId">vmo Id</param>
        /// <returns></returns>
        public IEnumerable<TProductInventory> GetByMVNO(int mvnoId)
        {
            return GetQuery().JoinQueryOver(x=>x.Product).Where(ee => ee.VMO.DealerID == mvnoId).Future();
        }

    }


}
