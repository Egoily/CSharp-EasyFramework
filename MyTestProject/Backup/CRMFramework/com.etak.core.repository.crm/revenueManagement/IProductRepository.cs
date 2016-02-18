using System;
using com.etak.core.model.revenueManagement;

namespace com.etak.core.repository.crm.revenueManagement
{
    /// <summary>
    /// Interface for repository of entity Product
    /// </summary>
    /// <typeparam name="TProduct">The type of the managed entity is or extends Product</typeparam>
    public interface IProductRepository<TProduct> : IRepository<TProduct, Int32> where TProduct : Product
    {

    }
}
