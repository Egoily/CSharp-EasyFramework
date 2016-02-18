using System.Runtime.Serialization;

namespace com.etak.core.app.BenefitsRenewalEngine.contract
{
    [DataContract(Namespace = Definition.NameSpace)]
    public enum RenewType : int
    {
        PreRenew = 1,
        Renew = 2
    }
}
