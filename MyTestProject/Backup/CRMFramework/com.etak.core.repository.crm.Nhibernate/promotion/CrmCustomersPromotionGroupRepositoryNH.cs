using com.etak.core.model;
using com.etak.core.repository.crm.promotion;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate
{
    /// <summary>
    /// Repository based on NHibernate for CrmCustomersPromotionGroup entity inheritance tree
    /// </summary>
    /// <typeparam name="TCrmCustomersPromotionGroup">the type of entity managed, is or extends CrmCustomersPromotionGroup</typeparam>
    public class CrmCustomersPromotionGroupRepositoryNH<TCrmCustomersPromotionGroup> : 
        NHibernateRepository<TCrmCustomersPromotionGroup, long>, 
        ICrmCustomersPromotionGroupRepository<TCrmCustomersPromotionGroup> where TCrmCustomersPromotionGroup : CrmCustomersPromotionGroup
    {
       
    }
}
