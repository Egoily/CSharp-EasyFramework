using System;
using System.Data;

namespace com.etak.core.repository
{
    /// <summary>
    /// An interface to abstract the connectivity to different storage systems ORMs
    /// </summary>
    public interface IPersistanceConnection : IDisposable
    {
        /// <summary>
        /// Sets The notifier to invoke when the Session is disposed
        /// </summary>
        /// <param name="notifier">The notifier to invoke when the Session is disposed</param>
        void SetNotifier(ClearSessionForThread notifier);

        /// <summary>
        /// Close the connection to the persitance layer
        /// </summary>
        void Close();

        /// <summary>
        /// Starts a transaction in the persistance layer
        /// </summary>
        /// <returns>The transaction open</returns>
        IPersistanceTransaction BeginTransaction();

        /// <summary>
        /// Starts a transaction in the persistance layer
        /// </summary>
        /// <param name="level">The data isolation level to start the transaction <see cref="System.Data.IsolationLevel"/></param>
        /// <returns>The transaction open</returns>
        IPersistanceTransaction BeginTransaction(IsolationLevel level);
    }
}