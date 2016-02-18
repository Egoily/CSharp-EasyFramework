namespace com.etak.core.repository
{
    /// <summary>
    /// Interface to provide a "factory like" connection generator. 
    /// </summary>
    public interface IConnectionProvider
    {
        /// <summary>
        /// Gets a connection from the persistance layer
        /// </summary>
        /// <returns>the new connection</returns>
        IPersistanceConnection GetConnection();
    }
}
