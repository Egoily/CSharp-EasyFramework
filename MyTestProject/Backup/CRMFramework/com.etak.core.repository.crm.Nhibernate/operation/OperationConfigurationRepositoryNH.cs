using System;
using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.model.operation;
using com.etak.core.repository.crm.configuration;
using com.etak.core.repository.crm.Nhibernate.Factory;
using com.etak.core.repository.NHibernate;

namespace com.etak.core.repository.crm.Nhibernate.operation
{
    /// <summary>
    /// NH Implemetation of IOperationConfigurationRepository
    /// </summary>
    /// <typeparam name="TOperationConfiguration"></typeparam>
    public class OperationConfigurationRepositoryNH<TOperationConfiguration> : 
        NHibernateRepository<TOperationConfiguration, Int32>
        , IOperationConfigurationRepository<TOperationConfiguration> where TOperationConfiguration : OperationConfiguration
    {
        /// <summary>
        /// gets the settings for a operation type (discriminator)
        /// </summary>
        /// <param name="dealer">the vmno owner of the configuration (or to which this configuratio  applies)</param>
        /// <param name="operation">Operation being configured</param>
        /// <returns>The list of settings</returns>
        public IEnumerable<TOperationConfiguration> GetByDiscriminatorAndDealerId(DealerInfo dealer, BusinessOperation operation)
        {
            return GetQuery().Where(x => x.MVNO == dealer && x.Operation == operation).
                Cacheable().
                CacheRegion(CacheRegions.SystemSettingsCacheRegion)
                .Future();

        }
    }
}
