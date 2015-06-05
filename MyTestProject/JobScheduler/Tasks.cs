using System;
using System.Collections.Generic;
using System.Linq;
using Quartz;
using Quartz.Impl;

namespace JobScheduler
{
    public class Tasks
    {
        private IScheduler scheduler;
        private IDictionary<string, IJobDetail> jobDetails;

        public Tasks()
        {
            scheduler = StdSchedulerFactory.GetDefaultScheduler();
            jobDetails = new Dictionary<string, IJobDetail>();
        }

        private void AddJob<T>(string jobSubId, string cronExpression)
        {
            string jobName = string.Format("JobName_{0}_{1}", typeof(T).Name, jobSubId);
            string jobGroup = string.Format("JobGroup_{0}_{1}", typeof(T).Name, jobSubId);
            string triggerName = string.Format("TriggerName_{0}_{1}", typeof(T).Name, jobSubId);
            string triggerGroup = string.Format("TriggerGroup_{0}_{1}", typeof(T).Name, jobSubId);
            var scheduleJobDetail = new JobDetailImpl(jobName, jobGroup, typeof(T));
            var scheduleTrigger = (ICronTrigger)TriggerBuilder.Create()
                                                   .WithIdentity(triggerName, triggerGroup)
                                                   .WithCronSchedule(cronExpression)
                                                   .Build();

            string jobKey = string.Format("{0}_{1}", typeof(T).Name, jobSubId);
            if (!jobDetails.ContainsKey(jobKey))
            {
                jobDetails.Add(new KeyValuePair<string, IJobDetail>(jobKey, scheduleJobDetail));
                scheduler.ScheduleJob(scheduleJobDetail, scheduleTrigger);
            }
            else
            {
                string message = string.Format("This job [{0}] has already existed.", jobKey);
                Console.WriteLine(message);
                throw new Exception(message);
            }
        }

        // "0/20 * * * * ?"

        private void ReTriggerJob<T>(string jobSubId, string cronExpression)
        {
            if (scheduler.InStandbyMode)
            {
                scheduler.Start();
            }
            string triggerName = string.Format("TriggerName_{0}", typeof(T).Name);
            string triggerGroup = string.Format("TriggerGroup_{0}", typeof(T).Name);
            var scheduleTrigger = (ICronTrigger)TriggerBuilder.Create()
                                                          .WithIdentity(triggerName, triggerGroup)
                                                          .WithCronSchedule(cronExpression)
                                                          .Build();
            string jobKey = string.Format("{0}_{1}", typeof(T).Name, jobSubId);
            var scheduleJobDetail = jobDetails.FirstOrDefault(x => x.Key == jobKey).Value;
            if (scheduleJobDetail != null)
            {
                scheduler.DeleteJob(scheduleJobDetail.Key);
                scheduler.ScheduleJob(scheduleJobDetail, scheduleTrigger);
            }
        }

        public void Start()
        {
            scheduler.Start();

            AddJob<MyJob1>("A", "0/1 * * * * ?");
            AddJob<MyJob1>("B", "0 * * * * ?");
            System.Threading.Thread.Sleep(10000);
            scheduler.Standby();
            ReTriggerJob<MyJob1>("A", "0/5 * * * * ?");
        }

        public void Stop()
        {
            if (scheduler != null)
            {
                scheduler.Shutdown(true);
            }
        }
    }
}