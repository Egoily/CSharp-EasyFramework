using System;
using com.etak.core.model.operation;
using com.etak.core.repository.crm.operation;
using com.etak.core.repository.NHibernate;
using com.etak.core.repository.crm.Nhibernate.Factory;
using System.Linq;


namespace com.etak.core.repository.crm.Nhibernate.operation
{
    /// <summary>
    /// Repository based on NHibernate for  BusinessOperationExecution entity inheritance tree
    /// </summary>
    /// <typeparam name="TBusinessOperation">The type of the managed entity, is or inherits BusinessOperationExecution</typeparam>
    public class BusinessOperationRepositoryNH<TBusinessOperation> : 
            NHibernateRepository<TBusinessOperation, Int32>
        , IBusinessOperationRepository<TBusinessOperation> where TBusinessOperation : BusinessOperation
    {
        /// <summary>
        /// Get method for BusinessOperation
        /// </summary>
        /// <returns></returns>
        public TBusinessOperation Get()
        {
          return  GetQuery().Cacheable().CacheRegion(CacheRegions.Constants).List().FirstOrDefault();
        }
    }
}
