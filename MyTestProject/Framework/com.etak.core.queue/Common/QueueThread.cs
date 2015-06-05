using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using log4net;

namespace com.etak.core.queue.Common
{
   
    public class QueueThread <T> 
    {
        protected const String ModuleName = "ClientQueueThread";
        private static readonly ILog log = log4net.LogManager.GetLogger("QueueThread");

        #region Class Members
        protected Boolean IsRunning = false;
        protected Boolean HasStopped = false;
        protected EventWaitHandle SignalingSemaphore;

        private readonly Thread RunningThread;
        private readonly AutoResetEvent RunningThreadFinishedSignal;
        private readonly Queue<QueueElementInformation<T>> Queue;
        private readonly IDequeuer<T> dequeuer;
        private readonly Object LockObject;
        private Boolean signaled = false;

        private const Int32 StopTimeoutMilileconds = 4000;
        private readonly Int32 MaxQueueSize = 0;
        private readonly Int32 PollingTimeMilliseconds;
        private readonly Int32 ElementsToBackupPerLoop;
        private readonly Int32 MaxElementsPerLoop;
        private readonly Int32 MaxRetryCount = 3;
        private readonly Int32 MinRetrySeconds = 30;
        private Int32 MaxQueuedElementsOnIdle = 15;
        private Boolean HasPendingBackup = true;
        #endregion

        #region Constructors
        public QueueThread(QueueThreadConfiguration QueueThreadConfiguration, IDequeuer<T> Dequeuer)
        {
            if (QueueThreadConfiguration.MaxQueuedElementsOnIdle < 0)
                throw new ArgumentException("MaxQueuedElementsOnIdle parameter needs to be greater or equael to 0");

            if (QueueThreadConfiguration.MaxQueueSize <= 0)
                throw new ArgumentException("MaxQueueSize parameter needs to be greater than 0");

            if (QueueThreadConfiguration.PollingTimeMilliseconds < 0)
                throw new ArgumentException("PollingTimeMilliseconds parameter needs to be greater or equal than 0");

            if (QueueThreadConfiguration.ElementsToBackupPerLoop <= 100)
                throw new ArgumentException("ElementsToBackupPerLoop parameter needs to be greater or equal than 100");

            if (QueueThreadConfiguration.MaxElementsPerLoop <= 0)
                throw new ArgumentException("MaxElementsPerLoop parameter needs to be greater than 0");

            if (QueueThreadConfiguration.MaxRetryCount <= 0)
                throw new ArgumentException("MaxRetryCount parameter needs to be greater than 0");

            if (QueueThreadConfiguration.MinRetrySeconds < 30)
                throw new ArgumentException("MinRetrySeconds parameter needs to be greater or equal than 30");

            if (Dequeuer == null)
                throw new ArgumentException("Dequeuer can't be null");
                

            this.MaxQueueSize = QueueThreadConfiguration.MaxQueueSize;
            this.MaxQueuedElementsOnIdle = QueueThreadConfiguration.MaxQueuedElementsOnIdle;
            this.PollingTimeMilliseconds = QueueThreadConfiguration.PollingTimeMilliseconds;
            this.ElementsToBackupPerLoop = QueueThreadConfiguration.ElementsToBackupPerLoop;
            this.MaxElementsPerLoop = QueueThreadConfiguration.MaxElementsPerLoop;
            this.MaxRetryCount = QueueThreadConfiguration.MaxRetryCount;
            this.MinRetrySeconds = QueueThreadConfiguration.MinRetrySeconds;
            this.dequeuer = Dequeuer;


            this.RunningThread = new Thread(this.Run);
            this.RunningThread.Name = QueueThreadConfiguration.QueueThreadName == null ? ModuleName : QueueThreadConfiguration.QueueThreadName;
            this.LockObject = new Object();
            this.SignalingSemaphore = new EventWaitHandle(false, EventResetMode.AutoReset);
            this.RunningThreadFinishedSignal = new AutoResetEvent(false);

            this.IsRunning = false;
            this.HasStopped = false;

            this.Queue = new Queue<QueueElementInformation<T>>(this.MaxQueueSize + 1);

            HasPendingBackup = true;
        }
        #endregion

        #region Queue Management
        public void Enqueue(T Object)
        {
            if (!IsRunning)
                throw new ThreadStateException(ModuleName + " is not running, can't be equeued");
            
            lock (LockObject)
            {
                Int32 QueueCount = Queue.Count();

                if (QueueCount >= MaxQueueSize)
                    throw new Exception("Unable to add element to queue, it's full");
                
                this.Queue.Enqueue(new QueueElementInformation<T>(Object));

                if (QueueCount >= MaxQueuedElementsOnIdle)
                {
                    if (!signaled)
                    {
                        SignalingSemaphore.Set();
                        signaled = true;
                    }
                }               
            }
        }

        public IList<QueueElementInformation<T>> DequeueWithFilter(Int32 MaxDequeueElements)
        {
            lock (LockObject)
            {
                Int32 QueueElementCount = Queue.Count();

                if (QueueElementCount == 0)
                    return (null);

                Int32 ElementsToReturn = MaxDequeueElements > QueueElementCount ? QueueElementCount : MaxDequeueElements;

                IList<QueueElementInformation<T>> Elements = new List<QueueElementInformation<T>>(ElementsToReturn);
                try
                {
                    for (int i = 0; i < ElementsToReturn; i++)
                    {
                        QueueElementInformation<T> element = Queue.Dequeue();

                        //In the case of element is first time to be processed.                       
                        if (element.TryCounter == 0)
                        {
                            Elements.Add(element);
                        }
                        //In the case of element is reprocessed. Only get the elements
                        //which reach timestamp to be processed.
                       
                        else
                        {
                            if (element.LastProcessedTimestamp.AddSeconds(MinRetrySeconds) < DateTime.Now)
                            {
                                Elements.Add(element);
                            }
                            else
                            {
                                Queue.Enqueue(element); // if it does not reach timestamp, enqueue it again.
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    log.Error("Error on queue", ex);
                }

                if (((QueueElementCount - ElementsToReturn) < MaxElementsPerLoop) && signaled)
                {
                    signaled = false;
                }

                return (Elements);
            }
        }

        public IList<QueueElementInformation<T>> Dequeue(Int32 MaxDequeueElements)
        {
            lock (LockObject)
            {
                Int32 QueueElementCount  = Queue.Count();
                
                if (QueueElementCount == 0)
                    return (null);

                Int32 ElementsToReturn = MaxDequeueElements > QueueElementCount ? QueueElementCount : MaxDequeueElements;

                IList<QueueElementInformation<T>> Elements = new List<QueueElementInformation<T>>(ElementsToReturn);
                try
                {
                    for (int i = 0; i < ElementsToReturn; i++)
                    {
                        QueueElementInformation<T> element = Queue.Dequeue();
                        Elements.Add(element);
                    }
                }
                catch (Exception ex)
                {
                    log.Error("Error on queue", ex);
                }

                if ( ((QueueElementCount - ElementsToReturn) < MaxElementsPerLoop) && signaled)
                {
                    signaled = false;
                }

                return (Elements);
            }
        }

        private void Requeue(IList<QueueElementInformation<T>> CurrentBlock)
        {
            //Because once Stop() is called, the 'IsRunning' flag will be set to false, and then 
            //the current processing element cannot be requeue for persisit.
            //
            //if (!IsRunning)
            //    throw new ThreadStateException(ModuleName + " is not running, can't be requeued");

            if (CurrentBlock == null)
                throw new ArgumentException("CurrentBlock parameter was null");

            Int32 CollectionToInsertCount = CurrentBlock.Count();

            if (CollectionToInsertCount == 0)
                return;

            lock (LockObject)
            {
                Int32 QueueCount = Queue.Count();

                if (QueueCount + CollectionToInsertCount >= MaxQueueSize)
                    throw new Exception("Unable to add element to queue, it's full");

                foreach (QueueElementInformation<T> element in CurrentBlock)
                    this.Queue.Enqueue(element);

                if (QueueCount >= MaxElementsPerLoop)
                {
                    if (!signaled)
                    {
                        SignalingSemaphore.Set();
                        signaled = true;
                    }
                }
            }
        }

        public void Enqueue(IList<T> CurrentBlock)
        {
            if (!IsRunning)
                throw new ThreadStateException(ModuleName + " is not running, can't be equeued");

            if (CurrentBlock == null)
                throw new ArgumentException("CurrentBlock parameter was null");

            Int32 CollectionToInsertCount = CurrentBlock.Count();

            if (CollectionToInsertCount == 0)
                return;

            lock (LockObject)
            {
                Int32 QueueCount = Queue.Count();

                if (QueueCount + CollectionToInsertCount  >= MaxQueueSize)
                    throw new Exception("Unable to add element to queue, it's full");
                
                foreach(T element in CurrentBlock)
                   this.Queue.Enqueue(new QueueElementInformation<T>(element));

                if (QueueCount >= MaxElementsPerLoop)
                {
                    if (!signaled)
                    {
                        SignalingSemaphore.Set();
                        signaled = true;
                    }
                }
            }
        }

        public bool IsQueueEmpty()
        {
            lock (LockObject)
            {
                return (!Queue.Any());
            }
        }

        protected bool IsQueueUsageOverThreshold()
        {
            lock (LockObject)
            {
                float QueueUSagePercentage = (float)Queue.Count() / (float)MaxQueueSize;                
                return (QueueUSagePercentage > 0.75);
            }
        }

        private bool IsReadyToRequeueBackup()
        {
            lock (LockObject)
            {
                float QueueUSagePercentage = ((float)(Queue.Count()+ this.ElementsToBackupPerLoop)) / (float)MaxQueueSize;               
                return (QueueUSagePercentage < 0.40);
            }
        }


        public Int32 GetQueueSize()
        {
            lock (LockObject)
            {
                return (Queue.Count());
            }
        }
        #endregion

        #region Thread management
        public void Start()
        {
            log.Info("Starting Queue Thread");
			this.IsRunning = true;
            this.RunningThread.Start();
        }

        public void Stop()
        {
            if (!IsRunning)
                throw new ThreadStateException(ModuleName + " is not running, can't be stopped");

            log.Info("Stoping queue thread");

            //First assign the variable to not runnig so we ensure that we don't queue elements we will not send.
            this.IsRunning = false;

            if (this.RunningThread.ThreadState == ThreadState.WaitSleepJoin)
                this.RunningThread.Interrupt();

            if (!this.RunningThread.Join(StopTimeoutMilileconds))
            {
                log.Info("The Queue thread did not ended propertly we need to Abort it");

                //Awake the clean up job of running thread.
                this.RunningThread.Abort(); 

                //Wait until running thread finishes its job.
                RunningThreadFinishedSignal.WaitOne();

                log.Info("Running thread stopped");
            }
            log.Info("Queue thread has stopped");
        }
        #endregion

        /// <summary>
        /// Checks if backup is pending to recover, it will append everythign on the queue until it is above the threshold
        /// </summary>
        private void TryRecoverBackup()
        {
            while (HasPendingBackup && IsReadyToRequeueBackup())
            {
                IList<T> ElementsToBeRecovered = this.RecoverElements();
                if (ElementsToBeRecovered != null && ElementsToBeRecovered.Count > 0)
                {
                    this.Enqueue(ElementsToBeRecovered);
                    log.Info(string.Format("Queue recovered {0} elements", ElementsToBeRecovered.Count));

                    ElementsToBeRecovered.Clear();
                    ElementsToBeRecovered = null;
                }
                else
                {
                    HasPendingBackup = false;
                }
            }
        }

        #region Worker Method
        /// <summary>
        /// Method that is going to be run on base.Start()
        /// This will loop over the queue to write the elements to the receiver.
        /// </summary>        
        protected void Run()
        {
            log.Info("Queue thread starting");
            RunningThreadFinishedSignal.Reset();
            dequeuer.StartUp();

            #region Recover elements when queue starts
            TryRecoverBackup();
            #endregion

            #region Loop while running
            while (IsRunning)
            {
                Boolean IsReceiverOnline = true;
                IList<QueueElementInformation<T>> CurrentBlock = null;
                IList<QueueElementInformation<T>> FailedElements = null;

                try
                {
                    //Give time to get some elements queued or be signaled by the queue status.
                    if (SignalingSemaphore.WaitOne(PollingTimeMilliseconds))
                    {
                        log.Debug("Signal received, we have elements to proccess");
                    }
                    else
                    {
                        log.Debug("Timeout reached, checking elements to be proccessed");
                    }

                    //First check emergency situation in which elements would need to go to emerency support.
                    while (IsQueueUsageOverThreshold())
                    {
                        log.Fatal("Queue was over the threshold, backing elements up");
                        IList<QueueElementInformation<T>> CurrentEmergencyBlock = Dequeue(ElementsToBackupPerLoop);
                        this.BackUpElements(CurrentEmergencyBlock.Select(x => x.Element).ToList());
                    }

                    //Dequeue the elements which reach the timestamp to be processed.
                    CurrentBlock = DequeueWithFilter(MaxElementsPerLoop);

                    while (CurrentBlock != null && CurrentBlock.Count != 0)
                    {
                        foreach (var queueElementInformation in CurrentBlock)
                            queueElementInformation.LastProcessedTimestamp = DateTime.Now;

                        log.Debug("Processing: " + CurrentBlock.Count() + " elements");
                        FailedElements = dequeuer.Process(CurrentBlock);

                        //Mark elements as proccesed and clear the pointer to allow memory deallocation
                        CurrentBlock.Clear();
                        CurrentBlock = null;

                        //Handle the failed elements
                        if (FailedElements != null && FailedElements.Count > 0)
                        {
                            log.Warn("Some elements were not submited, failedcount:" + FailedElements.Count());

                            //Log the failed elements which reach the MaxRetryCount, they have to be thrown.
                            List<QueueElementInformation<T>> ElementstToRemove = 
                                FailedElements.Where(e => e.TryCounter >= MaxRetryCount).ToList();
                            foreach (QueueElementInformation<T> e in ElementstToRemove)
                            {
                                log.Warn(string.Format("Threw failed element: \r\n[Request: {0}]", e.Element.ToString()));
                                FailedElements.Remove(e);
                            }

                            log.Warn("Requeueing failed elements:" + FailedElements.Count());
                            Requeue(FailedElements);


                            FailedElements.Clear();
                            FailedElements = null;
                        }
                        //Try to load more events
                        CurrentBlock = DequeueWithFilter(MaxElementsPerLoop);
                    }

                    //2. If the queue has not elements to be processed, recover elements 
                    //from persist files if they exist.
                    if (HasPendingBackup)
                    {
                        if (IsReadyToRequeueBackup())
                        {
                            IList<T> ElementsToRequeue = this.RecoverElements();
                            if (ElementsToRequeue != null && ElementsToRequeue.Count > 0)
                            {
                                this.Enqueue(ElementsToRequeue);
                                log.Info(string.Format("Queue recovered {0} elements", ElementsToRequeue.Count));
                                ElementsToRequeue.Clear();
                                ElementsToRequeue = null;
                            }
                            else { break; }
                        }
                        else { break; }
                    }

                    log.Debug("All elements in queue processed");
                }
                catch (ThreadAbortException)
                {
                    log.Error("Thread has been aborted, exting thread");
                    IsReceiverOnline = false;
                    Thread.ResetAbort();
                }
                catch (ThreadInterruptedException)
                {
                    log.Error("Thread has been changed from sleep to active");
                    IsReceiverOnline = false;
                }
                catch (Exception ex)
                {
                    log.Error("Unknown error dequeing elements:", ex);
                    //Mark the remote end as down
                    IsReceiverOnline = false;
                }
                finally
                {
                    if (!IsReceiverOnline)
                    {
                        //If any error happened requeue the elements
                        try
                        {
                            if (CurrentBlock != null && CurrentBlock.Count > 0)
                            {
                                log.Warn("Elements were not processed requeueing them");
                                Requeue(CurrentBlock);
                                CurrentBlock.Clear();
                            }

                            if (FailedElements != null && FailedElements.Count > 0)
                            {
                                log.Warn("Failed elements were not processed requeueing them");
                                Requeue(FailedElements.Where(e => e.TryCounter <= MaxRetryCount).ToList());
                                FailedElements.Clear();
                            }
                        }
                        catch (Exception ex)
                        {
                            log.Fatal("Could not enqueue proccesed and remote end is offline!!", ex);
                        }
                    }
                    //Don't rush on errors, avoid high CPU usage
                    Thread.Sleep(2 * 1000);
                }
            }
            #endregion

            #region Clean Up on exit
            //In any case dump any possible CDR at exit.
            try
            {
                while (!IsQueueEmpty())
                {
                    log.Warn("There were remaining elements in the queue, dumping to file");

                    IList<QueueElementInformation<T>> CurrentEmergencyBlock = Dequeue(ElementsToBackupPerLoop);
                    dequeuer.BackupElements(CurrentEmergencyBlock.Select(x=> x.Element).ToList());
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error dumping records to file on emergency exit, some records were lost", ex);
            }
            #endregion

            log.Info("Running thread exit");
            HasStopped = true;
            RunningThreadFinishedSignal.Set();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="blockToBeBackUp"></param>
        private void BackUpElements(IList<T> blockToBeBackUp)
        {
            lock (LockObject)
            {
                dequeuer.BackupElements(blockToBeBackUp);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private IList<T> RecoverElements()
        {
            lock (LockObject)
            {
                return dequeuer.RecoverElements();
            }
        }

        #endregion
    }
}
