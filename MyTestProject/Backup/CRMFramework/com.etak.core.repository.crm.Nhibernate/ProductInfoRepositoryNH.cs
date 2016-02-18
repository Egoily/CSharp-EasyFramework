using System;
using com.etak.core.model;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate
{
    /// <summary>
    /// Repository based on NHibernate for ProductInfo entity inheritance tree
    /// </summary>
    /// <typeparam name="TProductInfo">the type of entity managed, is or extends ProductInfo</typeparam>
    public class ProductInfoRepositoryNH<TProductInfo> : 
        NHibernateRepository<TProductInfo, Int32>, IProductInfoRepository<TProductInfo>
            where TProductInfo : ProductInfo
    {
    }
}
