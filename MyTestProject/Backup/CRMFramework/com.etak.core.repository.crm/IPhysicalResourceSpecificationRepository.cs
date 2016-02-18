using System;
using System.Collections.Generic;
using com.etak.core.model;
using com.etak.core.model.inventory;

namespace com.etak.core.repository.crm
{
    /// <summary>
    /// Repository interface for PhysicalResourceSpecification
    /// </summary>
    /// <typeparam name="TPhysicalResourceSpecification">The entity managed by the repository, is or extends PhysicalResourceSpecification</typeparam>
    public interface IPhysicalResourceSpecificationRepository<TPhysicalResourceSpecification> : IRepository<TPhysicalResourceSpecification, Int64> where TPhysicalResourceSpecification : PhysicalResourceSpecification
    {
        /// <summary>
        /// Get specification by sku.
        /// </summary>
        /// <param name="sku"></param>
        /// <returns></returns>
        TPhysicalResourceSpecification GetBySKU(string sku);
        /// <summary>
        /// GetAllSpeicication by MVNOId
        /// </summary>
        /// <returns></returns>
        IEnumerable<TPhysicalResourceSpecification> GetAll();
    }
}
