using System;
using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.repository.crm.Nhibernate.Factory;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate
{
    /// <summary>
    /// Nhibernate repository for inheritance tree of entity PackageRelationShips 
    /// </summary>
    /// <typeparam name="TPackageRelationShips">Entity managed by the repository, is or extends PackageRelationShips</typeparam>
    public class PackageRelationShipsRepositoryNH<TPackageRelationShips> : 
        NHibernateRepository<TPackageRelationShips, Int32>, 
        IPackageRelationShipsRepository<TPackageRelationShips> where TPackageRelationShips : PackageRelationShips
    {

        private const String CacheRegion = CacheRegions.SystemSettingsCacheRegion;

        /// <summary>
        /// gets all the package relationships
        /// </summary>
        /// <returns>the list of package relationships</returns>
        public IEnumerable<TPackageRelationShips> GetAll()
        {
            return GetQuery().Cacheable().CacheRegion(CacheRegion).Future();
        }
         
    }
}
