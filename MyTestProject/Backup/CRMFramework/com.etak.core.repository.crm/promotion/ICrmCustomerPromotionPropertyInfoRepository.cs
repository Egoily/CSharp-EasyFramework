using System;
using com.etak.core.model;

namespace com.etak.core.repository.crm.promotion
{
    /// <summary>
    /// Interface for repository of entity CrmCustomerPromotionPropertyInfo
    /// </summary>
    /// <typeparam name="TCrmCustomerPromotionPropertyInfo">The type of the managed entity is or extends CrmCustomerPromotionPropertyInfo</typeparam>
    public interface ICrmCustomerPromotionPropertyInfoRepository<TCrmCustomerPromotionPropertyInfo> : 
        IRepository<TCrmCustomerPromotionPropertyInfo, Int64> where TCrmCustomerPromotionPropertyInfo : CrmCustomerPromotionPropertyInfo
    {
    }
}
