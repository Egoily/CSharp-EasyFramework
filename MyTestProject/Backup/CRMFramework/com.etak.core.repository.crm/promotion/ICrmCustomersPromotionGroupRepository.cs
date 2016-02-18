using com.etak.core.model;

namespace com.etak.core.repository.crm.promotion
{
    /// <summary>
    /// Interface for repository of entity CrmCustomersPromotionGroup
    /// </summary>
    /// <typeparam name="TCrmCustomersPromotionGroup">The type of the managed entity is or extends CrmCustomersPromotionGroup</typeparam>
    public interface ICrmCustomersPromotionGroupRepository<TCrmCustomersPromotionGroup> :
        IRepository<TCrmCustomersPromotionGroup, long> where TCrmCustomersPromotionGroup : CrmCustomersPromotionGroup
    {
        
    }
}
