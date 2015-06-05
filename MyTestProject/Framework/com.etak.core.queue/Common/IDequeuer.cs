using System.Collections.Generic;

namespace com.etak.core.queue.Common
{
    public interface IDequeuer <T>
    {
        /// <summary>
        /// Method that will be invoked when the queue is overflowing or the thread must exit and the queue is not empty
        /// </summary>
        /// <param name="CurrentEmergencyBlock">list of elements</param>
        void BackupElements(IList<T> CurrentEmergencyBlock);

        /// <summary>
        /// Method that will be invoked when the getting the elements back from backup files.
        /// </summary>
        /// <returns>list of elements</returns>
        IList<T> RecoverElements();

        /// <summary>
        /// Proccess the elements T passed in current block, this method must return the elements not proccessed, 
        /// the list will be re enqueued.
        /// </summary>
        /// <param name="CurrentBlock">list of elements to be proccessed</param>
        /// <returns>list of elements which are processed failed.</returns>
        IList<QueueElementInformation<T>> Process(IList<QueueElementInformation<T>> CurrentBlock);

        /// <summary>
        /// This method will be invoked when the service is up after being down.
        /// </summary>
        //IList<T> ServiceIsOnline();

        /// <summary>
        /// This method is invoked before the first execution.
        /// </summary>
        void StartUp();
    }
}
