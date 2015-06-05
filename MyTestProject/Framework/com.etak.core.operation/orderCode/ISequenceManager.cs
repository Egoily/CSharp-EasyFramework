using System;

namespace com.etak.core.operation.orderCode
{
    /// <summary>
    /// Interface to manage sequences (mainly used for OrderCodes)
    /// </summary>
    public interface ISequenceManager
    {
        /// <summary>
        /// Gets the next sequence for a given sequence name
        /// </summary>
        /// <returns></returns>
        Int32 GetNextSequence();

        /// <summary>
        /// Set the name for the sequence to be managed.  
        /// </summary>
        /// <param name="sequenceName">the name of the sequence to get</param>
        void SetSequenceName(String sequenceName);
    }
}
