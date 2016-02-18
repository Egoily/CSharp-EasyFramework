using FluentNHibernate.Mapping;

namespace com.etak.core.app.BenifitsRenewalEngine.Actions.Mapping
{
    public class AccumulativePromotionRenewActionMap : SubclassMap<AccumulativePromotionRenewAction>    
    {
        public AccumulativePromotionRenewActionMap()
        {
            DiscriminatorValue("AP");
        }
    }
}
