using System;
using com.etak.core.model;

namespace com.etak.core.repository.crm
{
    /// <summary>
    /// Repository interface for ProductInfo
    /// </summary>
    /// <typeparam name="TProductInfo">The type of the entity managed by the repository, is or extends ProductInfo</typeparam>
    public interface IProductInfoRepository<TProductInfo> : IRepository<TProductInfo, Int32> where TProductInfo : ProductInfo
    {
    }
}
