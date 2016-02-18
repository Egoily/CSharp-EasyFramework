using System;

namespace com.etak.core.app.BenifitsRenewalEngine
{
    public class JobManagement : IJobManagement
    {
        public Boolean RunJob(JobTypes JobToRun)
        {
            return Tasks.GetLastInstance().RunJob(JobToRun);
        }
    }
}
