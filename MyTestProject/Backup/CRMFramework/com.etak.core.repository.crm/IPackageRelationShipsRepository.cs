using System;
using System.Collections.Generic;
using com.etak.core.model;

namespace com.etak.core.repository.crm
{
    /// <summary>
    /// Repository interface for PackageRelationShips
    /// </summary>
    /// <typeparam name="TPackageRelationShips">The entity managed by the repository, is or extends PackageRelationShips</typeparam>
    public interface IPackageRelationShipsRepository<TPackageRelationShips> : IRepository<TPackageRelationShips, Int32> 
        where TPackageRelationShips : PackageRelationShips
    {
        /// <summary>
        /// gets all the package relationships
        /// </summary>
        /// <returns>the list of package relationships</returns>
        IEnumerable<TPackageRelationShips> GetAll();
    }
}
