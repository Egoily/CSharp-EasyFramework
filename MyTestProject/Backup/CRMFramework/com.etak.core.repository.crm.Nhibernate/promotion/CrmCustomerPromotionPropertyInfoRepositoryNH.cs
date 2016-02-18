using System;
using com.etak.core.model;
using com.etak.core.repository.crm.promotion;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate
{
    /// <summary>
    /// Repository based on NHibernate for CrmCustomerPromotionPropertyInfo entity inheritance tree
    /// </summary>
    /// <typeparam name="TCrmCustomerPromotionPropertyInfo">the type of entity managed, is or extends CrmCustomerPromotionPropertyInfo</typeparam>
    public class CrmCustomerPromotionPropertyInfoRepositoryNH<TCrmCustomerPromotionPropertyInfo> : NHibernateRepository<TCrmCustomerPromotionPropertyInfo, Int64>,
        ICrmCustomerPromotionPropertyInfoRepository<TCrmCustomerPromotionPropertyInfo> where TCrmCustomerPromotionPropertyInfo : CrmCustomerPromotionPropertyInfo
    {

    }
}
