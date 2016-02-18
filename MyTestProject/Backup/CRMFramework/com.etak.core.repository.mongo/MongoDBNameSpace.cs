using System;

namespace com.etak.core.repository.mongo
{
    /// <summary>
    /// Class to represent a MongoDB namespace
    /// </summary>
    public class MongoDBNameSpace
    {
        private readonly String _database;
        private readonly String _collection;
        private readonly String _ns;

        /// <summary>
        /// The database part of the namespace
        /// </summary>
        public String Database { get { return _database; } }
        /// <summary>
        /// The collection part of the namespace
        /// </summary>
        public String Collection { get { return _collection; } }


        /// <summary>
        /// Constructor providign db and cooll
        /// </summary>
        /// <param name="database">the name of the database</param>
        /// <param name="collection">the name of the collection</param>
        public MongoDBNameSpace(String database, String collection)
        {
            if (String.IsNullOrWhiteSpace(database))
                throw new ArgumentException("argument can't be null or whitespace", "database");


            if (String.IsNullOrWhiteSpace(collection))
                throw new ArgumentException("argument can't be null or whitespace", "collection");

            _database = database;
            _collection = collection;
            _ns = database + "." + collection;
        }

        public override string ToString()
        {
            return (_ns);
        }
    }
}
