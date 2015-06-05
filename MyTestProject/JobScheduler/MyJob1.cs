using System;
using Quartz;

namespace JobScheduler
{
    public class MyJob1 : IJob
    {
        private static int count = 0;

        public void Execute(IJobExecutionContext context)
        {
            Console.WriteLine(string.Format("[{0}] Execute {1} [{2}]", DateTime.Now, context.JobDetail.Key.Name, ++count));
        }
    }
}