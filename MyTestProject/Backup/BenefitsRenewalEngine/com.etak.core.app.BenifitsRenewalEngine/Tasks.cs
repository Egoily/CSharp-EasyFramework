using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceProcess;
using com.etak.core.app.BenifitsRenewalEngine.Jobs;
using log4net;
using Quartz;
using Quartz.Impl;

namespace com.etak.core.app.BenifitsRenewalEngine
{
    [DataContract]
    public enum JobTypes
    {
        [EnumMember]
        BenifitsRenewalJob,
    }

    partial class Tasks : ServiceBase
    {
        private static readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType.Name);
        private static Tasks serviceLastInstance = null;

        private ServiceHost host;
        private IScheduler scheduler;
        private IJobDetail JobToBenifitsRenewal;

        private string strBenifitsRenewalSchedule = string.Empty;
        public static int AMOUNT_PER_FETCH = 5000;
        public static int SMS_LANGUAGE = 3082;
        public static int PortNO = 8888;

        public static Tasks GetLastInstance()
        {
            return serviceLastInstance;
        }

        public Tasks()
        {
            try
            {
                InitializeComponent();
                _logger.Info("Initializing Quartz");
                scheduler = StdSchedulerFactory.GetDefaultScheduler();
                _logger.Info("Reading configurations");

                int.TryParse(ConfigurationManager.AppSettings["AMOUNT_PER_FETCH"], out AMOUNT_PER_FETCH);
                int.TryParse(ConfigurationManager.AppSettings["TEST_PORT_NUMBER"], out PortNO);

                if (ConfigurationManager.AppSettings.Get("BenifitsRenewalTime") != null)
                    strBenifitsRenewalSchedule = ConfigurationManager.AppSettings.Get("BenifitsRenewalTime");

                if (serviceLastInstance == null)
                {
                    _logger.Info("Constructor called for first time, stornign reference of service");
                    serviceLastInstance = this;
                }
                else
                {
                    _logger.Warn("Multiple instances of the service has been created service management may not work");
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Error initializing service", ex);
                throw;
            }
            
        }

        protected override void OnStart(string[] args)
        {
            StartJob();
        }

        protected override void OnStop()
        {
            //shut down the scheduler
            scheduler.Shutdown();

            // Close the ServiceHost.
            host.Close();
        }

        public void StartJob()
        {
            // and start it off
            scheduler.Start();

            JobToBenifitsRenewal = JobBuilder.Create<BenifitsRenewalJob>()
                .WithIdentity("JobToBenifitsRenewal", "group1")
                .Build();

            //var renewalBuilder = SimpleScheduleBuilder.RepeatMinutelyForever(5);
            var renewalBuilder = CronScheduleBuilder.CronSchedule(strBenifitsRenewalSchedule);

            ITrigger triggerToBenifitsRenewal = TriggerBuilder.Create()
                .WithIdentity("TriggerToBenifitsRenewal", "group1")
                .WithSchedule(renewalBuilder)
                .ForJob(JobToBenifitsRenewal)
                .Build();

            scheduler.ScheduleJob(JobToBenifitsRenewal, triggerToBenifitsRenewal);


            #region create a self hosted WCF to control the

            List<Uri> addresses = new List<Uri>();  
            addresses.Add(new Uri(string.Format("http://localhost:{0}/ServiceManagement",PortNO)));
            //addresses.Add(new Uri(@"http://localhost:8888/ServiceManagement"));

            _logger.Info("Creating new service host");
            host = new ServiceHost(typeof(JobManagement), addresses.ToArray());

            // Enable metadata publishing.
            ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
            smb.HttpGetEnabled = true;
            smb.MetadataExporter.PolicyVersion = PolicyVersion.Policy15;

            host.Description.Behaviors.Add(smb);

            // Open the ServiceHost to start listening for messages. Since
            // no endpoints are explicitly configured, the runtime will create
            // one endpoint per base address for each service contract implemented
            // by the service.
            _logger.InfoFormat("Opening listening ports and service host ");
            host.Open();

            _logger.Info("The service is ready");

            #endregion create a self hosted WCF to control the
        }

        internal Boolean RunJob(JobTypes type)
        {
            _logger.InfoFormat("manual triggering job begin at {0} ", DateTime.Now);
            try
            {
                IJobDetail job2run = null;
                if (type == JobTypes.BenifitsRenewalJob)
                {
                    job2run = this.JobToBenifitsRenewal;
                    _logger.InfoFormat("Triggering job {0} on request", job2run.Key);
                    scheduler.TriggerJob(job2run.Key);
                }
                _logger.InfoFormat("manual triggering job end at {0} ", DateTime.Now);
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error("Error scheduling an manual execution", ex);
                return false;
            }
            
        }
    }
}
