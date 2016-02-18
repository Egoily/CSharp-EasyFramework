using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using com.etak.core.app.BenefitsRenewalEngine.contract;
using com.etak.core.app.BenifitsRenewalEngine.DTO;
using ILog = log4net.ILog;
using LogManager = log4net.LogManager;

namespace com.etak.core.app.BenifitsRenewalEngine.Jobs
{
    class RenewalWorker
    {
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType.Name);

        private readonly IBenefitRenewalFactory _renewalFactory;
        private readonly ConcurrentQueue<RenewalCandidates> _renewalCandidates;
        private readonly int _customersPerloop;

        private Boolean _shouldRun = true;
        private Thread _workerThread;

        public RenewalWorker(IBenefitRenewalFactory renewalFactory, ConcurrentQueue<RenewalCandidates> commonQueue, Int32 customersPerloop)
        {
            _renewalFactory = renewalFactory;
            _renewalCandidates = commonQueue;
            _customersPerloop = customersPerloop;
        }

        public void StartWithName(String threadName)
        {
            _workerThread = new Thread(RenewalProcess)
            {
                Name = threadName
            };
            _shouldRun = true;
            _workerThread.Start();
        }

        /// <summary>
        /// Renwal processes (Working thread) performing the actual renewal of the promotions
        /// </summary>
        private void RenewalProcess()
        {
            IBenefitsRenewalService service = _renewalFactory.GetInstance();

            if (Logger.IsDebugEnabled)
                Logger.Debug("Renewal worker started");

            Stopwatch watch = Stopwatch.StartNew();
            //sub thread will be closed when queue is empty.
            while (_shouldRun || !_renewalCandidates.IsEmpty)
            {
                
                RenewalCandidates renewalItem;
                List<Int32> customersToRenew = new List<int>();
                List<Int32> customersToPrerenw = new List<int>();
                
                //Fill a batch of customer to renew
                while (_renewalCandidates.TryDequeue(out renewalItem) && 
                    (customersToRenew.Count + customersToPrerenw.Count) < _customersPerloop)
                {
                    if (renewalItem.RenewType == RenewType.Renew)
                        customersToRenew.Add(renewalItem.CUSTOMERID);
                    else
                        customersToPrerenw.Add(renewalItem.CUSTOMERID);
                }


                if ((customersToRenew.Count + customersToPrerenw.Count) > 0)
                {
                    
                    if (customersToRenew.Count > 0)
                    {
                        if (Logger.IsDebugEnabled)
                            Logger.Debug("Renewing " + customersToRenew.Count + " customers");
                        Logger.ErrorFormat("Start Renewing customes:{0}", string.Join(",", customersToRenew.ToArray()));
                        try
                        {
                            watch.Restart();
                            service.RenewCustomersBenefits(customersToRenew);
                            Logger.Info("Renewed " + customersToRenew.Count + " customers in:" + watch.ElapsedMilliseconds);
                            Logger.ErrorFormat("Finish Renewing customes:{0}", string.Join(",", customersToRenew.ToArray()));
                        }
                        catch (Exception ex)
                        {
                            Logger.Error("Process renew for customers", ex);
                        }
                    }

                    if (customersToPrerenw.Count > 0)
                    {
                        if (Logger.IsDebugEnabled)
                            Logger.Debug("PreRenewing " + customersToPrerenw.Count + " Customers");
                        Logger.ErrorFormat("Start PreRenewing customes:{0}", string.Join(",", customersToPrerenw.ToArray()));
                        try
                        {
                            watch.Restart();
                            service.PreRenewCustomersBenefits(customersToPrerenw);
                            Logger.Info("PreRenewed " + customersToPrerenw.Count + " customers in:" + watch.ElapsedMilliseconds);
                            Logger.ErrorFormat("Finish PreRenewing customes:{0}", string.Join(",", customersToPrerenw.ToArray()));
                        }
                        catch (Exception ex)
                        {
                            Logger.Error("Process prerenew for customers", ex);
                        }
                    }
                }
                else
                {
                    Thread.Sleep(250);
                }
            }
        }

        internal void GracefullyStop()
        {
            _shouldRun = false;
            if (Logger.IsDebugEnabled)
                Logger.Debug("Waiting worker to complete");
            _workerThread.Join();
        }
    }
}
