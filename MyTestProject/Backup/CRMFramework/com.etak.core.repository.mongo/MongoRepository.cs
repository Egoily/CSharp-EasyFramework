using System.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;

namespace com.etak.core.repository.mongo
{
    /// <summary>
    /// Base implementation of repository based on mongo
    /// </summary>
    /// <typeparam name="TEntity">The type of entity to be persisted</typeparam>
    /// <typeparam name="TKey">The type of key of the entity</typeparam>
    public abstract class MongoRepository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class
    {
        protected MongoCollection<TEntity> _collection;

        private BsonClassMap map = BsonClassMap .LookupClassMap(typeof(TEntity));

        protected abstract MongoDatabaseSettings dbSettings { get;}
        protected abstract MongoCollectionSettings collSettings {get;}
        protected abstract MongoDBNameSpace nsMap { get; }

        public MongoRepository()
        {         

             var connection =  RepositoryManager.GetConnection() as MongoPersistanceConnection;
            MongoServer srv = connection.GetUnderlayingConnection().GetServer();
             MongoDatabase db = srv.GetDatabase(nsMap.Database, dbSettings);
             _collection = db.GetCollection<TEntity>(nsMap.Collection, collSettings);
        }

        protected IQueryable<TEntity> GetQuery()
        {            
           return  (_collection.AsQueryable());
        }

        /// <summary>
        /// Returns the T by its given id.
        /// </summary>
        /// <param name="id">The Id of the entity to retrieve.</param>
        /// <returns>The Entity T.</returns>
        public TEntity GetById(TKey id)
        {
            return _collection.FindOne(GetIdQuery(id));
        }

        TEntity IRepository<TEntity, TKey>.Create(TEntity entity)
        {
            WriteConcernResult res = _collection.Save(entity);            
            return (entity);
        }

        public TEntity Update(TEntity entity)
        {
            WriteConcernResult res = _collection.Save(entity);
            return (entity);
        }

        public void Delete(TEntity entity)
        {
            WriteConcernResult res = _collection.Remove(GetIdQuery(entity));
        }

        private IMongoQuery GetIdQuery(TEntity entity)
        {
            var value = map.IdMemberMap.Getter.Invoke(entity);
            return Query.EQ("_id", BsonValue.Create(value));           
        }

        private IMongoQuery GetIdQuery(TKey key)
        {           
            return Query.EQ("_id", BsonValue.Create(key));
        }
    }
}
