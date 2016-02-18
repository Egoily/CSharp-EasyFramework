using System;
using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.model.operation;

namespace com.etak.core.repository.crm.configuration
{

    /// <summary>
    /// The respository interface for <typeparamref name="TOperationConfiguration"/> entity
    /// </summary>
    /// <typeparam name="TOperationConfiguration">The entity managed by the interface, is or extends SystemConfigDataInfo</typeparam>
    public interface IOperationConfigurationRepository<TOperationConfiguration> :
        IRepository<TOperationConfiguration, Int32> where TOperationConfiguration : OperationConfiguration
    {
        /// <summary>
        /// gets the settings for a operation type (discriminator)
        /// </summary>
        /// <param name="dealer">the vmno owner of the configuration (or to which this configuratio  applies)</param>
        /// <param name="operation">the discriminator for the operation type</param>
        /// <returns>The list of settings</returns>
        IEnumerable<TOperationConfiguration> GetByDiscriminatorAndDealerId(DealerInfo dealer, BusinessOperation operation);
       

    }
}
