using System;
using MongoDB.Driver;

namespace com.etak.core.repository.mongo
{
    /// <summary>
    /// class to get conenctions to the Mongo DB
    /// </summary>
    public class MongoConnectionProvider : IConnectionProvider
    {
        MongoClient client;

        public MongoConnectionProvider (String connectionString)
        {
           client = new MongoClient(connectionString);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IPersistanceConnection GetConnection()
        {
            return new MongoPersistanceConnection(client);
        }
    }
}
