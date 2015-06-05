using System;
using System.Collections.Generic;
using com.etak.core.queue.Common;

namespace com.etak.core.queue.Server
{
    public class DBWriterDequeuer<T> : IDequeuer<T>
    {
        #region IDequeuer<T> Members

        public void BackupElements(IList<T> CurrentEmergencyBlock)
        {
            throw new NotImplementedException();
        }

        public IList<QueueElementInformation<T>> Process(IList<QueueElementInformation<T>> CurrentBlock)
        {
            throw new NotImplementedException();
        }

        public IList<T> ServiceIsOnline()
        {
            //read file and recover
            throw new NotImplementedException();
        }

        public IList<T> RecoverElements()
        {
            throw new NotImplementedException();
        }

        public void StartUp()
        {
            //throw new NotImplementedException();
            //No action needed
        }

        #endregion
    }
}
