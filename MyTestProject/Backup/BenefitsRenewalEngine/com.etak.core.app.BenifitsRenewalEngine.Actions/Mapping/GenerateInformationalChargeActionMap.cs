using FluentNHibernate.Mapping;

namespace com.etak.core.app.BenifitsRenewalEngine.Actions.Mapping
{
    public class GenerateInformationalChargeActionMap : SubclassMap<GenerateInformationalChargeAction>    
    {
        public GenerateInformationalChargeActionMap()
        {
            DiscriminatorValue("GC");
        }
    }
}
