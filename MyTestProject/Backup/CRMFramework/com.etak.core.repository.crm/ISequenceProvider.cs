using System;

namespace com.etak.core.repository.crm
{
    /// <summary>
    /// Interface to abstract the behaviour of sequence
    /// </summary>
    public interface ISequenceProvider
    {
        /// <summary>
        /// Gets the next number for the given sequence
        /// </summary>
        /// <param name="sequence">the name of the sequence from which next value will be gathered</param>
        /// <returns>the next number in the sequence</returns>
        Int32 GetNextSequence(String sequence);

        /// <summary>
        /// Gets the increment size on the storage engine
        /// </summary>
        /// <param name="sequence">the name of the sequence</param>
        /// <returns>the size of the increment in the storage engine</returns>
        int GetStepSize(String sequence);
    }
}
