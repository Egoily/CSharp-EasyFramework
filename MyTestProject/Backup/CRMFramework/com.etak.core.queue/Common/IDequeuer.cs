using System.Collections.Generic;

namespace com.etak.core.queue.Common
{
    /// <summary>
    /// Interface to implement a process that dequeues the elements of type T
    /// </summary>
    /// <typeparam name="T">The type of elements in the queue and that will be read</typeparam>
    public interface IDequeuer <T>
    {
        /// <summary>
        /// Method that will be invoked when the queue is overflowing or the thread must exit and the queue is not empty
        /// </summary>
        /// <param name="currentEmergencyBlock">list of elements</param>
        void BackupElements(IList<T> currentEmergencyBlock);

        /// <summary>
        /// Method that will be invoked when the getting the elements back from backup files.
        /// </summary>
        /// <returns>list of elements</returns>
        IList<T> RecoverElements();

        /// <summary>
        /// Proccess the elements T passed in current block, this method must return the elements not proccessed, 
        /// the list will be re enqueued.
        /// </summary>
        /// <param name="currentBlock">list of elements to be proccessed</param>
        /// <returns>list of elements which are processed failed.</returns>
        IList<QueueElementInformation<T>> Process(IList<QueueElementInformation<T>> currentBlock);

      
        /// <summary>
        /// This method is invoked before the first execution.
        /// </summary>
        void StartUp();
    }
}
