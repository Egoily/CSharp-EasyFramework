using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.app.BenefitsRenewalEngine.contract;

namespace com.etak.core.app.BenifitsRenewalEngine.Actions.service
{
    public class FactoryHelper
    {
        private static readonly LocalBenefitRenewalFactory localFactory = new LocalBenefitRenewalFactory();
        private static readonly RemoteWCFBenefitRenewalFactory remoteFactory = new RemoteWCFBenefitRenewalFactory();

        public static IBenefitRenewalFactory GetLocalFactory()
        {
            return localFactory;
        }

        public static IBenefitRenewalFactory GetRemoteWCFFactory()
        {
            return remoteFactory;
        }
    }
}
