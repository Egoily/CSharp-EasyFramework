using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace com.etak.core.repository
{
    /// <summary>
    /// Interface to any nulk repository, contains the basic CRUD methods in bulk mode that any repository must implement
    /// </summary>
    /// <typeparam name="TEntity">The type of entity that the repository implemntation will manage</typeparam>
    /// <typeparam name="TKey">The type of the key that the repository will manage</typeparam>
    public interface IBulkRepository<TEntity, in TKey> : IRepository<TEntity, TKey> where TEntity : class
    {
        /// <summary>
        /// Inserts in bulk a set of entities
        /// </summary>
        /// <param name="entities">the list of entities to be inserted</param>
        /// <returns>the list of entites after inserted</returns>
        IEnumerable<TEntity> BulkCreate(IEnumerable<TEntity> entities);

        /// <summary>
        /// Update in bulk a set of entities
        /// </summary>
        /// <param name="entities">the list of entities to be updated</param>
        /// <param name="path">the list of properties of the entities that needs to be updated</param>
        /// <returns>the list of entites after updated</returns>
        IEnumerable<TEntity> BulkUpdate(IEnumerable<TEntity> entities, Expression<Func<TEntity, object>> [] path);


        /// <summary>
        /// Delete in bulk a set of entities
        /// </summary>
        /// <param name="entities">the list of entities to be deleted</param>
        /// <returns>the list of entites after deleted</returns>
        IEnumerable<TEntity> BulkDelete(IEnumerable<TEntity> entities);
        
      
    }
}
