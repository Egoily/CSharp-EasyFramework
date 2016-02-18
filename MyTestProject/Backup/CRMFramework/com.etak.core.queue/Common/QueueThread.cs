using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using log4net;

namespace com.etak.core.queue.Common
{
   /// <summary>
   /// Thread that offers a thread safe queue and tries continuesly to dequeue it by 
   /// invoking the Dequuer provided in the constructor
   /// </summary>
   /// <typeparam name="T">The type of objects stored in the queue</typeparam>
    public class QueueThread <T> 
    {

        private const String ModuleName = "ClientQueueThread";
        // ReSharper disable once StaticFieldInGenericType
        private static readonly ILog log = log4net.LogManager.GetLogger("QueueThread");

        #region Class Members
        protected Boolean IsRunning = false;
        protected Boolean HasStopped = false;
        protected EventWaitHandle SignalingSemaphore;

        private readonly Thread _runningThread;
        private readonly AutoResetEvent _runningThreadFinishedSignal;
        private readonly Queue<QueueElementInformation<T>> _queue;
        private readonly IDequeuer<T> _dequeuer;
        private readonly Object _lockObject;
        private Boolean _signaled = false;

        private const Int32 StopTimeoutMilileconds = 4000;
        private readonly Int32 _maxQueueSize = 0;
        private readonly Int32 _pollingTimeMilliseconds;
        private readonly Int32 _elementsToBackupPerLoop;
        private readonly Int32 _maxElementsPerLoop;
        private readonly Int32 _maxRetryCount = 3;
        private readonly Int32 _minRetrySeconds = 30;
        private readonly Int32 _maxQueuedElementsOnIdle = 15;
        private Boolean _hasPendingBackup = true;
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new queue thread,holding elements of type T, that are being processed by the 
        /// dequeuer configured. 
        /// </summary>
        /// <param name="queueThreadConfiguration">The parameters to configure the behaviour of the queue</param>
        /// <param name="dequeuer">the implementaion IDequeuer that will be used to process the messages</param>
        public QueueThread(QueueThreadConfiguration queueThreadConfiguration, IDequeuer<T> dequeuer)
        {
            if (queueThreadConfiguration.MaxQueuedElementsOnIdle < 0)
                throw new ArgumentException("MaxQueuedElementsOnIdle parameter needs to be greater or equael to 0");

            if (queueThreadConfiguration.MaxQueueSize <= 0)
                throw new ArgumentException("MaxQueueSize parameter needs to be greater than 0");

            if (queueThreadConfiguration.PollingTimeMilliseconds < 0)
                throw new ArgumentException("PollingTimeMilliseconds parameter needs to be greater or equal than 0");

            if (queueThreadConfiguration.ElementsToBackupPerLoop <= 100)
                throw new ArgumentException("ElementsToBackupPerLoop parameter needs to be greater or equal than 100");

            if (queueThreadConfiguration.MaxElementsPerLoop <= 0)
                throw new ArgumentException("MaxElementsPerLoop parameter needs to be greater than 0");

            if (queueThreadConfiguration.MaxRetryCount <= 0)
                throw new ArgumentException("MaxRetryCount parameter needs to be greater than 0");

            if (queueThreadConfiguration.MinRetrySeconds < 30)
                throw new ArgumentException("MinRetrySeconds parameter needs to be greater or equal than 30");

            if (dequeuer == null)
                throw new ArgumentException("Dequeuer can't be null");
                

            _maxQueueSize = queueThreadConfiguration.MaxQueueSize;
            _maxQueuedElementsOnIdle = queueThreadConfiguration.MaxQueuedElementsOnIdle;
            _pollingTimeMilliseconds = queueThreadConfiguration.PollingTimeMilliseconds;
            _elementsToBackupPerLoop = queueThreadConfiguration.ElementsToBackupPerLoop;
            _maxElementsPerLoop = queueThreadConfiguration.MaxElementsPerLoop;
            _maxRetryCount = queueThreadConfiguration.MaxRetryCount;
            _minRetrySeconds = queueThreadConfiguration.MinRetrySeconds;
            _dequeuer = dequeuer;


            _runningThread = new Thread(Run);
            _runningThread.Name = queueThreadConfiguration.QueueThreadName ?? ModuleName;
            _lockObject = new Object();
            SignalingSemaphore = new EventWaitHandle(false, EventResetMode.AutoReset);
            _runningThreadFinishedSignal = new AutoResetEvent(false);

            IsRunning = false;
            HasStopped = false;

            _queue = new Queue<QueueElementInformation<T>>(_maxQueueSize + 1);

            _hasPendingBackup = true;
        }
        #endregion

        #region Queue Management
        /// <summary>
        /// Adds an object to the queue, this method is thread sage
        /// </summary>
        /// <param name="Object">The object to be enqueued</param>
        public void Enqueue(T Object)
        {
            if (!IsRunning)
                throw new ThreadStateException(ModuleName + " is not running, can't be equeued");
            
            lock (_lockObject)
            {
                Int32 queueCount = _queue.Count();

                if (queueCount >= _maxQueueSize)
                    throw new Exception("Unable to add element to queue, it's full");
                
                _queue.Enqueue(new QueueElementInformation<T>(Object));

                if (queueCount >= _maxQueuedElementsOnIdle)
                {
                    if (!_signaled)
                    {
                        SignalingSemaphore.Set();
                        _signaled = true;
                    }
                }               
            }
        }

        /// <summary>
        /// Extracts a maximun number of the queue that have the retry interval reached
        /// </summary>
        /// <param name="maxDequeueElements">the  maximun number of the queue to extract</param>
        /// <returns>a list with the elements dequeued</returns>
        private IList<QueueElementInformation<T>> DequeueWithFilter(Int32 maxDequeueElements)
        {
            lock (_lockObject)
            {
                Int32 queueElementCount = _queue.Count();

                if (queueElementCount == 0)
                    return (null);

                Int32 elementsToReturn = maxDequeueElements > queueElementCount ? queueElementCount : maxDequeueElements;

                IList<QueueElementInformation<T>> elements = new List<QueueElementInformation<T>>(elementsToReturn);
                try
                {
                    for (int i = 0; i < elementsToReturn; i++)
                    {
                        QueueElementInformation<T> element = _queue.Dequeue();

                        //In the case of element is first time to be processed.                       
                        if (element.TryCounter == 0)
                        {
                            elements.Add(element);
                        }
                        //In the case of element is reprocessed. Only get the elements
                        //which reach timestamp to be processed.
                       
                        else
                        {
                            if (element.LastProcessedTimestamp.AddSeconds(_minRetrySeconds) < DateTime.Now)
                            {
                                elements.Add(element);
                            }
                            else
                            {
                                _queue.Enqueue(element); // if it does not reach timestamp, enqueue it again.
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    log.Error("Error on queue", ex);
                }

                if (((queueElementCount - elementsToReturn) < _maxElementsPerLoop) && _signaled)
                {
                    _signaled = false;
                }

                return (elements);
            }
        }

        /// <summary>
        /// Deques a maximun number of items from a queue
        /// </summary>
        /// <param name="maxDequeueElements">the maximun number of elements to retrieve</param>
        /// <returns>a list with the elements dequeued</returns>
        private IList<QueueElementInformation<T>> Dequeue(Int32 maxDequeueElements)
        {
            lock (_lockObject)
            {
                Int32 queueElementCount  = _queue.Count();
                
                if (queueElementCount == 0)
                    return (null);

                Int32 elementsToReturn = maxDequeueElements > queueElementCount ? queueElementCount : maxDequeueElements;

                IList<QueueElementInformation<T>> elements = new List<QueueElementInformation<T>>(elementsToReturn);
                try
                {
                    for (int i = 0; i < elementsToReturn; i++)
                    {
                        QueueElementInformation<T> element = _queue.Dequeue();
                        elements.Add(element);
                    }
                }
                catch (Exception ex)
                {
                    log.Error("Error on queue", ex);
                }

                if ( ((queueElementCount - elementsToReturn) < _maxElementsPerLoop) && _signaled)
                {
                    _signaled = false;
                }

                return (elements);
            }
        }

        /// <summary>
        /// Adds the elements back to the queue 
        /// </summary>
        /// <param name="currentBlock">the elements to requue</param>
        private void Requeue(IList<QueueElementInformation<T>> currentBlock)
        {
            if (currentBlock == null)
                throw new ArgumentException("CurrentBlock parameter was null");

            Int32 collectionToInsertCount = currentBlock.Count();

            if (collectionToInsertCount == 0)
                return;

            lock (_lockObject)
            {
                Int32 queueCount = _queue.Count();

                if (queueCount + collectionToInsertCount >= _maxQueueSize)
                    throw new Exception("Unable to add element to queue, it's full");

                foreach (QueueElementInformation<T> element in currentBlock)
                    _queue.Enqueue(element);

                if (queueCount >= _maxElementsPerLoop)
                {
                    if (!_signaled)
                    {
                        SignalingSemaphore.Set();
                        _signaled = true;
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

            Int32 collectionToInsertCount = CurrentBlock.Count();

            if (collectionToInsertCount == 0)
                return;

            lock (_lockObject)
            {
                Int32 queueCount = _queue.Count();

                if (queueCount + collectionToInsertCount  >= _maxQueueSize)
                    throw new Exception("Unable to add element to queue, it's full");
                
                foreach(T element in CurrentBlock)
                   _queue.Enqueue(new QueueElementInformation<T>(element));

                if (queueCount >= _maxElementsPerLoop)
                {
                    if (!_signaled)
                    {
                        SignalingSemaphore.Set();
                        _signaled = true;
                    }
                }
            }
        }

        /// <summary>
        /// True if the queue is empty
        /// </summary>
        /// <returns>true if the queue is empty</returns>
        public bool IsQueueEmpty()
        {
            lock (_lockObject)
            {
                return (!_queue.Any());
            }
        }

        /// <summary>
        /// Determines if the queue is avobe the maximun save usage
        /// </summary>
        /// <returns>true if it's avobe the threhold, false otherwise</returns>
        protected bool IsQueueUsageOverThreshold()
        {
            lock (_lockObject)
            {
                float queueUSagePercentage = (float)_queue.Count() / (float)_maxQueueSize;                
                return (queueUSagePercentage > 0.75);
            }
        }

      
        /// <summary>
        /// Determines when the queue usage is below a theshold in which the backed
        /// up elements should be recovered and requeued again
        /// </summary>
        /// <returns>True if the queue usage is below 40%</returns>
        private bool IsReadyToRequeueBackup()
        {
            lock (_lockObject)
            {
                float queueUSagePercentage = ((float)(_queue.Count()+ this._elementsToBackupPerLoop)) / (float)_maxQueueSize;               
                return (queueUSagePercentage < 0.40);
            }
        }

        /// <summary>
        /// Gets the number of elements in the queue
        /// </summary>
        /// <returns>the number of elements in the queue</returns>
        public Int32 GetQueueSize()
        {
            lock (_lockObject)
            {
                return (_queue.Count());
            }
        }
        #endregion

        #region Thread management
        /// <summary>
        /// Starts the queue thread
        /// </summary>
        public void Start()
        {
            log.Info("Starting Queue Thread");
			IsRunning = true;
            _runningThread.Start();
        }

        /// <summary>
        /// Stops the queue thread
        /// </summary>
        public void Stop()
        {
            if (!IsRunning)
                throw new ThreadStateException(ModuleName + " is not running, can't be stopped");

            log.Info("Stoping queue thread");

            //First assign the variable to not runnig so we ensure that we don't queue elements we will not send.
            IsRunning = false;

            if (_runningThread.ThreadState == ThreadState.WaitSleepJoin)
                _runningThread.Interrupt();

            if (!_runningThread.Join(StopTimeoutMilileconds))
            {
                log.Info("The Queue thread did not ended propertly we need to Abort it");

                //Awake the clean up job of running thread.
                _runningThread.Abort(); 

                //Wait until running thread finishes its job.
                _runningThreadFinishedSignal.WaitOne();

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
            while (_hasPendingBackup && IsReadyToRequeueBackup())
            {
                IList<T> elementsToBeRecovered = RecoverElements();
                if (elementsToBeRecovered != null && elementsToBeRecovered.Count > 0)
                {
                    Enqueue(elementsToBeRecovered);
                    log.Info(string.Format("Queue recovered {0} elements", elementsToBeRecovered.Count));

                    elementsToBeRecovered.Clear();
                    elementsToBeRecovered = null;
                }
                else
                {
                    _hasPendingBackup = false;
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
            _runningThreadFinishedSignal.Reset();
            _dequeuer.StartUp();

            #region Recover elements when queue starts
            TryRecoverBackup();
            #endregion

            #region Loop while running
            while (IsRunning)
            {
                Boolean isReceiverOnline = true;
                IList<QueueElementInformation<T>> currentBlock = null;
                IList<QueueElementInformation<T>> failedElements = null;

                try
                {
                    //Give time to get some elements queued or be signaled by the queue status.
                    if (SignalingSemaphore.WaitOne(_pollingTimeMilliseconds))
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
                        IList<QueueElementInformation<T>> currentEmergencyBlock = Dequeue(_elementsToBackupPerLoop);
                        BackUpElements(currentEmergencyBlock.Select(x => x.Element).ToList());
                    }

                    //Dequeue the elements which reach the timestamp to be processed.
                    currentBlock = DequeueWithFilter(_maxElementsPerLoop);

                    while (currentBlock != null && currentBlock.Count != 0)
                    {
                        foreach (var queueElementInformation in currentBlock)
                            queueElementInformation.LastProcessedTimestamp = DateTime.Now;

                        log.Debug("Processing: " + currentBlock.Count() + " elements");
                        failedElements = _dequeuer.Process(currentBlock);

                        //Mark elements as proccesed and clear the pointer to allow memory deallocation
                        currentBlock.Clear();
                        currentBlock = null;

                        //Handle the failed elements
                        if (failedElements != null && failedElements.Count > 0)
                        {
                            log.Warn("Some elements were not submited, failedcount:" + failedElements.Count());

                            //Log the failed elements which reach the MaxRetryCount, they have to be thrown.
                            List<QueueElementInformation<T>> elementstToRemove = 
                                failedElements.Where(e => e.TryCounter >= _maxRetryCount).ToList();
                            foreach (QueueElementInformation<T> e in elementstToRemove)
                            {
                                log.Warn(string.Format("Threw failed element: \r\n[Request: {0}]", e.Element.ToString()));
                                failedElements.Remove(e);
                            }

                            log.Warn("Requeueing failed elements:" + failedElements.Count());
                            Requeue(failedElements);


                            failedElements.Clear();
                            failedElements = null;
                        }
                        //Try to load more events
                        currentBlock = DequeueWithFilter(_maxElementsPerLoop);
                    }

                    //2. If the queue has not elements to be processed, recover elements 
                    //from persist files if they exist.
                    if (_hasPendingBackup)
                    {
                        if (IsReadyToRequeueBackup())
                        {
                            IList<T> elementsToRequeue = RecoverElements();
                            if (elementsToRequeue != null && elementsToRequeue.Count > 0)
                            {
                                Enqueue(elementsToRequeue);
                                log.Info(string.Format("Queue recovered {0} elements", elementsToRequeue.Count));
                                elementsToRequeue.Clear();
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
                    isReceiverOnline = false;
                    Thread.ResetAbort();
                }
                catch (ThreadInterruptedException)
                {
                    log.Error("Thread has been changed from sleep to active");
                    isReceiverOnline = false;
                }
                catch (Exception ex)
                {
                    log.Error("Unknown error dequeing elements:", ex);
                    //Mark the remote end as down
                    isReceiverOnline = false;
                }
                finally
                {
                    if (!isReceiverOnline)
                    {
                        //If any error happened requeue the elements
                        try
                        {
                            if (currentBlock != null && currentBlock.Count > 0)
                            {
                                log.Warn("Elements were not processed requeueing them");
                                Requeue(currentBlock);
                                currentBlock.Clear();
                            }

                            if (failedElements != null && failedElements.Count > 0)
                            {
                                log.Warn("Failed elements were not processed requeueing them");
                                Requeue(failedElements.Where(e => e.TryCounter <= _maxRetryCount).ToList());
                                failedElements.Clear();
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

                    IList<QueueElementInformation<T>> currentEmergencyBlock = Dequeue(_elementsToBackupPerLoop);
                    _dequeuer.BackupElements(currentEmergencyBlock.Select(x=> x.Element).ToList());
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Error dumping records to file on emergency exit, some records were lost", ex);
            }
            #endregion

            log.Info("Running thread exit");
            HasStopped = true;
            _runningThreadFinishedSignal.Set();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="blockToBeBackUp"></param>
        private void BackUpElements(IList<T> blockToBeBackUp)
        {
            lock (_lockObject)
            {
                _dequeuer.BackupElements(blockToBeBackUp);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private IList<T> RecoverElements()
        {
            lock (_lockObject)
            {
                return _dequeuer.RecoverElements();
            }
        }

        #endregion
    }
}
