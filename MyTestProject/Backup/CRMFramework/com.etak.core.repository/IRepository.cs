namespace com.etak.core.repository
{
    /// <summary>
    /// Interface to any repository, contains the basic CRUD methods that any repository must implement
    /// </summary>
    /// <typeparam name="TEntity">The type of entity that the repository implemntation will manage</typeparam>
    /// <typeparam name="TKey">The type of the key that the repository will manage</typeparam>
    public interface IRepository<TEntity, in TKey> where TEntity : class
    {       

        /// <summary>
        /// Gets an entity by it's unique ID
        /// </summary>
        /// <param name="id">Id of the entity to retrieve</param>
        /// <returns>The updated entity</returns>
        TEntity GetById(TKey id);

        /// <summary>
        /// Inserts an entity into the repository
        /// </summary>
        /// <param name="entity">the entity to be stored</param>
        /// <returns>the updated entity</returns>
        TEntity Create(TEntity entity);
        
        /// <summary>
        /// Updates an entity in the repository
        /// </summary>
        /// <param name="entity">the entity to be updated</param>
        /// <returns>the </returns>
        TEntity Update(TEntity entity);

        /// <summary>
        /// Deletes an entity from the repository
        /// </summary>
        /// <param name="entity">the entity to be deleted</param>
        void Delete(TEntity entity);
    }
}
