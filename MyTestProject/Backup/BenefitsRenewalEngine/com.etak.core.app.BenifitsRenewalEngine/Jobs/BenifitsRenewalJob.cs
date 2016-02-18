using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using com.etak.core.app.BenefitsRenewalEngine.contract;
using com.etak.core.app.BenifitsRenewalEngine.Actions.service;
using com.etak.core.app.BenifitsRenewalEngine.DTO;
using com.etak.core.app.BenifitsRenewalEngine.Repository;
using com.etak.core.repository;
using log4net;
using Quartz;

namespace com.etak.core.app.BenifitsRenewalEngine.Jobs
{
    [DisallowConcurrentExecution]
    public class BenifitsRenewalJob : IJob
    {
        private const String LocalWorkerCountName = "LocalWorkersCount";
        private const String RemoteWorkerCountName = "RemoteWorkersCount";
        private const String RemoteServiceUrlName = "RemoteServiceURL";        
        private const String CustomersPerLoopName = "CustomersPerLoop";

        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType.Name);

        /// <summary>
        /// Main process fetching the candidates to be renewed
        /// </summary>
        public void Execute(IJobExecutionContext context)
        {
            Stopwatch timer = Stopwatch.StartNew();

           
            Int32 localWorkersCount = 5;
            Int32 remoteWorkersCount = 25;
            int customersPerLoop = 100;

            List<RenewalWorker> workers = new List<RenewalWorker>();
            ConcurrentQueue<RenewalCandidates> renewalCandidates = new ConcurrentQueue<RenewalCandidates>();
            
            Timer presentationTimer = new Timer(o =>
            {
                Logger.Info("Current queue size:" + renewalCandidates.Count);
                
            }, null, TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(2));
            
           
            
            Logger.Info("Starting Job " + GetType().Name + " JobKey:" + context.JobDetail.Key);

            Logger.Info("Reading configuration parameter:" + LocalWorkerCountName);
            if (ConfigurationManager.AppSettings[LocalWorkerCountName] != null)
                localWorkersCount = Convert.ToInt32(ConfigurationManager.AppSettings[LocalWorkerCountName]);
            else
                Logger.Warn("Unable to read " + LocalWorkerCountName + " from app settings, using default value:" +
                            localWorkersCount);

            Logger.Info("Reading configuration parameter:" + RemoteWorkerCountName);
            if (ConfigurationManager.AppSettings[RemoteWorkerCountName] != null)
                remoteWorkersCount = Convert.ToInt32(ConfigurationManager.AppSettings[RemoteWorkerCountName]);
            else
                Logger.Warn("Unable to read " + RemoteWorkerCountName + " from app settings, using default value:" +
                            remoteWorkersCount);

            Logger.Info("Reading configuration parameter:" + CustomersPerLoopName);
            if (ConfigurationManager.AppSettings[CustomersPerLoopName] != null)
                customersPerLoop = Convert.ToInt32(ConfigurationManager.AppSettings[CustomersPerLoopName]);
            else
                Logger.Warn("Unable to read " + CustomersPerLoopName + " from app settings, using default value:" +
                            customersPerLoop);


            if (remoteWorkersCount > 0)
            {
                Logger.Info("Reading configuration parameter:" + RemoteServiceUrlName);
                if (ConfigurationManager.AppSettings[RemoteServiceUrlName] == null)
                    throw new Exception("Unable to read " + RemoteServiceUrlName +
                                        " from app settings and remoteWorkersCount is greater than 0");

                Logger.Info("Prepariring connections for remote servers");     
                RemoteWCFBenefitRenewalFactory.CreateFactoryOfURL(ConfigurationManager.AppSettings[RemoteServiceUrlName]);
             
            }

                 
            
               
            
            Logger.Info("Launching local " + localWorkersCount + " workers");
            for (int i = 0; i < localWorkersCount; i++)
            {
                //spawn a worker thread
                RenewalWorker worker = new RenewalWorker(FactoryHelper.GetLocalFactory(), renewalCandidates,
                    customersPerLoop);
                worker.StartWithName("LocalWorker-" + i.ToString("000"));
                workers.Add(worker);
            }

            Logger.Info("Launching " + remoteWorkersCount + "remote workers");
            for (int i = 0; i < remoteWorkersCount; i++)
            {
                //spawn a worker thread
                RenewalWorker worker = new RenewalWorker(FactoryHelper.GetRemoteWCFFactory(), renewalCandidates,
                    customersPerLoop);
                worker.StartWithName("RemoteWorker-" + i.ToString("000"));
                workers.Add(worker);
            }

            try
            {
                Logger.Info("Loading candidates from the DB");
                //fetch candidates
                using (RepositoryManager.GetNewConnection())
                {
                    var fetchDateTime = DateTime.Now;
                    int[] iCount = {-1};
                    long customerId = 0;
                    var benifitsRenewalRepo =
                        RepositoryManager.GetRepository<IBenifitsRenewalRepository<RenewalCandidates>>();

                    //Fetch Pre Renewal(if pre-renewal and renewal in one process, we need run pre-renewal action first, because renewal action is based on pre-renewal action)
                    while (iCount[0] != 0)
                    {
                        iCount[0] = 0;
                        Logger.InfoFormat("begin fetch records for pre-renew candidates, time:{0}", fetchDateTime);
                        Logger.ErrorFormat("fetch records for pre-renew candidates,custoemrid:{0}", customerId);
                        var renewalDatas = benifitsRenewalRepo.FetchPreRenewCandidates(Tasks.AMOUNT_PER_FETCH,
                            fetchDateTime, customerId);

                        renewalDatas.ForEach(ee =>
                        {
                            ee.RenewType = RenewType.PreRenew;
                            renewalCandidates.Enqueue(ee);
                            customerId = ee.CUSTOMERID;
                            iCount[0]++;
                        });
                    }

                    //Fetch Renewal
                    customerId = 0;
                    iCount[0] = -1;
                    while (iCount[0] != 0)
                    {
                        iCount[0] = 0;
                        Logger.InfoFormat("begin fetch records for renew candidates, time:{0}", fetchDateTime);
                        Logger.ErrorFormat("fetch records for renew candidates,custoemrid:{0}", customerId);
                        var renewalDatas = benifitsRenewalRepo.FetchRenewCandidates(Tasks.AMOUNT_PER_FETCH,
                            fetchDateTime, customerId);

                        renewalDatas.ForEach(ee =>
                        {
                            ee.RenewType = RenewType.Renew;
                            renewalCandidates.Enqueue(ee);
                            customerId = ee.CUSTOMERID;
                            iCount[0]++;
                        });

                    }
                }
            }
            catch (Exception ee)
            {
                Logger.InfoFormat("Fetch renewal records error,{0} ", ee);
            }

            Logger.Info("Waiting workers to finish their batches");
            try
            {
                foreach (var renewalWorker in workers)
                {
                    renewalWorker.GracefullyStop();
                }
                Logger.Info("All processors complete");
                timer.Stop();
                presentationTimer.Dispose();
                Logger.Info("Job Complete in:" + timer.ElapsedMilliseconds + " ms");
            }
            catch (Exception ex)
            {
                Logger.Error("Error stopping the processors", ex);
                throw;
            }
           
          
        }
    }
}
