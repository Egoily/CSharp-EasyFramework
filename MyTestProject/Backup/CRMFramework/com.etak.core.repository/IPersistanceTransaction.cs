using System;

namespace com.etak.core.repository
{
    /// <summary>
    /// An abstraction of a transaction in the repository
    /// </summary>
    public interface IPersistanceTransaction : IDisposable
    {
        /// <summary>
        /// Commits changes done in the in the repositories
        /// </summary>
        void Commit();

        /// <summary>
        /// Rollback the changes done in the repositories
        /// </summary>
        void Rollback();
    }
}
