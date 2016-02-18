using FluentNHibernate.Mapping;

namespace com.etak.core.app.BenifitsRenewalEngine.Actions.Mapping
{
    public class PromotionRenewInAdvanceActionMap : SubclassMap<PromotionRenewInAdvanceAction>    
    {
        public PromotionRenewInAdvanceActionMap()
        {
            DiscriminatorValue("PRE");
        }
    }
}
