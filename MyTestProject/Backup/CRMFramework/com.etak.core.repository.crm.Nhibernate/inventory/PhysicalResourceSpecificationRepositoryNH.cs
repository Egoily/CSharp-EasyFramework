using com.etak.core.model.inventory;
using com.etak.core.repository.crm.Nhibernate.Factory;
using com.etak.core.repository.NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace com.etak.core.repository.crm.Nhibernate.inventory
{
    /// <summary>
    /// Nhibernate repository for inheritance tree of entity PhysicalResourceSpecification 
    /// </summary>
    /// <typeparam name="TPhysicalResourceSpecification">Entity managed by the repository, is or extends PhysicalResourceSpecification</typeparam>
    public class PhysicalResourceSpecificationRepositoryNH<TPhysicalResourceSpecification> :
        NHibernateRepository<TPhysicalResourceSpecification, long>, IPhysicalResourceSpecificationRepository<TPhysicalResourceSpecification> where TPhysicalResourceSpecification : PhysicalResourceSpecification
    {
        private const String CacheRegion = CacheRegions.SystemSettingsCacheRegion;
        /// <summary>
        /// Get specification by sku.
        /// </summary>
        /// <param name="sku"></param>
        /// <returns></returns>
        public TPhysicalResourceSpecification GetBySKU(string sku)
        {
            return GetQuery().Where(x => x.SKU == sku).SingleOrDefault();
        }
        /// <summary>
        /// GetAllSpeicication by MVNOId
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TPhysicalResourceSpecification> GetAll()
        {
            return GetQuery().Cacheable().CacheRegion(CacheRegion).Future();
        }
    }


}
