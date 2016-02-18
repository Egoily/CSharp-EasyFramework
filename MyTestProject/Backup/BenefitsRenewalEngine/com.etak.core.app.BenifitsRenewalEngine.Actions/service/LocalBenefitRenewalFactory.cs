using com.etak.core.app.BenefitsRenewalEngine.contract;

namespace com.etak.core.app.BenifitsRenewalEngine.Actions.service
{
    public class LocalBenefitRenewalFactory : IBenefitRenewalFactory
    {
        public IBenefitsRenewalService GetInstance()
        {
            return new BenefitRenewalService();
        }
    }
}
