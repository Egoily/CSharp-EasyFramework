using System;
using com.etak.core.model.revenueManagement;
using com.etak.core.repository.crm.revenueManagement;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate.revenueManagement
{
    /// <summary>
    /// NHibernate implmentation of IProductRepository
    /// </summary>
    /// <typeparam name="TProduct">the entity managed by the repository is or extends Product</typeparam>
    public class ProductRepositoryNH<TProduct> : 
        NHibernateRepository<TProduct, Int32>, IProductRepository<TProduct> where TProduct : Product
    {
    }
}
