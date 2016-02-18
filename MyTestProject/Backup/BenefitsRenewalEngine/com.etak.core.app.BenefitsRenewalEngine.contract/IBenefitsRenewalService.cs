using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace com.etak.core.app.BenefitsRenewalEngine.contract
{
    [ServiceContract(Namespace = Definition.NameSpace)]
    public interface IBenefitsRenewalService
    {
        [OperationContract]
        void RenewCustomersBenefits(List<Int32> customerIdsToRenew);

        [OperationContract]
        void PreRenewCustomersBenefits(List<Int32> customerIdsToRenew);
    }
}
