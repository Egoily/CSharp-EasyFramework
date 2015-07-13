using System.Data;
using MongoDB.Driver;

namespace com.etak.core.repository.mongo
{
    /// <summary>
    /// Adapter class between ETRepository and MongoDB connection
    /// </summary>
    public class MongoPersistanceConnection : IPersistanceConnection
    {
        readonly MongoClient _client;
        ClearSessionForThread _notifier;

        /// <summary>
        /// Constructor providing MongoDB Connection
        /// </summary>
        /// <param name="client">the connection</param>
        public MongoPersistanceConnection(MongoClient client)
        {
            _client = client;
        }

        internal MongoClient GetUnderlayingConnection()
        {
            return _client;
        }

        public void SetNotifier(ClearSessionForThread notifier)
        {
            _notifier = notifier;
        }

        /// <summary>
        /// Mongo DB does not need to close the session
        /// </summary>
        public void Close()
        {
           
        }

        /// <summary>
        /// Mongo DB currently does not support transactions
        /// </summary>
        /// <returns></returns>
        public IPersistanceTransaction BeginTransaction()
        {
            return new MongoTransaction();
        }

        /// <summary>
        /// Actually it does nothing as MongoDB does not support transactions at the moment
        /// </summary>
        /// <param name="level">the isolation level</param>
        /// <returns>a dummy class that does nothing</returns>
        public IPersistanceTransaction BeginTransaction(IsolationLevel level)
        {
            return new MongoTransaction();
        }

        public void Dispose()
        {
            _notifier.Invoke();
            Close();
        }
    }
}
