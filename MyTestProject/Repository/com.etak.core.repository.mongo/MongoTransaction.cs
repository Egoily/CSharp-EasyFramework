namespace com.etak.core.repository.mongo
{
    /// <summary>
    /// Fake transaction class for mongo, just to be able to be compatible with
    /// Repository interfaces and schema.
    /// </summary>
    public class MongoTransaction : IPersistanceTransaction
    {
        /// <summary>
        /// no commit available for mongo
        /// </summary>
        public void Commit()
        {
            
        }

        /// <summary>
        /// no rollback available for mongo
        /// </summary>
        public void Rollback()
        {
           
        }

        /// <summary>
        /// Not available for mongo
        /// </summary>
        public void Dispose()
        {
            
        }
    }
}
