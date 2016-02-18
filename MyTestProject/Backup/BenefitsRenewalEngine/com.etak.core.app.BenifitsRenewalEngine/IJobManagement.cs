using System;
using System.ServiceModel;

namespace com.etak.core.app.BenifitsRenewalEngine
{
    [ServiceContract(Namespace = "http://com.etak.internal.management")]
    public interface IJobManagement
    {
        [OperationContract]
        Boolean RunJob(JobTypes JobToRun);
    }
}
