namespace com.etak.core.app.BenefitsRenewalEngine.contract
{
    public interface IBenefitRenewalFactory
    {
        IBenefitsRenewalService GetInstance();
    }
}
