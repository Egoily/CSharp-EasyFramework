using System;
using NHibernate;

namespace com.etak.core.repository.NHibernate
{
    /// <summary>
    /// NHibernate generic repository, implements all basic CRUD operations. Manges the connection management
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity that this repository manages</typeparam>
    /// <typeparam name="TKey">The type of the key of the entity, tipically Int64 or Int32</typeparam>
    public class NHibernateRepository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class
    {
        /// <summary>
        /// Session obtained in the constructor via the repository manger
        /// </summary>
        protected ISession _session;

        /// <summary>
        /// constructor, gets the session from the repository manager.
        /// </summary>
        public NHibernateRepository()
        {
            SessionToPersistanceAdapter session = RepositoryManager.GetConnection() as SessionToPersistanceAdapter;
            _session = session.GetUndelayingSession();
        }

        /// <summary>
        /// Creates a QueryOver root query for the given entity
        /// </summary>
        /// <returns></returns>
        protected virtual IQueryOver<TEntity, TEntity> GetQuery()
        {
            return (_session.QueryOver<TEntity>());
        }

        /// <summary>
        /// Gets the entity by ID
        /// </summary>
        /// <param name="id">the Id of the entity</param>
        /// <returns>the fetched entity</returns>
        public virtual TEntity GetById(String id)
        {
            return _session.Get<TEntity>(id);
        }

        /// <summary>
        /// Implementation of Delete Repository, Deletes an entity from the DB
        /// </summary>
        /// <param name="entity">the entity to be deleted</param>
        public virtual void Delete(TEntity entity)
        {
            _session.Delete(entity);
        }

        /// <summary>
        /// Implementation of GetbyId using NHibernate
        /// </summary>
        /// <param name="id">the Id of the entity to look up</param>
        /// <returns>the fetched entity</returns>
        public virtual TEntity GetById(TKey id)
        {
            return (_session.Get<TEntity>(id));
        }

        /// <summary>
        /// Implementation of Create repository using Nhibernate, creates an entity in the DB
        /// </summary>
        /// <param name="entity">the entity to be created</param>
        /// <returns>The entity with the generated identifier</returns>
        public virtual TEntity Create(TEntity entity)
        {
            _session.Save(entity);
            return (entity);
        }

        /// <summary>
        /// Implementation of Update repository using NHibernate, updates the entity in the DB
        /// </summary>
        /// <param name="entity">the entity to be updated</param>
        /// <returns>the updated entity</returns>
        public virtual TEntity Update(TEntity entity)
        {
            _session.Update(entity);
            return (entity);
        }
    }
}